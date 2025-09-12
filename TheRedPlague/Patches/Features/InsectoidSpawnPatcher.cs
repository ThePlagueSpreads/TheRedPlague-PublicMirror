using System.Collections;
using HarmonyLib;
using Story;
using UnityEngine;

namespace TheRedPlague.Patches.Features;

[HarmonyPatch]
public static class InsectoidSpawnPatcher
{
    private const float OutcropFirstTimeSpawnRate = 0.07f;
    private const float OutcropConsecutiveSpawnRate = 0.032f;
    private const string OutcropFirstSpawnGoalKey = "InsectoidJumpscared";

    private const float CrateFirstTimeSpawnRate = 0.25f;
    private const float CrateConsecutiveSpawnRate = 0.1f;
    private const string CrateFirstSpawnGoalKey = "InsectoidJumpscaredCrate";

    [HarmonyPrefix]
    [HarmonyPatch(typeof(BreakableResource), nameof(BreakableResource.BreakIntoResources))]
    public static void BreakIntoResourcesPrefix(BreakableResource __instance)
    {
        if (__instance.broken)
            return;
        if (!EvaluateRandom(OutcropFirstSpawnGoalKey, OutcropFirstTimeSpawnRate, OutcropConsecutiveSpawnRate))
            return;
        __instance.StartCoroutine(SpawnInsectoid(__instance.transform.position, __instance.transform.rotation, false,
            OutcropFirstSpawnGoalKey));
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(SupplyCrate), nameof(SupplyCrate.ToggleOpenState))]
    public static void SupplyCrateOpenPostfix(SupplyCrate __instance)
    {
        if (__instance.open && EvaluateRandom(CrateFirstSpawnGoalKey, CrateFirstTimeSpawnRate,
                CrateConsecutiveSpawnRate))
        {
            __instance.StartCoroutine(SpawnInsectoid(__instance.transform.position, __instance.transform.rotation, true,
                CrateFirstSpawnGoalKey));
        }
    }

    private static bool EvaluateRandom(string firstSpawnGoal, float firstTimeSpawnRate, float consecutiveSpawnRate)
    {
        var goalManager = StoryGoalManager.main;
        if (goalManager == null || goalManager.IsGoalComplete(firstSpawnGoal))
            return Random.value < consecutiveSpawnRate;
        return Random.value < firstTimeSpawnRate;
    }

    private static IEnumerator SpawnInsectoid(Vector3 position, Quaternion rotation, bool jump, string goalKey)
    {
        var goalManager = StoryGoalManager.main;
        if (goalManager)
            goalManager.OnGoalComplete(goalKey);
        var task = CraftData.GetPrefabForTechTypeAsync(ModPrefabs.SmallInsectoid.TechType);
        yield return task;
        var insectoid = Object.Instantiate(task.GetResult(), position, rotation);
        var creature = insectoid.GetComponent<Creature>();
        creature.ScanCreatureActions();
        if (creature && creature.GetAnimator())
        {
            for (int i = 0; i < 10; i++)
            {
                creature.ScheduledUpdate();
            }

            creature.GetAnimator().SetTrigger("attack");
        }

        if (jump)
        {
            var jumpComponent = creature.GetComponent<CrawlerJumpRandom>();
            if (jumpComponent && creature)
            {
                jumpComponent.StartPerform(creature, Time.time);
            }
            else
            {
                Plugin.Logger.LogWarning("Insectoid is missing CrawlerJumpRandom or Creature component!");
            }
        }
    }
}