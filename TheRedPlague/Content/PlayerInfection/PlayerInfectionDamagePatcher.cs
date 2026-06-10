using HarmonyLib;
using TheRedPlague.Content.Infection;
using UnityEngine;

namespace TheRedPlague.Content.PlayerInfection;

[HarmonyPatch(typeof(Player))]
public static class PlayerInfectionDamagePatcher
{
    public const float InfectionDamageFromCreaturesPercent = 0.25f;
    public const float MaxInfectionDamageFromCreatures = 17f;
    
    [HarmonyPatch(nameof(Player.OnTakeDamage))]
    [HarmonyPostfix]
    public static void OnTakeDamagePostfix(Player __instance, DamageInfo damageInfo)
    {
        // Give infection damage from creature attacks
        if (damageInfo.type == DamageType.Normal && damageInfo.dealer != null)
        {
            if (RedPlagueHost.IsGameObjectInfected(damageInfo.dealer))
            {
                PlagueDamageStat.main.TakeInfectionDamage(
                    Mathf.Min(damageInfo.damage * InfectionDamageFromCreaturesPercent, MaxInfectionDamageFromCreatures));
            }
        }

        // Give infection damage from penetrative plague attacks
        if (damageInfo.type == CustomDamageTypes.PenetrativePlagueDamage)
        {
            PlagueDamageStat.main.TakeInfectionDamage(damageInfo.damage);
        }
    }
}