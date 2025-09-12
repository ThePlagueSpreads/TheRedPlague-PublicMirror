using System.Collections;
using HarmonyLib;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Patches.Features;

[HarmonyPatch]
public static class EnzymeCorrosionPatch
{
    private static readonly FMODAsset CorrodeSound = AudioUtils.GetFmodAsset("Enzyme42Corrosion");

    private const float AcidDamage = 3;
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(EnzymeBall), nameof(EnzymeBall.OnHandClick))]
    public static void OnHandClickPostfix(EnzymeBall __instance)
    {
        if (__instance.playerCured)
            return;
        if (!__instance.playerCinematicController.cinematicModeActive)
            return;
        UWE.CoroutineHost.StartCoroutine(InterruptCinematic(__instance, __instance.playerCinematicController));
    }

    private static IEnumerator InterruptCinematic(EnzymeBall ball, PlayerCinematicController controller)
    {
        yield return new WaitForSeconds(2);
        FMODUWE.PlayOneShot(CorrodeSound, Player.main.transform.position);
        var calculatedDamage = DamageSystem.CalculateDamage(AcidDamage, DamageType.Acid, Player.main.gameObject);
        if (Player.main.liveMixin.health - calculatedDamage > 0.1f && ball != null)
        {
            Player.main.liveMixin.TakeDamage(AcidDamage, ball.transform.position, DamageType.Acid, ball.gameObject);
        }
        yield return new WaitForSeconds(1);
        controller.OnPlayerCinematicModeEnd();
        StoryUtils.Enzyme42Warning.Trigger();
    }
}