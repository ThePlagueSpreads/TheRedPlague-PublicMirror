using HarmonyLib;
using TheRedPlague.Content.UpgradeModules;

namespace TheRedPlague.Patches.Fixes;

[HarmonyPatch(typeof(Exosuit))]
public static class FixDrillablePrompt
{
    [HarmonyPatch(nameof(Exosuit.HasDrill))]
    [HarmonyPostfix]
    public static void HasDrillPostfix(Exosuit __instance, ref bool __result)
    {
        if (__instance.modules.GetCount(ObsidianBladeArmModule.Info.TechType) >= 1)
        {
            __result = true;
        }
    }
}