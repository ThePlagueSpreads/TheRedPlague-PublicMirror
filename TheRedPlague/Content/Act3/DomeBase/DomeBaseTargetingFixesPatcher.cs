using System.Reflection;
using HarmonyLib;
using TheRedPlague.Content.Act1.Dome;
using UnityEngine;

namespace TheRedPlague.Content.Act3.DomeBase;

[HarmonyPatch(typeof(Targeting))]
public static class DomeBaseTargetingFixesPatcher
{
    private static MethodBase TargetMethod()
    {
        return AccessTools.Method(typeof(Targeting), nameof(Targeting.GetTarget),
            new[]
            {
                typeof(GameObject),
                typeof(float),
                typeof(GameObject).MakeByRefType(),
                typeof(float).MakeByRefType()
            });
    }
    
    private static void Prefix(ref float maxDistance)
    {
        if (DomeBaseUtils.GetIsPlayerInsideDomeBase())
        {
            maxDistance *= DomeBaseUtils.DomeBaseScale;
        }
    }
}