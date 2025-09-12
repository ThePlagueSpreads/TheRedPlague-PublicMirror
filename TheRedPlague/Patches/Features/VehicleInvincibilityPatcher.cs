using HarmonyLib;
using TheRedPlague.Mono.Util;

namespace TheRedPlague.Patches.Features;

[HarmonyPatch(typeof(Vehicle))]
public static class VehicleInvincibilityPatcher
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Vehicle.Start))]
    public static void StartPostfix(Vehicle __instance)
    {
        if (!IsVehicleSafeToModify(__instance)) return;
        
        var ecoTarget = __instance.GetComponent<EcoTarget>();
        if (ecoTarget == null)
        {
            Plugin.Logger.LogWarning($"Vehicle '{__instance.gameObject}' has no eco target component!");
            return;
        }

        var fix = __instance.gameObject.EnsureComponent<SetVehicleInvisibleWhenOutside>();
        fix.vehicle = __instance;    
        fix.target = ecoTarget;
    }

    private static bool IsVehicleSafeToModify(Vehicle vehicle)
    {
        return vehicle is SeaMoth or Exosuit;
    }
}