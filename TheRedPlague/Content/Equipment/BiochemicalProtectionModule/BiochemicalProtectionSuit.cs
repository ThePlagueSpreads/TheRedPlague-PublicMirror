using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using TheRedPlague.Framework.Inventory;
using UnityEngine;

namespace TheRedPlague.Content.Equipment.BiochemicalProtectionModule;

[PrefabClass]
public static class BiochemicalProtectionSuit
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("BiochemicalProtectionModule")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("BiochemicalProtectionModuleIcon"));
    
    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        var template = new CloneTemplate(Info, TechType.MapRoomUpgradeScanRange);
        template.ModifyPrefab += obj =>
        {
            obj.AddComponent<UsableItem>().SetOnUseAction(Info.ClassID, OnUse);
            obj.GetComponentInChildren<VFXFabricating>().posOffset = default;
        };
        prefab.SetPdaGroupCategoryAfter(TechGroup.Personal, TechCategory.Equipment, TechType.Compass);
        prefab.SetRecipe(new RecipeData(
                new Ingredient(TechType.ComputerChip, 1), new Ingredient(TechType.Silicone, 2)))
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithCraftingTime(5)
            .WithStepsToFabricatorTab(CraftTreeHandler.Paths.FabricatorEquipment);
        prefab.SetGameObject(template);
        prefab.Register();
        KnownTechHandler.SetAnalysisTechEntry(Info.TechType,
            System.Array.Empty<TechType>(), KnownTechHandler.DefaultUnlockData.BasicUnlockSound,
            AssetBundles.Core.LoadAsset<Sprite>("BiochemicalProtectionModulePopup"));
    }

    private static void OnUse(GameObject obj)
    {
        Act1Story.UseBiochemicalProtectionSuitEvent.Trigger();
    }
}