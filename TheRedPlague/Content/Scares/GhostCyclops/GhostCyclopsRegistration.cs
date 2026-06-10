namespace TheRedPlague.Content.Scares.GhostCyclops;

[PrefabClass]
public static class GhostCyclopsRegistration
{
    [PrefabRegistration]
    private static void Register()
    {
        new GhostCyclopsSpawnerPrefab("LRBase1",
            GhostCyclopsCinematic.Path.LostRiverBase, 60).Register();
        new GhostCyclopsSpawnerPrefab("LRBase2",
            GhostCyclopsCinematic.Path.LostRiverBaseCorridor, 50).Register();
        new GhostCyclopsSpawnerPrefab("LRAncientSkeleton",
            GhostCyclopsCinematic.Path.LostRiverAncientSkeleton, 60).Register();
    }
}