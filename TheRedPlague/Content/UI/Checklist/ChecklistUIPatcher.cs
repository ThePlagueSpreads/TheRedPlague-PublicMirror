using HarmonyLib;

namespace TheRedPlague.Content.UI.Checklist;

[HarmonyPatch]
public static class ChecklistUIPatcher
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(uGUI_LogTab), nameof(uGUI_LogTab.Awake))]
    public static void LogTabAwakePostfix(uGUI_LogTab __instance)
    {
        ChecklistUI.CreateChecklistUI(__instance);
    }
}