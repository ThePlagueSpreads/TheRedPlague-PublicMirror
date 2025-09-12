using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;
using UWE;

namespace TheRedPlague.PrefabFiles.Precursor;

public static class DestroyedAlienRobot
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DestroyedAlienRobot");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.Register();
    }
    
    private static IEnumerator CreatePrefab(IOut<GameObject> prefab)
    {
        var task = PrefabDatabase.GetPrefabAsync("4fae8fa4-0280-43bd-bcf1-f3cba97eed77");
        yield return task;
        if (!task.TryGetPrefab(out var alienRobot))
        {
            Plugin.Logger.LogError("Failed to get alien robot prefab");
            yield break;
        }
        var obj = Object.Instantiate(alienRobot.transform.Find("models/Precursor_Driod").gameObject);
        obj.SetActive(false);
        Object.DestroyImmediate(obj.GetComponent<Animator>());
        Object.DestroyImmediate(obj.GetComponent<VFXController>());
        Object.DestroyImmediate(obj.GetComponent<VFXOnAttack>());
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        obj.AddComponent<SphereCollider>();
        prefab.Set(obj);
    }
}