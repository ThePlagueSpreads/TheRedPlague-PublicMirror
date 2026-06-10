using HarmonyLib;
using UnityEngine;

namespace TheRedPlague.Framework.API.Building;

[HarmonyPatch]
public static class FloatingBuildablePatcher
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Builder), nameof(Builder.CheckAsSubModule))]
    public static void AllowPlacementOnWaterSurfacePostfix(ref bool __result)
    {
        if (__result || !FloatingBuildableUtils.FloatingTechTypes.Contains(Builder.constructableTechType)) return;

        var aimTransform = Builder.GetAimTransform();
        var originalPosition = aimTransform.position + aimTransform.forward * Builder.placeDefaultDistance;
        
        bool snapToWaterSurface = Mathf.Abs(originalPosition.y - FloatingBuildableUtils.GetSeaLevelForTechType(Builder.constructableTechType)) < Builder.placeDefaultDistance;

        if (snapToWaterSurface)
        {
            __result = true;
        }
    }

    private static Vector3 GetPlacedPosition(float seaLevel)
    {
        var aimTransform = Builder.GetAimTransform();
        var originalPosition = aimTransform.position + aimTransform.forward * Builder.placeDefaultDistance;
        return new Vector3(originalPosition.x, seaLevel, originalPosition.z);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Builder), nameof(Builder.SetDefaultPlaceTransform))]
    private static void SetDefaultPlaceTransformPostfix(ref Vector3 position)
    {
        if (!FloatingBuildableUtils.FloatingTechTypes.Contains(Builder.constructableTechType)) return;
        position = GetPlacedPosition(FloatingBuildableUtils.GetSeaLevelForTechType(Builder.constructableTechType));
    }
}