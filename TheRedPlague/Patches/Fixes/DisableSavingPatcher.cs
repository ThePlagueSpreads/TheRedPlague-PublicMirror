using HarmonyLib;
using TheRedPlague.Utilities;

namespace TheRedPlague.Patches.Fixes;

[HarmonyPatch(typeof(IngameMenu))]
public static class DisableSavingPatcher
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(IngameMenu.GetAllowSaving))]
    public static void GetAllowSavingPostfix(ref bool __result)
    {
        if (__result == false)
            return;
        
        if (PreventSavingUtils.GetSavingIsDisabled())
        {
            __result = false;
        }
    }
}