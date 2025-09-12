using HarmonyLib;
using TheRedPlague.Mono.StoryContent;
using UnityEngine;

namespace TheRedPlague.Patches.ObjectEdits;

[HarmonyPatch(typeof(LaunchRocket))]
public static class LaunchRocketPatch
{
    private static float _timeLastHint;
    
    [HarmonyPatch(nameof(LaunchRocket.IsRocketReady))]
    [HarmonyPostfix]
    public static void IsRocketReadyPostfix(ref bool __result)
    {
        //if (StoryGoalManager.main.IsGoalComplete(StoryUtils.EnzymeRainEnabled.key) && !StoryGoalManager.main.IsGoalComplete(StoryUtils.DisableDome.key))
        if (InfectionDomeController.main != null && InfectionDomeController.main.isActiveAndEnabled)
        {
            if (Time.time > _timeLastHint + 3)
            {
                ErrorMessage.AddMessage(Language.main.Get("LaunchRocketWhileDomeActiveMessage"));
                StoryUtils.LaunchRocketWhileDomeActive.Trigger();
                _timeLastHint = Time.time;
            }
            __result = false;
        }
    }
}