using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;
using UWE;

namespace TheRedPlague.Content.Environment.Precursor;

[PrefabClass]
public class RedPlaguePrecursorLight
{
    private PrefabInfo Info { get; }
    private string LightClassId { get; }

    public RedPlaguePrecursorLight(string classId, string lightClassId)
    {
        Info = PrefabInfo.WithTechType(classId)
            .WithFolderPath(TrpPrefabFolders.Precursor);
        LightClassId = lightClassId;
    }

    [PrefabRegistration]
    private static void RegisterAllVariants()
    {
        new RedPlaguePrecursorLight("RedPlaguePrecursorLight", "742b410c-14d4-42c6-ac84-0e2bcaff09c1").Register();
        new RedPlaguePrecursorLight("RedPlaguePrecursorLight2", "473a8c4d-162f-4575-bbef-16c1c97d1e9d").Register();
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info, "78009225-a9fa-4d21-9580-8719a3368373")
        {
            ModifyPrefabAsync = ModifyPrefabAsync
        });
        prefab.Register();
    }

    private IEnumerator ModifyPrefabAsync(GameObject prefab)
    {
        var lightRequest = PrefabDatabase.GetPrefabAsync(LightClassId);
        yield return lightRequest;
        if (!lightRequest.TryGetPrefab(out var lightPrefab))
        {
            yield break;
        }
        
        var light = Object.Instantiate(lightPrefab, prefab.transform);
        light.transform.localPosition = Vector3.up * 1.4f;
        light.transform.localRotation = Quaternion.identity;
        Object.DestroyImmediate(light.GetComponent<PrefabIdentifier>());
        Object.DestroyImmediate(light.GetComponent<LargeWorldEntity>());
    }
}