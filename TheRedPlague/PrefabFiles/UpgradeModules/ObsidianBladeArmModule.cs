using System;
using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Data;
using TheRedPlague.Mono.UpgradeModules;
using TheRedPlague.Mono.VFX;
using TheRedPlague.PrefabFiles.Buildable;
using TheRedPlague.PrefabFiles.Items;
using TheRedPlague.Utilities;
using TheRedPlague.Utilities.Gadgets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague.PrefabFiles.UpgradeModules;

public static class ObsidianBladeArmModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ObsidianBladeArmModule")
        .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("PrawnBladeIcon"));

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.SetUnlock(Info.TechType)
            .WithAnalysisTech(Plugin.AssetBundle.LoadAsset<Sprite>("PrawnBladePopup"),
                KnownTechHandler.DefaultUnlockData.BlueprintUnlockSound,
                KnownTechHandler.DefaultUnlockData.BlueprintUnlockMessage);
        prefab.SetRecipe(new RecipeData(new Ingredient(TechType.PlasteelIngot, 1),
                new Ingredient(PlagueIngot.Info.TechType, 2),
                new Ingredient(TechType.Diamond, 1),
                new Ingredient(Obsidian.Info.TechType, 3)))
            .WithCraftingTime(10f)
            .WithFabricatorType(PlagueAltar.CraftTreeType)
            .WithStepsToFabricatorTab(PlagueAltar.EquipmentTab);
        prefab.SetPdaGroupCategoryAfter(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades,
            TechType.ExosuitTorpedoArmModule);
        CraftDataHandler.SetEquipmentType(Info.TechType, EquipmentType.ExosuitArm);
        CraftDataHandler.SetQuickSlotType(Info.TechType, QuickSlotType.Selectable);

        CustomExosuitArmUtils.RegisterCustomExosuitArm(
            new CustomExosuitArmUtils.CustomArm(Info.TechType, GetPrawnSuitArmPrefab));

        prefab.SetBackgroundType(CraftData.BackgroundType.ExosuitArm);

        prefab.Register();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PrawnBladeItemPrefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(prefab);
        prefab.AddComponent<Pickupable>();
        PrefabUtils.AddVFXFabricating(prefab, "PrawnSuitBlade", -0.4f, 0.8f, new Vector3(0.4f, 0f, 0), 0.45f,
            new Vector3(0, 180, 90));
        result.Set(prefab);
        yield break;
    }

    private static IEnumerator GetPrawnSuitArmPrefab(IOut<GameObject> result)
    {
        // Load prawn suit
        var exosuitTask = CraftData.GetPrefabForTechTypeAsync(TechType.Exosuit);
        yield return exosuitTask;
        var exosuit = exosuitTask.GetResult();

        // Create prefab
        var prefab = UWE.Utils.InstantiateDeactivated(
            exosuit.GetComponent<Exosuit>().GetArmPrefab(TechType.ExosuitClawArmModule));
        prefab.name = Info.ClassID;
        var oldClawArmComponent = prefab.GetComponent<ExosuitClawArm>();
        Object.DestroyImmediate(oldClawArmComponent);
        var armRigParent = prefab.transform.Find("exosuit_01_armRight/ArmRig");
        armRigParent.Find("exosuit_hand_geo").gameObject.SetActive(false);
        var grapplingArm = armRigParent.Find("exosuit_grapplingHook_geo").gameObject;
        grapplingArm.SetActive(true);

        // Fix arm being culled on the screen
        var armRenderer = grapplingArm.GetComponent<SkinnedMeshRenderer>();
        armRenderer.localBounds = new Bounds(armRenderer.localBounds.center, armRenderer.localBounds.size * 1.5f);

        // Spawn blade model
        var bladeModel = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PrawnBladeArmPrefab"),
            armRigParent.Find("clavicle/shoulder/bicepPivot/elbow"));
        bladeModel.transform.localPosition = new Vector3(-0.630f, -0.050f, 0);
        bladeModel.transform.localEulerAngles = new Vector3(270, 90, 0);
        bladeModel.transform.localScale = Vector3.one * 0.9f;
        var eyeLook = bladeModel.transform.Find("PrawnSuitBladeArmModule/PrawnArmBladeArmature/Eye").gameObject
            .AddComponent<GenericEyeLook>();
        eyeLook.degreesPerSecond = 300;
        eyeLook.useLimits = true;
        eyeLook.dotLimit = 0.4f;
        MaterialUtils.ApplySNShaders(bladeModel);

        // Add core functionality
        var arm = prefab.AddComponent<ObsidianBladeArm>();
        arm.animator = prefab.GetComponentInChildren<Animator>();
        arm.front = bladeModel.transform.Find("Front");
        arm.fxControl = arm.GetComponent<VFXController>();

        // Connect everything back together
        prefab.GetComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();

        // Prevent the prefab from being destroyed
        Object.DontDestroyOnLoad(prefab);
        prefab.AddComponent<SceneCleanerPreserve>();

        result.Set(prefab);
    }
}