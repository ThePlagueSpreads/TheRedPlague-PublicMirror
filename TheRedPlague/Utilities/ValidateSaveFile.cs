using System.Collections;
using Nautilus.Handlers;
using Story;
using TheRedPlague.Mono.CreatureBehaviour.Chaos;
using TheRedPlague.Mono.StoryContent;
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
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.WaitForLeavingPlagueCave.key) &&
            !StoryGoalManager.main.IsGoalComplete(StoryUtils.LeavePlagueCaveGoal.key) &&
            WatchPlayerLeavingFleshCave.Instance == null)
        {
            // Waiting on this causes an infinite loading time
            CoroutineHost.StartCoroutine(StoryUtils.SpawnLeavePlagueCaveWaiter());
        }
    }

    private static void ValidateDestroyPlagueHeartTimer()
    {
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.BennetAddPlagueHeartInstructions.key) &&
            !StoryGoalManager.main.IsGoalComplete(StoryUtils.Act2EndingEvent.key) &&
            DestroyPlagueHeartTimer.Main == null)
        {
            // Waiting on this causes an infinite loading time
            CoroutineHost.StartCoroutine(StoryUtils.StartPlagueHeartDestructionTimerCoroutine());
        }
    }
    
    private static void ValidateRoamingChaosPrefab()
    {
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.SpawnRoamingChaosGoal.key))
        {
            RoamingChaosLeviathanManager.CreateManagerIfNoneExists();
        }
    }
}