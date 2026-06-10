using TheRedPlague.Content.Act2.FleshCave;
using TheRedPlague.Content.Creatures.Chaos;
using TheRedPlague.Content.Creatures.HoverPet;
using TheRedPlague.Content.PlayerInfection;
using TheRedPlague.Framework.Behaviour.Physics;
using TheRedPlague.Framework.SFX;
using TheRedPlague.Framework.VFX;
using UnityEngine;

namespace TheRedPlague.Framework.Essential;

public static class RuntimeEntryPoint
{
    public static void InstantiateManagers(GameObject player)
    {
        player.EnsureComponent<PlayerInfectedBiomeDamage>();
        
        player.EnsureComponent<PlayerInfectionDamageVisualization>();

        try
        {
            MainCamera.camera.gameObject.AddComponent<PlagueScreenFXController>();
        }
        catch (System.Exception exception)
        {
            Plugin.Logger.LogError(exception);
        }
        
        player.AddComponent<PlagueDamageStat>();

        var trpManagersRoot = new GameObject("TRPManagers").transform;
        
        var insanityManager = new GameObject("InsanityManager");
        insanityManager.AddComponent<InsanityManager>();
        insanityManager.transform.SetParent(trpManagersRoot);

        var hoverPetSpawner = new GameObject("HoverPetSpawner");
        hoverPetSpawner.AddComponent<HoverPetSpawner>();
        hoverPetSpawner.transform.SetParent(trpManagersRoot);

        var eventMusicPlayer = new GameObject("EventMusicPlayer");
        eventMusicPlayer.AddComponent<TrpEventMusicPlayer>();
        eventMusicPlayer.transform.SetParent(trpManagersRoot);

        var chaosEyes = new GameObject("ChaosEyeHallucinationManager");
        chaosEyes.AddComponent<ChaosEyeHallucinationManager>();
        chaosEyes.transform.SetParent(trpManagersRoot);
        
        var caveSounds = new GameObject("FleshCaveAmbienceSounds");
        caveSounds.AddComponent<FleshCaveSounds>();
        caveSounds.transform.SetParent(trpManagersRoot);
        
        var globalColliders = new GameObject("TrpGlobalWorldCollidersManager");
        globalColliders.AddComponent<GlobalWorldColliders>();
        globalColliders.transform.SetParent(trpManagersRoot);
    }
}