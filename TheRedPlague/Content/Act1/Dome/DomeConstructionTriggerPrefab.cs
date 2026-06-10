using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Act1.Dome;

[PrefabClass]
public static class DomeConstructionTriggerPrefab
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DomeConstructionTrigger")
        .WithFolderPath(TrpPrefabFolders.Technical);

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private static GameObject GetPrefab()
    {
        var obj = new GameObject(Info.ClassID);
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        obj.AddComponent<DomeConstructionTrigger>();
        return obj;
    }
}