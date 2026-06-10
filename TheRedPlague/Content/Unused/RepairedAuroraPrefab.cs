using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Unused;

[PrefabClass]
public static class RepairedAuroraPrefab
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("RepairedAurora");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("RepairedAurora"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(obj, 7f, 2f, 1f);
        var renderers = obj.GetComponentsInChildren<Renderer>();
        renderers.ForEach(r => r.enabled = false);
        prefab.Set(obj);
        yield break;
    }
}