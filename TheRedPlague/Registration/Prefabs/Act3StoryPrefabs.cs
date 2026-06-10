using TheRedPlague.Content.Act3.DomeBase;
using TheRedPlague.Framework.SFX;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class Act3StoryPrefabs
{
    [PrefabRegistration]
    private static void Register()
    {
        RegisterDomeBasePrefabs();
    }

    private static void RegisterDomeBasePrefabs()
    {
        new TrpMusicPlayer("DomeMusicPlayer", LargeWorldEntity.CellLevel.Far, "DomeBaseAmbience", 350, 400)
        {
            ModifyPrefab = obj =>
            {
                obj.AddComponent<DomeBaseAmbienceController>().ambientSoundPlayer = obj.GetComponent<GenericMusicPlayer>();
            }
        }.Register();
    }
}