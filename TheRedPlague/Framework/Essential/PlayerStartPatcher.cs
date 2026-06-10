using HarmonyLib;

namespace TheRedPlague.Framework.Essential;

[HarmonyPatch(typeof(Player))]
public static class PlayerStartPatcher
{
    [HarmonyPatch(nameof(Player.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(Player __instance)
    {
        RuntimeEntryPoint.InstantiateManagers(__instance.gameObject);
    }
}