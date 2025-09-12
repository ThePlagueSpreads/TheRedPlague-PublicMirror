using System;
using System.Collections;
using System.Linq;
using ECCLibrary;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using Story;
using TheRedPlague.Data;
using TheRedPlague.MaterialModifiers;
using TheRedPlague.Mono.CinematicEvents;
using TheRedPlague.Mono.CreatureBehaviour.MrTeeth;
using TheRedPlague.Mono.Equipment;
using TheRedPlague.Mono.InfectionLogic;
using TheRedPlague.Mono.SFX;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Mono.StoryContent.B3NT;
using TheRedPlague.Mono.StoryContent.Precursor;
using TheRedPlague.Mono.Systems;
using TheRedPlague.Mono.Triggers;
using TheRedPlague.Mono.UI;
using TheRedPlague.Mono.Util;
using TheRedPlague.Mono.VFX;
using TheRedPlague.PrefabFiles.Buildable;
using TheRedPlague.PrefabFiles.Creatures;
using TheRedPlague.PrefabFiles.Creatures.Misc;
using TheRedPlague.PrefabFiles.Decorations;
using TheRedPlague.PrefabFiles.DomeDrones;
using TheRedPlague.PrefabFiles.Equipment;
using TheRedPlague.PrefabFiles.Fragments;
using TheRedPlague.PrefabFiles.Plants;
using TheRedPlague.PrefabFiles.Precursor;
using TheRedPlague.PrefabFiles.Items;
using TheRedPlague.PrefabFiles.SFX;
using TheRedPlague.PrefabFiles.Special;
using TheRedPlague.PrefabFiles.Special.EasterEggs;
using TheRedPlague.PrefabFiles.StoryProps;
using TheRedPlague.PrefabFiles.StoryProps.Bases;
using TheRedPlague.PrefabFiles.StoryProps.MeteorSite;
using TheRedPlague.PrefabFiles.UpgradeModules;
using TheRedPlague.PrefabFiles.VFX;
using TheRedPlague.Utilities;
using TheRedPlague.Utilities.Gadgets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague;

public static class ModPrefabs
{
    private static PrefabInfo InfectionLaserInfo { get; } = PrefabInfo.WithTechType("InfectionLaser");

    public static PrefabInfo InfectedCorpseInfo { get; } = PrefabInfo.WithTechType("InfectedCorpse");
    public static PrefabInfo SkeletonCorpse { get; } = PrefabInfo.WithTechType("SkeletonCorpse");
    public static PrefabInfo NpcSurvivorManager { get; } = PrefabInfo.WithTechType("NpcSurvivorManager");

    public static PrefabInfo WarperHeart { get; } = PrefabInfo.WithTechType("WarperHeart");

    public static PrefabInfo MutantDiver1 { get; } = PrefabInfo.WithTechType("MutantDiver1");
    public static PrefabInfo MutantDiver2 { get; } = PrefabInfo.WithTechType("MutantDiver2");
    public static PrefabInfo MutantDiver3 { get; } = PrefabInfo.WithTechType("MutantDiver3");
    public static PrefabInfo MutantDiver3Small { get; } = PrefabInfo.WithTechType("MutantDiver3Small");
    public static PrefabInfo MutantDiver4 { get; } = PrefabInfo.WithTechType("MutantDiver4");
    public static PrefabInfo SuckerController { get; } = PrefabInfo.WithTechType("SuckerController");

    public static PrefabInfo MrTeeth { get; } = PrefabInfo.WithTechType("MrTeeth");
    public static PrefabInfo Insectoid { get; } = PrefabInfo.WithTechType("Insectoid");
    public static PrefabInfo SmallInsectoid { get; } = PrefabInfo.WithTechType("SmallInsectoid");

    // Drifter
    public static TechType Drifter { get; } = EnumHandler.AddEntry<TechType>("Drifter");
    public static PrefabInfo DrifterInfo { get; } = new("Drifter", "DrifterPrefab", Drifter);

    public static PrefabInfo DrifterFlyOnlyInfo { get; } = new("DrifterFlyOnly", "DrifterFlyOnlyPrefab", Drifter);

    public static PrefabInfo DrifterHivemindSpawn { get; } = PrefabInfo.WithTechType("DrifterHMSpawn");

    // Items
    public static PrefabInfo AmalgamatedBone { get; } = PrefabInfo.WithTechType("AmalgamatedBone");

    public static PrefabInfo EnzymeContainer { get; } = PrefabInfo.WithTechType("ConcentratedEnzymeContainer")
        .WithIcon(SpriteManager.Get(TechType.LabContainer3))
        .WithSizeInInventory(new Vector2int(2, 2));

    public static PrefabInfo EnzymeParticleInfo { get; private set; }

    public static PrefabInfo BananaInfo { get; } = PrefabInfo.WithTechType("Banana");

    public static PrefabInfo ProteinSnackInfo { get; } = PrefabInfo.WithTechType("ProteinSnack", true);

    private static PrefabInfo PrecursorPhoneInfo { get; } = PrefabInfo.WithTechType("PrecursorPhone");

    // Databoxes
    public static PrefabInfo PlagueKnifeDatabox { get; } = PrefabInfo.WithTechType("PlagueKnifeDatabox");
    public static PrefabInfo BoneArmorDatabox { get; } = PrefabInfo.WithTechType("BoneArmorDatabox");
    public static PrefabInfo PlagueGrapplerDatabox { get; } = PrefabInfo.WithTechType("PlagueGrapplerDatabox");

    public static PrefabInfo AssimilationGeneratorDatabox { get; } =
        PrefabInfo.WithTechType("AssimilationGeneratorDatabox");

    public static PrefabInfo BioBatteryDatabox { get; } = PrefabInfo.WithTechType("BioBatteryDatabox");
    public static PrefabInfo InsanityDeterrentDatabox { get; } = PrefabInfo.WithTechType("InsanityDeterrentDatabox");
    public static PrefabInfo ObsidianArmDatabox { get; } = PrefabInfo.WithTechType("ObsidianArmDatabox");

    // Tablets

    public static PrefabInfo InfectionTabletInfo { get; } = PrefabInfo.WithTechType("InfectionTablet");

    public static PrefabInfo InfectionTabletFragmentInfo { get; } = PrefabInfo.WithTechType("InfectionTabletFragment");

    public static PrefabInfo GoldTabletInfo { get; } = PrefabInfo.WithTechType("GoldTablet");

    // Story things

    public static PrefabInfo LeaveFleshCaveWaiter { get; } = PrefabInfo.WithTechType("LeaveFleshCaveWaiter");
    public static PrefabInfo ThrusterEventTrigger { get; } = PrefabInfo.WithTechType("ThrusterEventTrigger");

    // Misc data
    private static TechType HarvestableBoneTechType = EnumHandler.AddEntry<TechType>("HarvestableAmalgamatedBone");
    private static LiveMixinData _harvestableBoneLiveMixinData;
    internal static Sprite MiscDecoIcon { get; private set; }

    private static LiveMixinData HarvestableBoneLiveMixinData
    {
        get
        {
            if (_harvestableBoneLiveMixinData == null)
            {
                _harvestableBoneLiveMixinData = CreatureDataUtils.CreateLiveMixinData(80);
            }

            return _harvestableBoneLiveMixinData;
        }
    }

    public static mset.Sky PlagueCaveSky { get; private set; }

    public static void RegisterPrefabs()
    {
        // MUST BE CALLED FIRST
        RegisterCommonAssets();

        RegisterBiomes();
        RegisterStoryProps();
        RegisterWreckProps();
        RegisterPrecursorAssets();
        RegisterFleshAndBonePrefabs();
        RegisterLights();
        RegisterBuildables();
        // Items must be registered after buildables (e.g., custom fabricators)
        RegisterItems();
        RegisterManagers();
        RegisterCreaturesAndCorpses();
        RegisterDropPodPrefabs();
        RegisterEquipment();
        RegisterFood();
        RegisterEasterEggs();
        RegisterDataboxesAndConsoles();
        RegisterScenes();
        RegisterVFX();
        RegisterSoundPrefabs();

        if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("WorldHeightLib"))
        {
            RegisterFleshBlobs();
        }
        else
        {
            Plugin.Logger.LogWarning("Failed to register flesh blob entities; WorldHeightLib is not installed!");
        }
    }

    private static void RegisterCommonAssets()
    {
        MiscDecoIcon = Plugin.AssetBundle.LoadAsset<Sprite>("GenericDecoIcon");
    }

    private static void RegisterBiomes()
    {
        var infectedZoneSettings = BiomeUtils.CreateBiomeSettings(new Vector3(7f, 4.5f, 3f), 0.4f,
            new Color(1.05f, 1f, 1f, 1), 2f, new Color(1f, 0.3f, 0.3f), 0f, 10f, 0.5f, 0.9f, 20f);
        BiomeHandler.RegisterBiome("infectedzone", infectedZoneSettings,
            new BiomeHandler.SkyReference("SkyGrassyPlateaus"));
        var infectedZonePrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectedZoneVolume"));
        var infectedZoneTemplate = new AtmosphereVolumeTemplate(infectedZonePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "infectedzone");
        infectedZonePrefab.SetGameObject(infectedZoneTemplate);
        infectedZonePrefab.Register();

        var plagueHeartBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(14, 9, 9), 1.3f,
            new Color(1.05f, 1f, 1f, 1), 2f, new Color(1f, 0.3f, 0.3f), 0.05f, 10f, 0.5f, 0.5f, 18f);
        BiomeHandler.RegisterBiome("plagueheart", plagueHeartBiomeSettings,
            new BiomeHandler.SkyReference("SkyGrassyPlateaus"));

        var voidIslandSettings = BiomeUtils.CreateBiomeSettings(new Vector3(4f, 2, 2f), 0.4f,
            new Color(1.05f, 0.6f, 0.6f, 1), 3f, new Color(1f, 0.3f, 0.3f), 0f, 10f, 0.5f, 0.9f, 20f);
        BiomeHandler.RegisterBiome("plaguecyclopsisland", voidIslandSettings,
            new BiomeHandler.SkyReference("SkyGrassyPlateaus"));
        BiomeHandler.RegisterBiome("plaguecyclopsislandinterior", voidIslandSettings,
            new BiomeHandler.SkyReference("SkyMountains"));

        var voidIslandBiomePrefab = new CustomPrefab(PrefabInfo.WithTechType("VoidIslandBiomeVolume"));
        var voidIslandBiomeTemplate = new AtmosphereVolumeTemplate(voidIslandBiomePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "plaguecyclopsisland");
        voidIslandBiomePrefab.SetGameObject(voidIslandBiomeTemplate);
        voidIslandBiomePrefab.Register();

        var voidIslandInteriorBiomePrefab = new CustomPrefab(PrefabInfo.WithTechType("VoidIslandBiomeInteriorVolume"));
        var voidIslandInteriorBiomeTemplate = new AtmosphereVolumeTemplate(voidIslandInteriorBiomePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "plaguecyclopsislandinterior");
        voidIslandInteriorBiomePrefab.SetGameObject(voidIslandInteriorBiomeTemplate);
        voidIslandInteriorBiomePrefab.Register();

        var skyIslandSettings = BiomeUtils.CreateBiomeSettings(new Vector3(100, 18.3f, 3.5f), 1f,
            new Color(1, 1, 1, 1), 1f, new Color(0, 0.216f, 0.196f),
            10f, 10f, 3.2f, 1f, 34f);
        BiomeHandler.RegisterBiome("skyisland", skyIslandSettings,
            new BiomeHandler.SkyReference("SkyMountains"));
        var skyIslandBiomePrefab = new CustomPrefab(PrefabInfo.WithTechType("SkyIslandBiomeVolume"));
        var skyIslandBiomeTemplate = new AtmosphereVolumeTemplate(skyIslandBiomePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "skyisland", 11)
        {
            ModifyPrefab = obj =>
            {
                var islandMusic = obj.AddComponent<PlayIslandMusic>();
                islandMusic.emitter = obj.AddComponent<FMOD_CustomLoopingEmitter>();
                islandMusic.emitter.playOnAwake = false;
                islandMusic.emitter.restartOnPlay = false;
                islandMusic.emitter.SetAsset(AudioUtils.GetFmodAsset("SkyIslandMusic"));
            }
        };
        skyIslandBiomePrefab.SetGameObject(skyIslandBiomeTemplate);
        skyIslandBiomePrefab.Register();

        var mazeBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(8, 11, 13), 0.5f,
            new Color(0.341f, 0.427f, 0.447f), 0.6f, new Color(1, 0.906f, 0.96f), 0.02f, 25, 0.8f, 0.8f, 19);
        BiomeHandler.RegisterBiome("mazebase", mazeBiomeSettings,
            new BiomeHandler.SkyReference("SkyMountains"));
        BiomeHandler.AddBiomeMusic("mazebase", AudioUtils.GetFmodAsset("MazeBaseSoundtrack"));
        var mazeBiomePrefab = new CustomPrefab(PrefabInfo.WithTechType("MazeBaseBiomeVolume"));
        var mazeBaseBiomeTemplate = new AtmosphereVolumeTemplate(mazeBiomePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Cube, "mazebase", 11);
        mazeBiomePrefab.SetGameObject(mazeBaseBiomeTemplate);
        mazeBiomePrefab.Register();

        PlagueCaveSky = BiomeUtils
            .CreateSkyPrefab("FleshCaveSky", Plugin.AssetBundle.LoadAsset<Texture>("FleshCaveCubemap"), false).sky;

        var fleshCaveEntranceBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(14, 9, 9), 0.4f,
            new Color(0.441f, 0.1f, 0.1f), 0.2f, new Color(1, 0f, 0f), 0.05f, 30f, 0f, 0f, 36.1f);
        BiomeHandler.RegisterBiome("fleshcave_upper", fleshCaveEntranceBiomeSettings,
            new BiomeHandler.SkyReference(PlagueCaveSky.gameObject));
        var fleshCaveEntrancePrefab = new CustomPrefab(PrefabInfo.WithTechType("FleshCaveUpperVolume"));
        var fleshCaveEntranceTemplate = new AtmosphereVolumeTemplate(fleshCaveEntrancePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "fleshcave_upper", 50);
        fleshCaveEntrancePrefab.SetGameObject(fleshCaveEntranceTemplate);
        fleshCaveEntrancePrefab.Register();

        var fleshCaveChamberBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(14, 9, 9), 0.4f,
            new Color(0.441f, 0.1f, 0.1f), 0.22f, new Color(1, 0f, 0f), 0.025f, 30f, 0f, 0f, 36.1f);
        BiomeHandler.RegisterBiome("fleshcave_chamber", fleshCaveChamberBiomeSettings,
            new BiomeHandler.SkyReference(PlagueCaveSky.gameObject));
        var fleshCaveChamberPrefab = new CustomPrefab(PrefabInfo.WithTechType("FleshCaveChamberVolume"));
        var fleshCaveChamberTemplate = new AtmosphereVolumeTemplate(fleshCaveChamberPrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "fleshcave_chamber", 60);
        fleshCaveChamberPrefab.SetGameObject(fleshCaveChamberTemplate);
        fleshCaveChamberPrefab.Register();

        var fleshCaveCacheBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(0.2f, 0.2f, 0.2f), 0.01f,
            new Color(1, 1, 1), 1.5f, new Color(0, 0.8f, 1f), 0.002f, 10, 0f, 1f, 24f);
        BiomeHandler.RegisterBiome("fleshcave_cache", fleshCaveCacheBiomeSettings,
            new BiomeHandler.SkyReference("SkyMushroomForestCave"));
        var fleshCaveCachePrefab = new CustomPrefab(PrefabInfo.WithTechType("FleshCaveCacheVolume"));
        var fleshCaveCacheTemplate = new AtmosphereVolumeTemplate(fleshCaveCachePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "fleshcave_cache", 65);
        fleshCaveCachePrefab.SetGameObject(fleshCaveCacheTemplate);
        fleshCaveCachePrefab.Register();

        BiomeHandler.AddBiomeMusic("fleshcave", AudioUtils.GetFmodAsset("FleshCaveSoundtrack"));
        BiomeHandler.AddBiomeAmbience("fleshcave_chamber", AudioUtils.GetFmodAsset("ShrineSOSAmbience"),
            FMODGameParams.InteriorState.OnlyOutside);
        BiomeHandler.AddBiomeAmbience("fleshcave", AudioUtils.GetFmodAsset("PlagueCaveAmbience"),
            FMODGameParams.InteriorState.Always);
        BiomeHandler.AddBiomeAmbience("fleshcave_cache",
            AudioUtils.GetFmodAsset("event:/env/background/prec_cave_loop"),
            FMODGameParams.InteriorState.Always);

        var bloodPoolBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(14, 9, 9), 0.5f,
            new Color(0.441f, 0.1f, 0.1f), 0.8f, new Color(1, 0f, 0f), 0.3f, 3f, 0f, 1f, 38f);
        BiomeHandler.RegisterBiome("fleshcave_bloodpool", bloodPoolBiomeSettings,
            new BiomeHandler.SkyReference(PlagueCaveSky.gameObject));

        var shrineBaseBiomeSettings = BiomeUtils.CreateBiomeSettings(Vector3.zero, 0.0001f, Color.white, 0.001f,
            new Color(0.601f, 0.765f, 0.444f), 0, 25, 0, 0, 34);
        BiomeHandler.RegisterBiome("shrinebase_hallway", shrineBaseBiomeSettings,
            new BiomeHandler.SkyReference("SkyPrecursorPrisonAntechamber"));
        BiomeHandler.RegisterBiome("shrinebase_mainroom", shrineBaseBiomeSettings,
            new BiomeHandler.SkyReference("SkyPrecursorPrisonAntechamber"));
        RegisterBiomeVolumePrefab("ShrineBaseHallwayVolume", "shrinebase_hallway",
            AtmosphereVolumeTemplate.VolumeShape.Cube, 100);
        RegisterBiomeVolumePrefab("ShrineBaseMainRoomVolume", "shrinebase_mainroom",
            AtmosphereVolumeTemplate.VolumeShape.Cube, 100);
    }

    private static void RegisterBiomeVolumePrefab(string prefabId, string biomeId,
        AtmosphereVolumeTemplate.VolumeShape shape, int priority = 10)
    {
        var prefab = new CustomPrefab(PrefabInfo.WithTechType(prefabId));
        prefab.SetGameObject(new AtmosphereVolumeTemplate(prefab.Info, shape, biomeId, priority));
        prefab.Register();
    }

    private static void RegisterStoryProps()
    {
        // - DOME -

        NewInfectionDome.Register();
        DomeConstructionTriggerPrefab.Register();

        // - FLOATING ISLAND -
        var infectionCubePlatform = new CustomPrefab(PrefabInfo.WithTechType("InfectionCubePlatform"));
        var infectionCubeTemplate =
            new CloneTemplate(infectionCubePlatform.Info, "6b0104e8-979e-46e5-bc17-57c4ac2e6e39");
        infectionCubeTemplate.ModifyPrefab += go =>
        {
            Object.DestroyImmediate(go.GetComponent<DisableEmissiveOnStoryGoal>());
            go.transform.Find("CullVolumeManager").gameObject.SetActive(false);
            go.AddComponent<DestroyIfIdMatches>().ids = new[] { "1071b882-3848-4984-b9d4-b5e0a9a81dfb" };
        };
        infectionCubePlatform.SetGameObject(infectionCubeTemplate);
        infectionCubePlatform.Register();

        LowQualityForceFieldIsland.Register();

        var forceFieldIslandLightPrefab = new CustomPrefab(PrefabInfo.WithTechType("ForceFieldIslandLight"));
        var forceFieldIslandLightTemplate =
            new CloneTemplate(forceFieldIslandLightPrefab.Info, "081ef6c1-aa78-46fd-a20f-a6b63ca5c5f3");
        forceFieldIslandLightTemplate.ModifyPrefab += (go) =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponent<SkyApplier>().customSkyPrefab = null;
        };
        forceFieldIslandLightPrefab.SetGameObject(forceFieldIslandLightTemplate);
        forceFieldIslandLightPrefab.Register();

        var precursorPhone = new CustomPrefab(PrecursorPhoneInfo);
        var precursorPhoneTemplate =
            new CloneTemplate(precursorPhone.Info, "081ef6c1-aa78-46fd-a20f-a6b63ca5c5f3");
        precursorPhoneTemplate.ModifyPrefab += (go) =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponentInChildren<Light>().enabled = false;
            go.GetComponentInChildren<VFXVolumetricLight>().gameObject.SetActive(false);
            go.GetComponent<SkyApplier>().customSkyPrefab = null;
            go.transform.localScale = new Vector3(0.2f, 0.1f, 0.3f);
            go.gameObject.AddComponent<Pickupable>();
        };
        precursorPhone.SetGameObject(precursorPhoneTemplate);
        precursorPhone.SetSpawns(new SpawnLocation(new Vector3(-1324.541f, -206.655f, 266.916f),
            new Vector3(45.373f, 276.516f, 230.206f), new Vector3(0.2f, 0.1f, 0.3f)));
        precursorPhone.Register();

        var forceFieldIslandLight2Prefab = new CustomPrefab(PrefabInfo.WithTechType("ForceFieldIslandLight2"));
        var forceFieldIslandLight2Template =
            new CloneTemplate(forceFieldIslandLight2Prefab.Info, "081ef6c1-aa78-46fd-a20f-a6b63ca5c5f3");
        forceFieldIslandLight2Template.ModifyPrefab += (go) =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponentInChildren<Light>().enabled = false;
            go.GetComponent<SkyApplier>().customSkyPrefab = null;
        };
        forceFieldIslandLight2Prefab.SetGameObject(forceFieldIslandLight2Template);
        forceFieldIslandLight2Prefab.Register();

        var infectionLaserColumnPrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionLaserColumn"));
        var infectionLaserColumnTemplate =
            new CloneTemplate(infectionLaserColumnPrefab.Info, "3d625dbb-d15a-4351-bca0-0a0526f01e6e");
        infectionLaserColumnTemplate.ModifyPrefab += go =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<PrecursorGunCentralColumn>().ForEach(c => c.enabled = false);
            go.transform
                .Find(
                    "precursor_column_maze_08_06_08_hlpr/precursor_column_maze_08_06_08_ctrl/precursor_column_maze_08_06_08/light")
                .GetComponent<Light>().color = Color.red;
            go.transform
                .Find(
                    "precursor_column_maze_08_06_08_hlpr/precursor_column_maze_08_06_08_ctrl/precursor_column_maze_08_06_08/precursor_column_maze_08_06_08_glass_01")
                .GetComponent<Renderer>().material.color = Color.red;
            var renderer = go.transform
                .Find(
                    "precursor_column_maze_08_06_08_hlpr/precursor_column_maze_08_06_08_ctrl/precursor_column_maze_08_06_08/precursor_column_maze_08_06_08_glass_02_hlpr/precursor_column_maze_08_06_08_glass_02_ctrl/precursor_column_maze_08_06_08_glass_02")
                .GetComponent<Renderer>();
            var materials = renderer.materials;
            materials[1].color = Color.red;
            renderer.materials = materials;
            go.AddComponent<InfectAnything>();
        };
        infectionLaserColumnPrefab.SetGameObject(infectionLaserColumnTemplate);
        infectionLaserColumnPrefab.SetSpawns(new SpawnLocation(new Vector3(-59.640f, 301.000f, -24.840f),
            Vector3.up * 343, Vector3.one * 0.7f));
        infectionLaserColumnPrefab.Register();

        var infectionLaserTerminalPrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionLaserTerminal"));
        var infectionLaserTerminalTemplate =
            new CloneTemplate(infectionLaserTerminalPrefab.Info, "b1f54987-4652-4f62-a983-4bf3029f8c5b");
        infectionLaserTerminalTemplate.ModifyPrefab += go =>
        {
            go.AddComponent<InfectAnything>();
            Object.DestroyImmediate(go.GetComponent<DisableEmissiveOnStoryGoal>());
            var trigger = go.transform.Find("triggerArea");
            var terminalObj = go.transform.Find("terminal");
            var disable = trigger.GetComponent<PrecursorDisableGunTerminalArea>();
            var originalTerminal = terminalObj.GetComponent<PrecursorDisableGunTerminal>();
            Object.DestroyImmediate(disable);
            var terminal = terminalObj.gameObject.AddComponent<DisableDomeTerminal>();
            /*
            terminal.accessGrantedSound = originalTerminal.accessGrantedSound;
            terminal.accessDeniedSound = originalTerminal.accessDeniedSound;
            terminal.cinematic = originalTerminal.cinematic;
            terminal.useSound = originalTerminal.useSound;
            terminal.openLoopSound = originalTerminal.openLoopSound;
            terminal.onPlayerCuredGoal = originalTerminal.onPlayerCuredGoal;
            terminal.glowRing = originalTerminal.glowRing;
            terminal.glowMaterial = originalTerminal.glowMaterial;
            */
            // Object.DestroyImmediate(originalTerminal);
            // trigger.gameObject.AddComponent<DisableDomeArea>(); // .terminal = terminal;
            // implement custom modifications here
        };
        infectionLaserTerminalPrefab.SetGameObject(infectionLaserTerminalTemplate);
        infectionLaserTerminalPrefab.SetSpawns(new SpawnLocation(new Vector3(-60.053f, 301.044f, -23.278f),
            Vector3.up * 163));
        infectionLaserTerminalPrefab.Register();

        var infectionControlRoomPrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionControlRoom"));
        var infectionControlRoomTemplate =
            new CloneTemplate(infectionControlRoomPrefab.Info, "963fa3a3-9192-4912-8c8d-d0d98f22ed13");
        infectionControlRoomTemplate.ModifyPrefab += go =>
        {
            go.transform.GetChild(0).gameObject.SetActive(false);
            go.transform.GetChild(3).gameObject.SetActive(false);
            var colliders = go.transform.Find("Control_Room_Collision/Cube").GetComponents<Collider>();
            var colliderIndicesToDisable = new int[] { 11, 12, 19, 49 };
            foreach (var index in colliderIndicesToDisable)
            {
                colliders[index].enabled = false;
            }

            var modelParent = go.transform.Find("Precursor_Gun_ControlRoom");
            var modelIndicesToDisable = new int[] { 66, 67, 69, 70, 78, 100, 110, 111, 112, 113 };
            foreach (var index in modelIndicesToDisable)
            {
                modelParent.GetChild(index).gameObject.SetActive(false);
            }

            go.AddComponent<StupidAssInfectionControlRoomFix>();
        };
        infectionControlRoomPrefab.SetGameObject(infectionControlRoomTemplate);
        infectionControlRoomPrefab.Register();

        var infectionLaserDevicePrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionLaserDevice"));
        var infectionLaserDeviceTemplate =
            new CloneTemplate(infectionLaserTerminalPrefab.Info, "22fb9ee9-690d-426c-844f-a80e527b5fe6");
        infectionLaserDeviceTemplate.ModifyPrefab += go =>
        {
            go.GetComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            Object.DestroyImmediate(go.GetComponent<PrecursorGunStoryEvents>());
            Object.DestroyImmediate(go.GetComponent<PrecursorGunAim>());
            Object.DestroyImmediate(go.GetComponent<AnimateOnStoryGoal>());
            var modelParent = go.transform.Find("precursor_base/Instances");
            modelParent.Find("precursor_base_22").gameObject.SetActive(false);
            modelParent.Find("precursor_base_23").gameObject.SetActive(false);
            modelParent.Find("precursor_base_24").gameObject.SetActive(false);
            modelParent.Find("precursor_base_25").gameObject.SetActive(false);
        };
        infectionLaserDevicePrefab.SetGameObject(infectionLaserDeviceTemplate);
        infectionLaserDevicePrefab.SetSpawns(new SpawnLocation(new Vector3(-80.000f, 304, -47.790f),
            new Vector3(0, 0, 353), Vector3.one * 0.3f));
        infectionLaserDevicePrefab.Register();

        var infectionLaserPrefab = new CustomPrefab(InfectionLaserInfo);
        infectionLaserPrefab.SetGameObject(GetInfectionLaserPrefab);
        infectionLaserPrefab.SetSpawns(new SpawnLocation(Vector3.zero));
        infectionLaserPrefab.Register();

        // - SURVIVOR BASES -

        RegisterPdas();
        CassyHome.Register();
        MazeBase.Register();
        RedPlagueSurvivorBase1.Register();
        RedPlagueSurvivorBase2.Register();
        RedPlagueSurvivorBase3.Register();

        // - ISLAND ELEVATOR -

        IslandElevator.Register();
        IslandElevatorPlatform.Register();

        var islandElevatorTerminal = new CustomTabletTerminalPrefab<IslandElevatorKeyTerminalBehaviour>(
            "IslandElevatorTerminal",
            Plugin.AssetBundle.LoadAsset<Texture2D>("InfectionTabletKeyHolderIcon"),
            InfectionTabletInfo.TechType);
        islandElevatorTerminal.Register();

        // - MAJOR STORY CONTENT -

        CyclopsWreckPrefab.Register();

        PlagueCyclopsIslandWreckPrefab.Register();

        FleshCaveEntrance.Register();

        new TrpMusicPlayer("MrTeethMusicPlayer", LargeWorldEntity.CellLevel.Medium, "MrTeethMusic",
            32, 60)
        {
            ModifyPrefab = obj =>
            {
                var killMusic = obj.AddComponent<KillMusicOnStoryGoal>();
                killMusic.musicPlayer = obj.GetComponent<GenericMusicPlayer>();
                killMusic.goalKey = "AuroraRadiationFixed";
                killMusic.stayDisabledAfter = true;
            }
        }.Register();

        new ScriptHolderPrefab(PrefabInfo.WithTechType("CassyBehindAuroraDoor"), LargeWorldEntity.CellLevel.Near,
            obj => { obj.AddComponent<CassyTalkingBehindDoor>(); }).Register();

        PrecursorSatellite.Register();

        // plague armor unlock:
        new PlagueArmorFragment().Register();

        new ScriptHolderPrefab(PrefabInfo.WithTechType("PlagueCaveHintTrigger"), LargeWorldEntity.CellLevel.Near, obj =>
        {
            var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
            trigger.requiredGoal = StoryUtils.UnlockPlagueCaveSignalGoal;
            trigger.goal = StoryUtils.PlagueCaveEntranceHint;
            var collider = obj.AddComponent<SphereCollider>();
            collider.radius = 30f;
            collider.isTrigger = true;
        }).Register();

        // - SHRINE BASE -

        new TrpMusicPlayer("ShrineBaseMusicPlayer", LargeWorldEntity.CellLevel.Medium, "ShrineBaseMusic",
            83, 100).Register();
        new TrpMusicPlayer("ShrineBaseAmbiencePlayer", LargeWorldEntity.CellLevel.Medium, "ShrineBaseAmbience",
            83, 100).Register();

        new CustomForceFieldPrefab("ShrineBaseForceField",
                () => Plugin.AssetBundle.LoadAsset<GameObject>("ShrineBaseTallForceFieldPrefab"),
                StoryUtils.ShrineBaseForceFieldStoryGoal)
            .Register();

        new CustomTabletTerminalPrefab<StoryGoalTabletTerminal>("ShrineBaseTabletTerminal",
                Plugin.AssetBundle.LoadAsset<Texture2D>("ShrineBaseTabletKeyHolderIcon"),
                GoldTabletInfo.TechType)
            {
                ModifyComponent = c => c.associatedStoryGoal = StoryUtils.ShrineBaseForceFieldStoryGoal
            }
            .Register();

        // Bennet introduction trigger

        new ScriptHolderPrefab(PrefabInfo.WithTechType("B3NTIntroductionTrigger"), LargeWorldEntity.CellLevel.Near,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
                trigger.requiredGoal = StoryUtils.BennetInitialMeeting;
                trigger.goal = StoryUtils.BennetApproach;
                var collider = obj.AddComponent<SphereCollider>();
                collider.radius = 9;
                collider.isTrigger = true;
            }).Register();

        // Shrine base receptacle

        new CustomItemReceptacle<ShrineBaseReceptacle>(PrefabInfo.WithTechType("ShrineBaseReceptacle"))
        {
            ModifyPrefab = (obj, receptacle) =>
            {
                obj.transform.Find("precursor_Teleporter_Activation_Terminal_geo").gameObject.SetActive(false);
                var ui = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("CatalystReceptacleUI"),
                    obj.transform);
                ui.transform.localPosition = new Vector3(-0.3f, 2.3f, 0);
                ui.transform.localEulerAngles = new Vector3(0, 270, 0);
                ui.transform.localScale = Vector3.one * 1.4f;
                ui.SetActive(false);
                receptacle.collider = obj.GetComponent<Collider>();
                var bob = ui.AddComponent<UIBobAnimation>();
                bob.axis = Vector3.up;
                bob.distance = 0.03f;
                bob.speed = 2f;
                var fader = ui.AddComponent<CanvasFader>();
                fader.group = ui.GetComponent<CanvasGroup>();
                fader.speed = 0.4f;
                receptacle.fader = fader;
            }
        }.Register();

        // - B3NT CACHE -

        // Force field control
        new ScriptHolderPrefab(PrefabInfo.WithTechType("B3NTCacheForceFieldOpener"), LargeWorldEntity.CellLevel.Near,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
                trigger.requiredGoal = StoryUtils.BennetInitialMeeting;
                trigger.goal = new StoryGoal(StoryUtils.BennetCacheForceFieldStoryGoal, Story.GoalType.Story, 0);
                var collider = obj.AddComponent<SphereCollider>();
                collider.radius = 8;
                collider.isTrigger = true;
            }).Register();

        // Force field
        new CustomForceFieldPrefab("B3NTCacheForceField",
                () => Plugin.AssetBundle.LoadAsset<GameObject>("B3NTCacheForceFieldPrefab"),
                StoryUtils.BennetCacheForceFieldStoryGoal)
            .Register();

        // Cache music player
        new TrpMusicPlayer("B3NTCacheMusicPlayer", LargeWorldEntity.CellLevel.Medium, "SanctuaryOfTheCreator",
            78, 90).Register();

        // Player flesh cave leaving watcher
        new ScriptHolderPrefab(LeaveFleshCaveWaiter, LargeWorldEntity.CellLevel.Global,
                obj =>
                {
                    obj.AddComponent<WatchPlayerLeavingFleshCave>().goalToComplete = StoryUtils.LeavePlagueCaveGoal;
                })
            .Register();

        // Shrine base post act 2 watcher

        new ScriptHolderPrefab(PrefabInfo.WithTechType("PostAct2BennetShrineTrigger"), LargeWorldEntity.CellLevel.Near,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTriggerInDistance>();
                trigger.activationRadius = 30;
                trigger.requiredGoal = StoryUtils.LeavePlagueCaveGoal.key;
                trigger.goalToComplete = StoryUtils.BennetPostAct2SanctuaryVisit;
            }).Register();


        // - PLAGUE HEART -

        PlagueHeartBayWreck.Register();

        new ScriptHolderPrefab(PrefabInfo.WithTechType("PlagueHeartLocationTrigger"), LargeWorldEntity.CellLevel.Far,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTriggerInDistance>();
                trigger.requiredGoal = StoryUtils.BennetGiveInfectionTracker.key;
                trigger.goalToComplete = StoryUtils.BennetPlagueHeartReaction;
                trigger.activationRadius = 95;
            }).Register();

        ChaosPlagueHeartPrefab.Register();
        PlagueHeartHoleFog.Register();
        DestroyPlagueHeartTimerPrefab.Register();

        new MeteorBloodEmitterPrefab("MeteorMist1",
            () => Plugin.CreaturesBundle.LoadAsset<GameObject>("FleshBlobPrefab"), 4f).Register();
        new MeteorBloodEmitterPrefab("MeteorMist2", () => Plugin.AssetBundle.LoadAsset<GameObject>("FleshMass"), 5f)
            .Register();
        new MeteorBloodEmitterPrefab("MeteorMist3", () => Plugin.AssetBundle.LoadAsset<GameObject>("OrgansProp3"), 7f)
            .Register();

        // Thruster event

        new ScriptHolderPrefab(ThrusterEventTrigger, LargeWorldEntity.CellLevel.Global,
            obj => { obj.AddComponent<TriggerThrusterEventAtSurface>(); }).Register();
    }

    private static void RegisterPdas()
    {
        // RESEARCH TEAM 7
        new AbandonedPda("ResearchTeamADeathLogPDA", "ResearchTeamADeathLog").Register();
        new AbandonedPda("ResearchTeamATabletLogPDA", "ResearchTeamATabletLog").Register();

        // MAZE BASE
        for (int i = 1; i <= 4; i++)
        {
            new AbandonedPda($"WestLog{i}PDA", $"WestLog{i}").Register();
        }

        // AURORA
        new AbandonedPda("DriveCoreWarningLogPDA", "DriveCoreWarningLog").Register();
        new AbandonedPda("CassyPersonnelReportPDA", "CassyPersonnelReport").Register();

        // CYCLOPS WRECK
        new AbandonedPda("PlagueArmorCommentaryPDA", "PlagueArmorCommentary").Register();

        // ACT 2 SURVIVOR BASES
        new AbandonedPda("RedPlagueSurvivorBase1PDA", "RedPlagueSurvivorBase1Log").Register();
        new AbandonedPda("RedPlagueSurvivorBase1PDA2", "RedPlagueSurvivorBase1Log2").Register();
        new AbandonedPda("RedPlagueSurvivorBase2PDA", "RedPlagueSurvivorBase2Log").Register();
        new AbandonedPda("RedPlagueSurvivorBase2PDA2", "RedPlagueSurvivorBase2Log2").Register();
        new AbandonedPda("RedPlagueSurvivorBase3PDA", "RedPlagueSurvivorBase3Log").Register();
        new AbandonedPda("RedPlagueSurvivorBase3PDA2", "RedPlagueSurvivorBase3Log2").Register();

        // METEOR SITE
        new AbandonedPda("PlagueHeartRetrievalPDA1", "PlagueHeartRetrieval1").Register();
        new AbandonedPda("PlagueHeartRetrievalPDA2", "PlagueHeartRetrieval2").Register();
        new AbandonedPda("PlagueHeartRetrievalPDA3", "PlagueHeartRetrieval3").Register();
        new AbandonedPda("PlagueHeartRetrievalPDA4", "PlagueHeartRetrieval4").Register();
        
        // DERMAN
        new AbandonedPda("DermanConnectionLogPDA", "DermanConnectionLog").Register();
    }

    private static void RegisterScenes()
    {
        // Precursor bases
        var shrineBaseSceneData = ScriptableObject.CreateInstance<AdditiveSceneManager.Data>();
        shrineBaseSceneData.scenePath = "Assets/ShrineBase/ShrineBaseScene.unity";
        shrineBaseSceneData.sceneBounds = new Bounds(new Vector3(0, 0, -70), new Vector3(83, 62, 300));
        shrineBaseSceneData.scenePosition = new Vector3(-1517, -868, 920);
        shrineBaseSceneData.sceneRotation = new Vector3(0, 0, 0);
        shrineBaseSceneData.loadDistance = 110;
        shrineBaseSceneData.onSceneLoaded = obj =>
        {
            MaterialUtils.ApplySNShaders(obj, 5.8f, 1.6f, 1f,
                new ColorPropertyModifier(ShaderPropertyID._SpecColor, new Color(1f, 1.5f, 1f, 1f)),
                new ColorPropertyModifier(ShaderPropertyID._GlowColor, Color.white),
                new LightmapMaterialModifier(8),
                new ShrineBaseModifier());
            UWE.CoroutineHost.StartCoroutine(TrpPrefabUtils.GenerateShrineBaseAtmosphereVolumes(obj));
            obj.AddComponent<ShrineBaseController>().renderers = obj.GetComponentsInChildren<Renderer>();
        };
        var shrineBase = new AdditiveSceneObject("ShrineBaseScene", shrineBaseSceneData);
        shrineBase.Register();

        // Flesh cave
        var fleshCaveSceneData = ScriptableObject.CreateInstance<AdditiveSceneManager.Data>();
        fleshCaveSceneData.scenePath = "Assets/Biomes/FleshCave/FleshCaveScene.unity";
        fleshCaveSceneData.sceneBounds = new Bounds(new Vector3(50, 15, 0), new Vector3(740, 590, 1060));
        fleshCaveSceneData.scenePosition = new Vector3(-1500, -650, 350);
        fleshCaveSceneData.sceneRotation = new Vector3(0, 0, 0);
        fleshCaveSceneData.loadDistance = 50;
        fleshCaveSceneData.onSceneLoaded = obj =>
        {
            MaterialUtils.ApplySNShaders(obj, 6.2f, 0.5f);
            var sa = obj.AddComponent<SkyApplier>();
            sa.anchorSky = Skies.Custom;
            sa.renderers = obj.GetComponentsInChildren<Renderer>();
            sa.customSkyPrefab = PlagueCaveSky.gameObject;
            UWE.CoroutineHost.StartCoroutine(TrpPrefabUtils.GenerateFleshCaveAtmosphereVolumes(obj));
            foreach (var collider in obj.GetComponentsInChildren<Collider>())
            {
                collider.gameObject.layer = LayerMask.NameToLayer("TerrainCollider");
            }

            var cacheLod = obj.transform.Find("FleshCavePrefab/FleshCaveCache").gameObject.AddComponent<SimpleLOD>();
            cacheLod.toggledObject = cacheLod.transform.Find("FleshCaveCache-Collisions").gameObject;
            cacheLod.unloadDistance = 225;
        };

        fleshCaveSceneData.onSceneLoadedAsync = TrpPrefabUtils.OnFleshCaveLoadAsync;

        var fleshCave = new AdditiveSceneObject("FleshCaveScene", fleshCaveSceneData);
        fleshCave.Register();
    }

    private static void RegisterPrecursorAssets()
    {
        // Precursor props
        new CustomAnimatedLight("ShrineBaseAnimatedLight-Normal", 20, Vector3.one * 2.4f, 60).Register();
        new CustomAnimatedLight("ShrineBaseAnimatedLight-Bright", 24, Vector3.one * 3f, 65).Register();
        new CustomAnimatedLight("ShrineBaseAnimatedLight-Shrine", 24, Vector3.one * 1, 65, 3f, 55f).Register();

        new RedPlaguePrecursorLight("RedPlaguePrecursorLight", "742b410c-14d4-42c6-ac84-0e2bcaff09c1").Register();
        new RedPlaguePrecursorLight("RedPlaguePrecursorLight2", "473a8c4d-162f-4575-bbef-16c1c97d1e9d").Register();

        PrecursorThruster.Register();

        InfectedArchitectPrefab.Register();

        PlagueOrigin.Register();

        // Floating cache rings
        for (var variants = 1; variants <= 2; variants++)
        {
            var modelName = $"B3NTCacheRing{variants}Prefab";

            // 0 = Perpendicular, 1 = Parallel
            for (var axis = 0; axis <= 1; axis++)
            {
                var rotationAxis = (RotatingRingPrefab.RotationAxis)axis;

                new RotatingRingPrefab($"B3NTCacheRing{variants}-{rotationAxis}-Slow", modelName,
                    rotationAxis, 35).Register();
                new RotatingRingPrefab($"B3NTCacheRing{variants}-{rotationAxis}-Fast", modelName,
                    rotationAxis, 60).Register();
            }
        }

        DestroyedAlienRobot.Register();
        PrecursorRoombaPrefab.Register();
    }

    private static void RegisterFleshAndBonePrefabs()
    {
        var infectedReaperSkeleton = MakeInfectedClone(
            new PrefabInfo("InfectedReaperSkeleton", "InfectedReaperSkeletonPrefab", HarvestableBoneTechType),
            "8fe779a5-e907-4e9e-b748-1eee25589b34", 4f, true);
        infectedReaperSkeleton.Register();

        var reaperWithoutSkullModification = (GameObject go) =>
        {
            foreach (Transform child in go.transform)
            {
                var name = child.gameObject.name;
                if (name.Contains("bone") || name.Contains("skull"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        };

        // InfectedReaperSkeletonNoSkull
        var infectedReaperSkeletonNoSkull = MakeInfectedClone(
            new PrefabInfo("InfectedReaperSkeletonNoSkull", "InfectedReaperSkeletonNoSkullPrefab",
                HarvestableBoneTechType),
            "8fe779a5-e907-4e9e-b748-1eee25589b34", 4f, true, reaperWithoutSkullModification);
        infectedReaperSkeletonNoSkull.Register();

        // InfectedSkeleton1
        var infectedGenericSkeleton1 = MakeInfectedClone(
            new PrefabInfo("InfectedSkeleton1", "InfectedSkeleton1Prefab", HarvestableBoneTechType),
            "0b6ea118-1c0b-4039-afdb-2d9b26401ad2", 7f, true);
        infectedGenericSkeleton1.Register();

        // InfectedSkeleton2
        var infectedGenericSkeleton2 = MakeInfectedClone(
            new PrefabInfo("InfectedSkeleton2", "InfectedSkeleton2Prefab", HarvestableBoneTechType),
            "e10ff9a1-5f1e-4c4d-bf5f-170dba9e321b", 8f, true);
        infectedGenericSkeleton2.Register();

        // InfectedRib
        var infectedRib2 = MakeInfectedClone(
            new PrefabInfo("InfectedRib", "InfectedRibPrefab", HarvestableBoneTechType),
            "33c31a89-9d3b-4717-ad26-4cc8106a1f24", 2f, true);
        infectedRib2.Register();

        // Decals
        new FleshDecorationPrefab(PrefabInfo.WithTechType("FleshRoomDecal"), "FleshRoomDecalPrefab", false, true)
            .Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("FleshRoomDecal2"), "FleshRoomDecal2Prefab", false, true)
            .Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("MiscDecal1"), "MiscDecal1Prefab", false, false).Register();

        // Flesh blocks
        FleshBlockPrefab.RegisterAll();

        // Flesh props
        new SuperFleshDecorationPrefab("FleshMass", "FleshMass", false,
            new WavingEffectModifier(1) { Speed = new Vector4(0.1f, 0.2f) }).Register();
        new SuperFleshDecorationPrefab("FleshWall", "FleshWall", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();
        new SuperFleshDecorationPrefab("OrgansProp1", "OrgansProp1", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) },
            new FloatPropertyModifier("_Shininess", 2.5f)).Register();
        new SuperFleshDecorationPrefab("OrgansProp2", "OrgansProp2", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();
        new SuperFleshDecorationPrefab("OrgansProp3", "OrgansProp3", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();
        new SuperFleshDecorationPrefab("HangingFlesh", "HangingFlesh", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) })
        {
            HasGlobalVariant = true
        }.Register();

        new FleshDecorationPrefab(PrefabInfo.WithTechType("GorePile1"), "GorePile1Prefab", false, false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("GorePile2"), "GorePile2Prefab", false, false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("GorePile3"), "GorePile3Prefab", false, false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("CoreHolder"), "CoreHolderPrefab", false, true).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("CoreHolderGeneric"), "CoreHolderPrefab", false, false)
            .Register();
        new SuperFleshDecorationPrefab("Dangler", "DanglerPrefab", false, new WavingEffectModifier(1)).Register();
        new SuperFleshDecorationPrefab("Drooper", "Drooper", false, new WavingEffectModifier(1)).Register();
        new SuperFleshDecorationPrefab("Roofer", "Roofer", false, new WavingEffectModifier(1)).Register();

        new SuperFleshDecorationPrefab("FleshProp3", "FleshProp3Prefab", false,
            new WavingEffectModifier(0.1f)).Register();
        new SuperFleshDecorationPrefab("FleshProp4", "FleshProp4Prefab", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.06f, 0.03f, 0.06f, 0.05f) }).Register();
        new SuperFleshDecorationPrefab("FleshProp5", "FleshProp5", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();

        new SuperFleshDecorationPrefab("FuckYou", "FuckYouPrefab", false).Register();

        // later we can change this "cyclops island only" prop to remove the visuals and collisions, for future acts 
        new FleshDecorationPrefab(PrefabInfo.WithTechType("VineWall-CYCLOPSISLANDONLY"), "VineWallPrefab_ISLAND", false,
            false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("VineWall"), "VineWallPrefab", false,
            false, new DoubleSidedModifier(MaterialUtils.MaterialType.Transparent)).Register();

        // Veins
        new SuperFleshDecorationPrefab("Veins1", "Vein1_Prefab", false, new VeinsMaterialModifier()).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("DecoProps02Veins"), "DecoProps02Veins", false, false,
            new VeinsMaterialModifier()).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("PrecursorKeyTerminalVeins"), "PrecursorKeyTerminalVeins",
            false, false,
            new VeinsMaterialModifier()).Register();

        // Tentacles
        new SuperFleshDecorationPrefab("FleshTentacle", "DecorationalTentacle",
            false, new WavingEffectModifier(1)).Register();
        CyclopsTentaclePrefab.Register();

        // Bones
        for (var i = 1; i <= 4; i++)
        {
            new FleshDecorationPrefab(PrefabInfo.WithTechType("InfectedGhostSkeleton" + i),
                "GhostSkeletonP" + i, true, false)
            {
                CellLevel = LargeWorldEntity.CellLevel.VeryFar
            }.Register();
        }

        for (var i = 1; i <= 4; i++)
        {
            new FleshDecorationPrefab(PrefabInfo.WithTechType("InfectedReefbackSkeleton" + i),
                "ReefbackSkeletonP" + i, true, false)
            {
                CellLevel = LargeWorldEntity.CellLevel.Far
            }.Register();
        }

        var seaTreaderModelPrefix = "M_Seatreader_".ToLower();
        foreach (var seaTreaderBoneName in Plugin.AssetBundle.GetAllAssetNames()
                     .Where(name => name.Contains(seaTreaderModelPrefix)))
        {
            var fileName =
                seaTreaderBoneName.Substring(
                    seaTreaderBoneName.IndexOf(seaTreaderModelPrefix, StringComparison.Ordinal) +
                    seaTreaderModelPrefix.Length);
            new SeaTreaderBone("InfectedSeaTreaderSkeleton_" + fileName.Split('.')[0], seaTreaderBoneName).Register();
        }

        // Crystals
        LargePlagueCrystal.Register();

        // Plants
        var infectedHangingPlant = MakeInfectedClone(PrefabInfo.WithTechType("InfectedHangingPlant"),
            "8d7f308a-21db-4d1f-99c7-38860e5132e7", 1f, false,
            obj =>
            {
                obj.GetComponentInChildren<Renderer>().material.color = new Color(3, 0.3f, 0.3f);
                obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Far;
            });
        infectedHangingPlant.Register();
        FleshPlant1.Register();
        FleshPlant3.Register();
        InfectedBrainCoral.Register();
        MeatShroom.Register();
        PlaguePlantVariants.Register();
        new SuperFleshDecorationPrefab("MeatwormDecoration", "MeatwormDecorationPrefab", false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("OrnamentPlant1"), "OrnamentPlant1Prefab", false, false,
            new FloatPropertyModifier("_Shininess", 8)).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("OrnamentPlant2"), "OrnamentPlant2Prefab", false, false,
            new FloatPropertyModifier("_Shininess", 8)).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("SanguineBulb"), "SanguineBulbPrefab", false, false,
            new FloatPropertyModifier("_Shininess", 10)).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("MarrowVineLarge"), "MarrowVineLargePrefab", false, false,
            new FloatPropertyModifier("_Shininess", 7), new WavingEffectModifier(0.15f)
            {
                Scale = new Vector4(0.64f, 0, 0.6f, 0.2f),
                Speed = new Vector2(0.12f, 0.25f),
                Frequency = new Vector4(0.6f, 0.5f, 0.75f, 0.8f)
            }, new VectorPropertyModifier("_ObjectUp", new Vector4(0, 1, 0, 0))).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("MarrowVineSmall"), "MarrowVineSmallPrefab", false, false,
            new FloatPropertyModifier("_Shininess", 7), new WavingEffectModifier(0.06f)).Register();

        EyePrefabs.Register();
        AssimilationGeneratorProp.Register();

        // Corpses

        new FleshDecorationPrefab(PrefabInfo.WithTechType("ChainBaseCorpse"), "ChainBaseCorpsePrefab", false, false)
            .Register();
    }

    private static void RegisterLights()
    {
        var lights = new LightPrefab[]
        {
            new("RedPlagueLight1Low", Color.red, 1, 6),
            new("RedPlagueLight1LowBright", Color.red, 2, 6),
            new("RedPlagueLight1Medium", Color.red, 1, 10),
            new("RedPlagueLight1MediumBright", Color.red, 2, 10),
            new("RedPlagueLight1High", Color.red, 1, 14),
            new("RedPlagueLight1HighBright", Color.red, 2, 14),
            new("RedPlagueLight1Massive", Color.red, 1, 40),
            new("RedPlagueLight1MassiveBright", Color.red, 2, 40),

            new("RedPlagueLight2Low", Color.magenta, 1, 6),
            new("RedPlagueLight2LowBright", Color.magenta, 2, 6),
            new("RedPlagueLight2Medium", Color.magenta, 1, 10),
            new("RedPlagueLight2MediumBright", Color.magenta, 2, 10),
            new("RedPlagueLight2High", Color.magenta, 1, 14),
            new("RedPlagueLight2HighBright", Color.magenta, 2, 14),
            new("RedPlagueLight2Massive", Color.magenta, 1, 40),
            new("RedPlagueLight2MassiveBright", Color.magenta, 2, 40)
        };

        foreach (var light in lights)
        {
            light.Register();
        }
    }

    private static void RegisterWreckProps()
    {
        var assetNames = Plugin.AssetBundle.GetAllAssetNames();
        foreach (var assetName in assetNames)
        {
            if (!assetName.EndsWith(".prefab") || !GetFileName(assetName).StartsWith("pf_")) continue;
            var classId = "RPWreck_" +
                          assetName.Substring(2, assetName.LastIndexOf(".prefab", StringComparison.Ordinal) - 2);
            var info = PrefabInfo.WithTechType(classId);
            var prefab = new CustomPrefab(info);
            var template = new AssetBundleTemplate(Plugin.AssetBundle, assetName, info);
            PrefabUtils.AddBasicComponents(template.Prefab, info.ClassID, info.TechType,
                LargeWorldEntity.CellLevel.Near);
            MaterialUtils.ApplySNShaders(template.Prefab, 7.34f);
            prefab.SetGameObject(template);
            prefab.Register();
        }
    }

    private static string GetFileName(string path)
    {
        var indexOfSlash = path.LastIndexOf("/", StringComparison.Ordinal);
        return indexOfSlash == -1 ? string.Empty : path.Substring(indexOfSlash + 1);
    }

    private static void RegisterItems()
    {
        EnzymeParticleInfo = PrefabInfo.WithTechType("InfectedEnzyme")
            .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("InfectedEnzyme42"));
        var enzymeParticle = new CustomPrefab(EnzymeParticleInfo);
        enzymeParticle.SetGameObject(GetEnzymeParticlePrefab);
        enzymeParticle.Register();

        var enzymeContainer = new CustomPrefab(EnzymeContainer);
        var enzymeContainerTemplate = new CloneTemplate(enzymeContainer.Info, TechType.LabContainer3);
        enzymeContainerTemplate.ModifyPrefab += (go) =>
        {
            var renderer = go.transform.Find("biodome_lab_containers_tube_01/biodome_lab_containers_tube_01_glass")
                .GetComponent<Renderer>();
            var material = renderer.material;
            material.color = new Color(1, 1, 1, 0.19f);
            material.SetColor(ShaderPropertyID._SpecColor, new Color(20, 12, 0, 1));
            material.SetFloat("_Shininess", 3);
            if (go.GetComponent<VFXFabricating>() == null)
            {
                PrefabUtils.AddVFXFabricating(go, null, -0.05f, 0.3f);
            }
        };
        enzymeContainer.SetGameObject(enzymeContainerTemplate);
        enzymeContainer.SetRecipe(new RecipeData(new Ingredient(enzymeParticle.Info.TechType, 16),
                new Ingredient(TechType.Titanium, 2), new Ingredient(TechType.Glass, 1)))
            .WithStepsToFabricatorTab("Resources", "AdvancedMaterials").WithCraftingTime(6)
            .WithFabricatorType(CraftTree.Type.Fabricator);
        enzymeContainer.AddGadget(new ScanningGadget(enzymeContainer, TechType.None))
            .WithPdaGroupCategory(TechGroup.Resources, TechCategory.AdvancedMaterials)
            .WithAnalysisTech(Plugin.AssetBundle.LoadAsset<Sprite>("InfectedEnzymeStorageContainer_Popup"),
                KnownTechHandler.DefaultUnlockData.BlueprintUnlockSound,
                KnownTechHandler.DefaultUnlockData.BlueprintUnlockMessage);
        enzymeContainer.Register();

        // Infection tablet
        InfectionTabletInfo
            .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("InfectionTabletIcon"));
        new PrecursorTabletPrefab(InfectionTabletInfo,
            () => Plugin.AssetBundle.LoadAsset<Texture2D>("InfectionTabletTexture"),
            Plugin.AssetBundle.LoadAsset<Sprite>("InfectionTabletPopup"), new RecipeData(
                new Ingredient(RedPlagueSample.Info.TechType, 1),
                new Ingredient(TechType.Diamond, 1)),
            c => c.WithFabricatorType(AdminFabricator.AdminCraftTree),
            true).Register();

        new PrecursorTabletFragmentPrefab(InfectionTabletFragmentInfo,
            () => Plugin.AssetBundle.LoadAsset<Texture2D>("InfectionTabletTexture-Shattered"),
            InfectionTabletInfo.TechType, Color.red).Register();

        // Shrine base tablet

        GoldTabletInfo.WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("ShrineBaseTabletIcon"));
        new PrecursorTabletPrefab(GoldTabletInfo,
            () => Plugin.AssetBundle.LoadAsset<Texture2D>("ShrineBaseTabletTexture"),
            Plugin.AssetBundle.LoadAsset<Sprite>("ShrineBaseTabletPopup"), new RecipeData(
                new Ingredient(TechType.PrecursorIonCrystal, 1),
                new Ingredient(TechType.Gold, 2)),
            c => c.WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab(CraftTreeHandler.Paths.FabricatorEquipment),
            false).Register();

        WarperHeart.WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("WarperHeartIcon"));
        var warperHeart = new CustomPrefab(WarperHeart);
        warperHeart.SetGameObject(GetWarperInnardsPrefab);
        warperHeart.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        warperHeart.Register();

        AmalgamatedBone.WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("AmalgamatedBone"));
        var amalgamatedBonePrefab = new CustomPrefab(AmalgamatedBone);
        var amalgamatedBoneTemplate = new CloneTemplate(AmalgamatedBone, "42e1ac56-6fab-4a9f-95d9-eec5707fe62b");
        amalgamatedBoneTemplate.ModifyPrefab += (go) =>
        {
            foreach (Transform child in go.transform)
            {
                child.localScale *= 0.3f;
            }

            go.AddComponent<InfectAnything>().infectionHeightStrength = 0.2f;
            go.AddComponent<Pickupable>();
            go.AddComponent<TechTag>().type = AmalgamatedBone.TechType;
            var rb = go.EnsureComponent<Rigidbody>();
            rb.mass = 13;
            rb.useGravity = false;
            rb.isKinematic = true;
            var wf = go.EnsureComponent<WorldForces>();
            wf.useRigidbody = rb;
            wf.underwaterDrag = 2;
            PrefabUtils.AddResourceTracker(go, AmalgamatedBone.TechType);
        };
        amalgamatedBonePrefab.SetGameObject(amalgamatedBoneTemplate);
        amalgamatedBonePrefab.SetSpawns(new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_SandDune,
                count = 1,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_Grass,
                count = 1,
                probability = 0.4f
            });
        amalgamatedBonePrefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        amalgamatedBonePrefab.Register();
        CraftDataHandler.SetHarvestOutput(HarvestableBoneTechType, AmalgamatedBone.TechType);
        CraftDataHandler.SetHarvestType(HarvestableBoneTechType, HarvestType.DamageAlive);

        RedPlagueSample.Register();
        PlagueCatalyst.Register();
        DormantNeuralMatter.Register();
        PlagueIngot.Register();
        MysteriousRemains.Register();
        BloodQuartz.Register();
        Obsidian.Register();

        InsanityMedicine.Register();
        SuitCharge.Register();
        BioBattery.Register();

        new Meatball(PrefabInfo.WithTechType("Meatball").WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("MeatballIcon")))
            .Register();
    }

    private static void RegisterManagers()
    {
        var npcSurvivorManager = new CustomPrefab(NpcSurvivorManager);
        npcSurvivorManager.SetGameObject(GetNPCSurvivorManagerPrefab);
        npcSurvivorManager.SetSpawns(new SpawnLocation(Vector3.zero));
        npcSurvivorManager.Register();

        InfectionCaveHoleCutter.Register();

        new PropDestroyer(PrefabInfo.WithTechType("InfectionCavePropDestroyer"), new[]
        {
            TechType.Salt, TechType.DrillableSalt, TechType.DrillableTitanium, TechType.DrillableLead, TechType.Quartz,
            TechType.ScrapMetal, AmalgamatedBone.TechType, TechType.DrillableSilver, TechType.DrillableQuartz
        }, Array.Empty<string>(), 22).Register();

        new GhostCyclopsSpawnerPrefab("LRBase1",
            GhostCyclopsCinematic.Path.LostRiverBase, 60).Register();
        new GhostCyclopsSpawnerPrefab("LRBase2",
            GhostCyclopsCinematic.Path.LostRiverBaseCorridor, 50).Register();
        new GhostCyclopsSpawnerPrefab("LRAncientSkeleton",
            GhostCyclopsCinematic.Path.LostRiverAncientSkeleton, 60).Register();
    }

    private static void RegisterCreaturesAndCorpses()
    {
        var infectedCorpse = new CorpsePrefab(InfectedCorpseInfo, "DiverCorpse", true, false);
        infectedCorpse.Register();

        var skeletonCorpse = new CorpsePrefab(SkeletonCorpse, "SkeletonRagdoll", true, false);
        skeletonCorpse.Register();

        new FloatingCorpsePrefab("FloatingCorpse1", "FloatingCorpsePrefab").Register();

        var mutantDiver1 = new Mutant(MutantDiver1, "MutatedDiver1", Mutant.Settings.Normal, true);
        mutantDiver1.Register();

        var mutantDiver2 = new Mutant(MutantDiver2, "MutatedDiver2", Mutant.Settings.Normal, true);
        mutantDiver2.Register();

        var mutantDiver3 = new Mutant(MutantDiver3, "MutatedDiver3",
            Mutant.Settings.HeavilyMutated | Mutant.Settings.Large, true);
        mutantDiver3.Register();

        var mutantDiver3Small =
            new Mutant(MutantDiver3Small, "MutatedDiver3Small", Mutant.Settings.HeavilyMutated, false);
        mutantDiver3Small.Register();

        var mutantDiver4 = new Mutant(MutantDiver4, "MutatedDiver4",
            Mutant.Settings.HeavilyMutated | Mutant.Settings.Large, true);
        mutantDiver4.Register();

        new SuckerPrefab(PrefabInfo.WithTechType("Sucker"), true).Register();
        new SuckerPrefab(PrefabInfo.WithTechType("SuckerGeneric"), false).Register();

        new SuckerController(SuckerController).Register();

        var mrTeethSpawnPoint = new CustomPrefab(PrefabInfo.WithTechType("MrTeethSpawnPoint"));
        mrTeethSpawnPoint.SetGameObject(() =>
        {
            var obj = new GameObject("MrTeethSpawnPoint");
            PrefabUtils.AddBasicComponents(obj, mrTeethSpawnPoint.Info.ClassID, mrTeethSpawnPoint.Info.TechType,
                LargeWorldEntity.CellLevel.Near);
            obj.AddComponent<MrTeethSpawnPoint>();
            return obj;
        });
        mrTeethSpawnPoint.Register();

        var mrTeethSpawner = new CustomPrefab(PrefabInfo.WithTechType("MrTeethSpawner"));
        mrTeethSpawner.SetGameObject(() =>
        {
            var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("FleshMass"));
            PrefabUtils.AddBasicComponents(obj, mrTeethSpawner.Info.ClassID, mrTeethSpawner.Info.TechType,
                LargeWorldEntity.CellLevel.Near);
            MaterialUtils.ApplySNShaders(obj);
            obj.AddComponent<MrTeethSpawner>();
            return obj;
        });
        mrTeethSpawner.Register();

        var mrTeethReturnPoint = new CustomPrefab(PrefabInfo.WithTechType("MrTeethReturnPoint"));
        mrTeethReturnPoint.SetGameObject(() =>
        {
            var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("FleshMass"));
            PrefabUtils.AddBasicComponents(obj, mrTeethReturnPoint.Info.ClassID, mrTeethReturnPoint.Info.TechType,
                LargeWorldEntity.CellLevel.Near);
            MaterialUtils.ApplySNShaders(obj);
            obj.AddComponent<MrTeethReturnPoint>();
            return obj;
        });
        mrTeethReturnPoint.Register();

        var mrTeeth = new MrTeethPrefab(MrTeeth);
        mrTeeth.Register();

        AuroraTentaclePrefab.Register();
        RepairedAuroraPrefab.Register();

        var drifter = new DrifterPrefab(DrifterInfo, false);
        drifter.Register();
        PDAHandler.AddCustomScannerEntry(DrifterInfo.TechType, 2, false, "Drifter");
        PDAHandler.AddEncyclopediaEntry("Drifter", "Lifeforms/Fauna/PlagueCreations", null, null);
        var drifterFlyOnly = new DrifterPrefab(DrifterFlyOnlyInfo, true);
        drifterFlyOnly.Register();
        var drifterHiveMindSpawn =
            new SpawnAfterStoryGoalPrefab(DrifterHivemindSpawn, DrifterInfo.ClassID,
                () => StoryUtils.HiveMindReleasedGoal.key, LargeWorldEntity.CellLevel.VeryFar);
        drifterHiveMindSpawn.Register();

        new MimicPeeper(PrefabInfo.WithTechType("MimicPeeper")).Register();
        new MimicOculus(PrefabInfo.WithTechType("MimicOculus")).Register();
        new FleshStalker(PrefabInfo.WithTechType("FleshStalker")).Register();
        new PlagueBladderFish(PrefabInfo.WithTechType("PlagueBladderFish")).Register();
        new InvertedSpadeFish(PrefabInfo.WithTechType("InvertedSpadefish")).Register();
        new InfestedReefback(PrefabInfo.WithTechType("InfestedReefback")).Register();
        new MutantBoomerang(PrefabInfo.WithTechType("MutantBoomerang")).Register();
        new MimicGasopod(PrefabInfo.WithTechType("MimicGasopod")).Register();
        InfectionPod.Register();
        new TeethTeeth(PrefabInfo.WithTechType("TeethTeeth")).Register();
        new EyeyeCaptain(PrefabInfo.WithTechType("EyeyeCaptain")).Register();
        new PhantomLeviathan(PrefabInfo.WithTechType("PhantomLeviathan")).Register();
        new PlagueFloater(PrefabInfo.WithTechType("PlagueFloater")).Register();

        // CHAOS LEVIATHAN
        var chaosLeviathanTechType = EnumHandler.AddEntry<TechType>("ChaosLeviathan");
        new ChaosLeviathanPrefab(
            new PrefabInfo("RoamingChaosLeviathan", "PrefabFile_RoamingChaosLeviathan", chaosLeviathanTechType),
            true).Register();
        // Register the normal command-spawnable one second so that it is used for commands
        new ChaosLeviathanPrefab(
            new PrefabInfo("ChaosLeviathan", "PrefabFile_ChaosLeviathan", chaosLeviathanTechType),
            false).Register();
        PDAHandler.AddEncyclopediaEntry("ChaosLeviathan", CustomPdaPaths.PlagueCreationsPath, null, null, null,
            Plugin.CreaturesBundle.LoadAsset<Sprite>("ChaosPopup"), PDAHandler.UnlockBasic);
        PDAHandler.AddCustomScannerEntry(chaosLeviathanTechType, 4, encyclopediaKey: "ChaosLeviathan");

        // Possessed vehicles
        new PossessedVehicle(TechType.Seamoth).Register();
        new PossessedVehicle(TechType.Exosuit).Register();

        ScaryMannySpawnPoint.Register();
        ScaryMannyTrigger.Register();

        // Stationary creatures
        StabbyPrefab.Register();
        GrabberPrefab.Register();

        // Insectoids
        new InsectoidPrefab(Insectoid, 0.4f).Register();
        new InsectoidPrefab(SmallInsectoid, 0.23f).Register();

        // The observer / plague cat
        var observerInfo = PrefabInfo.WithTechType("Observer");
        new Observer(observerInfo).Register();
        var observerSpawner =
            new SpawnAfterStoryGoalPrefab(PrefabInfo.WithTechType("ObserverSpawner"), observerInfo.ClassID,
                () => StoryUtils.DomeConstructionEvent.key, LargeWorldEntity.CellLevel.Far);
        observerSpawner.Register();

        // Pets
        new Gilbert().Register();
        new ConsciousNeuralMatter().Register();
        new Hippopenomenon().Register();

        // Crawling flesh masses
        new CrawlingFleshPrefab(PrefabInfo.WithTechType("CrawlingFleshMass"),
            () => Plugin.AssetBundle.LoadAsset<GameObject>("FleshMass"), LargeWorldEntity.CellLevel.Medium,
            new CrawlingFleshPrefab.MovementSettings(
                0.5f, 4f, 40, 0.5f, 0f, 3f, 15f),
            new WavingEffectModifier(1)
                { Speed = new Vector4(0.1f, 0.2f), Scale = new Vector4(0.02f, 0.02f, 0.02f, 0.02f) }).Register();
    }

    private static void RegisterEquipment()
    {
        InfectionTracker.Register();

        BoneArmor.Register();

        PlagueKnife.Register();

        AirStrikeDevice.Register();

        PlagueCyclopsCore.Register();

        AlterraBomb.Register();

        BoneCannonPrefab.Register();

        BiochemicalProtectionSuit.Register();

        InfectionSamplerTool.Register();
        InfectionSamplerFragment.Register();

        AntiPossessionModule.Register();

        PlagueGrappler.Register();

        ObsidianBladeArmModule.Register();
    }

    private static void RegisterFood()
    {
        // HAM
        var hamInfo = PrefabInfo.WithTechType("RedPlagueHam")
            .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("Ham"));
        var hamPrefab = new CustomPrefab(hamInfo);
        hamPrefab.SetGameObject(() => TrpPrefabUtils.CreateLootCubePrefab(hamInfo));
        hamPrefab.Register();

        // CHEESE
        var cheeseInfo = PrefabInfo.WithTechType("RedPlagueCheese")
            .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("Cheese"));
        var cheeseObject = Plugin.AssetBundle.LoadAsset<GameObject>("CheesePrefab");
        PrefabUtils.AddBasicComponents(cheeseObject, cheeseInfo.ClassID, cheeseInfo.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(cheeseObject);
        var cheeseEatable = cheeseObject.EnsureComponent<Eatable>();
        cheeseEatable.decomposes = false;
        cheeseEatable.foodValue = 5;
        cheeseEatable.waterValue = 0;
        cheeseObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(cheeseObject, 0.028f, 0.5f);
        var cheesePrefab = new CustomPrefab(cheeseInfo);
        cheesePrefab.SetGameObject(cheeseObject);
        cheesePrefab.Register();

        // THE REGULAR
        var theRegularInfo = PrefabInfo.WithTechType("TheRegular", true)
            .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("TheRegular"));
        var theRegularObject = Plugin.AssetBundle.LoadAsset<GameObject>("TheRegularSandwich");
        PrefabUtils.AddBasicComponents(theRegularObject, theRegularInfo.ClassID, theRegularInfo.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(theRegularObject);
        var eatable = theRegularObject.EnsureComponent<Eatable>();
        eatable.decomposes = false;
        eatable.foodValue = 20;
        eatable.waterValue = -2;
        theRegularObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(theRegularObject, 1f, isKinematic: true);
        PrefabUtils.AddVFXFabricating(theRegularObject, "ham-and-cheese", 0, 0.3f, new Vector3(0, 0.024f, 0), 50,
            new Vector3(270, 0, 0));
        var theRegularPrefab = new CustomPrefab(theRegularInfo);
        theRegularPrefab.SetGameObject(theRegularObject);
        theRegularPrefab.SetRecipe(new RecipeData(new Ingredient(hamInfo.TechType, 2),
                new Ingredient(cheeseInfo.TechType, 1)))
            .WithFabricatorType(AdminFabricator.AdminCraftTree);
        theRegularPrefab.Register();

        // BANANA
        var bananaObject = Plugin.AssetBundle.LoadAsset<GameObject>("BananaPrefab");
        PrefabUtils.AddBasicComponents(bananaObject, BananaInfo.ClassID, BananaInfo.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(bananaObject);
        var bananaEatable = bananaObject.EnsureComponent<Eatable>();
        bananaEatable.decomposes = false;
        bananaEatable.foodValue = 18;
        bananaEatable.waterValue = 0;
        bananaObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(bananaObject, 0.6f, 0.7f, isKinematic: true);
        BananaInfo.WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("BananaIcon"));
        var bananaPrefab = new CustomPrefab(BananaInfo);
        bananaPrefab.SetGameObject(bananaObject);
        bananaPrefab.Register();
        
        // MONIKA (CINNY ROLL)
        var monikaInfo = PrefabInfo.WithTechType("Monika");
        var monikaObject = Plugin.AssetBundle.LoadAsset<GameObject>("MonikaPrefab");
        PrefabUtils.AddBasicComponents(monikaObject, monikaInfo.ClassID, monikaInfo.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(monikaObject);
        var monikaEatable = monikaObject.EnsureComponent<Eatable>();
        monikaEatable.decomposes = false;
        monikaEatable.foodValue = 15;
        monikaEatable.waterValue = 4;
        monikaObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(monikaObject, 0.5f, 0.6f, isKinematic: true);
        monikaInfo.WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("MonikaIcon"));
        var monikaPrefab = new CustomPrefab(monikaInfo);
        monikaPrefab.SetGameObject(monikaObject);
        monikaPrefab.Register();

        // PROTEIN SNACK
        // uses the creature bundle to not redistribute the bladderfish assets between two different bundles
        var proteinSnackObject = Plugin.CreaturesBundle.LoadAsset<GameObject>("ProteinSnackPrefab");
        PrefabUtils.AddBasicComponents(proteinSnackObject, ProteinSnackInfo.ClassID, ProteinSnackInfo.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(proteinSnackObject);
        var proteinSnackEatable = proteinSnackObject.EnsureComponent<Eatable>();
        proteinSnackEatable.foodValue = 52;
        proteinSnackEatable.waterValue = -7;
        proteinSnackObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(proteinSnackObject, 1.3f, 0.8f);
        PrefabUtils.AddVFXFabricating(proteinSnackObject, "Model", -0.2f, 0.1f, new Vector3(0, 0.024f, 0), 0.1f,
            new Vector3(0, 0, 90));
        proteinSnackObject.AddComponent<UsableItem>().SetOnUseAction(ProteinSnackInfo.ClassID,
            () => { PlagueDamageStat.main.TakeInfectionDamage(40); });
        ProteinSnackInfo.WithIcon(Plugin.CreaturesBundle.LoadAsset<Sprite>("ProteinSnackIcon"));
        var proteinSnackPrefab = new CustomPrefab(ProteinSnackInfo);
        proteinSnackPrefab.SetGameObject(proteinSnackObject);
        proteinSnackPrefab.SetRecipe(new RecipeData(new Ingredient(PlagueIngot.Info.TechType, 1),
                new Ingredient(MeatShroom.Info.TechType, 1)))
            .WithFabricatorType(PlagueAltar.CraftTreeType)
            .WithStepsToFabricatorTab(PlagueAltar.ConsumableTab);
        proteinSnackPrefab.SetPdaGroupCategory(TechGroup.Survival, TechCategory.CookedFood);
        proteinSnackPrefab.SetUnlock(MeatShroom.Info.TechType);
        proteinSnackPrefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        proteinSnackPrefab.Register();
    }

    private static void RegisterDataboxesAndConsoles()
    {
        new DataboxPrefab(PlagueKnifeDatabox, PlagueKnife.Info.TechType).Register();
        new DataboxPrefab(BoneArmorDatabox, BoneArmor.Info.TechType).Register();
        new DataboxPrefab(PlagueGrapplerDatabox, PlagueGrappler.Info.TechType).Register();
        new DataboxPrefab(AssimilationGeneratorDatabox, AssimilationGenerator.Info.TechType).Register();
        new DataboxPrefab(BioBatteryDatabox, BioBattery.Info.TechType).Register();
        new DataboxPrefab(InsanityDeterrentDatabox, InsanityDeterrent.Info.TechType).Register();
        new DataboxPrefab(ObsidianArmDatabox, ObsidianBladeArmModule.Info.TechType).Register();
    }

    private static void RegisterDropPodPrefabs()
    {
        AdministratorDropPod.Register();
        AdminDropPodBeacon.Register();
        AdminFabricatorFragment.Register();
    }

    private static void RegisterBuildables()
    {
        // Alterra tech
        ShuttlePad.Register();
        AdminFabricator.Register();
        InsanityDeterrent.Register();

        // Dome construction drones
        DomeDronePrefab.Register();
        DomeDroneFormationPrefab.Register();

        // Plague tech
        PlagueNeutralizer.Register();
        PlagueNeutralizerFragment.Register();
        PlagueAltar.Register();
        PlagueAltarFragment.Register();

        AssimilationGenerator.Register();

        // Derman tech
        PlagueRefinery.Register();
        PdaExploder.Register();

        // Precursor tech
        SatelliteCommunicationDevice.Register();
    }

    private static void RegisterEasterEggs()
    {
        var cigarretePrefab = new CustomPrefab(PrefabInfo.WithTechType("500Cigarettes"));
        var cigTemplate = new AssetBundleTemplate(Plugin.AssetBundle, "500Cigarettes", cigarretePrefab.Info);
        cigarretePrefab.SetGameObject(cigTemplate);
        PrefabUtils.AddBasicComponents(cigTemplate.Prefab, cigarretePrefab.Info.ClassID, cigarretePrefab.Info.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(cigTemplate.Prefab);
        cigarretePrefab.Register();

        new Toy("Chrissy", "ChrissyPrefab", "ChrissyIcon").Register();
        new Toy("PrecursorSkull", "PrecursorSkull", "PrecursorSkullIcon").Register();
        new Toy("BingBong", "BingBongPrefab", "GenericDecoIcon").Register();
        new PosterPrefab(PrefabInfo.WithTechType("XenaPoster")
                .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("XenaPosterIcon")),
            () => Plugin.AssetBundle.LoadAsset<Texture2D>("XenaPoster")).Register();

        void RegisterClericalPropRetexture(string classId, string originalPropClassId, Func<Texture2D> texture)
        {
            var prefab = new CustomPrefab(PrefabInfo.WithTechType(classId)
                .WithIcon(MiscDecoIcon));
            prefab.SetEquipment(EquipmentType.Hand);
            var template = new CloneTemplate(prefab.Info, originalPropClassId)
            {
                ModifyPrefab = obj =>
                {
                    var renderer = obj.GetComponentInChildren<Renderer>();
                    var material = renderer.material;
                    var loadedTexture = texture.Invoke();
                    material.mainTexture = loadedTexture;
                    material.SetTexture(ShaderPropertyID._SpecTex, loadedTexture);
                    obj.AddComponent<Pickupable>();
                    var placeTool = obj.AddComponent<PlaceTool>();
                    placeTool.allowedOnGround = true;
                    placeTool.allowedInBase = true;
                    placeTool.allowedOnConstructable = true;
                    placeTool.mainCollider = obj.GetComponentInChildren<Collider>();
                    placeTool.hasAnimations = false;
                    var viewModel = new GameObject("ViewModel");
                    viewModel.transform.parent = obj.transform;
                    var fpModel = obj.AddComponent<FPModel>();
                    fpModel.propModel = renderer.gameObject;
                    fpModel.viewModel = viewModel;
                    PrefabUtils.AddWorldForces(obj, 1, 0.5f, 0.5f, true);
                }
            };
            prefab.SetGameObject(template);
            prefab.Register();
        }

        RegisterClericalPropRetexture("SockDrawing", "a7519acf-6dec-429e-82ed-bbcf7a616c50",
            () => Plugin.AssetBundle.LoadAsset<Texture2D>("docking_clerical_trp_variants_1"));

        RegisterClericalPropRetexture("CrumpledMazieDrawing", "32e48451-8e81-428e-9011-baca82e9cd32",
            () => Plugin.AssetBundle.LoadAsset<Texture2D>("docking_clerical_trp_variants_1"));

        RegisterClericalPropRetexture("CrimxsenCard", "45af7cd6-36a9-4ced-a7b9-2b522022f2c8",
            () => Plugin.AssetBundle.LoadAsset<Texture2D>("docking_clerical_trp_variants_1"));

        CorruptedRadioPrefab.Register();
    }

    private static void RegisterVFX()
    {
        var bloodParticles = new BloodParticle[]
        {
            new(PrefabInfo.WithTechType("VFX_BloodSplashContinuous01"), "Blood_Splash_01_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodSplashContinuous02"), "Blood_Splash_02_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodDecalContinuous01"), "BloodDecal_01_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodDecalContinuous02"), "BloodDecal_02_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodTrail01"), "BloodDecal_01_Trail"),
            new(PrefabInfo.WithTechType("VFX_BloodTrail02"), "BloodDecal_02_Trail")
        };
        foreach (var bloodParticle in bloodParticles) bloodParticle.Register();

        var bloodPools = new BloodPool[]
        {
            new("VFX_BloodPool1", "d931cce0-b6b3-4f70-aa08-e1ed5ef12b29")
        };
        foreach (var bloodPool in bloodPools) bloodPool.Register();

        var bloodWaterFall = new BloodWaterfall[]
        {
            new("VFX_BloodWaterfall1", "e712fdde-4d3d-4242-b618-cd43a08f0e96")
        };
        foreach (var bloodWaterfall in bloodWaterFall) bloodWaterfall.Register();
    }

    private static void RegisterSoundPrefabs()
    {
        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("WhispersOfTheDeadA"),
                AudioUtils.GetFmodAsset("WhispersOfTheDeadA"), 5, 12, 35f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXFemaleScreamA"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamA"), 8, 10, 18)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXFemaleScreamB"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamB"), 8, 10, 18)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMaleScreamA"),
                AudioUtils.GetFmodAsset("TrpMaleScreamA"), 3, 5, 18)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMaleScreamB"),
                AudioUtils.GetFmodAsset("TrpMaleScreamB"), 3, 5, 18)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXFemaleScreamA-OneShot"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamA"), 6, 10, 15f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXFemaleScreamB-OneShot"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamB"), 6, 10, 15f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMaleScreamA-OneShot"),
                AudioUtils.GetFmodAsset("TrpMaleScreamA"), 6, 10, 15f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMaleScreamB-OneShot"),
                AudioUtils.GetFmodAsset("TrpMaleScreamB"), 6, 10, 15f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXDemonAmbience"),
                AudioUtils.GetFmodAsset("TrpDemonAmbience"), 5, 8, 40f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXDoorKnockA"),
                AudioUtils.GetFmodAsset("TrpDoorKnockA"), 5, 12, 30, false, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXDoorKnockB"),
                AudioUtils.GetFmodAsset("TrpDoorKnockB"), 5, 12, 30, false, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXDoorKnockC"),
                AudioUtils.GetFmodAsset("TrpDoorKnockC"), 5, 10, 30, false, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXDoorKnockA-OneShot"),
                AudioUtils.GetFmodAsset("TrpDoorKnockA"), 5, 12, 16, true, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXDoorKnockB-OneShot"),
                AudioUtils.GetFmodAsset("TrpDoorKnockB"), 5, 12, 16, true, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXDoorKnockC-OneShot"),
                AudioUtils.GetFmodAsset("TrpDoorKnockC"), 5, 10, 16, true, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMetalGrowlA"),
                AudioUtils.GetFmodAsset("TrpMetalGrowlA"), 11, 21, 35f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMetalScrapeA"),
                AudioUtils.GetFmodAsset("TrpMetalScrapeA"), 7, 14, 35f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMrTeethScream-Close-OneShot"),
                AudioUtils.GetFmodAsset("MrTeethScreamOneShot"), 7, 14, 10f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMrTeethScream-Medium-OneShot"),
                AudioUtils.GetFmodAsset("MrTeethScreamOneShot"), 7, 14, 20f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMrTeethScream-Far-OneShot"),
                AudioUtils.GetFmodAsset("MrTeethScreamOneShot"), 7, 14, 30f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXObserver"),
                AudioUtils.GetFmodAsset("TrpObserverAmbience"), 8, 11, 40f)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXObserver-OneShot"),
                AudioUtils.GetFmodAsset("TrpObserverAmbience"), 8, 11, 40f, true)
            .Register();

        new RandomSoundPlayerPrefab(PrefabInfo.WithTechType("TrpSFXMetalDoorClose-OneShot"),
                AudioUtils.GetFmodAsset("TrpMetalDoorClose"), 8, 11, 26f, true)
            .Register();
    }

    private static void RegisterFleshBlobs()
    {
        FleshBlobLeaders.RegisterAll();
    }

    private static CustomPrefab MakeInfectedClone(PrefabInfo info, string cloneClassID, float scale, bool isBone,
        Action<GameObject> modifyPrefab = null)
    {
        var prefab = new CustomPrefab(info);
        var template = new CloneTemplate(prefab.Info, cloneClassID);
        if (modifyPrefab != null)
        {
            template.ModifyPrefab += modifyPrefab;
        }

        template.ModifyPrefab += go =>
        {
            var infect = go.AddComponent<InfectAnything>();
            infect.infectionScale = Vector3.one * 2;
            infect.infectionAmount = 1;
            if (Math.Abs(scale - 1f) > 0.001f)
            {
                var scaler = new GameObject("Scaler").transform;
                scaler.parent = go.transform;
                scaler.localPosition = Vector3.zero;
                while (go.transform.childCount > 1)
                {
                    go.transform.GetChild(0).parent = scaler;
                }

                scaler.transform.localScale = Vector3.one * scale;
            }

            if (isBone)
            {
                go.EnsureComponent<TechTag>().type = HarvestableBoneTechType;
                go.AddComponent<LiveMixin>().data = HarvestableBoneLiveMixinData;
            }
        };

        prefab.SetGameObject(template);
        return prefab;
    }

    private static IEnumerator GetInfectionLaserPrefab(IOut<GameObject> prefab)
    {
        var solarPanelRequest = CraftData.GetPrefabForTechTypeAsync(TechType.SolarPanel);
        yield return solarPanelRequest;
        var solarPanelPrefab = solarPanelRequest.GetResult();
        var obj = Object.Instantiate(solarPanelPrefab.GetComponent<PowerFX>().vfxPrefab);
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, InfectionLaserInfo.ClassID, InfectionLaserInfo.TechType,
            LargeWorldEntity.CellLevel.Global);
        var line = obj.GetComponent<LineRenderer>();
        var newMaterial = new Material(line.material);
        newMaterial.color = new Color(1.5f, 0.142f, 0.285f);
        line.material = newMaterial;
        line.widthMultiplier = 30;
        line.endWidth = 20;
        line.SetPositions(new[] { new Vector3(-78.393f, 341.175f, -57.684f), new Vector3(0, 2055, 0) });
        obj.AddComponent<LaserMaterialManager>();
        prefab.Set(obj);
    }

    private static IEnumerator GetEnzymeParticlePrefab(IOut<GameObject> prefab)
    {
        var request = UWE.PrefabDatabase.GetPrefabAsync("505e7eff-46b3-4ad2-84e1-0fadb7be306c");
        yield return request;
        request.TryGetPrefab(out var reference);
        var go = Object.Instantiate(reference);
        PrefabUtils.AddBasicComponents(go, EnzymeParticleInfo.ClassID, EnzymeParticleInfo.TechType,
            LargeWorldEntity.CellLevel.VeryFar);
        Object.DestroyImmediate(go.GetComponent<EnzymeBall>());
        Object.DestroyImmediate(go.transform.Find("collider").GetComponent<GenericHandTarget>());
        var renderer = go.transform.Find("Leviathan_enzymeBall_anim/enzymeBall_geo").GetComponent<Renderer>();
        var material = renderer.material;
        material.color = Color.red;
        material.SetColor(ShaderPropertyID._SpecColor, new Color(0.877f, 1f, 0.838f));
        go.AddComponent<Pickupable>();
        prefab.Set(go);
    }

    private static IEnumerator GetNPCSurvivorManagerPrefab(IOut<GameObject> prefab)
    {
        var go = new GameObject(NpcSurvivorManager.ClassID);
        PrefabUtils.AddBasicComponents(go, NpcSurvivorManager.ClassID, NpcSurvivorManager.TechType,
            LargeWorldEntity.CellLevel.Global);
        go.SetActive(false);
        go.AddComponent<NpcSurvivorManager>();
        var johnKyle = go.AddComponent<NpcSurvivor>();
        johnKyle.survivorName = "JohnKyle";
        var sylvie = go.AddComponent<NpcSurvivor>();
        sylvie.survivorName = "Sylvie";
        sylvie.model = NpcSurvivor.ModelType.PrawnSuit;
        var simon = go.AddComponent<NpcSurvivor>();
        simon.survivorName = "Simon";
        prefab.Set(go);
        yield break;
    }

    private static IEnumerator GetWarperInnardsPrefab(IOut<GameObject> prefab)
    {
        var go = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("WarperInnards_Prefab"));
        go.SetActive(false);
        PrefabUtils.AddBasicComponents(go, WarperHeart.ClassID, WarperHeart.TechType,
            LargeWorldEntity.CellLevel.Near);
        go.AddComponent<Pickupable>();
        var rb = go.AddComponent<Rigidbody>();
        rb.mass = 10;
        rb.useGravity = false;
        rb.isKinematic = true;
        var wf = go.AddComponent<WorldForces>();
        wf.useRigidbody = rb;
        var warperTask = CraftData.GetPrefabForTechTypeAsync(TechType.Warper);
        yield return warperTask;
        var heartMaterial = new Material(warperTask.GetResult().transform.Find("warper_anim/warper_geos/Warper_geo")
            .gameObject.GetComponent<Renderer>().materials[1]);
        heartMaterial.color = Color.red;
        heartMaterial.SetColor(ShaderPropertyID._SpecColor, Color.red * 4);
        heartMaterial.SetColor(ShaderPropertyID._GlowColor, Color.red * 4);
        heartMaterial.SetFloat("_Shininess", 8);
        go.GetComponentInChildren<Renderer>().material = heartMaterial;
        PrefabUtils.AddResourceTracker(go, WarperHeart.TechType);
        prefab.Set(go);
    }
}