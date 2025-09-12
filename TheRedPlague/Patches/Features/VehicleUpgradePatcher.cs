using System;
using System.Collections.Generic;
using HarmonyLib;

namespace TheRedPlague.Patches.Features;

[HarmonyPatch(typeof(Vehicle))]
public static class VehicleUpgradePatcher
{
    internal static readonly Dictionary<TechType, Action<Vehicle, int>> OnChanged = new();
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Vehicle.OnUpgradeModuleChange))]
    private static void OnModuleChangeDelegate(Vehicle __instance, int slotID, TechType techType, bool added)
    {
        if (OnChanged.TryGetValue(techType, out var action))
        {
            action.Invoke(__instance, slotID);
        }
    }
}