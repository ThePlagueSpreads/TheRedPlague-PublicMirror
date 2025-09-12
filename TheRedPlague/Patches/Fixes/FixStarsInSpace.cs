using HarmonyLib;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Patches.Fixes;

[HarmonyPatch(typeof(uSkyManager))]
public static class FixStarsInSpace
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(uSkyManager.Update))]
    public static void UpdatePostfix(uSkyManager __instance)
    {
        if (OuterSpaceUtils.InSpace)
        {
            Graphics.DrawMesh(__instance.starsMesh, Vector3.zero,
                Quaternion.identity, __instance.starMaterial, 0);
        }
    }
}