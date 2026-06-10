using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Flesh;

[PrefabClass]
public static class AssimilationGeneratorProp
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AssimilationGeneratorProp").WithFolderPath(TrpPrefabFolders.Environment.Flesh);

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var go = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("AssimilationGenerator"));
        go.SetActive(false);
        PrefabUtils.AddBasicComponents(go, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);
        MaterialUtils.ApplySNShaders(go);
        go.AddComponent<ConstructionObstacle>();
        return go;
    }
}