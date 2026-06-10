using Nautilus.Assets;

namespace TheRedPlague.Content.Environment.Blood;

[PrefabClass]
public static class BloodPrefabs
{
    [PrefabRegistration]
    private static void RegisterVFX()
    {
        var bloodParticles = new BloodParticle[]
        {
            new(PrefabInfo.WithTechType("VFX_BloodSplashContinuous01")
                .WithFolderPath(TrpPrefabFolders.Vfx), "Blood_Splash_01_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodSplashContinuous02")
                .WithFolderPath(TrpPrefabFolders.Vfx), "Blood_Splash_02_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodDecalContinuous01")
                .WithFolderPath(TrpPrefabFolders.Vfx), "BloodDecal_01_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodDecalContinuous02")
                .WithFolderPath(TrpPrefabFolders.Vfx), "BloodDecal_02_Continuous"),
            new(PrefabInfo.WithTechType("VFX_BloodTrail01")
                .WithFolderPath(TrpPrefabFolders.Vfx), "BloodDecal_01_Trail"),
            new(PrefabInfo.WithTechType("VFX_BloodTrail02")
                .WithFolderPath(TrpPrefabFolders.Vfx), "BloodDecal_02_Trail")
        };
        foreach (var bloodParticle in bloodParticles) bloodParticle.Register();

        var bloodPools = new BloodPool[]
        {
            new("VFX_BloodPool1", "d931cce0-b6b3-4f70-aa08-e1ed5ef12b29")
        };
        foreach (var bloodPool in bloodPools) bloodPool.Register();

        var bloodWaterFall = new BloodWaterfall[]
        {
            new("VFX_BloodWaterfall1", "e712fdde-4d3d-4242-b618-cd43a08f0e96")
        };
        foreach (var bloodWaterfall in bloodWaterFall) bloodWaterfall.Register();
    }
}