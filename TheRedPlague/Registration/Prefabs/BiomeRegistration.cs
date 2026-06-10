using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Content.Act1.FloatingIsland;
using UnityEngine;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class BiomeRegistration
{
    public static mset.Sky PlagueCaveSky { get; private set; }

    [PrefabRegistration]
    private static void Register()
    {
        RegisterSkies();
        RegisterBiomes();
    }

    private static void RegisterSkies()
    {
        PlagueCaveSky = BiomeUtils
            .CreateSkyPrefab("FleshCaveSky", AssetBundles.Core.LoadAsset<Texture>("FleshCaveCubemap"), false).sky;
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
        
        var fleshCaveEntranceBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(16, 13, 13), 1f,
            new Color(0.441f, 0.1f, 0.1f), 0.2f, new Color(1, 0f, 0f), 0.007f, 20f, 0f, 0f, 36.1f);
        BiomeHandler.RegisterBiome("fleshcave_upper", fleshCaveEntranceBiomeSettings,
            new BiomeHandler.SkyReference(PlagueCaveSky.gameObject));
        var fleshCaveEntrancePrefab = new CustomPrefab(PrefabInfo.WithTechType("FleshCaveUpperVolume"));
        var fleshCaveEntranceTemplate = new AtmosphereVolumeTemplate(fleshCaveEntrancePrefab.Info,
            AtmosphereVolumeTemplate.VolumeShape.Sphere, "fleshcave_upper", 50);
        fleshCaveEntrancePrefab.SetGameObject(fleshCaveEntranceTemplate);
        fleshCaveEntrancePrefab.Register();

        var fleshCaveChamberBiomeSettings = BiomeUtils.CreateBiomeSettings(new Vector3(16, 13, 13), 1f,
            new Color(0.441f, 0.1f, 0.1f), 0.2f, new Color(1, 0f, 0f), 0.007f, 20f, 0f, 0f, 36.1f);
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
}