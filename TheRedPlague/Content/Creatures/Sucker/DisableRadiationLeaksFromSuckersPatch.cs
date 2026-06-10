using HarmonyLib;

namespace TheRedPlague.Content.Creatures.Sucker;

[HarmonyPatch(typeof(RadiationLeak))]
public static class DisableRadiationLeaksFromSuckersPatch
{
    [HarmonyPatch(nameof(RadiationLeak.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(RadiationLeak __instance)
    {
        if (RadiationLeakSuckerObscuring.main != null)
            return;
        var leaksObj = __instance.transform.parent.parent.gameObject;
        leaksObj.EnsureComponent<RadiationLeakSuckerObscuring>();
    }
}