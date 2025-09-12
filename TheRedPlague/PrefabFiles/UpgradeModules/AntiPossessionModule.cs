using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using TheRedPlague.Mono.UpgradeModules;
using TheRedPlague.PrefabFiles.Items;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.UpgradeModules;

public static class AntiPossessionModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AntiPossessionModule")
        .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("AntiPossessionModule"));

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info, TechType.SeamothSolarCharge)
        {
            ModifyPrefab = obj => obj.GetComponent<Rigidbody>().isKinematic = true
        });
        prefab.SetEquipment(EquipmentType.VehicleModule);
        VehicleUpgradeUtils.SetOnUpgradeChanged(Info.TechType, OnChanged);
        prefab.SetUnlock(Info.TechType)
            .WithAnalysisTech(null, KnownTechHandler.DefaultUnlockData.BlueprintUnlockSound,
                KnownTechHandler.DefaultUnlockData.BlueprintUnlockMessage);
        prefab.SetRecipe(new RecipeData(new Ingredient(TechType.SeamothElectricalDefense, 1),
            new Ingredient(DormantNeuralMatter.Info.TechType, 1)))
            .WithCraftingTime(4.6f)
            .WithFabricatorType(CraftTree.Type.SeamothUpgrades)
            .WithStepsToFabricatorTab(CraftTreeHandler.Paths.VehicleUpgradesCommonModules);
        prefab.SetPdaGroupCategoryAfter(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades, TechType.VehicleStorageModule);

        prefab.Register();
    }

    private static void OnChanged(Vehicle vehicle, int slot)
    {
        var count = vehicle.modules.GetCount(Info.TechType);
        if (count > 0)
        {
            vehicle.gameObject.EnsureComponent<AntiPossessionModuleBehaviour>();
        }
        else
        {
            Object.Destroy(vehicle.gameObject.GetComponent<AntiPossessionModuleBehaviour>());
        }
    }
}