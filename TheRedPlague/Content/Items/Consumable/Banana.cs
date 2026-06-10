using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class Banana
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Banana");

    [PrefabRegistration]
    private static void Register()
    {
        var bananaObject = AssetBundles.Core.LoadAsset<GameObject>("BananaPrefab");
        PrefabUtils.AddBasicComponents(bananaObject, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(bananaObject);
        var bananaEatable = bananaObject.EnsureComponent<Eatable>();
        bananaEatable.decomposes = false;
        bananaEatable.foodValue = 18;
        bananaEatable.waterValue = 0;
        bananaObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(bananaObject, 0.6f, 0.7f, isKinematic: true);
        Info.WithIcon(AssetBundles.Core.LoadAsset<Sprite>("BananaIcon"));
        var bananaPrefab = new CustomPrefab(Info);
        bananaPrefab.SetGameObject(bananaObject);
        bananaPrefab.Register();
    }
}