using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Act2.ThrusterEvent;

[PrefabClass]
public static class AuroraTentaclePrefab
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AuroraTentacle");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("AuroraTentacle"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        obj.GetComponent<SkyApplier>().anchorSky = Skies.SafeShallow;
        MaterialUtils.ApplySNShaders(obj, 7f, 1, 1f);
        var behavior = obj.AddComponent<AuroraTentacleBehavior>();
        behavior.animator = obj.GetComponentInChildren<Animator>();
        behavior.renderer = obj.GetComponentInChildren<Renderer>();
        behavior.renderer.enabled = false;
        prefab.Set(obj);
        yield break;
    }
}