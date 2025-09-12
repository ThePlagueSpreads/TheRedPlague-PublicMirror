using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.StoryProps.MeteorSite;

public static class PlagueHeartBayWreck
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueHeartBayWreck");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PlagueHeartBayWreckPrefab"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.VeryFar);
        MaterialUtils.ApplySNShaders(obj, 7f, 1f, 0.5f);
        obj.AddComponent<ConstructionObstacle>();
        return obj;
    }
}