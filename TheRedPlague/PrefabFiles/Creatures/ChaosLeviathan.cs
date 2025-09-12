using System.Collections;
using ECCLibrary;
using ECCLibrary.Data;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.MaterialModifiers;
using TheRedPlague.Mono.CreatureBehaviour.Chaos;
using TheRedPlague.Mono.Insanity;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Creatures;

public class ChaosLeviathanPrefab : CreatureAsset
{
    public static readonly FMODAsset CloseRoarShort = AudioUtils.GetFmodAsset("ChaosLeviathanRoarCloseShort");
    public static readonly FMODAsset CloseRoarLong = AudioUtils.GetFmodAsset("ChaosLeviathanRoarCloseLong");
    public static readonly FMODAsset FarRoarShort = AudioUtils.GetFmodAsset("ChaosLeviathanRoarFarShort");
    public static readonly FMODAsset FarRoarLong = AudioUtils.GetFmodAsset("ChaosLeviathanRoarFarLong");
    
    private bool Roaming { get; }
    
    public ChaosLeviathanPrefab(PrefabInfo prefabInfo, bool isRoaming) : base(prefabInfo)
    {
        Roaming = isRoaming;
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => Plugin.CreaturesBundle.LoadAsset<GameObject>("ChaosLeviathanPrefab"),
            BehaviourType.Leviathan, EcoTargetType.Leviathan, 66666666)
        {
            CanBeInfected = false,
            LocomotionData = new LocomotionData(9, 0.15f, 1, 0.3f),
            SwimBehaviourData = new SwimBehaviourData(0.4f),
            SwimRandomData = new SwimRandomData(0.2f, 13, new Vector3(25, 4, 25), 4, 1, true),
            AvoidTerrainData = new AvoidTerrainData(0.9f, 15, 30, 30),
            AcidImmune = true,
            AnimateByVelocityData = new AnimateByVelocityData(9, 30, 50),
            BehaviourLODData = new BehaviourLODData(100, 350, 600),
            RespawnData = new RespawnData(false),
            FleeOnDamageData = null
        };
        CreatureTemplateUtils.SetCreatureDataEssentials(template, Roaming ? LargeWorldEntity.CellLevel.Global : LargeWorldEntity.CellLevel.VeryFar, 5000, -0.4f);
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddPlagueCreationComponents(prefab);

        var curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        var spineRoot = prefab.transform.Find("ChaosLeviathan/ChaosArmature/Root/Spine");
        var mainTrailManager = new TrailManagerBuilder(components, spineRoot, 5.6f);
        mainTrailManager.SetTrailArrayToChildrenWithCondition(t => t.gameObject.name.StartsWith("Spine"));
        mainTrailManager.AllowDisableOnScreen = false;
        mainTrailManager.SetAllMultiplierAnimationCurves(curve);
        mainTrailManager.Apply();

        var eyeSpawner = prefab.AddComponent<ChaosSpawnRandomEyes>();
        var spineBones = mainTrailManager.Trails;
        eyeSpawner.bones = spineBones;

        var pelvisRoot = spineRoot.transform.SearchChild("Pelvis");

        foreach (Transform child in pelvisRoot)
        {
            var trailManagerRoot = child.GetChild(0);
            if (!trailManagerRoot.gameObject.name.StartsWith("Tentacle"))
                continue;
            var tentacleTrail = new TrailManagerBuilder(components, trailManagerRoot, 1f);
            tentacleTrail.SetTrailArrayToAllChildren();
            tentacleTrail.AllowDisableOnScreen = false;
            tentacleTrail.MaxSegmentOffset = 2;
            tentacleTrail.SetAllMultiplierAnimationCurves(curve);
            tentacleTrail.Apply();
        }

        var roar = prefab.AddComponent<ChaosLeviathanRoar>();
        roar.closeRoarLong = CloseRoarLong;
        roar.farRoarLong = FarRoarLong;
        roar.closeRoarShort = CloseRoarShort;
        roar.farRoarShort = FarRoarShort;
        var roarEmitter = prefab.AddComponent<FMOD_CustomEmitter>();
        roarEmitter.followParent = true;
        roarEmitter.restartOnPlay = true;
        roarEmitter.playOnAwake = false;
        roar.emitter = roarEmitter;
        roar.animator = components.Animator;
        roar.playSoundOnStart = true;

        var zone = prefab.AddComponent<InsanityOverrideZone>();
        zone.overrideValue = 60;
        zone.onlyIndoors = false;
        zone.radius = 100;

        prefab.AddComponent<ChaosScreenFXRoot>();

        if (Roaming)
        {
            prefab.AddComponent<ChaosBennetVoiceLine>();
            prefab.AddComponent<InfectionTrackerTarget>().priority = 10;
            prefab.AddComponent<RoamingChaos>().evaluatePriority = 0.3f;
        }

        prefab.AddComponent<ChaosLeviathanSetInvincible>().liveMixin = components.LiveMixin;

        yield break;
    }

    protected override void ApplyMaterials(GameObject prefab)
    {
        ApplyChaosLeviathanMaterials(prefab);
    }

    public static void ApplyChaosLeviathanMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab, 5.5f, 2, 1, new ChaosLeviathanMaterialModifier());
    }
}