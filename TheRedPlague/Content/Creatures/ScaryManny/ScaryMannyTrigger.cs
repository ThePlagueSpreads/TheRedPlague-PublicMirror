using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.ScaryManny;

public static class ScaryMannyTrigger
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ScaryMannyTrigger")
        .WithFolderPath(TrpPrefabFolders.Creatures.Miscellaneous);

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var obj = new GameObject(Info.ClassID);
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        obj.AddTriggerCollider(0.5f);
        obj.AddComponent<SpawnScaryManny>();
        return obj;
    }
}