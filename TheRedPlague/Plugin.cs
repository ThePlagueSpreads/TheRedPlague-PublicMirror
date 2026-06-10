using System.Collections;
using System.Diagnostics;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Utility.ModMessages;
using TheRedPlague.Compatibility;
using TheRedPlague.Content.TitleScreen;
using TheRedPlague.Framework.API.StructureLoading;
using TheRedPlague.Registration;
using TheRedPlague.Registration.Audio;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("com.snmodding.nautilus", "1.0.0.50")]
[BepInDependency("com.lee23.ecclibrary", "2.2.3")]
[BepInDependency("WorldHeightLib", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("Esper89.TerrainPatcher", "1.2.4")]
[BepInDependency("com.aci.thesilence", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("com.lee23.bloopandblaza", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("com.lee23.kalliesproppack", "1.3.5")]
public class Plugin : BaseUnityPlugin
{
    public new static ManualLogSource Logger { get; private set; }

    internal static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

    public static AssetBundle RedPlagueMainMenu { get; } =
        AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly, "trp_mainmenu");

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

        // early loading tasks
        WaitScreenHandler.RegisterEarlyAsyncLoadTask(PluginInfo.PLUGIN_NAME, LoadRedPlagueAsync,
            "Loading the Red Plague");
        WaitScreenHandler.RegisterEarlyAsyncLoadTask(PluginInfo.PLUGIN_NAME,
            BaseGamePrefabModifications.ModifyBaseGamePrefabs, "Modifying prefabs...");

        // late loading tasks
        WaitScreenHandler.RegisterLateAsyncLoadTask(PluginInfo.PLUGIN_NAME, ValidateSaveFile.DoValidation,
            "Validating save file...");

        RegisterForFindMyUpdates();

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void LoadStartupAssets()
    {
        ModAudio.RegisterMainMenuAudio();
        TrpTitleScreen.RegisterTitleScreenCompatibility(this);
        LanguageHandler.RegisterLocalizationFolder();
    }
    
    private void RegisterForFindMyUpdates()
    {
        ModMessageSystem.SendGlobal("FindMyUpdates", "https://raw.githubusercontent.com/ThePlagueSpreads/TheRedPlague-PublicMirror/refs/heads/main/Version.json");
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

        yield return LogProcess(task, "asset bundles");

        yield return AssetBundles.LoadRedPlagueAsync(task, LogProcess);
        
        yield return LogProcess(task, "essentials");

        EssentialAssets.RegisterEssentials();
        CustomBackgroundTypes.RegisterCustomBackgroundTypes();
        CustomTechCategories.RegisterAll();

        yield return LogProcess(task, "prefabs");

        AutoPrefabLoader.RegisterAllPrefabs(Assembly);
        
        yield return LogProcess(task, "commands");
        ConsoleCommandsHandler.RegisterConsoleCommands(typeof(Commands));
        Commands.RegisterTeleportCommands();
        
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
        StructureRegistrationUtils.RegisterStructures(StructureRegistrationUtils.GetStructuresFolderPath(Assembly));

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
}