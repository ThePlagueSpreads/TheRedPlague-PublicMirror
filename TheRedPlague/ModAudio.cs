using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD;
using Nautilus.Extensions;
using Nautilus.FMod;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague;

public static class ModAudio
{
    private static AssetBundle Bundle => Plugin.AudioBundle;

    private const string SfxBus = "bus:/master/SFX_for_pause/PDA_pause/all/SFX";
    private const string IndoorBus = "bus:/master/SFX_for_pause/PDA_pause/all/indoorsounds";
    private const string ReverbSendBus = "bus:/master/SFX_for_pause/PDA_pause/all/SFX/reverbsend";
    private const string SfxCreatures = "bus:/master/SFX_for_pause/PDA_pause/all/SFX/creatures";

    internal static bool AudioFinishedLoading { get; private set; }

    public static void RegisterMainMenuAudio()
    {
        // MAIN MENU MUSIC:
        var sound = AudioUtils.CreateSound(Plugin.RedPlagueMainMenu.LoadAsset<AudioClip>("Project TRP"),
            AudioUtils.StandardSoundModes_2D | MODE.LOOP_NORMAL);
        sound.AddFadeOut(2);
        CustomSoundHandler.RegisterCustomSound("ProjectTRP", sound, AudioUtils.BusPaths.Music);
    }

    public static IEnumerator RegisterAudioAsync(WaitScreenHandler.WaitScreenTask task)
    {
        yield return RegisterAudioAsyncInternal(task);
        AudioFinishedLoading = true;
    }

    private static IEnumerator RegisterAudioAsyncInternal(WaitScreenHandler.WaitScreenTask task)
    {
        // ====== GENERAL SOUNDS ======
        task.Status = "audio (general)";
        yield return null;

        RegisterSound("InfectionLaserShot", "InfectionLaserShot",
            ReverbSendBus, 2, 20);
        Register2DSound("SeaEmperorJumpscare", "SeaEmperorJumpscare", SfxBus);
        Register2DSound("DieFromInfection", "DieFromInfection", SfxBus);
        RegisterSound("DisableDomeSound", "DisableDomeSound", SfxBus, 40, 10000);
        RegisterSound("IslandElevatorActivation", "IslandElevatorActivation", SfxBus);
        // yep, i skip #1
        RegisterSoundWithVariants("WarperJumpscare",
            new[] { "Jumpscare2", "Jumpscare3", "Jumpscare4", "Jumpscare5" },
            SfxBus, 3f, 70);
        RegisterSoundWithVariants("InfectedWarperIdle",
            new[]
                { "WarperSound1", "WarperSound2", "WarperSound3", "WarperSound4", "WarperSound5", "WarperSound6" },
            SfxBus, 5f, 30f);
        RegisterSoundWithVariants("CloseJumpScare",
            // yep, I skip #5
            new[] { "CloseScare1", "CloseScare2", "CloseScare3", "CloseScare4", "CloseScare6" },
            ReverbSendBus, 5f, 30f);
        RegisterSoundWithVariants("ZombieRoar", new[] { "ZombieRoar1", "ZombieRoar2" },
            ReverbSendBus, 5f, 30f);
        RegisterSoundWithVariants("ZombieBite", new[] { "ZombieBite1", "ZombieBite2", "ZombieBite3" },
            ReverbSendBus, 5f, 30f);
        RegisterSoundWithVariants("SmallZombieBite",
            new[] { "SmallZombieBite1", "SmallZombieBite2", "SmallZombieBite3" },
            SfxCreatures, 2f, 6f);

        yield return null;

        RegisterSound("FleshBlobActivate", "FleshBlobActivate", SfxBus, 10, 500);
        RegisterSound("FleshBlobTornadoLoop", "PlagueTornadoLoop", SfxBus, 40, 300);
        RegisterSoundWithVariants("FleshBlobGroan",
            new[]
            {
                "FleshBlobGroan1", "FleshBlobGroan2", "FleshBlobGroan3", "FleshBlobGroan4", "FleshBlobGroan5"
            },
            ReverbSendBus, 10f, 250f);
        RegisterSound("FleshBlobHunt", "FleshBlobApproach", SfxBus, 10, 500);
        RegisterSound("FleshBlobAlarm", "FleshBlobAlarm", SfxBus, 10, 500);
        RegisterSoundWithVariants("FleshBlobWalk", new[] { "FleshBlobWalk1", "FleshBlobWalk2" },
            ReverbSendBus, 10f, 140);
        RegisterSoundWithVariants("FleshBlobRoar",
            new[] { "FleshBlobGroanClose1", "FleshBlobGroanClose2", "FleshBlobGroanClose3", "FleshBlobScream3" },
            ReverbSendBus, 5f, 120f);

        // ====== CREATURE SOUNDS ======
        task.Status = "audio (creatures)";
        yield return null;

        // Mr teeth

        RegisterSoundWithVariants("MrTeethScream", new[] { "MrTeethScream1", "MrTeethScream2", "MrTeethScream3" },
            ReverbSendBus, 5f, 120f);
        RegisterSound("MrTeethGrab", "MrTeethGrab", SfxBus, 5f, 50);
        RegisterSound("MrTeethBury", "MrTeethBury", SfxBus, 5f, 20f);

        // Suckers

        RegisterSound("SuckerDeath", "SuckerDeath", SfxBus, 3f, 50);
        RegisterSound("PossessedVehicleExplodeTimer", "PossessedVehicleExplode", SfxBus,
            3f, 20f);
        RegisterSound("PossessedVehicleExplosion", "Underwater Explosion", SfxBus,
            5f, 50f);
        RegisterSound("PdaExplosion", "Underwater Explosion", SfxBus,
            5f, 70f);

        // Stabby

        RegisterSoundWithVariants("StabbyDamage", new[] { "StabbyDamage1", "StabbyDamage2", "StabbyDamage3" },
            SfxCreatures, 2f, 10f);

        // Grabber

        RegisterSoundWithVariants("GrabberGrowl", new[] { "GrabberGrowl1", "GrabberGrowl2", "GrabberGrowl3" },
            SfxCreatures, 5f, 50f);
        RegisterSoundWithVariants("GrabberFlesh", new[] { "GrabberFlesh1", "GrabberFlesh2", "GrabberFlesh3" },
            SfxCreatures, 5f, 40f);
        RegisterSound("GrabberAttackStart", "GrabberAttackStart", SfxBus, 5f, 30f);
        RegisterSound("GrabberAttackLoop", "GrabberAttackLoop", SfxBus, 5f, 30f, 2f, true);

        // Stalker

        RegisterSoundWithVariants("MutantStalkerBite",
            new[] { "MutantStalkerBite1", "MutantStalkerBite2", "MutantStalkerBite3" },
            SfxCreatures, 2f, 15f);
        RegisterSoundWithVariants("MutantStalkerIdle",
            new[]
            {
                "MutantStalkerIdle1", "MutantStalkerIdle2", "MutantStalkerIdle3", "MutantStalkerIdle4",
                "MutantStalkerRoar1", "MutantStalkerRoar2", "MutantStalkerRoar3", "MutantStalkerRoar4",
                "MutantStalkerRoar5"
            },
            SfxCreatures, 2f, 35f);
        RegisterSound("MutantStalkerDeath", "MutantStalkerDeath", SfxCreatures, 2f, 15f);

        // Drifter
        RegisterSound("DrifterIdle", "DrifterIdle", SfxBus, 5, 80, 0, true);

        // Gilbert

        RegisterSoundWithVariants("GilbertIdle", new[]
            {
                "GilbertIdle1", "GilbertIdle2", "GilbertIdle3", "GilbertIdle4"
            },
            SfxCreatures, 1f, 9f);

        yield return null;

        // Mutants

        RegisterSoundWithVariants("LargeMutantBite",
            new[] { "LargeMutantBite1", "LargeMutantBite2", "LargeMutantBite3" },
            SfxCreatures, 2f, 24f);
        RegisterSoundWithVariants("LargeMutantIdle",
            new[] { "LargeMutantIdle1", "LargeMutantIdle2", "LargeMutantIdle3" },
            SfxCreatures, 2f, 20f);
        RegisterSoundWithVariants("NormalMutantBite",
            new[] { "NormalMutantBite1", "NormalMutantBite2", "NormalMutantBite3", "NormalMutantBite4" },
            SfxCreatures, 2f, 10f);
        RegisterSoundWithVariants("NormalMutantIdle",
            new[] { "NormalMutantIdle1", "NormalMutantIdle2", "NormalMutantIdle3" },
            SfxCreatures, 2f, 16f);

        // Infected reefback

        RegisterSoundWithVariants("InfestedReefbackCall", new[] { "InfestedReefback1", "InfestedReefback2" },
            ReverbSendBus, 15f, 300f);

        // Phantom leviathan

        RegisterSoundWithVariants("PhantomLeviathanRoarClose",
            new[]
            {
                "INFECTED_GHOST_LEVIATHAN_CLOSE-001", "INFECTED_GHOST_LEVIATHAN_CLOSE-002",
                "INFECTED_GHOST_LEVIATHAN_CLOSE-003", "INFECTED_GHOST_LEVIATHAN_CLOSE-004",
                "INFECTED_GHOST_LEVIATHAN_CLOSE-005"
            },
            ReverbSendBus, 8f, 400f);

        RegisterSoundWithVariants("PhantomLeviathanRoarFar",
            new[]
            {
                "INFECTED_GHOST_LEVIATHAN_FAR-001", "INFECTED_GHOST_LEVIATHAN_FAR-002",
                "INFECTED_GHOST_LEVIATHAN_FAR-003", "INFECTED_GHOST_LEVIATHAN_FAR-004",
                "INFECTED_GHOST_LEVIATHAN_FAR-005"
            },
            ReverbSendBus, 8f, 550f);

        RegisterSoundWithVariants("PhantomLeviathanAttack",
            new[]
            {
                "INFECTED_GHOST_LEVIATHAN_CLOSE-004", "INFECTED_GHOST_LEVIATHAN_CLOSE-005",
                "infected ghost leviathan LONG"
            },
            ReverbSendBus, 8f, 400f);

        // Insectoid

        RegisterSoundWithVariants("InsectoidScreech",
            new[] { "InsectoidScreech1", "InsectoidScreech2" },
            SfxBus, 1f, 14f);

        RegisterSound("InsectoidWalk", "InsectoidWalkLoop", SfxBus, 1f, 18f, 0, true);

        // Chaos leviathan

        RegisterSoundWithVariants("ChaosLeviathanRoarCloseLong", new[]
        {
            "ChaosRoarLong1",
            "ChaosRoarLong2",
            "ChaosRoarLong3",
            "ChaosRoarLong4",
            "ChaosRoarLong5",
            "ChaosRoarLong6",
            "ChaosRoarLong7"
        }, ReverbSendBus, 10, 800);
        RegisterSoundWithVariants("ChaosLeviathanRoarCloseShort", new[]
        {
            "ChaosRoarShort1",
            "ChaosRoarShort2",
            "ChaosRoarShort3",
            "ChaosRoarShort4",
        }, ReverbSendBus, 10, 800);
        RegisterSoundWithVariants("ChaosLeviathanRoarFarLong", new[]
        {
            "ChaosRoarLongFar1",
            "ChaosRoarLongFar2",
            "ChaosRoarLongFar3",
            "ChaosRoarLongFar4",
            "ChaosRoarLongFar5",
            "ChaosRoarLongFar6",
            "ChaosRoarLongFar7"
        }, ReverbSendBus, 10, 800);
        RegisterSoundWithVariants("ChaosLeviathanRoarFarShort", new[]
        {
            "ChaosRoarShortFar1",
            "ChaosRoarShortFar2",
            "ChaosRoarShortFar3",
            "ChaosRoarShortFar4",
        }, ReverbSendBus, 10, 800);

        yield return null;

        // ====== CINEMATIC SOUNDS ======
        task.Status = "audio (cinematics)";
        yield return null;

        // Cinematics
        Register2DSound("DomeConstruction", "DomeConstruction", SfxBus);
        RegisterSound("NuclearExplosion", "Nuclear Explosion", SfxBus, 5f, 5000f);
        RegisterSound("NuclearShockwave", "Nuclear Shockwave", SfxBus, 5f, 5000f);

        RegisterSound("ShuttleAscend", "ShuttleAscend", SfxBus, 5f, 160f);
        RegisterSound("ShuttleDescend", "ShuttleDescend", SfxBus, 5f, 80f);
        RegisterSound("ShuttleTakeOff", "ShuttleTakeOff", SfxBus, 15f, 500f);
        RegisterSound("ShuttleExplosion", "Underwater Explosion 2", SfxBus,
            10, 500);
        
        // Base game events
        
        Register2DSound("Enzyme42Corrosion", "Enzyme42Damage", SfxBus);

        // Bennet
        Register2DSound("B3NTFirstMeet", "B3NT-FirstMeet", SfxBus);
        Register2DSound("B3NTCatalystCinematic", "B3NT-CatalystCinematic", SfxBus);

        // PDA destruction
        
        RegisterSound("PdaDestructionVoiceLine", "PdaDestructionVoiceLine", SfxBus, 2, 22);
        RegisterSound("PdaExplodeSFX", "PdaExplodeSFX", SfxBus, 10f, 40f);

        // Act 2 ending cinematic
        RegisterSound("SatelliteCommunicationDeviceActivate", "SatelliteCommunicationDevice", SfxBus, 10f, 400f);
        RegisterSound("PlagueHeartExplosion", "PlagueHeartExplosion", SfxBus, 10f, 500f);
        RegisterSound("PlagueHeartForcefieldLoop", "PlagueHeartForcefieldLoop", SfxBus, 10f, 100f, 0.2f, true);
        RegisterSound("PlagueHeartForcefieldPowerDown", "PlagueHeartForcefieldPowerDown", SfxBus, 10, 130);
        RegisterSound("PlagueHeartHatchOpen", "PlagueHeartHatchOpen", SfxBus, 10, 200);
        Register2DSound("TrpSpaceCinematicSFX", "SpaceCinematicSFX", SfxBus);

        // ====== TECH SOUNDS ======
        task.Status = "audio (technology)";
        yield return null;

        // Air strike device
        Register2DSound("AirStrike", "AirStrike", SfxBus);
        RegisterSoundWithVariants("AirStrikeExplosion", new[] { "Underwater Explosion", "Underwater Explosion 2" },
            ReverbSendBus, 10f, 300f);

        // Assimilation generator
        RegisterSound("AssimilationGeneratorActivate", "AssimilationGeneratorActivate", SfxBus, 6f, 50f);
        RegisterSound("AssimilationGeneratorBite", "AssimilationGeneratorBite", SfxBus, 6f, 20f);
        RegisterSound("AssimilationGeneratorLoop", "AssimilationGeneratorLoop", SfxBus, 5f, 30f, 5f);

        // Plague refinery
        RegisterSound("PlagueRefineryFinish", "PlagueRefineryFinish", IndoorBus, 5f, 20f);
        RegisterSound("PlagueRefineryInsert", "PlagueRefineryInsert", IndoorBus, 5f, 10f);
        RegisterSound("PlagueRefineryWorking", "PlagueRefineryWorking", IndoorBus, 3f, 18f);

        // Plague altar
        RegisterSound("PlagueAltarFabricating", "FleshFabricatorCreate", IndoorBus, 2f, 14f, 1f);
        RegisterSound("PlagueAltarIdle", "FleshFabricatorIdle", IndoorBus, 1.5f, 8f);
        RegisterSoundWithVariants("PlagueAltarInteract",
            new[] { "FleshFabricatorInteract1", "FleshFabricatorInteract2", "FleshFabricatorInteract3" }, IndoorBus,
            3f, 8f);

        // Plague neutralizer
        RegisterSound("PlagueNeutralizerFabricating", "PlagueNeutralizerLoop", IndoorBus, 2, 10, 1, true);

        RegisterSound("PlagueNeutralizerAddCatalyst", "PlagueNeutralizerStart", IndoorBus, 2f, 10f, 1f);
        RegisterSoundWithVariants("PlagueNeutralizerInteract",
            new[] { "PlagueNeutralizerUse1", "PlagueNeutralizerUse2" }, IndoorBus,
            3f, 8f);

        // Insanity deterrent
        RegisterSound("InsanityDeterrentWorking", "InsanityDeterrentLoop", IndoorBus, 1f, 15f, 0.5f, true);

        // Plague grappler
        Register2DSound("PlagueGrapplerCollide", "PlagueGrapplerCollide", SfxBus);
        Register2DSound("PlagueGrapplerFire", "PlagueGrapplerFire", SfxBus);
        Register2DSound("PlagueGrapplerGrab", "PlagueGrapplerGrab", SfxBus);
        Register2DSound("PlagueGrapplerTraverse", "PlagueGrapplerTraverse", SfxBus);
        RegisterSound("PlagueCaveOpenSFX", "PlagueCaveOpenSFX", SfxBus);

        // Infection tracker
        RegisterSoundWithVariants("InfectionTrackerPing",
            new[] { "InfectionTracker1", "InfectionTracker2", "InfectionTracker3" }, AudioUtils.BusPaths.PlayerSFXs, 5f,
            20f, 1f);
        RegisterSound("InfectionTrackerClose", "InfectionTrackerCloseSfx",
            AudioUtils.BusPaths.PlayerSFXs, 5f, 20f, 0.2f, true);

        // Drifter cannon
        RegisterSound("DrifterCannonFire", "DrifterCannonFire", SfxBus, 5f, 15f);
        RegisterSound("DrifterCannonFireNoAmmo", "DrifterCannonFireNoAmmo", SfxBus, 5f, 15f);

        // ====== PLAGUE CYCLOPS SOUNDS ======
        task.Status = "audio (Plague Cyclops)";
        yield return null;

        RegisterSound("PlagueCyclopsAheadFlank", "aheadflank",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsAheadSlow", "aheadslow",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsAheadStandard", "aheadstandard",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsAssimilationSuccessful", "assimilationsuccessful",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, 200f);
        RegisterSoundWithVariants("PlagueCyclopsScream", new[] { "cyclopsscream1", "cyclopsscream2", "cyclopsscream3" },
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, 200f);
        RegisterSound("PlagueCyclopsEngineMaintenance", "enginemaintenance",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsEnginePoweringDown", "EnginePoweringDown",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsEnginePoweringUp", "EnginePoweringUp",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsPropellerObstruction", "propellorobstruction",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsWelcomeAboard", "welcomeaboard",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsWelcomeAboardGlitched", "WelcomeAboardGlitched",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsWreckSpeech", "wreckspeech",
            "bus:/master/SFX_for_pause/all_no_pda_pause/all_voice_no_pda_pause/VOs", 5f, -1);
        RegisterSound("PlagueCyclopsDeath", "PlagueCyclopsDeath",
            "bus:/master/SFX_for_pause/PDA_pause/all/all voice/cyclops voice", 5f, -1);
        RegisterSound("PlagueCyclopsFall", "cyclopscrash", SfxBus, 5f, 100);
        RegisterSound("PlagueCyclopsTentaclesSpawn", "cyclopsupgradeanimation", SfxBus, 5f, 20);
        RegisterSound("PlagueCyclopsEngineBreak", "CyclopsEngineBreak", SfxBus, 5f, 40);

        // ====== MUSIC ======
        task.Status = "audio (music)";
        yield return null;

        // MUSIC!

        RegisterMusic("RedPlagueThemeMusic", "the plague spreads", 10, true);
        RegisterMusic("VoidIslandMusic", "voidislandcave");
        RegisterMusic("IslandElevator", "into the sky", 0, true);
        RegisterMusic("SkyIslandMusic", "skyisland");
        RegisterMusic("MazeBaseSoundtrack", new[] { "InTheHallsOfTheInsane", "theplagueisalie" }, 3f);
        RegisterMusic("FleshCaveSoundtrack",
            new[] { "nothingleft", "theplagueisalie", "bloodstains", "dissonance in the harmony" }, 3f);
        RegisterMusic("ShrineBaseMusic", "Panacea", 4f);
        RegisterMusic("MrTeethMusic", "mrteeth", 8f);
        RegisterMusic("AssimilationSuccessful", "assimilationsuccessfulmusic", 2f);
        RegisterMusic("LullabyOfTheDamned", "lullaby of the damned", 3f);
        RegisterMusic("BioweaponShort", "bioweapon (shortened)", 2f);
        RegisterMusic("RedPlaguePartyTime", "Party time", 1f);
        RegisterMusic("SanctuaryOfTheCreator", "Sanctuary of the Creator", 3f);
        RegisterMusic("EndOfTheWorld", "endoftheworld", 3f);
        RegisterMusic("LuesRubra", "Lues Rubra", 0.5f);
        RegisterMusic("NotOurShipAnymore", "not our ship anymore", 0.5f);
        // remember to consider youtubers tend to turn off music volume
        
        RegisterSound("CorruptedRadioMusic", "CorruptedRadioMusic", SfxBus, 2f, 30f, 0f, true);

        // ====== VOICE LINE SOUNDS ======
        task.Status = "audio (voice lines)";
        yield return null;

        // Non-story voice lines

        Register2DSound("PDAReceiveShuttle", "PDAReceiveShuttle", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("PDASendShuttle", "PDASendShuttle", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("PDAShuttleContactLost", "PDAShuttleContactLost", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("ShuttleInvalidItem4", "PDAShuttleInvalidItem4", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("PDAShuttleContractTerminated", "PDAShuttleContractTerminated", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("PDAShuttleItemNotWanted", "PDAShuttleInvalidItem3", AudioUtils.BusPaths.VoiceOvers);

        // Hover pet lines

        Register2DSound("HoverPetPDA1", "HoverPetPDA1", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("HoverPetPDA2", "HoverPetPDA2", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("HoverPetPDA3", "HoverPetPDA3", AudioUtils.BusPaths.VoiceOvers);
        Register2DSound("HoverPetPDA4", "HoverPetPDA4", AudioUtils.BusPaths.VoiceOvers);
        
        // Satellite communicator
        
        Register2DSound("SatelliteCommunicatorFail", "SatelliteCommunicatorFail", AudioUtils.BusPaths.VoiceOvers);
        
        // Cassy

        RegisterSound("CassyBehindDoors0", "CassyBehindDoors0", ReverbSendBus, 3, 25);
        RegisterSound("CassyBehindDoors1", "CassyBehindDoors1", ReverbSendBus, 3, 25);
        RegisterSound("CassyBehindDoors2", "CassyBehindDoors2", ReverbSendBus, 3, 25);
        RegisterSound("CassyBehindDoors3", "CassyBehindDoors3", ReverbSendBus, 3, 25);
        RegisterSound("CassyBehindDoors4", "CassyBehindDoors4", ReverbSendBus, 3, 25);

        // ====== INSANITY SOUNDS ======
        task.Status = "audio (hallucinations)";
        yield return null;

        RegisterSoundWithVariants("RandomFootsteps", new[] { "FootstepsSlow", "FootstepsFast" },
            ReverbSendBus, 5f, 120f);
        RegisterSoundWithVariants("RandomAuroraFootsteps", new[] { "FootstepsSlow", "FootstepsFast" },
            ReverbSendBus, 5f, 120f);
        RegisterSound("EnterBaseScare", "EnterBaseScare", SfxBus, 3f, 50f);

        RegisterSoundWithVariants("CyclopsBaseLightFlicker",
            new[] { "CyclopsBaseLightFlicker1", "CyclopsBaseLightFlicker2", "CyclopsBaseLightFlicker3" },
            SfxBus, -1f, -1f, 0.5f);
        RegisterSoundWithVariants("PrawnsuitLightFlicker",
            new[] { "PrawnsuitLightFlicker1", "PrawnsuitLightFlicker2", "PrawnsuitLightFlicker3" },
            SfxBus, -1f, -1f, 0.5f);
        RegisterSoundWithVariants("SeamothLightFlicker",
            new[] { "SeamothLightFlicker1", "SeamothLightFlicker2", "SeamothLightFlicker3" },
            SfxBus, -1f, -1f, 0.5f);
        RegisterSoundWithVariants("ToolsLightFlicker",
            new[] { "ToolsLightFlicker1", "ToolsLightFlicker2", "ToolsLightFlicker3" },
            SfxBus, -1f, -1f, 0.5f);
        RegisterSound("CyclopsVoiceGlitch", "CyclopsVoiceGlitch", SfxBus, -1f, -1f);
        RegisterSound("SeamothVoiceGlitch", "SeamothVoiceGlitch", SfxBus, -1f, -1f);

        var hallucinationSounds = Bundle.LoadAllAssetsWithPrefix<AudioClip>("SoundHallucination");
        foreach (var hallucinationSound in hallucinationSounds)
        {
            var sound = AudioUtils.CreateSound(hallucinationSound, AudioUtils.StandardSoundModes_3D);
            sound.set3DMinMaxDistance(1, 100f);

            CustomSoundHandler.RegisterCustomSound(hallucinationSound.name, sound, SfxBus);
        }

        RegisterSound("FakeOxygen10", "FakeOxygen10", AudioUtils.BusPaths.VoiceOvers, -1f, -1f);
        RegisterSound("FakeOxygen25", "FakeOxygen25", AudioUtils.BusPaths.VoiceOvers, -1f, -1f);
        RegisterSound("FakeOxygenBeep", "FakeOxygenBeep", AudioUtils.BusPaths.VoiceOvers, -1f, -1f);

        // ====== AMBIENT SOUNDS ======
        task.Status = "audio (ambience)";
        yield return null;

        // Biome/background ambience
        RegisterAmbience("InfectedDunesAmbience", "InfectedDunesAmbience");
        RegisterAmbience("ShrineSOSAmbience", "ShrineSOSAmbience");
        RegisterAmbience("PlagueCaveAmbience", "PlagueCaveAmbience");
        RegisterAmbience("ShrineBaseAmbience", "ShrineBaseAmbience", 2f, AudioUtils.BusPaths.SFX);

        // Scares
        RegisterSoundWithVariants("WhispersOfTheDeadA",
            LoadAllAssetsWithPrefix<AudioClip>(Bundle, "WhispersOfTheDeadA").Select(a => a.name).ToArray(),
            SfxBus, 10f, 40f);

        // Aurora misc
        RegisterSound("UnlockTurretScream", "UnlockTurretScream", SfxBus, 5f, 100f);
        RegisterSound("AuroraThrusterEvent", "AuroraThrusterEventFinal", SfxBus, 5f, 10000f);

        // Screams and Aurora SFX
        RegisterSoundWithVariants("TrpFemaleScreamA",
            new[] { "FemaleScreamA1", "FemaleScreamA2", "FemaleScreamA3", "FemaleScreamA4", "FemaleScreamA5" },
            SfxBus, 3f, 20f);

        RegisterSoundWithVariants("TrpFemaleScreamB",
            new[] { "FemaleScreamB1", "FemaleScreamB2", "FemaleScreamB3", "FemaleScreamB4" },
            SfxBus, 3f, 20f);

        RegisterSoundWithVariants("TrpMaleScreamA",
            new[] { "MaleScreamA1", "MaleScreamA2", "MaleScreamA3" },
            SfxBus, 3f, 20f);

        RegisterSoundWithVariants("TrpMaleScreamB",
            new[] { "MaleScreamB1", "MaleScreamB2", "MaleScreamB3", "MaleScreamB4", "MaleScreamB5" },
            SfxBus, 3f, 20f);

        RegisterSoundWithVariants("TrpDemonAmbience",
            new[] { "DemonBreatheA1", "DemonBreatheA2", "DemonBreatheA3" },
            SfxBus, 3f, 18f);

        RegisterSoundWithVariants("TrpDoorKnockA",
            new[] { "DoorKnockA1", "DoorKnockA2", "DoorKnockA3", "DoorKnockA4", "DoorKnockCommon1" },
            SfxBus, 3f, 22f);

        RegisterSoundWithVariants("TrpDoorKnockB",
            new[] { "DoorKnockB1", "DoorKnockB2", "DoorKnockB3", "DoorKnockB4", "DoorKnockCommon2" },
            SfxBus, 3f, 22f);

        RegisterSoundWithVariants("TrpDoorKnockC",
            new[] { "DoorKnockCommon1", "DoorKnockCommon2", "DoorKnockB3" },
            SfxBus, 3f, 22f);

        RegisterSoundWithVariants("TrpMetalGrowlA",
            new[] { "MetalGrowlA1", "MetalGrowlA2", "MetalGrowlA3", "MetalGrowlA4", "MetalGrowlB1", "MetalGrowlB2" },
            SfxBus, 3f, 50f);

        RegisterSoundWithVariants("TrpMetalScrapeA",
            new[] { "MetalScrape1", "MetalScrape2", "MetalScrape3", "MetalScrape4", "MetalScrape5", "MetalScrape6" },
            SfxBus, 3f, 40f);

        RegisterSound("TrpMetalDoorClose", "MetalDoorClose", SfxBus, 4f, 60f);

        RegisterSoundWithVariants("MrTeethScreamOneShot",
            new[]
            {
                "MrTeethScreamOneShot1", "MrTeethScreamOneShot2", "MrTeethScreamOneShot3", "MrTeethScreamOneShot4"
            },
            ReverbSendBus, 5f, 100f);

        RegisterSoundWithVariants("TrpObserverAmbience",
            new[] { "ObserverAmbience1", "ObserverAmbience2", "ObserverAmbience3" },
            AudioUtils.BusPaths.UnderwaterCreatures, 3f, 90f);

        RegisterSoundWithVariants("ObserverCloseSounds",
            new[] { "DemonBreatheA1", "DemonBreatheA2", "DemonBreatheA3" },
            AudioUtils.BusPaths.UnderwaterCreatures, 1f, 12f);

        RegisterSoundWithVariants("SkeletonAmbience",
            new[] { "SkeletonAmbience1", "SkeletonAmbience2", "SkeletonAmbience3" },
            AudioUtils.BusPaths.SFX, 1f, 30f);
        
        RegisterSoundWithVariants("TrpCaveSounds",
            new[] { "CaveSound1", "CaveSound2", "CaveSound3", "CaveSound4", "CaveSound5", "CaveSound6", "CaveSound7", "CaveSound8" },
            AudioUtils.BusPaths.UnderwaterAmbient, 5f, 70f);
        
        RegisterSoundWithVariants("TrpCreepySing",
            new[] { "CreepySing1", "CreepySing2", "CreepySing3", "CreepySing4", "CreepySing5" },
            SfxBus, 3f, 160);
    }

    public static IEnumerable<T> LoadAllAssetsWithPrefix<T>(this AssetBundle assetBundle, string prefix)
        where T : Object
    {
        return assetBundle.LoadAllAssets<T>().Where(c => c.name.StartsWith(prefix));
    }

    private static void Register2DSound(string id, string clipName, string bus, float fadeOutDuration = -1f)
    {
        var sound = AudioUtils.CreateSound(Bundle.LoadAsset<AudioClip>(clipName), AudioUtils.StandardSoundModes_2D);

        if (fadeOutDuration > 0)
        {
            sound.AddFadeOut(fadeOutDuration);
        }

        CustomSoundHandler.RegisterCustomSound(id, sound, bus);
    }

    private static void RegisterMusic(string id, string clipName, float fadeOutDuration = 10f,
        bool useSoundEffectsBus = false, bool looping = false)
    {
        var mode = AudioUtils.StandardSoundModes_2D;
        if (looping)
            mode |= MODE.LOOP_NORMAL;
        var sound = AudioUtils.CreateSound(Bundle.LoadAsset<AudioClip>(clipName), mode);
        if (fadeOutDuration > Mathf.Epsilon)
            sound.AddFadeOut(fadeOutDuration);
        CustomSoundHandler.RegisterCustomSound(id, sound,
            useSoundEffectsBus ? "bus:/master/SFX_for_pause/PDA_pause/all" : AudioUtils.BusPaths.Music);
    }

    private static void RegisterAmbience(string id, string clipName, float fadeOutDuration = 2f,
        string bus = "bus:/master/SFX_for_pause/PDA_pause/all/SFX/backgrounds")
    {
        var sound = AudioUtils.CreateSound(Bundle.LoadAsset<AudioClip>(clipName),
            AudioUtils.StandardSoundModes_2D | MODE.LOOP_NORMAL);
        sound.AddFadeOut(fadeOutDuration);
        CustomSoundHandler.RegisterCustomSound(id, sound, bus);
    }

    private static void RegisterMusic(string id, string[] clipNames, float fadeOutDuration = 10f,
        bool useSoundEffectsBus = false)
    {
        var clipList = new List<AudioClip>();
        clipNames.ForEach(clipName => clipList.Add(Bundle.LoadAsset<AudioClip>(clipName)));

        var sounds = AudioUtils.CreateSounds(clipList, AudioUtils.StandardSoundModes_2D).ToArray();
        sounds.ForEach(sound => sound.AddFadeOut(fadeOutDuration));

        var multiSoundsEvent = new FModMultiSounds(sounds,
            useSoundEffectsBus ? "bus:/master/SFX_for_pause/PDA_pause/all" : AudioUtils.BusPaths.Music, true);

        CustomSoundHandler.RegisterCustomSound(id, multiSoundsEvent);
    }

    private static void RegisterSound(string id, string clipName, string bus, float minDistance = 10f,
        float maxDistance = 200f, float fadeDuration = 0, bool looping = false)
    {
        var mode = maxDistance >= 0 ? AudioUtils.StandardSoundModes_3D : AudioUtils.StandardSoundModes_2D;
        if (looping)
            mode |= MODE.LOOP_NORMAL;
        var sound = AudioUtils.CreateSound(Bundle.LoadAsset<AudioClip>(clipName), mode);
        if (maxDistance >= 0)
            sound.set3DMinMaxDistance(minDistance, maxDistance);

        if (fadeDuration > 0)
        {
            sound.AddFadeOut(fadeDuration);
        }

        CustomSoundHandler.RegisterCustomSound(id, sound, bus);
    }

    private static void RegisterSoundWithVariants(string id, string[] clipNames, string bus, float minDistance = 10f,
        float maxDistance = 200f, float fadeDuration = -1f)
    {
        var clipList = new List<AudioClip>();
        clipNames.ForEach(clipName => clipList.Add(Bundle.LoadAsset<AudioClip>(clipName)));

        var sounds = AudioUtils.CreateSounds(clipList,
            maxDistance >= 0 ? AudioUtils.StandardSoundModes_3D : AudioUtils.StandardSoundModes_2D);
        sounds.ForEach(sound =>
        {
            if (maxDistance >= 0)
                sound.set3DMinMaxDistance(minDistance, maxDistance);
            if (fadeDuration > 0)
                sound.AddFadeOut(fadeDuration);
        });


        var multiSoundsEvent = new FModMultiSounds(sounds.ToArray(), bus, true);

        CustomSoundHandler.RegisterCustomSound(id, multiSoundsEvent);
    }
}