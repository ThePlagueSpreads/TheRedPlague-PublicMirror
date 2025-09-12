using HarmonyLib;
using TheRedPlague.Interfaces;

namespace TheRedPlague.Patches.Features;

[HarmonyPatch(typeof(TooltipFactory))]
public static class InventoryBarValuePatcher
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(TooltipFactory.GetBarValue), typeof(Pickupable))]
    public static void GetBarValuePostfix(Pickupable pickupable, ref float __result)
    {
        if (pickupable == null)
            return;
        var customValue = pickupable.gameObject.GetComponent<ICustomInventoryBarValue>();
        if (customValue == null)
            return;
        __result = customValue.GetBarPercentage();
    }
}