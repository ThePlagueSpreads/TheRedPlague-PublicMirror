using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.AdministratorFabricator;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class TheRegular
{
    [PrefabRegistration]
    private static void Register()
    {
        var theRegularInfo = PrefabInfo.WithTechType("TheRegular", true)
            .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("TheRegular"));
        var theRegularObject = AssetBundles.Core.LoadAsset<GameObject>("TheRegularSandwich");
        PrefabUtils.AddBasicComponents(theRegularObject, theRegularInfo.ClassID, theRegularInfo.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(theRegularObject);
        var eatable = theRegularObject.EnsureComponent<Eatable>();
        eatable.decomposes = false;
        eatable.foodValue = 20;
        eatable.waterValue = -2;
        theRegularObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(theRegularObject, 1f, isKinematic: true);
        PrefabUtils.AddVFXFabricating(theRegularObject, "ham-and-cheese", 0, 0.3f, new Vector3(0, 0.024f, 0), 50,
            new Vector3(270, 0, 0));
        var theRegularPrefab = new CustomPrefab(theRegularInfo);
        theRegularPrefab.SetGameObject(theRegularObject);
        theRegularPrefab.SetRecipe(new RecipeData(new Ingredient(Ham.Info.TechType, 2),
                new Ingredient(Cheese.Info.TechType, 1)))
            .WithFabricatorType(AdminFabricator.AdminCraftTree);
        theRegularPrefab.Register();
    }
}