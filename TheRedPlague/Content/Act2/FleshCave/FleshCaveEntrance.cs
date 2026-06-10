using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Act2.FleshCave;

[PrefabClass]
public static class FleshCaveEntrance
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("FleshCaveEntrance");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("PlagueCaveEntrance"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.VeryFar);
        MaterialUtils.ApplySNShaders(obj);
        var entrance = obj.AddComponent<PlagueCaveEntrance>();
        entrance.animator = obj.GetComponentInChildren<Animator>();
        entrance.blocker = obj.transform.Find("Blocker").gameObject;
        var hatch = obj.transform.Find("Model/Hatch Pivot").gameObject.AddComponent<PlagueCaveHatch>();
        hatch.entrance = entrance;
        prefab.Set(obj);
        yield break;
    }
}