using System.Collections;
using Nautilus.Assets;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.PlagueAltar;
using UnityEngine;

namespace TheRedPlague.Content.Fragments;

[PrefabClass]
public static class PlagueAltarFragment
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueAltarFragment")
        .WithFolderPath(TrpPrefabFolders.FragmentsAndDataboxes);

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.Register();
        WorldEntityDatabaseHandler.AddCustomInfo(Info.ClassID, Info.TechType, Vector3.one, false,
            LargeWorldEntity.CellLevel.Near, EntitySlot.Type.Medium);
        PDAHandler.AddCustomScannerEntry(Info.TechType, PlagueAltar.Info.TechType,
            true, 1, 6, false);
    }
    
    private static IEnumerator CreatePrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("PlagueAltarFragment_Prefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(prefab, 7, 1, 1, new PlagueAltar.PlagueAltarMaterialModifier());
        var modelParent = prefab.transform.Find("Plague Altar Animated");
        
        var eyes = new[]
        {
            modelParent.Find("Eye.001"),
            modelParent.Find("Eye.002"),
            modelParent.Find("Eye.003"),
            modelParent.Find("Eye.004"),
            modelParent.Find("Eye.005"),
            modelParent.Find("Eye.006"),
        };

        foreach (var eye in eyes)
        {
            var lastNumber = int.Parse(eye.name[^1].ToString());
            eye.gameObject.AddComponent<PlagueAltarEye>().flip = lastNumber >= 4;
        }
        
        result.Set(prefab);
        yield break;
    }

}