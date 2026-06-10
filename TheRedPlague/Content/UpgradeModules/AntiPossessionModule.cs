using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.API.Upgrades;
using UnityEngine;

namespace TheRedPlague.Content.UpgradeModules;

[PrefabClass]
public static class AntiPossessionModule
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AntiPossessionModule")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("AntiPossessionModule"));

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info, TechType.SeamothSolarCharge)
        {
            ModifyPrefab = obj => obj.GetComponent<Rigidbody>().isKinematic = true
        });
        prefab.SetEquipment(EquipmentType.VehicleModule)
            .WithQuickSlotType(QuickSlotType.Passive);
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