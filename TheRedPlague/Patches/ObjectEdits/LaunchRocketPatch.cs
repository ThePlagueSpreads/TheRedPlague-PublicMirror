using HarmonyLib;
using TheRedPlague.Content.Act1.Dome;
using UnityEngine;

namespace TheRedPlague.Patches.ObjectEdits;

[HarmonyPatch(typeof(LaunchRocket))]
public static class LaunchRocketPatch
{
    private static float _timeLastHint;
    
    [HarmonyPatch(nameof(LaunchRocket.IsRocketReady))]
    [HarmonyPostfix]
    public static void IsRocketReadyPostfix(LaunchRocket __instance, ref bool __result)
    {
        var dome = InfectionDomeController.main;
        //if (StoryGoalManager.main.IsGoalComplete(StoryUtils.EnzymeRainEnabled.key) && !StoryGoalManager.main.IsGoalComplete(StoryUtils.DisableDome.key))
        if (dome != null && dome.isActiveAndEnabled && dome.GetIsPositionInsideDome(__instance.transform.position))
        {
            if (Time.time > _timeLastHint + 3)
            {
                ErrorMessage.AddMessage(Language.main.Get("LaunchRocketWhileDomeActiveMessage"));
                GeneralStory.LaunchRocketWhileDomeActive.Trigger();
                _timeLastHint = Time.time;
            }
            __result = false;
        }
    }

    [HarmonyPatch(nameof(LaunchRocket.SetLaunchStarted))]
    [HarmonyPostfix]
    public static void SetLaunchStartedPostfix()
    {
        var dome = InfectionDomeController.main;
        if (dome != null)
        {
            Object.Destroy(dome.gameObject);
        }
    }
}