using System.Collections;
using System.Linq;
using ECCLibrary;
using ECCLibrary.Data;
using ECCLibrary.Mono;
using Nautilus.Assets;
using Nautilus.Extensions;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Content.Infection;
using TheRedPlague.Framework.Behaviour.Spawning;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.Drifter;

[PrefabClass]
public class DrifterPrefab : CreatureAsset
{
    public static TechType DrifterTechType { get; } = EnumHandler.AddEntry<TechType>("Drifter");
    public static PrefabInfo DrifterInfo { get; } = new("Drifter", "DrifterPrefab", DrifterTechType);

    public static PrefabInfo DrifterFlyOnlyInfo { get; } = new("DrifterFlyOnly", "DrifterFlyOnlyPrefab", DrifterTechType);

    public static PrefabInfo DrifterHivemindSpawn { get; } = PrefabInfo.WithTechType("DrifterHMSpawn");
    
    public const float BaseVelocity = 10;

    private readonly bool _flyOnly;

    private static FMODAsset VocalizeNear { get; } = AudioUtils.GetFmodAsset("DrifterVocalizeClose");
    private static FMODAsset VocalizeFar { get; } = AudioUtils.GetFmodAsset("DrifterVocalizeFar");

    public DrifterPrefab(PrefabInfo prefabInfo, bool flyOnly) : base(prefabInfo)
    {
        _flyOnly = flyOnly;
    }

    [PrefabRegistration]
    private static void RegisterDrifters()
    {
        var drifter = new DrifterPrefab(DrifterInfo, false);
        drifter.Register();
        PDAHandler.AddCustomScannerEntry(DrifterInfo.TechType, 2, false, "Drifter");
        PDAHandler.AddEncyclopediaEntry("Drifter", "Lifeforms/Fauna/PlagueCreations");
        var drifterFlyOnly = new DrifterPrefab(DrifterFlyOnlyInfo, true);
        drifterFlyOnly.Register();
        var drifterHiveMindSpawn =
            new SpawnAfterStoryGoalPrefab(DrifterHivemindSpawn, DrifterInfo.ClassID,
                () => Act2Story.HiveMindReleasedGoal.key, LargeWorldEntity.CellLevel.VeryFar);
        drifterHiveMindSpawn.Register();
        
        // Drifters
        
        var randomGenerator = new System.Random(51034581);
        for (int i = 0; i < 80; i++)
        {
            var angle = (float) randomGenerator.NextDouble() * Mathf.PI * 2f;
            var distance = Mathf.Pow((float) randomGenerator.NextDouble(), 1/2f) * 1500f;
            var height = 20 + (float) randomGenerator.NextDouble() * 30;
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo(DrifterHivemindSpawn.ClassID,
                new Vector3(Mathf.Cos(angle) * distance, height, Mathf.Sin(angle) * distance)));
        }
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => AssetBundles.Creatures.LoadAsset<GameObject>("DrifterPrefab"),
            BehaviourType.Leviathan, EcoTargetType.None, 5000)
        {
            LocomotionData = new LocomotionData(5f, 0.1f, 1f, 1f, true, true),
            SwimRandomData = new SwimRandomData(0.1f, BaseVelocity, new Vector3(50, 3, 50), 5f, 0.8f),
            Mass = 3000,
            RespawnData = new RespawnData(false),
            CellLevel = LargeWorldEntity.CellLevel.VeryFar,
            CanBeInfected = false,
            AnimateByVelocityData = new AnimateByVelocityData(BaseVelocity * 2),
            BehaviourLODData = new BehaviourLODData(100, 200, 400)
        };
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddPlagueCreationComponents(prefab);
        
        var wf = components.WorldForces;
        wf.aboveWaterGravity = 0;
        wf.aboveWaterDrag = 1;
        wf.underwaterDrag = 1;
        
        var loopingEmitter = prefab.AddComponent<FMOD_CustomLoopingEmitter>();
        loopingEmitter.SetAsset(AudioUtils.GetFmodAsset("DrifterIdle"));
        loopingEmitter.followParent = true;
        loopingEmitter.playOnAwake = true;

        var gasopodTask = CraftData.GetPrefabForTechTypeAsync(TechType.Gasopod);
        yield return gasopodTask;
        var gasopodGas = gasopodTask.GetResult().GetComponent<GasoPod>().gasFXprefab;
        var mistPrefab = Object.Instantiate(gasopodGas, prefab.transform);
        mistPrefab.SetActive(false);
        mistPrefab.AddComponent<DrifterMistInstance>();
        foreach (var renderer in mistPrefab.GetComponentsInChildren<Renderer>(true))
        {
            renderer.material.color = new Color(0.2f, 0.04f, 0.04f, 0.4f);
            renderer.material.SetColor("_ColorStrengthAtNight", Color.gray);
        }

        foreach (var ps in mistPrefab.GetComponentsInChildren<ParticleSystem>(true))
        {
            var main = ps.main;
            main.startSizeMultiplier *= 15f;
            main.startLifetimeMultiplier *= 10f;
            var sizeOverLifetime = ps.sizeOverLifetime;
            sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f,
                new AnimationCurve(new(0, 0.3f), new(1, 1)));
        }

        foreach (var trail in mistPrefab.GetComponentsInChildren<Trail_v2>(true))
        {
            trail.gameObject.SetActive(false);
        }

        var destroyAfterSeconds = mistPrefab.GetComponent<VFXDestroyAfterSeconds>();
        destroyAfterSeconds.lifeTime = 20f;

        mistPrefab.transform.Find("xGasopodSmoke/xSmkMesh").gameObject.SetActive(false);

        var spawnMist = prefab.AddComponent<DrifterSprayMist>();
        spawnMist.mistPrefab = mistPrefab;
        spawnMist.rb = components.Rigidbody;
        
        if (!_flyOnly)
            prefab.EnsureComponent<DrifterHoverAboveTerrain>();

        prefab.AddComponent<DrifterAnimationController>().animator = components.Animator;

        var infect = prefab.AddComponent<InfectAnything>();
        infect.infectionAmount = 1;
        infect.infectionHeightStrength = 0;
        infect.renderers = prefab.GetComponentsInChildren<Renderer>(true)
            .Where(r => r is not ParticleSystemRenderer).ToArray();
        
        var spineTrailManagerBuilder =
            new TrailManagerBuilder(components, prefab.transform.SearchChild("Spine.002"), 3f);
        spineTrailManagerBuilder.SetTrailArrayToChildrenWithKeywords("Spine");
        spineTrailManagerBuilder.AllowDisableOnScreen = false;
        spineTrailManagerBuilder.Apply();
        
        var tailTrailManagers = new[]
        {
            "Tail.L",
            "Tail.R",
            "Tail.U"
        };
        
        foreach (var tail in tailTrailManagers)
        {
            var builder =
                new TrailManagerBuilder(components, prefab.transform.SearchChild(tail).GetChild(0), 2f);
            builder.SetTrailArrayToChildrenWithCondition(t => !t.name.Contains("Hook") && !t.name.Contains("end"));
            builder.Apply();
        }

        var voiceEmitter = prefab.AddComponent<FMOD_CustomEmitter>();
        voiceEmitter.SetAsset(VocalizeNear);
        voiceEmitter.followParent = true;
        voiceEmitter.playOnAwake = false;
        
        var voice = prefab.AddComponent<CreatureVoice>();
        voice.closeIdleSound = VocalizeNear;
        voice.farIdleSound = VocalizeFar;
        voice.emitter = voiceEmitter;
        voice.farThreshold = 30;
        voice.minInterval = 20;
        voice.maxInterval = 37;
    }

    protected override void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab, 5f, 1f, 2f, new DrifterMaterialModifier());
    }

    private class DrifterMaterialModifier : MaterialModifier
    {
        public override void EditMaterial(Material material, Renderer renderer, int materialIndex,
            MaterialUtils.MaterialType materialType)
        {
            if (materialType == MaterialUtils.MaterialType.Transparent && material.name.ToLower().Contains("pustule"))
            {
                material.SetFloat("_SpecInt", 50);
                material.SetFloat("_IBLreductionAtNight", 0.5f);
                material.SetFloat("_Fresnel", 0.8f);
            }

            if (materialType == MaterialUtils.MaterialType.Opaque)
            {
                material.SetFloat("_InfectionHeightStrength", 0);
                material.SetVector("_InfectionScale", new Vector4(2, 2, 2, 0));
                material.SetVector("_InfectionOffset", new Vector4(0.428f, 0.047f, 0, 0));
                material.SetFloat("_Fresnel", 0);
            }
        }

        public override bool BlockShaderConversion(Material material, Renderer renderer,
            MaterialUtils.MaterialType materialType)
        {
            return renderer is ParticleSystemRenderer;
        }
    }
}