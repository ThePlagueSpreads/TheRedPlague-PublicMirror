using System.Collections;
using Nautilus.Handlers;
using Story;
using TheRedPlague.Content.Act1.Dome;
using TheRedPlague.Content.Act1.DomeDrones;
using TheRedPlague.Content.Buildables.Shuttle;
using TheRedPlague.Content.Equipment.BiochemicalProtectionModule;
using TheRedPlague.Content.Equipment.InfectionSampler;
using TheRedPlague.Content.Items.Consumable;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Content.TitleScreen;
using TheRedPlague.Content.UI.Sanity;
using UnityEngine;

namespace TheRedPlague.Registration.TrpStory;

public static class Act1Story
{
    private static readonly Vector3 DomeConstructionTriggerLocation = new Vector3(-60, 315, -55);

    public static CompoundGoal RedPlagueIntroduction { get; private set; }
    public static CompoundGoal AngelinaIntroductionRadioEvent { get; private set; }
    public static CompoundGoal BiochemicalProtectionSuitUnlockEvent { get; private set; }
    public static StoryGoal UseBiochemicalProtectionSuitEvent { get; private set; }
    public static CompoundGoal UnlockSuitChargeItemEvent { get; private set; }
    public static CompoundGoal TransfuserMissionEvent { get; private set; }
    public static StoryGoal UnlockTransfuserEvent { get; private set; }
    public static StoryGoal TransfuserSampleTakenEvent { get; private set; }
    public static CompoundGoal AngelinaFirstSampleShuttlePadInstructions { get; private set; }
    public static CompoundGoal UnlockShuttlePadEvent { get; private set; }
    public static CompoundGoal SampleReminderTrigger { get; private set; }
    public static StoryGoal SampleReminderLog { get; private set; }
    public static StoryGoal SendPlagueSampleViaShuttleEvent { get; private set; }
    public static CompoundGoal UnlockDomeDroneEvent { get; private set; }
    public static StoryGoal OnBuildDrones { get; private set; }
    public static StoryGoal DronesReadyForDomeConstruction { get; private set; }
    public static ItemGoal ResearchTeamARadioGoal { get; private set; }
    public static StoryGoal IslandElevatorActivatedGoal { get; private set; }
    public static StoryGoal UseElevatorGoal { get; private set; }
    public static StoryGoal PrepareForDomeConstruction { get; private set; } // Can be skipped with commands
    public static StoryGoal DomeConstructionEvent { get; private set; }
    public static CompoundGoal DomeSpeakEvent { get; private set; }
    public static CompoundGoal DomeConstructionFinishedEvent { get; private set; }
    public static CompoundGoal LeaveDomeArgument { get; private set; }

    internal static void RegisterAct1()
    {
        RedPlagueIntroduction =
            StoryGoalHandler.RegisterCompoundGoal("RedPlagueIntroduction", Story.GoalType.PDA, 60, "PlayerDiving");
        StoryUtils.RegisterVoiceLog("RedPlagueIntroduction", "PDARedPlagueIntroduction",
            EssentialAssets.VoiceLogIconPda);

        AngelinaIntroductionRadioEvent = StoryGoalHandler.RegisterCompoundGoal("AngelinaIntroduction",
            Story.GoalType.Radio,
            0, "PlayerDiving");
        StoryUtils.RegisterVoiceLog("AngelinaIntroduction", "AngelinaIntroduction",
            EssentialAssets.VoiceLogIconAlterra);

        BiochemicalProtectionSuitUnlockEvent = StoryGoalHandler.RegisterCompoundGoal(
            "PDABiochemicalProtectionSuitUnlock", Story.GoalType.PDA,
            50, "OnPlayAngelinaIntroduction");
        StoryUtils.RegisterVoiceLog(BiochemicalProtectionSuitUnlockEvent.key, "PDABiochemicalProtectionSuitUnlock",
            EssentialAssets.VoiceLogIconPda);
        StoryGoalHandler.RegisterOnGoalUnlockData(BiochemicalProtectionSuitUnlockEvent.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = BiochemicalProtectionSuit.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        UseBiochemicalProtectionSuitEvent = new StoryGoal("PDABiochemSuitEquipped", Story.GoalType.PDA, 0f);
        StoryUtils.RegisterVoiceLog(UseBiochemicalProtectionSuitEvent.key, "PDABiochemSuitEquipped",
            EssentialAssets.VoiceLogIconPda);
        StoryGoalHandler.RegisterCustomEvent(UseBiochemicalProtectionSuitEvent.key,
            () => { PlagueDamageUI.Main.Refresh(); });
        UnlockSuitChargeItemEvent = StoryGoalHandler.RegisterCompoundGoal("UnlockSuitChargeItem",
            Story.GoalType.Story, 0, UseBiochemicalProtectionSuitEvent.key);
        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockSuitChargeItemEvent.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = SuitCharge.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        TransfuserMissionEvent =
            StoryGoalHandler.RegisterCompoundGoal("TransfuserMission", Story.GoalType.PDA, 14,
                "PDABiochemSuitEquipped");
        StoryUtils.RegisterVoiceLog("TransfuserMission", "TransfuserMission", EssentialAssets.VoiceLogIconAlterra);

        UnlockTransfuserEvent = new StoryGoal("PDATransfuserUnlocked", Story.GoalType.PDA, 2);
        StoryUtils.RegisterVoiceLog("PDATransfuserUnlocked", "PDATransfuserUnlocked", EssentialAssets.VoiceLogIconPda);
        InfectionSamplerTool.RegisterLateStoryData();

        TransfuserSampleTakenEvent = new StoryGoal("PDAInfectionSampleTaken", Story.GoalType.PDA, 0.5f);
        StoryUtils.RegisterVoiceLog("PDAInfectionSampleTaken", "PDAInfectionSampleTaken",
            EssentialAssets.VoiceLogIconPda);

        AngelinaFirstSampleShuttlePadInstructions =
            StoryGoalHandler.RegisterCompoundGoal("AngelinaFirstSampleShuttlePadInstructions", Story.GoalType.PDA, 45,
                TransfuserSampleTakenEvent.key);
        StoryUtils.RegisterVoiceLog(AngelinaFirstSampleShuttlePadInstructions.key,
            "AngelinaFirstSampleShuttlePadInstructions",
            EssentialAssets.VoiceLogIconAlterra);

        UnlockShuttlePadEvent = StoryGoalHandler.RegisterCompoundGoal("PDAUnlockShuttlePad", Story.GoalType.PDA, 18,
            "AngelinaFirstSampleShuttlePadInstructions");
        StoryUtils.RegisterVoiceLog("PDAUnlockShuttlePad", "PDAUnlockShuttlePlatform",
            EssentialAssets.VoiceLogIconPda);

        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockShuttlePadEvent.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = ShuttlePad.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        SendPlagueSampleViaShuttleEvent = new StoryGoal("SendPlagueSampleViaShuttle", Story.GoalType.PDA, 60);
        StoryUtils.RegisterVoiceLog(SendPlagueSampleViaShuttleEvent.key, "AlterraReceiveSample",
            EssentialAssets.VoiceLogIconAlterra);

        // Reminder for the infection sampler
        SampleReminderTrigger = StoryGoalHandler.RegisterCompoundGoal("SampleReminderTrigger", Story.GoalType.Story,
            2 * 60 * 60,
            TransfuserMissionEvent.key);
        SampleReminderLog = new StoryGoal("SampleReminder", Story.GoalType.PDA, 0);
        StoryUtils.RegisterVoiceLog("SampleReminder", "SampleReminder", EssentialAssets.VoiceLogIconAlterra);
        StoryGoalHandler.RegisterCustomEvent(SampleReminderTrigger.key, () =>
        {
            var goalManager = StoryGoalManager.main;
            if (goalManager && !goalManager.IsGoalComplete(SendPlagueSampleViaShuttleEvent.key) &&
                !goalManager.IsGoalComplete(DomeConstructionEvent.key))
            {
                SampleReminderLog.Trigger();
            }
        });

        // Dome drones unlock message
        UnlockDomeDroneEvent = StoryGoalHandler.RegisterCompoundGoal("UnlockDomeDroneEvent", Story.GoalType.Story, 48,
            SendPlagueSampleViaShuttleEvent.key);

        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockDomeDroneEvent.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = DomeDroneFormationPrefab.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        OnBuildDrones = new StoryGoal("PDAOnBuildDrones", Story.GoalType.PDA, 2);
        StoryUtils.RegisterVoiceLog(OnBuildDrones.key, "PDAOnBuildDrones", EssentialAssets.VoiceLogIconPda);

        DronesReadyForDomeConstruction = new StoryGoal("DronesReadyForDomeConstruction", Story.GoalType.PDA, 2);
        StoryUtils.RegisterVoiceLog(DronesReadyForDomeConstruction.key, "DronesReadyForDomeConstruction",
            EssentialAssets.VoiceLogIconAlterra);

        StoryGoalHandler.RegisterOnGoalUnlockData(DronesReadyForDomeConstruction.key,
            signals: new[]
            {
                new UnlockSignalData
                {
                    targetPosition = DomeConstructionTriggerLocation,
                    targetDescription = "DomeConstructionLocationSignal"
                }
            });

        StoryGoalHandler.RegisterCustomEvent(DronesReadyForDomeConstruction.key,
            () => UWE.CoroutineHost.StartCoroutine(SpawnDomeTriggerCoroutine()));

        // Cassy & Michael base
        ResearchTeamARadioGoal = StoryGoalHandler.RegisterItemGoal("ResearchTeamARadioLog", Story.GoalType.Radio,
            RedPlagueSample.Info.TechType, 180);
        StoryUtils.RegisterVoiceLog(ResearchTeamARadioGoal.key, "ResearchTeamARadioLog",
            EssentialAssets.VoiceLogIconRadio);
        StoryGoalHandler.RegisterOnGoalUnlockData("OnPlayResearchTeamARadioLog", signals: new UnlockSignalData[]
        {
            new()
            {
                targetPosition = new Vector3(-111, -190, -985),
                targetDescription = "ResearchTeamASignal"
            }
        });

        PDAHandler.AddEncyclopediaEntry("ResearchTeamADeathLog", CustomPdaPaths.RedPlagueVictimsPath, null, null, null,
            null, PDAHandler.UnlockBasic,
            StoryUtils.RegisterDatabankEntryVoiceLog("ResearchTeamADeathLog", "ResearchTeamADeathLog"));
        PDAHandler.AddEncyclopediaEntry("ResearchTeamATabletLog", CustomPdaPaths.RedPlagueVictimsPath, null, null, null,
            null, PDAHandler.UnlockBasic,
            StoryUtils.RegisterDatabankEntryVoiceLog("ResearchTeamATabletLog", "ResearchTeamATabletLog"));

        // Island elevator
        IslandElevatorActivatedGoal = new StoryGoal("IslandElevatorActivated", Story.GoalType.Story, 0);
        UseElevatorGoal = new StoryGoal("UseIslandElevator", Story.GoalType.Story, 0);

        // Dome construction
        PrepareForDomeConstruction = new StoryGoal("PrepareForDomeConstruction", Story.GoalType.PDA, 0);
        StoryUtils.RegisterVoiceLog(PrepareForDomeConstruction.key, "PrepareForDomeConstruction",
            EssentialAssets.VoiceLogIconAlterra);

        DomeConstructionEvent = new StoryGoal("DomeConstructionEvent", Story.GoalType.Story, 0);

        DomeSpeakEvent =
            StoryGoalHandler.RegisterCompoundGoal("DomeSpeakEvent", Story.GoalType.PDA, 37, "DomeConstructionEvent");
        StoryUtils.RegisterVoiceLog(DomeSpeakEvent.key, "DomeVoiceV1", EssentialAssets.VoiceLogIconDome);

        DomeConstructionFinishedEvent = StoryGoalHandler.RegisterCompoundGoal("DomeConstructionFinishedEvent",
            Story.GoalType.PDA, 45, "DomeConstructionEvent");
        StoryUtils.RegisterVoiceLog(DomeConstructionFinishedEvent.key, "SendingScienceTeam",
            EssentialAssets.VoiceLogIconAlterra);

        LeaveDomeArgument = StoryGoalHandler.RegisterCompoundGoal("LeaveDomeArgument", Story.GoalType.PDA, 15,
            DomeConstructionFinishedEvent.key);
        StoryUtils.RegisterVoiceLog(LeaveDomeArgument.key, "LeaveDomeArgument", EssentialAssets.VoiceLogIconAlterra);
    }

    #region Custom Events

    private static IEnumerator SpawnDomeTriggerCoroutine()
    {
        var task = CraftData.GetPrefabForTechTypeAsync(DomeConstructionTriggerPrefab.Info.TechType);
        yield return task;
        var trigger = Object.Instantiate(task.GetResult());
        trigger.transform.position = DomeConstructionTriggerLocation;
        trigger.transform.localScale = Vector3.one * 20;
        trigger.AddTriggerCollider(0.5f);
        trigger.SetActive(true);
        GlobalRedPlagueProgressTracker.OnProgressAchieved(GlobalRedPlagueProgressTracker.ProgressStatus
            .DomeConstructed);
    }

    #endregion
}