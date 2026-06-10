using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class Cheese
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("RedPlagueCheese")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("Cheese"));
    
    [PrefabRegistration]
    private static void Register()
    {
        var cheeseObject = AssetBundles.Core.LoadAsset<GameObject>("CheesePrefab");
        PrefabUtils.AddBasicComponents(cheeseObject, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(cheeseObject);
        var cheeseEatable = cheeseObject.EnsureComponent<Eatable>();
        cheeseEatable.decomposes = false;
        cheeseEatable.foodValue = 5;
        cheeseEatable.waterValue = 0;
        cheeseObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(cheeseObject, 0.028f, 0.5f);
        var cheesePrefab = new CustomPrefab(Info);
        cheesePrefab.SetGameObject(cheeseObject);
        cheesePrefab.Register();
    }
}