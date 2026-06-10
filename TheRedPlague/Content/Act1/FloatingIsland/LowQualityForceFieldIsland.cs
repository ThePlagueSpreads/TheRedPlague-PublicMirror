using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;
using UnityEngine.Rendering;
using UWE;

namespace TheRedPlague.Content.Act1.FloatingIsland;

[PrefabClass]
public static class LowQualityForceFieldIsland
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ForceFieldIslandLowQuality");

    [PrefabRegistration]
    private static void Register()
    {
        var lowQualityForceFieldIsland = new CustomPrefab(Info);
        lowQualityForceFieldIsland.SetGameObject(GetLowQualityForceFieldIslandPrefab);
        lowQualityForceFieldIsland.SetSpawns(new SpawnLocation(new Vector3(-128, 160, -128), new Vector3(0, 180, 0),
            new Vector3(1, 1, -1)));
        lowQualityForceFieldIsland.Register();
    }
    
    private static IEnumerator GetLowQualityForceFieldIslandPrefab(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("ForcefieldIslandLowQuality"));
        obj.SetActive(false);
        MaterialUtils.ApplySNShaders(obj);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Global);
        obj.AddComponent<LowQualityIslandMesh>();
        var obstructionRockTask = PrefabDatabase.GetPrefabAsync("20637667-05a4-45cd-8e31-b33799f63118");
        yield return obstructionRockTask;
        if (obstructionRockTask.TryGetPrefab(out var rockPrefab))
        {
            var material = new Material(rockPrefab.GetComponentInChildren<Renderer>().sharedMaterial);
            material.SetFloat("_SideScale", 0.005f);
            var renderers = obj.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                renderer.sharedMaterial = material;
                renderer.shadowCastingMode = ShadowCastingMode.Off;
            }
        }
        yield return null;
        prefab.Set(obj);
    }
}