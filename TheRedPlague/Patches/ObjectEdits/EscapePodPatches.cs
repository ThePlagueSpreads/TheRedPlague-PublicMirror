using HarmonyLib;
using TheRedPlague.Content.Scares;
using TheRedPlague.Framework.Behaviour.Horror;

namespace TheRedPlague.Patches.ObjectEdits;

[HarmonyPatch(typeof(EscapePod))]
public static class EscapePodPatches
{
    [HarmonyPatch(nameof(EscapePod.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(EscapePod __instance)
    {
        __instance.gameObject.EnsureComponent<LifePodScare>();
    }
}