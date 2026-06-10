using System.Collections;
using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using Story;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Buildables.PlagueAltar;

[PrefabClass(RegistrationStage.CraftStation)]
public static class PlagueAltar
{
    private static readonly int TintColor = Shader.PropertyToID("_TintColor");

    private const string CraftTreeName = "PlagueAltar";
    public const string PetsTab = "Pets";
    public const string EquipmentTab = "Equipment";
    public const string ConsumableTab = "Consumable";
    public const string ResourcesTab = "Resources";

    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueAltar")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("PlagueAltarIcon"));

    public static CraftTree.Type CraftTree
    {
        get
        {
            if ((int)CraftTreeType == 0)
            {
                Plugin.Logger.LogError("Attempting to access PlagueAltar.CraftTree before it is initialized!");
            }
            return CraftTreeType;
        }
    }
    
    private static CraftTree.Type CraftTreeType { get; set; }
    
    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.SetPdaGroupCategoryAfter(TechGroup.InteriorModules, TechCategory.InteriorModule, TechType.Workbench);
        prefab.SetRecipe(new RecipeData(
            new Ingredient(PlagueIngot.Info.TechType, 3),
            new Ingredient(RedPlagueSample.Info.TechType, 1),
            new Ingredient(AmalgamatedBone.Info.TechType, 1)));
        prefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        prefab.Register();
        
        CraftTreeType = EnumHandler.AddEntry<CraftTree.Type>(CraftTreeName).CreateCraftTreeRoot(out var root);
        root.AddTabNode(ConsumableTab, null, AssetBundles.Core.LoadAsset<Sprite>("PlagueAltarTab_Consumable"));
        root.AddTabNode(EquipmentTab, null, AssetBundles.Core.LoadAsset<Sprite>("PlagueAltarTab_Equipment"));
        root.AddTabNode(PetsTab, null, AssetBundles.Core.LoadAsset<Sprite>("PlagueAltarTab_Lifeforms"));
        root.AddTabNode(ResourcesTab, null, AssetBundles.Core.LoadAsset<Sprite>("PlagueAltarTab_Resources"));
    }

    public static void RegisterLateStoryData()
    {
        KnownTechHandler.SetAnalysisTechEntry(new KnownTech.AnalysisTech
        {
            techType = Info.TechType,
            unlockSound = KnownTechHandler.DefaultUnlockData.BasicUnlockSound,
            storyGoals = new List<StoryGoal> { Act2Story.ScanPlagueAltarGoal },
            unlockPopup = AssetBundles.Core.LoadAsset<Sprite>("PlagueAltarPopup"),
            unlockTechTypes = new List<TechType>(),
            unlockMessage = KnownTechHandler.DefaultUnlockData.BlueprintUnlockMessage
        });
    }

    private static IEnumerator CreatePrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("PlagueAltar_Prefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(prefab, 6, 1, 1, new PlagueAltarMaterialModifier(),
            new IgnoreParticleSystemsModifier());
        var modelParent = prefab.transform.Find("Plague Altar Animated");
        PrefabUtils.AddConstructable(prefab, Info.TechType,
            ConstructableFlags.Base | ConstructableFlags.Ground | ConstructableFlags.Inside |
            ConstructableFlags.Rotatable | ConstructableFlags.Submarine,
            modelParent.gameObject);
        var bounds = prefab.AddComponent<ConstructableBounds>();
        bounds.bounds = new OrientedBounds(new Vector3(0f, 1.77f, 0f), Quaternion.identity,
            new Vector3(2.3f, 1.25f, 2.1f) / 2f);

        var eyes = new[]
        {
            modelParent.Find("Eye.001"),
            modelParent.Find("Eye.002"),
            modelParent.Find("Eye.003"),
            modelParent.Find("Eye.004"),
            modelParent.Find("Eye.005"),
            modelParent.Find("Eye.006"),
        };
        
        var eyeComponents = new PlagueAltarEye[eyes.Length];
        
        for (int i = 0; i < eyes.Length; i++)
        {
            var lastNumber = int.Parse(eyes[i].name[^1].ToString());
            var eye = eyes[i].gameObject.AddComponent<PlagueAltarEye>();
            eye.flip = lastNumber >= 4;
            eyeComponents[i] = eye;
        }

        var baseFabricatorTask = CraftData.GetPrefabForTechTypeAsync(TechType.Fabricator);
        yield return baseFabricatorTask;
        var baseFabricator = baseFabricatorTask.GetResult();

        var baseFabricatorCrafterGhost = baseFabricator.GetComponent<CrafterGhostModel>();
        var ghostCrafterModel = prefab.AddComponent<CrafterGhostModel>();
        ghostCrafterModel._EmissiveTex = baseFabricatorCrafterGhost._EmissiveTex;
        ghostCrafterModel._NoiseTex = baseFabricatorCrafterGhost._NoiseTex;
        ghostCrafterModel.itemSpawnPoint = prefab.transform.Find("ItemSpawnPoint");

        var craftSoundLoop = prefab.AddComponent<FMOD_CustomLoopingEmitter>();
        craftSoundLoop.SetAsset(AudioUtils.GetFmodAsset("PlagueAltarFabricating"));

        var aliveSoundLoop = prefab.AddComponent<FMOD_CustomLoopingEmitter>();
        aliveSoundLoop.SetAsset(AudioUtils.GetFmodAsset("PlagueAltarIdle"));
        aliveSoundLoop.playOnAwake = true;
        aliveSoundLoop.followParent = true;

        var crafterLogic = prefab.AddComponent<CrafterLogic>();

        var crafter = prefab.AddComponent<PlagueAltarCrafter>();
        crafter.craftTree = CraftTreeType;
        crafter.ghost = ghostCrafterModel;
        crafter.crafterLogic = crafterLogic;
        crafter.closeDistance = 4;
        var animator = prefab.transform.Find("Plague Altar Animated").GetComponent<Animator>();
        crafter.animator = animator;
        crafter.interactSound = AudioUtils.GetFmodAsset("PlagueAltarInteract");
        crafter.craftSoundEmitter = craftSoundLoop;
        crafter.handOverText = "UsePlagueAltar";

        // VFX:

        var beamRootBoneNames = new[]
        {
            "Bone.003",
            "Bone.007",
            "Bone.011",
            "Bone.015"
        };

        var rootBone = prefab.transform.Find("Plague Altar Animated/Armature/Bone");
        var beamEnds = new Transform[beamRootBoneNames.Length];
        for (int i = 0; i < beamRootBoneNames.Length; i++)
        {
            var deepest = rootBone.Find(beamRootBoneNames[i]);
            while (deepest.childCount > 0)
                deepest = deepest.GetChild(0);
            beamEnds[i] = deepest;
        }

        var beamPrefab = baseFabricator.transform.Find("submarine_fabricator_01/printer_left/fabricatorBeam")
            .gameObject;

        var altarParticlePrefab =
            Object.Instantiate(baseFabricator.GetComponent<Fabricator>().sparksPS, prefab.transform, true);
        altarParticlePrefab.SetActive(false);
        altarParticlePrefab.GetComponent<ParticleSystemRenderer>().material.color = new Color(3, 0.7f, 1);
        altarParticlePrefab.transform.Find("xFlash").GetComponent<ParticleSystemRenderer>().material.color =
            new Color(2, 0, 0.3f);
        altarParticlePrefab.transform.Find("xSparkDot").GetComponent<ParticleSystemRenderer>().material.color =
            new Color(3, 1, 2);
        altarParticlePrefab.transform.Find("xSparks").GetComponent<ParticleSystemRenderer>().material.color =
            new Color(3, 0.3f, 0.1f);

        var beamRenderers = new Renderer[beamEnds.Length];
        for (int i = 0; i < beamEnds.Length; i++)
        {
            var beam = Object.Instantiate(beamPrefab, beamEnds[i], true);
            beam.transform.localPosition = Vector3.zero;
            beam.transform.localEulerAngles = Vector3.right * 270;
            beam.transform.localScale *= 0.5f;
            var beamRenderer = beam.GetComponent<Renderer>();
            beamRenderer.material.SetColor(TintColor, new Color(2, 0.1f, 0.3f));
            beamRenderers[i] = beamRenderer;
        }

        crafter.sparksPrefab = altarParticlePrefab;
        crafter.beamEndPoints = beamEnds;
        crafter.beams = beamRenderers;

        var intrusion = prefab.AddComponent<AltarIntrusionEvent>();
        intrusion.eyes = eyeComponents;
        intrusion.animator = animator;
        intrusion.crafter = crafter;
        intrusion.vomitParticles =
            prefab.transform.Find("BloodVomitParticles").GetComponentsInChildren<ParticleSystem>();
        intrusion.hemorrhageParticles =
            prefab.transform.Find("HemorrhageParticles").GetComponentsInChildren<ParticleSystem>();
        intrusion.sparksParent = prefab.transform.Find("IntrusionSparks");

        result.Set(prefab);
    }

    public class PlagueAltarMaterialModifier : MaterialModifier
    {
        public override void EditMaterial(Material material, Renderer renderer, int materialIndex,
            MaterialUtils.MaterialType materialType)
        {
            var rendererName = renderer.gameObject.name.ToLower();
            if (rendererName.Contains("eye"))
            {
                material.SetColor(ShaderPropertyID._GlowColor, new Color(0.5f, 0.5f, 0.5f));
            }
            else if (rendererName.Contains("body"))
            {
                material.color = new Color(0.7f, 1, 1);
            }
        }

        public override bool BlockShaderConversion(Material material, Renderer renderer, MaterialUtils.MaterialType materialType)
        {
            return renderer.gameObject.name.StartsWith("fabricatorBeam");
        }
    }
}