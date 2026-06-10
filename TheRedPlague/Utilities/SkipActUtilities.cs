using System.Collections;
using Story;
using TheRedPlague.Content.Buildables.AdministratorFabricator;
using TheRedPlague.Content.Buildables.PlagueAltar;
using TheRedPlague.Content.Buildables.PlagueNeutralizer;
using TheRedPlague.Content.Buildables.PlagueRefinery;
using TheRedPlague.Content.Creatures.Neural;
using TheRedPlague.Content.Equipment.BiochemicalProtectionModule;
using TheRedPlague.Content.Equipment.BoneRegurgitator;
using TheRedPlague.Content.Equipment.InfectionSampler;
using TheRedPlague.Content.Equipment.InfectionTracker;
using TheRedPlague.Content.Equipment.PlagueArmor;
using TheRedPlague.Content.Equipment.PlagueGrappler;
using TheRedPlague.Content.Equipment.PlagueKnife;
using TheRedPlague.Content.Items.Consumable;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Content.UpgradeModules;
using UnityEngine;

namespace TheRedPlague.Utilities;

public static class SkipActUtilities
{
    private static IEnumerator SkipDays(int days)
    {
        for (var i = 0; i < days; i++)
        {
            DayNightCycle.main.OnConsoleCommand_day(new NotificationCenter.Notification(new Hashtable()));
            yield return new WaitForSeconds(1f);
        }
    }
    
    public static void StartAct2()
    {
        UWE.CoroutineHost.StartCoroutine(SkipAct1Coroutine());
    }

    private static IEnumerator SkipAct1Coroutine()
    {
        Act1Story.UseBiochemicalProtectionSuitEvent.Trigger();
        Act1Story.SendPlagueSampleViaShuttleEvent.Trigger();
        Act1Story.IslandElevatorActivatedGoal.Trigger(); // Unlock island elevator

        // Dome sequence
        Act1Story.UnlockDomeDroneEvent.Trigger();
        Act1Story.OnBuildDrones.Trigger();
        Act1Story.DronesReadyForDomeConstruction.Trigger();
        Commands.SpawnDome(true);
        Act2Story.ArriveToDomeGoal.Trigger();
        
        // Unlock Act 1 items
        KnownTech.Add(InfectionSamplerTool.Info.TechType);
        KnownTech.Add(AdminFabricator.Info.TechType);
        KnownTech.Add(PrecursorTabletRegistration.InfectionTabletInfo.TechType);
        KnownTech.Add(PlagueNeutralizer.Info.TechType);
        KnownTech.Add(BiochemicalProtectionSuit.Info.TechType);
        KnownTech.Add(SuitCharge.Info.TechType);
        KnownTech.Add(InsanityMedicine.Info.TechType);

        yield return SkipDays(3);
    }

    public static void StartAct3()
    {
        UWE.CoroutineHost.StartCoroutine(StartAct3Coroutine());
    }
    
    private static IEnumerator StartAct3Coroutine()
    {
        yield return SkipAct1Coroutine();
        yield return SkipAct2Coroutine();
    }

    private static IEnumerator SkipAct2Coroutine()
    {
        // Add necessary items:
        CraftData.AddToInventory(PlagueKnife.Info.TechType);
        CraftData.AddToInventory(BoneArmor.Info.TechType);
        Commands.GivePlagueResources();

        // -------------

        // Unlock Act 2 items
        KnownTech.Add(PrecursorTabletRegistration.GoldTabletInfo.TechType);
        KnownTech.Add(ConsciousNeuralMatter.Info.TechType);

        // Unlock Act 2 equipment
        KnownTech.Add(PlagueGrappler.Info.TechType);
        KnownTech.Add(InfectionTracker.Info.TechType);
        KnownTech.Add(BoneArmor.Info.TechType);
        KnownTech.Add(BoneCannonPrefab.Info.TechType);
        KnownTech.Add(AntiPossessionModule.Info.TechType);
        KnownTech.Add(PlagueKnife.Info.TechType);

        // Unlock Act 2 tech
        KnownTech.Add(PlagueAltar.Info.TechType);
        KnownTech.Add(PlagueRefinery.Info.TechType);

        // -------------

        // Maze base
        Act2Story.MazeBaseDiscoveryGoal.Trigger();
        
        // Complete checklist
        Commands.UnlockChecklist();
        var goals = StoryGoalManager.main;
        goals.OnGoalComplete(StoryUtils.GetStoryGoalKeyForShuttleDelivery(AmalgamatedBone.Info.ClassID));
        goals.OnGoalComplete(StoryUtils.GetStoryGoalKeyForShuttleDelivery(WarperHeart.Info.ClassID));
        goals.OnGoalComplete(StoryUtils.GetStoryGoalKeyForShuttleDelivery(PlagueCatalyst.Info.ClassID));
        goals.OnGoalComplete(StoryUtils.GetStoryGoalKeyForShuttleDelivery(PlagueIngot.Info.ClassID));
        goals.OnGoalComplete(StoryUtils.GetStoryGoalKeyForShuttleDelivery(DormantNeuralMatter.Info.ClassID));
        goals.OnGoalComplete(StoryUtils.GetStoryGoalKeyForShuttleDelivery(MysteriousRemains.Info.ClassID));
        Commands.CompleteChecklist();

        // Open plague cave
        Act2Story.OpenPlagueCave.Trigger();
        Commands.OpenPlagueCave();
        
        // Spawn satellite
        Act2Story.SpawnPrecursorSatelliteGoal.Trigger();

        // Bennet
        Act2Story.BennetIntroduction.Trigger();
        
        // Act 2 ending
        Commands.ObeyCinematic();
        
        // Thruster event
        Commands.ThrusterEvent();

        yield return SkipDays(4);
    }
}