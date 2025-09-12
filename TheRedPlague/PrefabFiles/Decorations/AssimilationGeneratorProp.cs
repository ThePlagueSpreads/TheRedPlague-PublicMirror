using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Decorations;

public static class AssimilationGeneratorProp
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AssimilationGeneratorProp");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var go = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("AssimilationGenerator"));
        go.SetActive(false);
        PrefabUtils.AddBasicComponents(go, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);
        MaterialUtils.ApplySNShaders(go);
        go.AddComponent<ConstructionObstacle>();
        return go;
    }
}