using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using TheRedPlague.Content.Buildables.VendingMachineStarshipSouvenir;
using UnityEngine;

namespace TheRedPlague.Content.Fragments;

[PrefabClass]
public static class AuroraVendingMachineFragment
{
    private static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AuroraVendingMachineFragment")
        .WithFolderPath(TrpPrefabFolders.FragmentsAndDataboxes);
    
    [PrefabRegistration]
    private static void Register()
    {
        var clone = new CloneTemplate(Info, VendingMachineStarshipSouvenir.Инфо.TechType)
        {
            ModifyPrefab = obj =>
            {
                Object.DestroyImmediate(obj.GetComponent<Constructable>());
            }
        };
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(clone);
        prefab.Register();
        PDAHandler.AddCustomScannerEntry(Info.TechType, VendingMachineStarshipSouvenir.Инфо.TechType, true, 1, 3f, false);
    }
}