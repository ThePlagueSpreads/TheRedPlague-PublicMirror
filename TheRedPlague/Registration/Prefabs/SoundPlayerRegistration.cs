using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.SFX;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public class SoundPlayerRegistration
{
    [PrefabRegistration]
    private static void RegisterSoundPrefabs()
    {
        new RandomSoundPlayerPrefab(GetInfo("WhispersOfTheDeadA"),
                AudioUtils.GetFmodAsset("WhispersOfTheDeadA"), 5, 12, 35f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXFemaleScreamA"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamA"), 8, 10, 18, RepeatMode.FewTimes)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXFemaleScreamB"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamB"), 8, 10, 18, RepeatMode.FewTimes)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMaleScreamA"),
                AudioUtils.GetFmodAsset("TrpMaleScreamA"), 3, 5, 18, RepeatMode.FewTimes)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMaleScreamB"),
                AudioUtils.GetFmodAsset("TrpMaleScreamB"), 3, 5, 18, RepeatMode.FewTimes)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXFemaleScreamA-OneShot"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamA"), 6, 10, 15f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXFemaleScreamB-OneShot"),
                AudioUtils.GetFmodAsset("TrpFemaleScreamB"), 6, 10, 15f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMaleScreamA-OneShot"),
                AudioUtils.GetFmodAsset("TrpMaleScreamA"), 6, 10, 15f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMaleScreamB-OneShot"),
                AudioUtils.GetFmodAsset("TrpMaleScreamB"), 6, 10, 15f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXDemonAmbience"),
                AudioUtils.GetFmodAsset("TrpDemonAmbience"), 5, 8, 40f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXDoorKnockA"),
                AudioUtils.GetFmodAsset("TrpDoorKnockA"), 5, 12, 30, RepeatMode.FewTimes, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXDoorKnockB"),
                AudioUtils.GetFmodAsset("TrpDoorKnockB"), 5, 12, 30, RepeatMode.FewTimes, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXDoorKnockC"),
                AudioUtils.GetFmodAsset("TrpDoorKnockC"), 5, 10, 30, RepeatMode.FewTimes, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXDoorKnockA-OneShot"),
                AudioUtils.GetFmodAsset("TrpDoorKnockA"), 5, 12, 16, RepeatMode.Once, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXDoorKnockB-OneShot"),
                AudioUtils.GetFmodAsset("TrpDoorKnockB"), 5, 12, 16, RepeatMode.Once, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXDoorKnockC-OneShot"),
                AudioUtils.GetFmodAsset("TrpDoorKnockC"), 5, 10, 16, RepeatMode.Once, 1.5f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMetalGrowlA"),
                AudioUtils.GetFmodAsset("TrpMetalGrowlA"), 11, 21, 35f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMetalScrapeA"),
                AudioUtils.GetFmodAsset("TrpMetalScrapeA"), 7, 14, 35f, RepeatMode.FewTimes)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMrTeethScream-Close-OneShot"),
                AudioUtils.GetFmodAsset("MrTeethScreamOneShot"), 7, 14, 10f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMrTeethScream-Medium-OneShot"),
                AudioUtils.GetFmodAsset("MrTeethScreamOneShot"), 7, 14, 20f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMrTeethScream-Far-OneShot"),
                AudioUtils.GetFmodAsset("MrTeethScreamOneShot"), 7, 14, 30f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXObserver"),
                AudioUtils.GetFmodAsset("TrpObserverAmbience"), 8, 11, 40f)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXObserver-OneShot"),
                AudioUtils.GetFmodAsset("TrpObserverAmbience"), 8, 11, 40f, RepeatMode.Once)
            .Register();

        new RandomSoundPlayerPrefab(GetInfo("TrpSFXMetalDoorClose-OneShot"),
                AudioUtils.GetFmodAsset("TrpMetalDoorClose"), 8, 11, 26f, RepeatMode.Once)
            .Register();
    }

    private static PrefabInfo GetInfo(string classId) => PrefabInfo.WithTechType(classId)
        .WithFolderPath(TrpPrefabFolders.Sfx.SoundPlayers);
}