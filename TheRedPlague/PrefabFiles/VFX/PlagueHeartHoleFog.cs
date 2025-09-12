using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;
using UWE;

namespace TheRedPlague.PrefabFiles.VFX;

public class PlagueHeartHoleFog
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueHeartHoleFog");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> result)
    {
        var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PlagueHeartCaveFog"));
        obj.SetActive(false);
        
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        
        var fallingRockTask = PrefabDatabase.GetPrefabAsync("e019dd4a-88e3-49c8-93a4-fe909b9b6391");
        yield return fallingRockTask;
        if (!fallingRockTask.TryGetPrefab(out var fallingRockPrefab))
        {
            Plugin.Logger.LogError("FallingRockPrefab is null");
        }

        var smokeMaterial = new Material(fallingRockPrefab.GetComponent<VFXFallingRocks>().startPrefab.transform
            .Find("HeavySmk")
            .GetComponent<ParticleSystemRenderer>().sharedMaterial);
        
        obj.GetComponent<ParticleSystemRenderer>().sharedMaterial = smokeMaterial;
        
        result.Set(obj);
    }
}