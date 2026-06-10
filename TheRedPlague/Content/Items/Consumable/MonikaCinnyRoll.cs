using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class MonikaCinnyRoll
{
    [PrefabRegistration]
    private static void Register()
    {
        var monikaInfo = PrefabInfo.WithTechType("Monika");
        var monikaObject = AssetBundles.Core.LoadAsset<GameObject>("MonikaPrefab");
        PrefabUtils.AddBasicComponents(monikaObject, monikaInfo.ClassID, monikaInfo.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(monikaObject);
        var monikaEatable = monikaObject.EnsureComponent<Eatable>();
        monikaEatable.decomposes = false;
        monikaEatable.foodValue = 15;
        monikaEatable.waterValue = 4;
        monikaObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(monikaObject, 0.5f, 0.6f, isKinematic: true);
        monikaInfo.WithIcon(AssetBundles.Core.LoadAsset<Sprite>("MonikaIcon"));
        var monikaPrefab = new CustomPrefab(monikaInfo);
        monikaPrefab.SetGameObject(monikaObject);
        monikaPrefab.Register();
    }
}