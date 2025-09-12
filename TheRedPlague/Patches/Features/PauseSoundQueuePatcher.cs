using HarmonyLib;
using TheRedPlague.Utilities;

namespace TheRedPlague.Patches.Features;

[HarmonyPatch(typeof(SoundQueue))]
public static class PauseSoundQueuePatcher
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SoundQueue.Update))]
    public static bool UpdatePrefix()
    {
        return !MutePdaUtils.GetPdaQueueDisabled();
    }
}