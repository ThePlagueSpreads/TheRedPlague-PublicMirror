using System.Collections;
using Nautilus.Handlers;
using Story;
using TheRedPlague.Managers;
using TheRedPlague.Mono.UI;
using TheRedPlague.PrefabFiles.Buildable;
using TheRedPlague.PrefabFiles.DomeDrones;
using TheRedPlague.PrefabFiles.Equipment;
using TheRedPlague.PrefabFiles.Items;
using TheRedPlague.PrefabFiles.StoryProps;
using UnityEngine;

namespace TheRedPlague;

public static partial class StoryUtils
{
    public static CompoundGoal RedPlagueIntroduction { get; private set; }
    public static CompoundGoal AngelinaIntroductionEvent { get; private set; }
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
    public static StoryGoal PrepareForDomeConstruction { get; private set; }
    public static StoryGoal DomeConstructionEvent { get; private set; }
    public static CompoundGoal DomeSpeakEvent { get; private set; }
    public static CompoundGoal DomeConstructionFinishedEvent { get; private set; }
    public static CompoundGoal LeaveDomeArgument { get; private set; }
    
    private static void RegisterAct1()
    {
        RedPlagueIntroduction =
            StoryGoalHandler.RegisterCompoundGoal("RedPlagueIntroduction", Story.GoalType.PDA, 60, "PlayerDiving");
        RegisterVoiceLog("RedPlagueIntroduction", "PDARedPlagueIntroduction", VoiceLogIconPda);

        AngelinaIntroductionEvent = StoryGoalHandler.RegisterCompoundGoal("AngelinaIntroduction", Story.GoalType.Radio,
            0, "PlayerDiving");
        RegisterVoiceLog("AngelinaIntroduction", "AngelinaIntroduction", VoiceLogIconAlterra);

        BiochemicalProtectionSuitUnlockEvent = StoryGoalHandler.RegisterCompoundGoal(
            "PDABiochemicalProtectionSuitUnlock", Story.GoalType.PDA,
            50, "OnPlayAngelinaIntroduction");
        RegisterVoiceLog(BiochemicalProtectionSuitUnlockEvent.key, "PDABiochemicalProtectionSuitUnlock",
            VoiceLogIconPda);
        StoryGoalHandler.RegisterOnGoalUnlockData(BiochemicalProtectionSuitUnlockEvent.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = BiochemicalProtectionSuit.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        UseBiochemicalProtectionSuitEvent = new StoryGoal("PDABiochemSuitEquipped", Story.GoalType.PDA, 0f);
        RegisterVoiceLog(UseBiochemicalProtectionSuitEvent.key, "PDABiochemSuitEquipped", VoiceLogIconPda);
        StoryGoalHandler.RegisterCustomEvent(UseBiochemicalProtectionSuitEvent.key, () =>
        {
            PlagueDamageUI.Main.Refresh();
        });
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
        RegisterVoiceLog("TransfuserMission", "TransfuserMission", VoiceLogIconAlterra);

        UnlockTransfuserEvent = new StoryGoal("PDATransfuserUnlocked", Story.GoalType.PDA, 2);
        RegisterVoiceLog("PDATransfuserUnlocked", "PDATransfuserUnlocked", VoiceLogIconPda);
        InfectionSamplerTool.RegisterLateStoryData();

        TransfuserSampleTakenEvent = new StoryGoal("PDAInfectionSampleTaken", Story.GoalType.PDA, 0.5f);
        RegisterVoiceLog("PDAInfectionSampleTaken", "PDAInfectionSampleTaken", VoiceLogIconPda);

        AngelinaFirstSampleShuttlePadInstructions =
            StoryGoalHandler.RegisterCompoundGoal("AngelinaFirstSampleShuttlePadInstructions", Story.GoalType.PDA, 45,
                TransfuserSampleTakenEvent.key);
        RegisterVoiceLog(AngelinaFirstSampleShuttlePadInstructions.key, "AngelinaFirstSampleShuttlePadInstructions",
            VoiceLogIconAlterra);

        UnlockShuttlePadEvent = StoryGoalHandler.RegisterCompoundGoal("PDAUnlockShuttlePad", Story.GoalType.PDA, 18,
            "AngelinaFirstSampleShuttlePadInstructions");
        RegisterVoiceLog("PDAUnlockShuttlePad", "PDAUnlockShuttlePlatform",
            VoiceLogIconPda);

        StoryGoalHandler.RegisterOnGoalUnlockData(UnlockShuttlePadEvent.key, new[]
        {
            new UnlockBlueprintData
            {
                techType = ShuttlePad.Info.TechType,
                unlockType = UnlockBlueprintData.UnlockType.Available
            }
        });

        SendPlagueSampleViaShuttleEvent = new StoryGoal("SendPlagueSampleViaShuttle", Story.GoalType.PDA, 60);
        RegisterVoiceLog(SendPlagueSampleViaShuttleEvent.key, "AlterraReceiveSample", VoiceLogIconAlterra);

        // Reminder for the infection sampler
        SampleReminderTrigger = StoryGoalHandler.RegisterCompoundGoal("SampleReminderTrigger", Story.GoalType.Story,
            2 * 60 * 60,
            TransfuserMissionEvent.key);
        SampleReminderLog = new StoryGoal("SampleReminder", Story.GoalType.PDA, 0);
        RegisterVoiceLog("SampleReminder", "SampleReminder", VoiceLogIconAlterra);
        StoryGoalHandler.RegisterCustomEvent(SampleReminderTrigger.key, () =>
        {
            var goalManager = StoryGoalManager.main;
            if (goalManager && !goalManager.IsGoalComplete(SendPlagueSampleViaShuttleEvent.key) && !goalManager.IsGoalComplete(DomeConstructionEvent.key))
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
        RegisterVoiceLog(OnBuildDrones.key, "PDAOnBuildDrones", VoiceLogIconPda);

        DronesReadyForDomeConstruction = new StoryGoal("DronesReadyForDomeConstruction", Story.GoalType.PDA, 2);
        RegisterVoiceLog(DronesReadyForDomeConstruction.key, "DronesReadyForDomeConstruction", VoiceLogIconAlterra);

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

        PrepareForDomeConstruction = new StoryGoal("PrepareForDomeConstruction", Story.GoalType.PDA, 0);
        RegisterVoiceLog(PrepareForDomeConstruction.key, "PrepareForDomeConstruction", VoiceLogIconAlterra);

        DomeConstructionEvent = new StoryGoal("DomeConstructionEvent", Story.GoalType.Story, 0);

        DomeSpeakEvent =
            StoryGoalHandler.RegisterCompoundGoal("DomeSpeakEvent", Story.GoalType.PDA, 37, "DomeConstructionEvent");
        RegisterVoiceLog(DomeSpeakEvent.key, "DomeVoiceV1", VoiceLogIconDome);

        DomeConstructionFinishedEvent = StoryGoalHandler.RegisterCompoundGoal("DomeConstructionFinishedEvent",
            Story.GoalType.PDA, 45, "DomeConstructionEvent");
        RegisterVoiceLog(DomeConstructionFinishedEvent.key, "SendingScienceTeam", VoiceLogIconAlterra);

        LeaveDomeArgument = StoryGoalHandler.RegisterCompoundGoal("LeaveDomeArgument", Story.GoalType.PDA, 15,
            DomeConstructionFinishedEvent.key);
        RegisterVoiceLog(LeaveDomeArgument.key, "LeaveDomeArgument", VoiceLogIconAlterra);
    }
    
    #region Custom Events

    private static IEnumerator SpawnDomeTriggerCoroutine()
    {
        var task = CraftData.GetPrefabForTechTypeAsync(DomeConstructionTriggerPrefab.Info.TechType);
        yield return task;
        var trigger = Object.Instantiate(task.GetResult());
        trigger.transform.position = DomeConstructionTriggerLocation;
        trigger.transform.localScale = Vector3.one * 20;
        trigger.AddComponent<SphereCollider>().isTrigger = true;
        trigger.SetActive(true);
        GlobalRedPlagueProgressTracker.OnProgressAchieved(GlobalRedPlagueProgressTracker.ProgressStatus.DomeConstructed);
    }
    
    #endregion
}