using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.MaterialModifiers;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Geological;

[PrefabClass]
public static class LargePlagueCrystal
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("LargePlagueCrystal");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("LargePlagueCrystal"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Far);
        MaterialUtils.ApplySNShaders(obj, 6f, 1f, 0.05f,
            new PlagueCatalystMaterialModifier());
        return obj;
    }
}