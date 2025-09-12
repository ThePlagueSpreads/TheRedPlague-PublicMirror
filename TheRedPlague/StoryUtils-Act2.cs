using System.Collections;
using Nautilus.Handlers;
using Nautilus.Utility;
using Story;
using TheRedPlague.Data;
using TheRedPlague.Managers;
using TheRedPlague.Mono.CinematicEvents;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Mono.Systems;
using TheRedPlague.PrefabFiles.Buildable;
using TheRedPlague.PrefabFiles.Creatures;
using TheRedPlague.PrefabFiles.Equipment;
using TheRedPlague.PrefabFiles.Items;
using TheRedPlague.PrefabFiles.StoryProps;
using UnityEngine;
using UWE;

namespace TheRedPlague;

public static partial class StoryUtils
{
    // Introduction
    public static CompoundGoal ArriveToDomeGoal { get; private set; }
    public static CompoundGoal DermanFirstContact { get; private set; }
    public static CompoundGoal Act2MissionInstructionsRadio { get; private set; }

    public static CompoundGoal DermanFirstContact2 { get; private set; }

    // Primary mission

    public static StoryGoal OfferingToThePlagueMission { get; private set; }
    public static StoryGoal MrTeethMission { get; private set; }

    public static StoryGoal JohnKyleMission { get; private set; }

    // (THIS IS A CHECKLIST GOAL)
    public static StoryGoal PlagueResearchMission { get; private set; }

    public static StoryGoal Act2MissionsFollowUp1 { get; private set; }
    public static StoryGoal Act2MissionsFollowUp1Derman { get; private set; }
    public static StoryGoal Act2MissionsFollowUp2 { get; private set; }
    public static StoryGoal Act2MissionsFollowUp2Derman { get; private set; }
    public static StoryGoal Act2MissionsFollowUp2DermanTech { get; private set; }
    public static StoryGoal Act2MissionsFollowUp2DermanTechUnlock { get; private set; }
    public static StoryGoal Act2MissionsFollowUp3 { get; private set; }
    public static StoryGoal Act2MissionsFollowUp3Alterra { get; private set; }
    public static StoryGoal Act2MissionsFollowUp3Joke { get; private set; }
    public static StoryGoal Act2MissionsFollowUp4 { get; private set; }
    public static CompoundGoal UnlockPlagueCaveSignalGoal { get; private set; }
    public static CompoundGoal SpawnPrecursorSatelliteGoal { get; private set; }
    public static StoryGoal PlagueCaveEntranceHint { get; private set; }
    public static StoryGoal OpenPlagueCave { get; private set; }

    // Item collection
    public static ItemGoal PlagueCatalystItemGoal { get; private set; }
    public static CompoundGoal SendManifestationsShuttleReminder { get; private set; }
    public static ItemGoal MysteriousRemainsItemGoal { get; private set; }
    public static ItemGoal DormantNeuralMatterItemGoal { get; private set; }
    public static ItemGoal NeutralizedPlagueIngotItemGoal { get; private set; }
    public static ItemGoal AmalgamatedBoneItemGoal { get; private set; }
    public static ItemGoal WarperHeartItemGoal { get; private set; }

    // Shuttle delivery
    public static CompoundGoal UnlockFollowUpPlagueKnife { get; private set; }
    public static CompoundGoal UnlockFollowUpGilbert { get; private set; }
    public static CompoundGoal UnlockFollowUpNeuralMatter { get; private set; }
    public static CompoundGoal UnlockFollowUpBoneRegurgitator { get; private set; }

    // Maze base
    public static StoryGoal HoverPetReachedMazeBaseGoal { get; private set; }
    public static LocationGoal MazeBaseDiscoveryGoal { get; private set; }

    // (THIS IS A CHECKLIST GOAL)
    public static StoryGoal
        ScanPlagueAltarGoal { get; private set; } // Calls the other altar goals when not in creative

    public static StoryGoal UnlockPlagueAltarPda { get; private set; }

    public static CompoundGoal UnlockPlagueAltarAlterra { get; private set; }

    // Aurora

    public static StoryGoal CassyEncounter1 { get; private set; }
    public static LocationGoal CassyEncounter2MainTrigger { get; private set; }
    public static LocationGoal CassyEncounter2AltEntranceTrigger { get; private set; }
    public static StoryGoal CassyEncounter2 { get; private set; }
    public static LocationGoal DriveCoreHallwayGoal { get; private set; }

    public static LocationGoal DriveCoreDetectingBlueprintDataGoal { get; private set; }

    // (THIS IS A CHECKLIST GOAL)
    public static StoryGoal UnlockPlagueGrapplerGoal { get; private set; }
    public static StoryGoal UnlockPlagueGrapplerVoiceLine { get; private set; }

    // Lost river cyclops wreck

    public static CompoundGoal LostRiverCyclopsWreckRadio { get; private set; }

    public static LocationGoal LostRiverCyclopsWreckNearby { get; private set; }

    // (THIS IS A CHECKLIST GOAL)
    public static StoryGoal UnlockPlagueArmorGoal { get; private set; }

    // Flesh cave
    public static LocationGoal FleshCaveEntranceGoal { get; private set; }
    public static CompoundGoal FleshCaveLocalScanGoal { get; private set; }
    public static LocationGoal FleshCaveGlitchGoal { get; private set; }
    public static LocationGoal FleshCaveConnectionLostGoal { get; private set; }
    public static LocationGoal ShrineSosGoal { get; private set; }

    // General
    public static StoryGoal PlagueNeutralizerFirstUse { get; private set; }
    public static StoryGoal PlagueAltarFirstUse { get; private set; }

    // Shrine base

    public const string ShrineBaseForceFieldStoryGoal = "ShrineBaseForceField";
    public const string BennetCacheForceFieldStoryGoal = "B3NTCacheForceField";

    // Bennet
    public static LocationGoal ShrineBaseEntryGoal { get; private set; }
    public static CompoundGoal FakePdaGoalAfterMeetingBennet { get; private set; }
    public static CompoundGoal BennetInitialMeeting { get; private set; }
    public static CompoundGoal AfterBennetInitialMeeting { get; private set; }
    public static CompoundGoal BennetTabletInstructions { get; private set; }
    public static StoryGoal BennetApproach { get; private set; }
    public static CompoundGoal BennetIntroduction { get; private set; }
    public static CompoundGoal BennetAcceptingCatalyst { get; private set; }
    public static StoryGoal BennetReceivedCatalyst { get; private set; }
    public static CompoundGoal BennetCatalystResponse { get; private set; }
    public static CompoundGoal BennetGiveInfectionTracker { get; private set; }
    public static CompoundGoal BennetDermanInteraction { get; private set; }
    public static CompoundGoal BennetAngelinaInteraction { get; private set; }
    public static CompoundGoal BennetAddPlagueHeartInstructions { get; private set; }
    public static ItemGoal BennetPlagueHeartExplanation { get; private set; }
    public static StoryGoal BennetPlagueHeartReaction { get; private set; }
    public static StoryGoal BennetPostAct2SanctuaryVisit { get; private set; }

    // Post flesh cave
    public static StoryGoal WaitForLeavingPlagueCave { get; private set; }
    public static StoryGoal LeavePlagueCaveGoal { get; private set; }
    public static CompoundGoal UnlockPdaExploder { get; private set; }
    public static StoryGoal ExplodePda { get; private set; }

    // Act 2 ending cinematic
    public static StoryGoal BennetExplodePdaEvent { get; private set; }
    public static StoryGoal SatelliteCommunicatorSuccess { get; private set; }
    public static StoryGoal ObeyBennetEvent { get; private set; }
    public static StoryGoal ObeyBennetFreedomLine { get; private set; }
    public static StoryGoal ObeyBennetLines { get; private set; }
    public static StoryGoal DisobeyBennetEvent { get; private set; }
    public static StoryGoal DisobeyBennetLines { get; private set; }
    public static StoryGoal DisobeyBennetLines2 { get; private set; }
    public static StoryGoal Act2EndingEvent { get; private set; }
    public static CompoundGoal AlterraSatelliteReaction { get; private set; }
    public static StoryGoal TeleportToSpaceEvent { get; private set; }
    public static StoryGoal MeteorExplodeGoal { get; private set; }
    public static StoryGoal MeteorEggHatchGoal { get; private set; }
    public static StoryGoal PlagueHeartForceFieldDisableGoal { get; private set; }
    public static StoryGoal OpenPlagueHeartHatchGoal { get; private set; }
    public static CompoundGoal SpawnRoamingChaosGoal { get; private set; }
    public static CompoundGoal DermanEnd { get; private set; }
    public static CompoundGoal YouAreNextRadioSignal { get; private set; }
    public static CompoundGoal BennetChaosApproachPrecondition { get; private set; }
    public static StoryGoal BennetChaosApproach { get; private set; }

    private static void RegisterAct2()
    {
        RegisterAct2Part1();
        RegisterAct2Part2();
        RegisterAct2GeneralGoals();
    }

    private static void RegisterAct2Part1()
    {
        // Introduction
        ArriveToDomeGoal = StoryGoalHandler.RegisterCompoundGoal("ArriveToDome", Story.GoalType.PDA, 200,
            LeaveDomeArgument.key);
        RegisterVoiceLog(ArriveToDomeGoal.key, "ArriveToDome", VoiceLogIconPda);

        DermanFirstContact = StoryGoalHandler.RegisterCompoundGoal("Derman_FirstContact", Story.GoalType.PDA, 200,
            ArriveToDomeGoal.key);
        RegisterVoiceLog(DermanFirstContact.key, "Derman_FirstContact", VoiceLogIconDerman);

        Act2MissionInstructionsRadio = StoryGoalHandler.RegisterCompoundGoal("Act2MissionInstructions",
            Story.GoalType.Radio,
            10, ArriveToDomeGoal.key);
        RegisterVoiceLog(Act2MissionInstructionsRadio.key, "Act2MissionInstructions", VoiceLogIconAlterra);

        StoryGoalHandler.RegisterOnGoalUnlockData(Act2MissionInstructionsRadio.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = ShuttlePad.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        DermanFirstContact2 = StoryGoalHandler.RegisterCompoundGoal("Derman_FirstContact2", Story.GoalType.PDA, 500,
            "OnPlay" + Act2MissionInstructionsRadio.key);
        RegisterVoiceLog(DermanFirstContact2.key, "Derman_FirstContact2", VoiceLogIconDerman);

        // -- Main objectives --

        OfferingToThePlagueMission = new StoryGoal("OfferingToThePlagueMission", Story.GoalType.Story, 0);
        MrTeethMission = new StoryGoal("MrTeethMission", Story.GoalType.Story, 0);
        JohnKyleMission = new StoryGoal("JohnKyleMission", Story.GoalType.Story, 0);
        PlagueResearchMission = new StoryGoal("PlagueResearchMission", Story.GoalType.Story, 0);

        var mainMissionGoals = GetAct2ChecklistMissionGoals();
        foreach (var mainMissionGoal in mainMissionGoals)
        {
            StoryGoalHandler.RegisterCustomEvent(mainMissionGoal.key, ProcessAct2FollowUpMessages);
        }

        Act2MissionsFollowUp1 = new StoryGoal("Act2MissionsFollowUp1", Story.GoalType.PDA, 5);
        RegisterVoiceLog(Act2MissionsFollowUp1.key, "Act2MissionsCompleted_1", VoiceLogIconPda);

        Act2MissionsFollowUp1Derman = StoryGoalHandler.RegisterCompoundGoal("Act2MissionsFollowUp1Derman",
            Story.GoalType.PDA, 300, Act2MissionsFollowUp1.key);
        RegisterVoiceLog(Act2MissionsFollowUp1Derman.key, "Derman_FollowUpMessage1", VoiceLogIconDerman);

        Act2MissionsFollowUp2 = new StoryGoal("Act2MissionsFollowUp2", Story.GoalType.PDA, 5);
        RegisterVoiceLog(Act2MissionsFollowUp2.key, "Act2MissionsCompleted_2", VoiceLogIconPda);

        Act2MissionsFollowUp2Derman = StoryGoalHandler.RegisterCompoundGoal("Act2MissionsFollowUp2Derman",
            Story.GoalType.PDA, 300, Act2MissionsFollowUp2.key);
        RegisterVoiceLog(Act2MissionsFollowUp2Derman.key, "Derman_FollowUpMessage2", VoiceLogIconDerman);

        Act2MissionsFollowUp2DermanTech = StoryGoalHandler.RegisterCompoundGoal("Act2MissionsFollowUp2DermanTech",
            Story.GoalType.PDA, 60 * 9, Act2MissionsFollowUp2Derman.key);
        RegisterVoiceLog(Act2MissionsFollowUp2DermanTech.key, "Derman_FollowUpMessage2_Unlock", VoiceLogIconDerman);

        Act2MissionsFollowUp2DermanTechUnlock = StoryGoalHandler.RegisterCompoundGoal(
            "Act2MissionsFollowUp2DermanTechUnlock", Story.GoalType.Story, 25, Act2MissionsFollowUp2DermanTech.key);
        StoryGoalHandler.RegisterOnGoalUnlockData(Act2MissionsFollowUp2DermanTechUnlock.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = PlagueRefinery.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        Act2MissionsFollowUp3 = new StoryGoal("Act2MissionsFollowUp3", Story.GoalType.PDA, 30);
        RegisterVoiceLog(Act2MissionsFollowUp3.key, "Act2MissionsCompleted_3", VoiceLogIconPda);

        Act2MissionsFollowUp3Alterra = StoryGoalHandler.RegisterCompoundGoal("Act2MissionsFollowUp3Alterra",
            Story.GoalType.PDA, 65, Act2MissionsFollowUp3.key);
        RegisterVoiceLog(Act2MissionsFollowUp3Alterra.key, "Act2FollowUpMessage3", VoiceLogIconAlterra);

        Act2MissionsFollowUp3Joke = StoryGoalHandler.RegisterCompoundGoal("Act2MissionsFollowUp3Joke",
            Story.GoalType.PDA, 60, Act2MissionsFollowUp3.key);
        RegisterVoiceLog(Act2MissionsFollowUp3Joke.key, "EmployeeOfTheMinuteNoMore", VoiceLogIconPda);

        Act2MissionsFollowUp4 = new StoryGoal("Act2MissionsFollowUp4", Story.GoalType.PDA, 10);
        RegisterVoiceLog(Act2MissionsFollowUp4.key, "Act2FollowUpMessageFinal", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterCustomEvent(Act2MissionsFollowUp4.key, OnAct2ChecklistCompletion);

        UnlockPlagueCaveSignalGoal = StoryGoalHandler.RegisterCompoundGoal("UnlockPlagueCaveSignal",
            Story.GoalType.Story, 32, Act2MissionsFollowUp4.key);
        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockPlagueCaveSignalGoal.key, signals: new[]
        {
            new UnlockSignalData
            {
                targetPosition = new Vector3(-1387, -335, -135),
                targetDescription = "PlagueCaveSignalDescription"
            }
        });

        SpawnPrecursorSatelliteGoal = StoryGoalHandler.RegisterCompoundGoal("SpawnPrecursorSatellite",
            Story.GoalType.Story, 0, UnlockPlagueCaveSignalGoal.key);
        StoryGoalHandler.RegisterCustomEvent(SpawnPrecursorSatelliteGoal.key,
            () => UWE.CoroutineHost.StartCoroutine(SpawnPrecursorSatelliteCoroutine()));

        PlagueCaveEntranceHint = new StoryGoal("PlagueCaveEntranceHint", Story.GoalType.PDA, 1f);
        RegisterVoiceLog(PlagueCaveEntranceHint.key, "OverPlagueCaveEntrance", VoiceLogIconAlterra);

        OpenPlagueCave = new StoryGoal("OpenPlagueCave", Story.GoalType.Story, 0);

        // Item collection

        PlagueCatalystItemGoal = StoryGoalHandler.RegisterItemGoal("ItemGoal_PlagueCatalyst", Story.GoalType.Story,
            PlagueCatalyst.Info.TechType);

        SendManifestationsShuttleReminder = StoryGoalHandler.RegisterCompoundGoal("SendManifestationsShuttleReminder",
            Story.GoalType.PDA, 60, PlagueCatalystItemGoal.key, SendPlagueSampleViaShuttleEvent.key);
        RegisterVoiceLog(SendManifestationsShuttleReminder.key, "ItemGoal_PlagueCatalyst", VoiceLogIconAlterra);

        MysteriousRemainsItemGoal = StoryGoalHandler.RegisterItemGoal("ItemGoal_MysteriousRemains", Story.GoalType.PDA,
            MysteriousRemains.Info.TechType, 3);
        RegisterVoiceLog(MysteriousRemainsItemGoal.key, "ItemGoal_MysteriousRemains", VoiceLogIconPda);

        DormantNeuralMatterItemGoal = StoryGoalHandler.RegisterItemGoal("ItemGoal_NeuralPlagueMatter",
            Story.GoalType.PDA, DormantNeuralMatter.Info.TechType, 2);
        RegisterVoiceLog(DormantNeuralMatterItemGoal.key, "ItemGoal_NeuralPlagueMatter", VoiceLogIconPda);

        NeutralizedPlagueIngotItemGoal = StoryGoalHandler.RegisterItemGoal("ItemGoal_NeutralizedPlagueMatter",
            Story.GoalType.PDA, PlagueIngot.Info.TechType, 2);
        RegisterVoiceLog(NeutralizedPlagueIngotItemGoal.key, "ItemGoal_NeutralizedPlagueMatter", VoiceLogIconPda);
        StoryGoalHandler.RegisterCustomEvent(NeutralizedPlagueIngotItemGoal.key,
            () => TrpEventMusicPlayer.PlayMusic(AudioUtils.GetFmodAsset("BioweaponShort"), 59, false));

        AmalgamatedBoneItemGoal = StoryGoalHandler.RegisterItemGoal("ItemGoal_AmalgamatedBone",
            Story.GoalType.PDA, ModPrefabs.AmalgamatedBone.TechType, 4);
        RegisterVoiceLog(AmalgamatedBoneItemGoal.key, "ItemGoal_AmalgamatedBone", VoiceLogIconPda);

        WarperHeartItemGoal = StoryGoalHandler.RegisterItemGoal("ItemGoal_WarperHeart",
            Story.GoalType.PDA, ModPrefabs.WarperHeart.TechType, 12);
        RegisterVoiceLog(WarperHeartItemGoal.key, "ItemGoal_WarperHeart", VoiceLogIconAlterra);

        // Shuttle delivery

        // PLAGUE KNIFE
        UnlockFollowUpPlagueKnife = StoryGoalHandler.RegisterCompoundGoal("UnlockFollowUp_PlagueKnife",
            Story.GoalType.PDA, 60 * 5,
            GetStoryGoalKeyForShuttleDelivery(ModPrefabs.AmalgamatedBone.ClassID));
        RegisterVoiceLog(UnlockFollowUpPlagueKnife.key, "UnlockFollowUp_PlagueKnife", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockFollowUpPlagueKnife.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = PlagueKnife.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        // GILBERT
        UnlockFollowUpGilbert = StoryGoalHandler.RegisterCompoundGoal("UnlockFollowUp_Gilbert",
            Story.GoalType.PDA, 60 * 3,
            Act2MissionInstructionsRadio.key,
            GetStoryGoalKeyForShuttleDelivery(PlagueIngot.Info.ClassID),
            GetStoryGoalKeyForShuttleDelivery(ModPrefabs.WarperHeart.ClassID),
            GetStoryGoalKeyForShuttleDelivery(MysteriousRemains.Info.ClassID),
            GetStoryGoalKeyForShuttleDelivery(ModPrefabs.AmalgamatedBone.ClassID),
            GetStoryGoalKeyForShuttleDelivery(PlagueCatalyst.Info.ClassID),
            GetStoryGoalKeyForShuttleDelivery(DormantNeuralMatter.Info.ClassID));
        RegisterVoiceLog(UnlockFollowUpGilbert.key, "UnlockFollowUp_Gilbert", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockFollowUpGilbert.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = Gilbert.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        // NEURAL MATTER
        UnlockFollowUpNeuralMatter = StoryGoalHandler.RegisterCompoundGoal("UnlockFollowUp_NeuralMatter",
            Story.GoalType.PDA, 60 * 5,
            Act2MissionInstructionsRadio.key,
            GetStoryGoalKeyForShuttleDelivery(DormantNeuralMatter.Info.ClassID));
        RegisterVoiceLog(UnlockFollowUpNeuralMatter.key, "UnlockFollowUp_NeuralMatter", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockFollowUpNeuralMatter.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = ConsciousNeuralMatter.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        // BONE REGURGITATOR
        UnlockFollowUpBoneRegurgitator = StoryGoalHandler.RegisterCompoundGoal("UnlockFollowUp_BoneRegurgitator",
            Story.GoalType.PDA, 60 * 10,
            Act2MissionInstructionsRadio.key,
            GetStoryGoalKeyForShuttleDelivery(PlagueIngot.Info.ClassID),
            GetStoryGoalKeyForShuttleDelivery(DormantNeuralMatter.Info.ClassID));
        RegisterVoiceLog(UnlockFollowUpBoneRegurgitator.key, "UnlockFollowUp_BoneRegurgitator", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockFollowUpBoneRegurgitator.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = BoneCannonPrefab.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        // Maze base

        HoverPetReachedMazeBaseGoal = new StoryGoal("HoverPetReachedMazeBase", Story.GoalType.Story, 0);

        MazeBaseDiscoveryGoal = StoryGoalHandler.RegisterLocationGoal("MazeBaseDiscoveryGoal", Story.GoalType.PDA,
            new Vector3(-1251, -228, 708), 200, 1);
        StoryGoalHandler.RegisterOnGoalUnlockData(MazeBaseDiscoveryGoal.key, signals: new UnlockSignalData[]
        {
            new()
            {
                targetPosition = new Vector3(-1255.3f, -224, 698),
                targetDescription = "MazeBaseDiscoveryGoalSignal",
            }
        });
        RegisterVoiceLog(MazeBaseDiscoveryGoal.key, "LocationGoal_MazeBase", VoiceLogIconAlterra);

        for (var i = 1; i <= 4; i++)
        {
            PDAHandler.AddEncyclopediaEntry($"WestLog{i}", CustomPdaPaths.RedPlagueVictimsPath, null, null, null,
                null, PDAHandler.UnlockBasic,
                RegisterDatabankEntryVoiceLog($"WestLog{i}", $"WestLog{i}"));
        }

        UnlockPlagueAltarPda = new StoryGoal("PDAUnlockPlagueAltar", Story.GoalType.PDA, 0);
        RegisterVoiceLog(UnlockPlagueAltarPda.key, "PDAUnlockPlagueAltar", VoiceLogIconPda);

        UnlockPlagueAltarAlterra = StoryGoalHandler.RegisterCompoundGoal("PlagueAltarMissionFollowUp_Alterra",
            Story.GoalType.PDA, 45, UnlockPlagueAltarPda.key);
        RegisterVoiceLog(UnlockPlagueAltarAlterra.key, "PlagueAltarMissionFollowUp_Alterra", VoiceLogIconAlterra);

        ScanPlagueAltarGoal = new StoryGoal("ScanPlagueAltar", Story.GoalType.Story, 0);
        StoryGoalHandler.RegisterCustomEvent(ScanPlagueAltarGoal.key, () =>
        {
            if (!InCreativeMode())
                UnlockPlagueAltarPda.Trigger();
        });
        PlagueAltar.RegisterLateStoryData();

        // Aurora

        CassyEncounter1 = StoryGoalHandler.RegisterLocationGoal("CassyEncounter_Aurora", Story.GoalType.PDA,
            new Vector3(1053, 30, 90), 50, 0.1f, 10);
        RegisterVoiceLog(CassyEncounter1.key, "CassyEncounter_Aurora", VoiceLogIconAlterra);

        CassyEncounter2MainTrigger = StoryGoalHandler.RegisterLocationGoal("CassyEncounter2MainTrigger",
            Story.GoalType.Story, new Vector3(977, 24, 50), 25, 0.1f);
        CassyEncounter2AltEntranceTrigger = StoryGoalHandler.RegisterLocationGoal("CassyEncounter2AltEntranceTrigger",
            Story.GoalType.Story, new Vector3(1040, 6, 45), 20, 0.2f);
        CassyEncounter2 = new StoryGoal("CassyEncounter_InsideAurora", Story.GoalType.PDA, 3);
        RegisterVoiceLog(CassyEncounter2.key, "CassyEncounter_InsideAurora", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterCustomEvent(CassyEncounter2MainTrigger.key, () => CassyEncounter2.Trigger());
        StoryGoalHandler.RegisterCustomEvent(CassyEncounter2AltEntranceTrigger.key, () => CassyEncounter2.Trigger());

        DriveCoreHallwayGoal = StoryGoalHandler.RegisterLocationGoal("LocationGoal_TrpDriveCoreHallway",
            Story.GoalType.PDA,
            new Vector3(915, 0, -7), 20, 0f);
        RegisterVoiceLog(DriveCoreHallwayGoal.key, "LocationGoal_DriveCoreHallway", VoiceLogIconAlterra);

        DriveCoreDetectingBlueprintDataGoal = StoryGoalHandler.RegisterLocationGoal(
            "PDADetectingDriveCoreBlueprintData", Story.GoalType.PDA, new Vector3(863, -20.5f, 5), 21, 0.5f, 2f);
        RegisterVoiceLog(DriveCoreDetectingBlueprintDataGoal.key, "PDADetectingDriveCoreBlueprintData",
            VoiceLogIconPda);

        PDAHandler.AddEncyclopediaEntry("DriveCoreWarningLog", CustomPdaPaths.RedPlagueVictimsPath, null, null, null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("DriveCoreWarningLog", "MrTeethWarningLog"));

        UnlockPlagueGrapplerGoal = new StoryGoal("UnlockGoal_PlagueGrapplerInitial", Story.GoalType.Story, 0);
        StoryGoalHandler.RegisterCustomEvent(UnlockPlagueGrapplerGoal.key, PlagueGrapplerUnlockEvent);

        UnlockPlagueGrapplerVoiceLine = new StoryGoal("UnlockGoal_PlagueGrappler", Story.GoalType.PDA, 5);
        RegisterVoiceLog(UnlockPlagueGrapplerVoiceLine.key, "UnlockGoal_PlagueGrappler", VoiceLogIconAlterra);

        PlagueGrappler.RegisterLateStoryData();

        // Lost river cyclops

        LostRiverCyclopsWreckRadio = StoryGoalHandler.RegisterCompoundGoal("LostRiverCyclopsBroadcast",
            Story.GoalType.Radio, 60, "AuroraRadiationFixed");
        RegisterVoiceLog(LostRiverCyclopsWreckRadio.key, "LostRiverCyclopsBroadcast", VoiceLogIconAlterra);

        PDAHandler.AddEncyclopediaEntry("PlagueArmorCommentary", CustomPdaPaths.RedPlagueVictimsPath, null, null, null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("PlagueArmorCommentary", "Databank_PlagueArmor"));

        LostRiverCyclopsWreckNearby = StoryGoalHandler.RegisterLocationGoal("LocationGoal_PlagueCyclops",
            Story.GoalType.PDA, new Vector3(-173, -793, 335), 150, 1f);
        RegisterVoiceLog(LostRiverCyclopsWreckNearby.key, "LocationGoal_PlagueCyclops", VoiceLogIconPda);
        StoryGoalHandler.RegisterCustomEvent(LostRiverCyclopsWreckNearby.key, PlayPlagueCyclopsOst);

        UnlockPlagueArmorGoal = new StoryGoal("UnlockPlagueArmor", Story.GoalType.Story, 0);
        BoneArmor.RegisterLateStoryData();
    }

    private static void RegisterAct2Part2()
    {
        RegisterFleshCaveGoals();
        RegisterInitialBennetGoals();
        RegisterPostFleshCaveGoals();
        RegisterAct2EndingGoals();
    }

    private static void RegisterFleshCaveGoals()
    {
        FleshCaveEntranceGoal = StoryGoalHandler.RegisterLocationGoal("FleshCaveEntranceGoal", Story.GoalType.PDA,
            new Vector3(-1386, -385, -130), 28, 1f, 5f);
        RegisterVoiceLog(FleshCaveEntranceGoal.key, "BiomeGoal_PlagueCaveEntrance", VoiceLogIconDerman);

        FleshCaveLocalScanGoal = StoryGoalHandler.RegisterCompoundGoal("FleshCaveLocalScanGoal",
            Story.GoalType.Encyclopedia, 14, FleshCaveEntranceGoal.key);
        PDAHandler.AddEncyclopediaEntry(FleshCaveLocalScanGoal.key, CustomPdaPaths.RedPlagueEnvironmentalData, null,
            null, null, null, PDAHandler.UnlockBasic);

        FleshCaveGlitchGoal = StoryGoalHandler.RegisterLocationGoal("FleshCaveGlitch", Story.GoalType.PDA,
            new Vector3(-1355, -445, -20), 100, 1, 16);
        RegisterVoiceLog(FleshCaveGlitchGoal.key, "PDAFleshCaveGlitch", VoiceLogIconPda);

        FleshCaveConnectionLostGoal = StoryGoalHandler.RegisterLocationGoal("FleshCaveConnectionLost",
            Story.GoalType.PDA, new Vector3(-1510, -787, 281), 100, 0.3f, 0.3f);
        RegisterVoiceLog(FleshCaveConnectionLostGoal.key, "Derman_WhereTheShrimp", VoiceLogIconDerman);

        ShrineSosGoal =
            StoryGoalHandler.RegisterLocationGoal("ShrineSOS", Story.GoalType.PDA, new Vector3(-1510, -830, 446), 100,
                2, 6);
        RegisterVoiceLog(ShrineSosGoal.key, "ShrineSOS", VoiceLogIconUnknown);
    }

    private static void RegisterInitialBennetGoals()
    {
        ShrineBaseEntryGoal = StoryGoalHandler.RegisterLocationGoal("B3NT_ShrineBaseEntry", Story.GoalType.PDA,
            new Vector3(-1515, -882, 864), 20, 0);
        RegisterVoiceLog(ShrineBaseEntryGoal.key, "1. Hallway Lines (with bass)", VoiceLogIconUnknown);

        FakePdaGoalAfterMeetingBennet = StoryGoalHandler.RegisterCompoundGoal("FakeBennetPDAMission",
            Story.GoalType.PDA, 60, ShrineBaseEntryGoal.key);
        RegisterVoiceLog(FakePdaGoalAfterMeetingBennet.key, "PDABennetFakeMission", VoiceLogIconPda);

        BennetInitialMeeting = StoryGoalHandler.RegisterCompoundGoal("B3NT_InitialMeeting", Story.GoalType.PDA,
            60 * 2.5f,
            ShrineBaseEntryGoal.key);
        RegisterVoiceLog(BennetInitialMeeting.key, "2. Initial Meeting", VoiceLogIconUnknown);

        AfterBennetInitialMeeting = StoryGoalHandler.RegisterCompoundGoal("B3NT_AfterInitialMeeting",
            Story.GoalType.Story, 30f, BennetInitialMeeting.key);
        StoryGoalHandler.RegisterCustomEvent(AfterBennetInitialMeeting.key, OnBennetInitialMeetingFollowUp);

        BennetTabletInstructions = StoryGoalHandler.RegisterCompoundGoal("B3NT_TabletInstructions", Story.GoalType.PDA,
            0, AfterBennetInitialMeeting.key);
        RegisterVoiceLog(BennetTabletInstructions.key, "2a. Door Unopened", VoiceLogIconUnknown);
        StoryGoalHandler.RegisterOnGoalUnlockData(BennetTabletInstructions.key, signals: new[]
        {
            new UnlockSignalData
            {
                targetPosition = new Vector3(-1361, -860, 650),
                targetDescription = "B3NTCacheSignalDescription",
            }
        });

        BennetApproach = new StoryGoal("B3NT_Approach", Story.GoalType.Story, 0);

        BennetIntroduction =
            StoryGoalHandler.RegisterCompoundGoal("B3NT_Introduction", Story.GoalType.PDA, 14, BennetApproach.key);
        RegisterVoiceLog(BennetIntroduction.key, "3. Introduction", VoiceLogIconB3NT);

        BennetAcceptingCatalyst = StoryGoalHandler.RegisterCompoundGoal("B3NT_AcceptingCatalyst", Story.GoalType.Story,
            76, BennetIntroduction.key);

        BennetReceivedCatalyst = new StoryGoal("B3NT_ReceivedCatalyst", Story.GoalType.Story, 0);

        BennetCatalystResponse = StoryGoalHandler.RegisterCompoundGoal("B3NT_CatalystResponse", Story.GoalType.PDA,
            17, BennetReceivedCatalyst.key);
        RegisterVoiceLog(BennetCatalystResponse.key, "4. After Placing The Plague Catalyst", VoiceLogIconB3NT);

        BennetGiveInfectionTracker = StoryGoalHandler.RegisterCompoundGoal("B3NT_InfectionTrackerUnlock",
            Story.GoalType.PDA,
            38.2f, BennetCatalystResponse.key);
        RegisterVoiceLog(BennetGiveInfectionTracker.key, "PDAUnlockInfectionTracker", VoiceLogIconPda);
        StoryGoalHandler.RegisterOnGoalUnlockData(BennetGiveInfectionTracker.key, blueprints: new[]
        {
            new UnlockBlueprintData
            {
                techType = InfectionTracker.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });
    }

    private static void RegisterPostFleshCaveGoals()
    {
        WaitForLeavingPlagueCave = StoryGoalHandler.RegisterCompoundGoal("WaitForLeavingPlagueCave",
            Story.GoalType.Story, 0, BennetGiveInfectionTracker.key);
        StoryGoalHandler.RegisterCustomEvent(WaitForLeavingPlagueCave.key, WaitForLeavingPlagueCaveEvent);

        LeavePlagueCaveGoal = new StoryGoal("LeavePlagueCave", Story.GoalType.PDA, 0);
        RegisterVoiceLog(LeavePlagueCaveGoal.key, "OnLeaveFleshCave", VoiceLogIconAlterra);

        BennetPlagueHeartExplanation = StoryGoalHandler.RegisterItemGoal("B3NT_PlagueHeartExplanation",
            Story.GoalType.PDA, InfectionTracker.Info.TechType, 20);
        RegisterVoiceLog(BennetPlagueHeartExplanation.key, "6. Plague Heart Explanation", VoiceLogIconB3NT);

        BennetPlagueHeartReaction = new StoryGoal("B3NT_PlagueHeartReaction", Story.GoalType.PDA, 1);
        RegisterVoiceLog(BennetPlagueHeartReaction.key, "8. Approaching the Plague Heart Music", VoiceLogIconB3NT);
        StoryGoalHandler.RegisterCustomEvent(BennetPlagueHeartReaction.key, PlayPlagueHeartDiscoveryOst);

        BennetDermanInteraction = StoryGoalHandler.RegisterCompoundGoal("B3NT_DermanInteraction", Story.GoalType.PDA,
            60, BennetPlagueHeartReaction.key);
        RegisterVoiceLog(BennetDermanInteraction.key, "5. Derman and Bennet", VoiceLogIconAlterra);

        UnlockPdaExploder = StoryGoalHandler.RegisterCompoundGoal("UnlockPdaExploder", Story.GoalType.Story, 42,
            BennetDermanInteraction.key);
        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockPdaExploder.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = PdaExploder.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        ExplodePda = new StoryGoal("TrpExplodePda", Story.GoalType.Story, 0);

        BennetAddPlagueHeartInstructions = StoryGoalHandler.RegisterCompoundGoal("InfectedArchitectInstructions",
            Story.GoalType.Encyclopedia, 74, BennetDermanInteraction.key);
        PDAHandler.AddEncyclopediaEntry("InfectedArchitectInstructions", CustomPdaPaths.RedPlagueResearchPath, null,
            null);
        StoryGoalHandler.RegisterOnGoalUnlockData(BennetAddPlagueHeartInstructions.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = SatelliteCommunicationDevice.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });
        StoryGoalHandler.RegisterCustomEvent(BennetAddPlagueHeartInstructions.key, StartPlagueHeartDestructionTimer);

        BennetAngelinaInteraction = StoryGoalHandler.RegisterCompoundGoal("B3NT_AngelinaInteraction",
            Story.GoalType.PDA,
            120, BennetAddPlagueHeartInstructions.key);
        RegisterVoiceLog(BennetAngelinaInteraction.key, "7. Angelina & Bennet", VoiceLogIconAlterra);
    }

    private static void RegisterAct2EndingGoals()
    {
        BennetExplodePdaEvent = new StoryGoal("BennetExplodePda", Story.GoalType.PDA, 0);
        RegisterVoiceLog(BennetExplodePdaEvent.key, "(optional) Bennet explode", VoiceLogIconB3NT);
        StoryGoalHandler.RegisterCustomEvent(BennetExplodePdaEvent.key, DestroyPlagueHeartTimer.SpeedUpTimer);

        SatelliteCommunicatorSuccess = new StoryGoal("SatelliteCommunicatorSuccess", Story.GoalType.PDA, 0);
        RegisterVoiceLog(SatelliteCommunicatorSuccess.key, "SatelliteCommunicatorSuccess", VoiceLogIconUnknown);

        ObeyBennetEvent = new StoryGoal("ObeyBennet", Story.GoalType.Story, 0);
        StoryGoalHandler.RegisterCustomEvent(ObeyBennetEvent.key, OnBennetObey);

        ObeyBennetFreedomLine =
            StoryGoalHandler.RegisterCompoundGoal("ObeyBennetFreedomLine", Story.GoalType.PDA, 20, ObeyBennetEvent.key);
        RegisterVoiceLog(ObeyBennetFreedomLine.key, "BennetObeyCinematicFreedom", VoiceLogIconB3NT);

        ObeyBennetLines =
            StoryGoalHandler.RegisterCompoundGoal("ObeyBennetLine", Story.GoalType.PDA, 63, ObeyBennetEvent.key);
        RegisterVoiceLog(ObeyBennetLines.key, "10. Obey Bennet", VoiceLogIconB3NT);

        DisobeyBennetEvent = new StoryGoal("DisobeyBennet", Story.GoalType.Story, 0);
        StoryGoalHandler.RegisterCustomEvent(DisobeyBennetEvent.key, OnBennetDisobey);

        DisobeyBennetLines =
            StoryGoalHandler.RegisterCompoundGoal("DisobeyBennetLine1", Story.GoalType.PDA, 19, DisobeyBennetEvent.key);
        RegisterVoiceLog(DisobeyBennetLines.key, "9. Disobey Bennet Part 1", VoiceLogIconB3NT);

        DisobeyBennetLines2 =
            StoryGoalHandler.RegisterCompoundGoal("DisobeyBennetLine2", Story.GoalType.PDA, 53, DisobeyBennetLines.key);
        RegisterVoiceLog(DisobeyBennetLines2.key, "9. Disobey Bennet Part 2", VoiceLogIconB3NT);

        Act2EndingEvent = new StoryGoal("Act2EndingEvent", Story.GoalType.Story, 0);
        StoryGoalHandler.RegisterCustomEvent(Act2EndingEvent.key, LockSatelliteCommunicator);

        AlterraSatelliteReaction =
            StoryGoalHandler.RegisterCompoundGoal("AlterraSatelliteReaction", Story.GoalType.PDA, 3,
                Act2EndingEvent.key);
        RegisterVoiceLog(AlterraSatelliteReaction.key, "FredSatelliteReaction", VoiceLogIconAlterra);

        TeleportToSpaceEvent = new StoryGoal("TeleportToSpaceEvent", Story.GoalType.Story, 0);

        MeteorExplodeGoal = new StoryGoal("MeteorExplode", Story.GoalType.Story, 0);
        PlagueHeartForceFieldDisableGoal = new StoryGoal("PlagueHeartForceFieldDisable", Story.GoalType.Story, 0);
        OpenPlagueHeartHatchGoal = new StoryGoal("OpenPlagueHeartHatchGoal", Story.GoalType.Story, 0);
        MeteorEggHatchGoal = new StoryGoal("MeteorEggHatch", Story.GoalType.Story, 0);

        HiveMindReleasedGoal = new StoryGoal("HiveMindReleased", Story.GoalType.PDA, 0);
        RegisterVoiceLog(HiveMindReleasedGoal.key, "Act2SendOff", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterCustomEvent(HiveMindReleasedGoal.key, OnHiveMindRelease);
        
        SpawnRoamingChaosGoal = StoryGoalHandler.RegisterCompoundGoal("SpawnRoamingChaos", Story.GoalType.Story, 0, HiveMindReleasedGoal.key);

        DermanEnd = StoryGoalHandler.RegisterCompoundGoal("Derman_End", Story.GoalType.PDA, 80,
            HiveMindReleasedGoal.key);
        RegisterVoiceLog(DermanEnd.key, "Derman_End", VoiceLogIconDerman);

        BennetChaosApproachPrecondition = StoryGoalHandler.RegisterCompoundGoal("BennetChaosApproachPrecondition",
            Story.GoalType.Story, 60, DermanEnd.key);

        PDAHandler.AddEncyclopediaEntry("PlagueHeartRetrieval1", CustomPdaPaths.RedPlagueResearchPath, null, null,
            voiceLog: RegisterDatabankEntryVoiceLog("PlagueHeartRetrieval1", "MeteorSiteLog1"));
        PDAHandler.AddEncyclopediaEntry("PlagueHeartRetrieval2", CustomPdaPaths.RedPlagueResearchPath, null, null,
            voiceLog: RegisterDatabankEntryVoiceLog("PlagueHeartRetrieval2", "MeteorSiteLog2"));
        PDAHandler.AddEncyclopediaEntry("PlagueHeartRetrieval3", CustomPdaPaths.RedPlagueResearchPath, null, null,
            voiceLog: RegisterDatabankEntryVoiceLog("PlagueHeartRetrieval3", "MeteorSiteLog3"));
        PDAHandler.AddEncyclopediaEntry("PlagueHeartRetrieval4", CustomPdaPaths.RedPlagueResearchPath, null, null,
            voiceLog: RegisterDatabankEntryVoiceLog("PlagueHeartRetrieval4", "MeteorSiteLog4"));

        BennetPostAct2SanctuaryVisit = new StoryGoal("BennetPostAct2SanctuaryVisit", Story.GoalType.PDA, 3);
        RegisterVoiceLog(BennetPostAct2SanctuaryVisit.key, "bennet enter sanctuary after act", VoiceLogIconB3NT);

        YouAreNextRadioSignal = StoryGoalHandler.RegisterCompoundGoal("YouAreNextRadioSignal", Story.GoalType.Radio,
            333, HiveMindReleasedGoal.key);
        RegisterVoiceLog(YouAreNextRadioSignal.key, "YOU_ARE_NEXT", VoiceLogIconUnknown);

        BennetChaosApproach = new StoryGoal("BennetChaosApproach", Story.GoalType.PDA, 1);
        RegisterVoiceLog(BennetChaosApproach.key, "bennet chaos approach", VoiceLogIconB3NT);
    }

    private static void RegisterAct2GeneralGoals()
    {
        PlagueNeutralizerFirstUse = new StoryGoal("PlagueNeutralizerFirstUse", Story.GoalType.Story, 0);

        PlagueAltarFirstUse = new StoryGoal("PlagueAltarFirstUse", Story.GoalType.Story, 0);
    }

    #region Custom Events

    private static void OnBennetObey()
    {
        PlagueHeartDestructionEvent.StartEvent();
    }

    private static void OnBennetDisobey()
    {
        PlagueHeartDestructionEvent.StartEvent();
    }

    private static void StartPlagueHeartDestructionTimer()
    {
        CoroutineHost.StartCoroutine(StartPlagueHeartDestructionTimerCoroutine());
    }

    internal static IEnumerator StartPlagueHeartDestructionTimerCoroutine()
    {
        var task = CraftData.GetPrefabForTechTypeAsync(DestroyPlagueHeartTimerPrefab.Info.TechType);
        yield return task;
        if (task.GetResult() == null)
        {
            ErrorMessage.AddMessage("Critical story error: Plague heart destruction timer prefab is missing.");
        }

        Object.Instantiate(task.GetResult(), new Vector3(666, 666, 666), Quaternion.identity);
    }

    private static void OnHiveMindRelease()
    {
        GlobalRedPlagueProgressTracker.OnProgressAchieved(GlobalRedPlagueProgressTracker.ProgressStatus.HiveMindReleased);
        CoroutineHost.StartCoroutine(OnHiveMindReleaseCoroutine());
    }

    private static IEnumerator OnHiveMindReleaseCoroutine()
    {
        var task = CraftData.GetPrefabForTechTypeAsync(ModPrefabs.ThrusterEventTrigger.TechType);
        yield return task;
        if (task.GetResult() == null)
        {
            ErrorMessage.AddMessage("Thruster event prefab is missing.");
        }

        Object.Instantiate(task.GetResult(), new Vector3(333, 333, 333), Quaternion.identity);
    }

    private static void PlayPlagueCyclopsOst()
    {
        if (!Player.main) return;
        TrpEventMusicPlayer.PlayMusic(AudioUtils.GetFmodAsset("AssimilationSuccessful"), 170, false);
    }

    private static void LockSatelliteCommunicator()
    {
        KnownTech.Remove(SatelliteCommunicationDevice.Info.TechType);
    }

    private static void PlayPlagueHeartDiscoveryOst()
    {
        if (!Player.main) return;
        TrpEventMusicPlayer.PlayMusic(AudioUtils.GetFmodAsset("EndOfTheWorld"), 210, true);
    }

    private static void OnAct2ChecklistCompletion()
    {
        GlobalRedPlagueProgressTracker.OnProgressAchieved(GlobalRedPlagueProgressTracker.ProgressStatus
            .InitialChecklistMissionsCompleted);
        TrpEventMusicPlayer.PlayMusic(AudioUtils.GetFmodAsset("LullabyOfTheDamned"), 220, false);
    }

    private static IEnumerator SpawnPrecursorSatelliteCoroutine()
    {
        var task = CraftData.GetPrefabForTechTypeAsync(PrecursorSatellite.Info.TechType);
        yield return task;
        var satellite = Object.Instantiate(task.GetResult());
        satellite.transform.position = new Vector3(0, -2000, 0);
        satellite.SetActive(true);
    }

    private static void OnBennetInitialMeetingFollowUp()
    {
        if (StoryGoalManager.main == null)
            return;

        if (!StoryGoalManager.main.IsGoalComplete(ShrineBaseForceFieldStoryGoal))
        {
            BennetTabletInstructions.Trigger();
        }
    }

    private static void PlagueGrapplerUnlockEvent()
    {
        if (!InCreativeMode())
        {
            UnlockPlagueGrapplerVoiceLine.Trigger();
        }
    }

    private static void WaitForLeavingPlagueCaveEvent()
    {
        CoroutineHost.StartCoroutine(SpawnLeavePlagueCaveWaiter());
    }

    internal static IEnumerator SpawnLeavePlagueCaveWaiter()
    {
        yield return new WaitUntil(() => !WaitScreen.IsWaiting);
        var task = CraftData.GetPrefabForTechTypeAsync(ModPrefabs.LeaveFleshCaveWaiter.TechType);
        yield return task;
        LargeWorld.main.streamer.cellManager.RegisterGlobalEntity(Object.Instantiate(task.GetResult()));
    }

    #endregion

    private static StoryGoal[] GetAct2ChecklistMissionGoals()
    {
        return new[] { OfferingToThePlagueMission, MrTeethMission, JohnKyleMission, PlagueResearchMission };
    }

    internal static void ProcessAct2FollowUpMessages()
    {
        if (InCreativeMode())
            return;

        var goals = GetAct2ChecklistMissionGoals();
        var completedGoals = 0;
        foreach (var goal in goals)
        {
            if (StoryGoalManager.main.IsGoalComplete(goal.key))
                completedGoals++;
        }

        if (completedGoals >= 1)
            Act2MissionsFollowUp1.Trigger();
        if (completedGoals >= 2)
            Act2MissionsFollowUp2.Trigger();
        if (completedGoals >= 3)
            Act2MissionsFollowUp3.Trigger();
        if (completedGoals >= 4)
            Act2MissionsFollowUp4.Trigger();
    }
}