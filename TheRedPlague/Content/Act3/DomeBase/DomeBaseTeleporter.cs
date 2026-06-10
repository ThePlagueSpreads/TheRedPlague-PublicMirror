using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Content.Act1.Dome;
using UnityEngine;

namespace TheRedPlague.Content.Act3.DomeBase;

public static class DomeBaseTeleporter
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DomeBaseTeleporter");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static IEnumerator GetGameObject(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("DomeBaseTeleporterPrefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(prefab, 8);
        prefab.EnsureComponent<ConstructionObstacle>();

        var colliders = prefab.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.gameObject.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
        }
        
        prefab.transform.Find("TeleportTrigger").gameObject.AddComponent<DomeBaseTeleportTrigger>()
            .targetPosition = new Vector3(2.8f, 2105f, 42.2f);
        
        yield return null;
        result.Set(prefab);
    }
}