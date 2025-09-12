using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Mono.CinematicEvents;
using TheRedPlague.Mono.Triggers;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Special;

public class GhostCyclopsSpawnerPrefab
{
    private GhostCyclopsCinematic.Path Path { get; }
    private PrefabInfo Info { get; }
    private float Radius { get; }

    public GhostCyclopsSpawnerPrefab(string classIdPostfix, GhostCyclopsCinematic.Path path, float radius)
    {
        Info = PrefabInfo.WithTechType("GhostCyclopsTrigger" + classIdPostfix);
        Path = path;
        Radius = radius;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private GameObject GetGameObject()
    {
        var prefab = new GameObject(Info.ClassID);
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        var collider = prefab.AddComponent<SphereCollider>();
        collider.radius = Radius;
        collider.isTrigger = true;
        var trigger = prefab.AddComponent<GhostCyclopsTrigger>();
        trigger.path = Path;
        return prefab;
    }
}