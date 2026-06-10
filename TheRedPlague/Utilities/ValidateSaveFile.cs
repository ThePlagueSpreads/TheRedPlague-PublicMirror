using System.Collections;
using Nautilus.Handlers;
using Story;
using TheRedPlague.Content.Act2.Ending;
using TheRedPlague.Content.Act2.FleshCave;
using TheRedPlague.Content.Creatures.Chaos;
using UWE;

namespace TheRedPlague.Utilities;

public static class ValidateSaveFile
{
    // EXECUTED AS A LATE LOADING EVENT
    public static IEnumerator DoValidation(WaitScreenHandler.WaitScreenTask task)
    {
        Plugin.Logger.LogMessage("Validating save file");
        yield return ValidationCoroutine();
    }

    private static IEnumerator ValidationCoroutine()
    {
        if (StoryGoalManager.main == null)
        {
            Plugin.Logger.LogError("Story goal manager not found! Skipping save file validation.");
            yield break;
        }

        ValidateExitFleshCaveWatcher();
        ValidateDestroyPlagueHeartTimer();
        ValidateRoamingChaosPrefab();
    }

    private static void ValidateExitFleshCaveWatcher()
    {
        if (StoryGoalManager.main.IsGoalComplete(Act2Story.WaitForLeavingPlagueCave.key) &&
            !StoryGoalManager.main.IsGoalComplete(Act2Story.LeavePlagueCaveGoal.key) &&
            WatchPlayerLeavingFleshCave.Instance == null)
        {
            // Waiting on this causes an infinite loading time
            CoroutineHost.StartCoroutine(Act2Story.SpawnLeavePlagueCaveWaiter());
        }
    }

    private static void ValidateDestroyPlagueHeartTimer()
    {
        if (StoryGoalManager.main.IsGoalComplete(Act2Story.BennetAddPlagueHeartInstructions.key) &&
            !StoryGoalManager.main.IsGoalComplete(Act2Story.Act2EndingEvent.key) &&
            DestroyPlagueHeartTimer.Main == null)
        {
            // Waiting on this causes an infinite loading time
            CoroutineHost.StartCoroutine(Act2Story.StartPlagueHeartDestructionTimerCoroutine());
        }
    }
    
    private static void ValidateRoamingChaosPrefab()
    {
        if (StoryGoalManager.main.IsGoalComplete(Act2Story.SpawnRoamingChaosGoal.key))
        {
            RoamingChaosLeviathanManager.CreateManagerIfNoneExists();
        }
    }
}