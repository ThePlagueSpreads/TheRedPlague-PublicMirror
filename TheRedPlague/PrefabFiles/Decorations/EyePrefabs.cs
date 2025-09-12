using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Mono.VFX;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Decorations;

public static class EyePrefabs
{
    private static PrefabInfo LonelyEyeInfo { get; } = PrefabInfo.WithTechType("LonelyEye");
    private static PrefabInfo ObservantLonelyEyeInfo { get; } = PrefabInfo.WithTechType("ObservantLonelyEye");

    public static void Register()
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
        var prefab = Object.Instantiate(Plugin.CreaturesBundle.LoadAsset<GameObject>("ObserverEyePrefab"));
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