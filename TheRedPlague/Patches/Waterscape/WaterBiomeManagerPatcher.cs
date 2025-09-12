using HarmonyLib;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Patches.Waterscape;

[HarmonyPatch(typeof(WaterBiomeManager))]
public static class WaterBiomeManagerPatcherPatcher
{
    [HarmonyPatch(nameof(WaterBiomeManager.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(WaterBiomeManager __instance)
    {
        foreach (var settings in __instance.biomeSettings)
        {
            if (settings.name != "dunes") continue;
            
            settings.settings = BiomeUtils.CreateBiomeSettings(
                new Vector3(13, 11, 10),
                0.5f,
                new Color(0.381f, 0.427f, 0.447f),
                0.2f,
                new Color(0.2f, 0.036f, 0.03f),
                0.02f,
                25,
                1,
                1,
                20
            );
            return;
        }
    }
}