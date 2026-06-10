using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Act1.IslandElevator;

[PrefabClass]
public static class IslandElevatorPlatform
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("IslandElevatorPlatform");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("IslandElevatorPlatform"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(obj, 7f, 2f, 2f);
        obj.AddComponent<ConstructionObstacle>();
        return obj;
    }
}