using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.PlagueNeutralizer;
using TheRedPlague.Framework.Gadgets;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Items.Resources;

[PrefabClass]
public static class PlagueIngot
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueIngot")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("PlagueIngotIcon"));

    [PrefabRegistration]
    private static void Register()
    {
        var ingredientPrefab = new CustomPrefab(PrefabInfo.WithTechType("PlagueResourceIngredient"));
        ingredientPrefab.SetGameObject(TrpPrefabUtils.CreateLootCubePrefab(ingredientPrefab.Info));
        ingredientPrefab.Register();
        
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.SetUnlock(PlagueNeutralizer.Info.TechType)
            .WithPdaGroupCategory(CustomTechCategories.PlagueBiotechGroup, CustomTechCategories.PlagueBiotechCategory);
        prefab.SetRecipe(new RecipeData(new Ingredient(PlagueCatalyst.Info.TechType, 1),
            new Ingredient(ingredientPrefab.Info.TechType, 1)));
        prefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        prefab.Register();
        
        BaseBioReactor.charge[Info.TechType] = 600;
    }

    private static IEnumerator CreatePrefab(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("PlagueIngot"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj);
        obj.AddComponent<Pickupable>();
        PrefabUtils.AddWorldForces(obj, 20);
        prefab.Set(obj);
        yield break;
    }
}