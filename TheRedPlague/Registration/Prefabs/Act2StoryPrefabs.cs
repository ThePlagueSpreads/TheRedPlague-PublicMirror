using Nautilus.Assets;
using Nautilus.Utility;
using Story;
using TheRedPlague.Content.Act2.B3NT;
using TheRedPlague.Content.Act2.FleshCave;
using TheRedPlague.Content.Act2.PlagueHeart;
using TheRedPlague.Content.Act2.ShrineBase;
using TheRedPlague.Content.Act2.ThrusterEvent;
using TheRedPlague.Content.Aurora;
using TheRedPlague.Content.Environment.Precursor;
using TheRedPlague.Content.PlayerInfection;
using TheRedPlague.Framework.Behaviour.Optimization;
using TheRedPlague.Framework.Behaviour.Precursor;
using TheRedPlague.Framework.Behaviour.Story;
using TheRedPlague.Framework.CommonPrefabs;
using TheRedPlague.Framework.Environment.Scenes;
using TheRedPlague.Framework.MaterialModifiers;
using TheRedPlague.Framework.SFX;
using TheRedPlague.Framework.Triggers;
using TheRedPlague.Framework.UI;
using TheRedPlague.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class Act2StoryPrefabs
{
    public static TechType LeaveFleshCaveWaiter { get; private set; }
    public static TechType ThrusterEventTrigger { get; private set; }
    
    [PrefabRegistration]
    private static void RegisterPrefabs()
    {
        RegisterAuroraPrefabs();
        RegisterPlagueCavePrefabs();
        RegisterShrineBasePrefabs();
        RegisterFleshCaveCachePrefabs();
        RegisterMeteorSitePrefabs();
        RegisterScenes();
    }

    private static void RegisterAuroraPrefabs()
    {
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

        new ScriptHolderPrefab("CassyBehindAuroraDoor", LargeWorldEntity.CellLevel.Near,
            obj => { obj.AddComponent<CassyTalkingBehindDoor>(); }).Register();
        CassyTalkingBehindDoor.RegisterSaveData();
        
        new ScriptHolderPrefab("CassyEncounter1Trigger", LargeWorldEntity.CellLevel.Medium,
            obj =>
            {
                obj.AddTriggerCollider(50);
                var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
                trigger.requiredGoal = Act1Story.IslandElevatorActivatedGoal;
                trigger.goal = Act2Story.CassyEncounter1;
            }).Register();
        
        new ScriptHolderPrefab("CassyEncounter2Trigger", LargeWorldEntity.CellLevel.Near,
            obj =>
            {
                obj.AddTriggerCollider(25);
                var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
                trigger.requiredGoal = Act1Story.IslandElevatorActivatedGoal;
                trigger.goal = Act2Story.CassyEncounter2;
            }).Register();
        
        var thrusterEventTrigger = new ScriptHolderPrefab("ThrusterEventTrigger", LargeWorldEntity.CellLevel.Global,
            obj => { obj.AddComponent<TriggerThrusterEventAtSurface>(); });
        thrusterEventTrigger.Register();
        ThrusterEventTrigger = thrusterEventTrigger.Info.TechType;
    }

    private static void RegisterPlagueCavePrefabs()
    {
        new ScriptHolderPrefab("PlagueCaveHintTrigger", LargeWorldEntity.CellLevel.Near, obj =>
        {
            var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
            trigger.requiredGoal = Act2Story.UnlockPlagueCaveSignalGoal;
            trigger.goal = Act2Story.PlagueCaveEntranceHint;
            obj.AddTriggerCollider(30);
        }).Register();
        
        // Player flesh cave leaving watcher
        
        var leaveFleshCaveWaiter = new ScriptHolderPrefab("LeaveFleshCaveWaiter", LargeWorldEntity.CellLevel.Global,
                obj =>
                {
                    obj.AddComponent<WatchPlayerLeavingFleshCave>().goalToComplete = Act2Story.LeavePlagueCaveGoal;
                });
        leaveFleshCaveWaiter.Register();
        LeaveFleshCaveWaiter = leaveFleshCaveWaiter.Info.TechType;
    }

    private static void RegisterShrineBasePrefabs()
    {
        new TrpMusicPlayer("ShrineBaseMusicPlayer", LargeWorldEntity.CellLevel.Medium, "ShrineBaseMusic",
            83, 100).Register();
        new TrpMusicPlayer("ShrineBaseAmbiencePlayer", LargeWorldEntity.CellLevel.Medium, "ShrineBaseAmbience",
            83, 100).Register();

        new CustomForceFieldPrefab("ShrineBaseForceField",
                () => AssetBundles.Core.LoadAsset<GameObject>("ShrineBaseTallForceFieldPrefab"),
                Act2Story.ShrineBaseForceFieldStoryGoal)
            .Register();

        new CustomTabletTerminalPrefab<StoryGoalTabletTerminal>("ShrineBaseTabletTerminal",
                AssetBundles.Core.LoadAsset<Texture2D>("ShrineBaseTabletKeyHolderIcon"),
                PrecursorTabletRegistration.GoldTabletInfo.TechType)
            {
                ModifyComponent = c => c.associatedStoryGoal = Act2Story.ShrineBaseForceFieldStoryGoal
            }
            .Register();
        
        // Bennet introduction trigger

        new ScriptHolderPrefab("B3NTIntroductionTrigger", LargeWorldEntity.CellLevel.Near,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
                trigger.requiredGoal = Act2Story.BennetInitialMeeting;
                trigger.goal = Act2Story.BennetApproach;
                obj.AddTriggerCollider(9);
            }).Register();

        // Shrine base receptacle

        new CustomItemReceptacle<ShrineBaseReceptacle>("ShrineBaseReceptacle")
        {
            ModifyPrefab = (obj, receptacle) =>
            {
                obj.transform.Find("precursor_Teleporter_Activation_Terminal_geo").gameObject.SetActive(false);
                var ui = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("CatalystReceptacleUI"),
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
    }

    private static void RegisterFleshCaveCachePrefabs()
    {
        // Force field control
        new ScriptHolderPrefab("B3NTCacheForceFieldOpener", LargeWorldEntity.CellLevel.Near,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTrigger>();
                trigger.requiredGoal = Act2Story.BennetInitialMeeting;
                trigger.goal = new StoryGoal(Act2Story.BennetCacheForceFieldStoryGoal, Story.GoalType.Story, 0);
                obj.AddTriggerCollider(8);
            }).Register();

        // Force field
        new CustomForceFieldPrefab("B3NTCacheForceField",
                () => AssetBundles.Core.LoadAsset<GameObject>("B3NTCacheForceFieldPrefab"),
                Act2Story.BennetCacheForceFieldStoryGoal)
            .Register();

        // Cache music player
        new TrpMusicPlayer("B3NTCacheMusicPlayer", LargeWorldEntity.CellLevel.Medium, "SanctuaryOfTheCreator",
            78, 90).Register();

        // Cache sanity healer
        new ScriptHolderPrefab("B3NTCacheSanityHealer", LargeWorldEntity.CellLevel.Far, obj =>
        {
            var heal = obj.AddComponent<HealSanityNearby>();
            heal.radius = 80;
            heal.biomeFilter = "PrecursorGun";
            heal.sanityPerSecond = 0.5f;
        }).Register();

        // Shrine base post act 2 watcher

        new ScriptHolderPrefab("PostAct2BennetShrineTrigger", LargeWorldEntity.CellLevel.Near,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTriggerInDistance>();
                trigger.activationRadius = 30;
                trigger.requiredGoal = Act2Story.Act2EndingEvent.key;
                trigger.goalToComplete = Act2Story.BennetPostAct2SanctuaryVisit;
            }).Register();
    }

    private static void RegisterMeteorSitePrefabs()
    {
        new ScriptHolderPrefab("PlagueHeartLocationTrigger", LargeWorldEntity.CellLevel.Far,
            obj =>
            {
                var trigger = obj.AddComponent<ConditionalStoryGoalTriggerInDistance>();
                trigger.requiredGoal = Act2Story.BennetGiveInfectionTracker.key;
                trigger.goalToComplete = Act2Story.BennetPlagueHeartReaction;
                trigger.activationRadius = 95;
            }).Register();
        
        new MeteorBloodEmitterPrefab("MeteorMist1",
            () => AssetBundles.Creatures.LoadAsset<GameObject>("FleshBlobPrefab"), 4f).Register();
        new MeteorBloodEmitterPrefab("MeteorMist2", () => AssetBundles.Core.LoadAsset<GameObject>("FleshMass"), 5f)
            .Register();
        new MeteorBloodEmitterPrefab("MeteorMist3", () => AssetBundles.Core.LoadAsset<GameObject>("OrgansProp3"), 7f)
            .Register();
    }

    private static void RegisterScenes()
    {
        // Precursor bases
        var shrineBaseSceneData = ScriptableObject.CreateInstance<AdditiveSceneManager.Data>();
        shrineBaseSceneData.scenePath = "Assets/Act2/ShrineBaseLevel/ShrineBaseScene.unity";
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
            sa.customSkyPrefab = BiomeRegistration.PlagueCaveSky.gameObject;
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
}