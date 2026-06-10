using HarmonyLib;
using mset;
using UnityEngine;

namespace TheRedPlague.Framework.API.Lighting;

[HarmonyPatch]
public static class CustomLightingControllerPatcher
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(SkyApplier), nameof(SkyApplier.GetEnvironment))]
    public static void GetEnvironmentPostfix(GameObject gameObject, ref GameObject __result)
    {
        if (__result != null) return;
        var link = gameObject.GetComponentInParent<LightControllerSkyLink>();
        if (link != null)
        {
            __result = link.gameObject;
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(SkyApplier), nameof(SkyApplier.GetSkyForEnvironment))]
    public static void GetSkyForEnvironmentPostfix(GameObject environment, Skies anchorMode, ref Sky __result)
    {
        if (__result != null) return;
        if (environment == null)
        {
            return;
        }
        var link = environment.GetComponentInParent<LightControllerSkyLink>();
        if (link != null)
        {
            if (anchorMode == Skies.BaseGlass)
            {
                __result = link.glassSky;
            }
            else
            {
                __result = link.interiorSky;
            }
        }
    }
}