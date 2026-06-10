using Nautilus.Assets;

namespace TheRedPlague.Content.Environment.Corpses;

[PrefabClass]
public static class CorpsePrefabs
{
    public static PrefabInfo InfectedCorpseInfo { get; } = PrefabInfo.WithTechType("InfectedCorpse");
    public static PrefabInfo SkeletonCorpse { get; } = PrefabInfo.WithTechType("SkeletonCorpse");

    [PrefabRegistration]
    private static void Register()
    {
        var infectedCorpse = new CorpsePrefab(InfectedCorpseInfo, "DiverCorpse", true, false);
        infectedCorpse.Register();

        var skeletonCorpse = new CorpsePrefab(SkeletonCorpse, "SkeletonRagdoll", true, false);
        skeletonCorpse.Register();

        new FloatingCorpsePrefab("FloatingCorpse1", "FloatingCorpsePrefab").Register();
    }
}