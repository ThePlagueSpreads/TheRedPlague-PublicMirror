using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.Behaviour.Animation;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Flesh;

[PrefabClass]
public static class EyePrefabs
{
    private static PrefabInfo LonelyEyeInfo { get; } = PrefabInfo.WithTechType("LonelyEye").WithFolderPath(TrpPrefabFolders.Environment.Flesh);
    private static PrefabInfo ObservantLonelyEyeInfo { get; } = PrefabInfo.WithTechType("ObservantLonelyEye").WithFolderPath(TrpPrefabFolders.Environment.Flesh);

    [PrefabRegistration]
    private static void RegisterAll()
    {
        var lonelyEye = new CustomPrefab(LonelyEyeInfo);
        lonelyEye.SetGameObject(GetLonelyEyePrefab);
        lonelyEye.Register();
        
        var observantLonelyEye = new CustomPrefab(ObservantLonelyEyeInfo);
        observantLonelyEye.SetGameObject(GetObservantLonelyEyePrefab);
        observantLonelyEye.Register();
    }

    private static GameObject GetLonelyEyePrefab()
    {
        var prefab = Object.Instantiate(AssetBundles.Creatures.LoadAsset<GameObject>("ObserverEyePrefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, LonelyEyeInfo.ClassID, LonelyEyeInfo.TechType, LargeWorldEntity.CellLevel.Medium);
        MaterialUtils.ApplySNShaders(prefab, 7);
        return prefab;
    }

    private static GameObject GetObservantLonelyEyePrefab()
    {
        var prefab = GetLonelyEyePrefab();
        // yes this is safe to do because it uses EnsureComponent
        PrefabUtils.AddBasicComponents(prefab, ObservantLonelyEyeInfo.ClassID, ObservantLonelyEyeInfo.TechType, LargeWorldEntity.CellLevel.Medium);
        var eyeLook = prefab.transform.Find("ObserverEyeDeco").gameObject.AddComponent<GenericEyeLook>();
        eyeLook.degreesPerSecond = 240;
        eyeLook.flipDirection = true;
        eyeLook.dotLimit = 0;
        eyeLook.useLimits = true;
        return prefab;
    }
}