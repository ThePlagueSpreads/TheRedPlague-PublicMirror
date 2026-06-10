using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.Gadgets;
using TheRedPlague.Framework.MaterialModifiers;
using UnityEngine;

namespace TheRedPlague.Content.Items.Resources;

[PrefabClass]
public static class PlagueCatalyst
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueResource")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("PlagueCrystalIcon"));

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("PlagueCrystal_Prefab"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj, 6f, 1f, 0.05f,
            new PlagueCatalystMaterialModifier());
        PrefabUtils.AddWorldForces(obj, 14, isKinematic: true);
        obj.AddComponent<Pickupable>();
        PrefabUtils.AddResourceTracker(obj, Info.TechType);
        return obj;
    }
}