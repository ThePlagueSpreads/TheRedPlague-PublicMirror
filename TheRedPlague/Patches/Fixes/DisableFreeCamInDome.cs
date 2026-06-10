using HarmonyLib;
using TheRedPlague.Content.Act1.Dome;

namespace TheRedPlague.Patches.Fixes;

[HarmonyPatch(typeof(FreecamController))]
public static class DisableFreeCamInDome
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(FreecamController.OnConsoleCommand_freecam))]
    private static bool FreeCamPrefix(FreecamController __instance)
    {
        return CanFreeCam(__instance);
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(FreecamController.OnConsoleCommand_ghost))]
    private static bool GhostPrefix(FreecamController __instance)
    {
        return CanFreeCam(__instance);
    }

    private static bool CanFreeCam(FreecamController controller)
    {
        // Allow disabling it
        if (controller.mode)
            return true;
        
        if (DomeBaseUtils.GetIsPlayerInsideDomeBase())
        {
            ErrorMessage.AddMessage("Can't enable freecam while inside the dome base!");
            return false;
        }

        return true;
    }
}