using HarmonyLib;
using TheRedPlague.Utilities;

namespace TheRedPlague.Patches.Features;

[HarmonyPatch]
public static class HideHudPatches
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(uGUI_SceneHUD), nameof(uGUI_SceneHUD.IsActive))]
    public static void HideSceneHUD(ref bool __result)
    {
        if (HideHudUtils.HidingHud)
        {
            __result = false;
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(uGUI_DepthCompass), nameof(uGUI_DepthCompass.IsCompassEnabled))]
    public static void HideDepthCompass(ref bool __result)
    {
        if (HideHudUtils.HidingHud)
        {
            __result = false;
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(uGUI_DepthCompass), nameof(uGUI_DepthCompass.GetDepthInfo))]
    public static void OverrideDepthCompassDepthMode(ref uGUI_DepthCompass.DepthMode __result)
    {
        if (HideHudUtils.HidingHud)
        {
            __result = uGUI_DepthCompass.DepthMode.None;
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(uGUI_QuickSlots), nameof(uGUI_QuickSlots.GetTarget))]
    public static void HideQuickSlots(ref IQuickSlots __result)
    {
        if (HideHudUtils.HidingHud)
        {
            __result = null;
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(uGUI_Pings), nameof(uGUI_Pings.IsVisibleNow))]
    public static void HidePings(ref bool __result)
    {
        if (HideHudUtils.HidingHud)
        {
            __result = false;
        }
    }
    
    // Disable rebreather messages for cinematics   
    [HarmonyPrefix]
    [HarmonyPatch(typeof(RebreatherDepthWarnings), nameof(RebreatherDepthWarnings.Update))]
    public static bool DisableRebreatherMessages()
    {
        return !HideHudUtils.HidingHud;
    }
}