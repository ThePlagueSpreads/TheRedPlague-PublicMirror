using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Compatibility;
using TheRedPlague.Data;
using TheRedPlague.Mono.CreatureBehaviour.Chaos;
using TheRedPlague.Mono.CreatureBehaviour.Mimics;
using TheRedPlague.Mono.SFX;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("com.snmodding.nautilus", "1.0.0.44")]
[BepInDependency("com.lee23.ecclibrary", "2.2.0")]
[BepInDependency("WorldHeightLib")]
[BepInDependency("Esper89.TerrainPatcher", "1.2.2")]
[BepInDependency("com.aci.thesilence", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("com.lee23.bloopandblaza", BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BaseUnityPlugin
{
    public new static ManualLogSource Logger { get; private set; }

    internal static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

    public static AssetBundle RedPlagueMainMenu { get; } =
        AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly, "trp_mainmenu");

    public static AssetBundle AssetBundle { get; private set; }

    public static AssetBundle CreaturesBundle { get; private set; }

    public static AssetBundle ScenesAssetBundle { get; private set; }
    
    public static AssetBundle CharactersBundle { get; private set; }

    public static AssetBundle AudioBundle { get; private set; }

    public static Texture2D ZombieInfectionTexture { get; private set; }
    public static RedPlagueOptions Options { get; } = OptionsPanelHandler.RegisterModOptions<RedPlagueOptions>();

    private static readonly Stopwatch Stopwatch = new();

    internal static bool RedPlagueLoaded { get; private set; }

    private static string _previousLoadingTask;

    private static bool _alreadyLoadedRedPlagueCore;

    private const float LongTimeForSoundThresholdSeconds = 15;
    private const float AudioRefreshStatusDelay = 0.7f;

    private static Plugin _pluginInstance; // for checking if the plugin gets destroyed

    private void Awake()
    {
        _pluginInstance = this;
        
        Logger = base.Logger;

        Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");

        LoadStartupAssets();
        RegisterSaveDataCaches();

        // early loading tasks
        WaitScreenHandler.RegisterEarlyAsyncLoadTask(PluginInfo.PLUGIN_NAME, LoadRedPlagueAsync,
            "Loading the Red Plague");
        WaitScreenHandler.RegisterEarlyAsyncLoadTask(PluginInfo.PLUGIN_NAME,
            BaseGamePrefabModifications.ModifyBaseGamePrefabs, "Modifying prefabs...");

        // late loading tasks
        WaitScreenHandler.RegisterLateAsyncLoadTask(PluginInfo.PLUGIN_NAME, ValidateSaveFile.DoValidation,
            "Validating save file...");

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void LoadStartupAssets()
    {
        ModAudio.RegisterMainMenuAudio();
        TrpTitleScreen.RegisterTitleScreenCompatibility(this);
        LanguageHandler.RegisterLocalizationFolder();
    }

    private IEnumerator LoadRedPlagueAsync(WaitScreenHandler.WaitScreenTask task)
    {
        if (_alreadyLoadedRedPlagueCore)
        {
            task.Status = "Already loaded!";
            yield break;
        }

        if (_pluginInstance == null)
        {
            ErrorMessage.AddMessage("<color=#FF0000>The Red Plague has failed to load. Try updating the BepInEx pack.</color>");
            yield break;
        }

        _alreadyLoadedRedPlagueCore = true;

        _previousLoadingTask = null;

        Stopwatch.Start();

        yield return LogProcess(task, "core assets");
        var coreAssetsBundleTask = AssetBundle.LoadFromFileAsync(GetAssetBundlePath("theredplague"));
        yield return coreAssetsBundleTask;
        AssetBundle = coreAssetsBundleTask.assetBundle;
        ZombieInfectionTexture = AssetBundle.LoadAsset<Texture2D>("zombie_infection_bloody");

        yield return LogProcess(task, "creature assets");
        var creatureBundleTask = AssetBundle.LoadFromFileAsync(GetAssetBundlePath("redplaguecreatures"));
        yield return creatureBundleTask;
        CreaturesBundle = creatureBundleTask.assetBundle;

        yield return LogProcess(task, "scenes");
        var scenesBundleTask = AssetBundle.LoadFromFileAsync(GetAssetBundlePath("redplaguescenes"));
        yield return scenesBundleTask;
        ScenesAssetBundle = scenesBundleTask.assetBundle;
        
        yield return LogProcess(task, "characters");
        var charactersBundleTask = AssetBundle.LoadFromFileAsync(GetAssetBundlePath("redplaguecharacters"));
        yield return charactersBundleTask;
        CharactersBundle = charactersBundleTask.assetBundle;

        yield return LogProcess(task, "miscellaneous data");
        CustomBackgroundTypes.RegisterCustomBackgroundTypes();
        CustomTechCategories.RegisterAll();

        yield return LogProcess(task, "prefabs");
        ModPrefabs.RegisterPrefabs();

        yield return LogProcess(task, "coordinated spawns");
        CoordinatedSpawns.RegisterCoordinatedSpawns();

        yield return LogProcess(task, "commands");
        ConsoleCommandsHandler.RegisterConsoleCommands(typeof(Commands));
        Commands.RegisterTeleportCommands();

        yield return LogProcess(task, "audio bundle");
        var audioBundleTask = AssetBundle.LoadFromFileAsync(GetAssetBundlePath("theredplagueaudio"));
        yield return audioBundleTask;
        AudioBundle = audioBundleTask.assetBundle;

        yield return LogProcess(task, "audio");
        StartCoroutine(ModAudio.RegisterAudioAsync(task));
        var longTimeThreshold = Time.realtimeSinceStartup + LongTimeForSoundThresholdSeconds;
        while (!ModAudio.AudioFinishedLoading)
        {
            yield return new WaitForSecondsRealtime(AudioRefreshStatusDelay);
            if (Time.realtimeSinceStartup > longTimeThreshold)
            {
                task.Status = "audio (this seems to be taking a long time...)";
            }
        }

        yield return LogProcess(task, "story");
        StoryUtils.RegisterStory();
        StoryUtils.RegisterLanguageLines();
        PdaChecklistAPI.RegisterTrpEntries();

        yield return LogProcess(task, "prefabs");
        ModCompatibilityManager.RegisterAllCompatibility();

        yield return LogProcess(task, "structures");
        StructureLoading.RegisterStructures();

        yield return LogProcess(task, "Done!");

        RedPlagueLoaded = true;
    }

    private static IEnumerator LogProcess(WaitScreenHandler.WaitScreenTask task, string upcomingTask)
    {
        if (!string.IsNullOrEmpty(_previousLoadingTask))
        {
            Logger.LogInfo($"Finished task '{_previousLoadingTask}' ({Stopwatch.ElapsedMilliseconds}ms)");
        }

        _previousLoadingTask = upcomingTask;
        task.Status = $"Loading {upcomingTask}";
        yield return null;
        Stopwatch.Restart();
    }

    private static string GetAssetBundlePath(string assetBundleFileName)
    {
        return Path.Combine(Path.GetDirectoryName(Assembly.Location), "Assets", assetBundleFileName);
    }

    private static void RegisterSaveDataCaches()
    {
        BlisterbackResourceSpawns.RegisterSaveData();
        DestroyPlagueHeartTimer.RegisterSaveData();
        CassyTalkingBehindDoor.RegisterSaveData();
        RoamingChaosLeviathanManager.RegisterSaveData();
    }
}