using HarmonyLib;

namespace TheRedPlague.Framework.API.PDA;

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