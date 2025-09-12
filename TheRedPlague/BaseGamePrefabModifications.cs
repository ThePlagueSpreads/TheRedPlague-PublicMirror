using System.Collections;
using System.Diagnostics;
using Nautilus.Handlers;
using TheRedPlague.Data;
using TheRedPlague.Mono.InfectionLogic;
using TheRedPlague.Mono.Util;
using TheRedPlague.Mono.VFX;
using TheRedPlague.Utilities;
using UnityEngine;
using UWE;

namespace TheRedPlague;

public static class BaseGamePrefabModifications
{
    private static bool _alreadyModifiedPrefabs;

    public static IEnumerator ModifyBaseGamePrefabs(WaitScreenHandler.WaitScreenTask task)
    {
        if (_alreadyModifiedPrefabs)
        {
            task.Status = "Already modified prefabs";
            yield break;
        }
        task.Status = "Modifying prefabs";
        _alreadyModifiedPrefabs = true;
        yield return MainProcess();
    }

    private static IEnumerator MainProcess()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        yield return ModifyCoralTubes();
        yield return ModifyWreckProps();
        yield return ModifyJellyShrooms();
        yield return ModifyGarryFish();
        yield return ModifyPrawnSuit();
        yield return ModifyMeteorSite();
        yield return ModifyPlantInfection();
        stopwatch.Stop();
        Plugin.Logger.LogInfo($"Finished modifying prefabs ({stopwatch.ElapsedMilliseconds}ms)");
    }

    // Modify coral tubes to remove the two around the elevator platform
    private static IEnumerator ModifyCoralTubes()
    {
        yield return ModifyToDestroyIfIdMatches("f0295655-8f4f-4b18-b67d-925982a472d7", new[]
        {
            "32b32bec-4665-4735-9c1a-7c2c5291a0ee",
            "679e863d-69dc-48b7-af0e-a7c6d311020b"
        });
        
        yield return ModifyToDestroyIfIdMatches("06562999-e575-4b02-b880-71d37616b5b9", new[]
        {
            "5add3650-1466-4b9d-8985-5c3d5b75ff48"
        });
    }

    // Modify wreck props to remove the one clipping into the maze base
    private static IEnumerator ModifyWreckProps()
    {
        var wreckDunes6 = PrefabDatabase.GetPrefabAsync("38f4a1d4-7cbc-4a21-a953-02b3f667975f");
        yield return wreckDunes6;
        if (wreckDunes6.TryGetPrefab(out var wreckDunes6Prefab))
        {
            wreckDunes6Prefab.transform.Find("Interactable/Starship_exploded_debris_41(Placeholder)").gameObject
                .SetActive(false);
        }
        else
        {
            Plugin.Logger.LogError("Could not find WreckDunes6 prefab");
        }
    }

    // Modify jelly shrooms to replace with flesh plant 1
    private static IEnumerator ModifyJellyShrooms()
    {
        yield return ModifyToDestroyIfIdMatches("400fa668-152d-4b81-ad8f-a3cef16efed8", new[]
        {
            "11f224f8-13f2-4f92-b571-16e2386dc368",
            "598cd9ce-8d85-40c0-8af6-197fdfdd9a0a",
            "f40792f9-4a72-4a6f-af07-f1c011b2445c",
            "443026f6-3f48-42d7-b714-e10213d74474",
            "3c78d3be-897a-4f1b-918e-82126e1aeffc",
            "e6f6353c-9857-499e-98b6-b52f1ba5bdc0",
            "7e0b887a-db70-490d-8072-cf51d5db6ee3"
        });

        yield return ModifyToDestroyIfIdMatches("8d0b24b7-c71f-42ab-8df9-7bfe05616ab4", new[]
        {
            "f8751fb6-6996-4935-a203-c1345e131f05"
        });
    }

    // Modify Garry Fish to make them immune
    private static IEnumerator ModifyGarryFish()
    {
        var garryFishTask = PrefabDatabase.GetPrefabAsync("5de7d617-c04c-4a83-b663-ebf1d3dd90a1");
        yield return garryFishTask;
        if (garryFishTask.TryGetPrefab(out var garryFishPrefab))
        {
            garryFishPrefab.EnsureComponent<RedPlagueHost>().mode = RedPlagueHost.Mode.Immune;
        }
        else
        {
            Plugin.Logger.LogError("Could not find Garry Fish prefab");
        }
    }

    // Modify Prawn Suit for custom arms
    private static IEnumerator ModifyPrawnSuit()
    {
        var customArms = CustomExosuitArmUtils.GetCustomExosuitArms();

        if (customArms.Count == 0)
        {
            Plugin.Logger.LogError("No custom arms found! Skipping Prawn Suit modification");
            yield break;
        }

        var successes = 0;
        var failures = 0;

        var exosuitTask = PrefabDatabase.GetPrefabAsync("ba3fb98d-e408-47eb-aa6c-12e14516446b");
        yield return exosuitTask;
        if (exosuitTask.TryGetPrefab(out var exosuitPrefab))
        {
            var exosuit = exosuitPrefab.GetComponent<Exosuit>();
            var oldArmsLength = exosuit.armPrefabs.Length;
            var newArmPrefabsArray = new Exosuit.ExosuitArmPrefab[oldArmsLength + customArms.Count];
            exosuit.armPrefabs.CopyTo(newArmPrefabsArray, 0);

            for (var i = 0; i < customArms.Count; i++)
            {
                var prefabResult = new TaskResult<GameObject>();
                yield return customArms[i].Prefab.Invoke(prefabResult);
                if (prefabResult.value == null)
                {
                    Plugin.Logger.LogError($"Prefab for prawn suit arm {customArms[0].TechType} returned null");
                    failures++;
                    continue;
                }

                newArmPrefabsArray[oldArmsLength + i] = new Exosuit.ExosuitArmPrefab
                {
                    techType = customArms[i].TechType,
                    prefab = prefabResult.value
                };

                successes++;
            }

            exosuit.armPrefabs = newArmPrefabsArray;

            Plugin.Logger.LogInfo($"Added {successes} arms to Prawn Suit ({failures} failed to load)");
        }
        else
        {
            Plugin.Logger.LogError("Could not find Prawn Suit prefab; failed to register custom arms");
        }
    }

    private static IEnumerator ModifyMeteorSite()
    {
        yield return ModifyToDestroyIfIdMatches("25be99bf-267a-4598-ab77-95c0af595ab1",
            new[] { "f9267a0a-e98a-4e5b-a84b-9fa69d6068dd" });
        yield return ModifyToDestroyIfIdMatches("4594f9c0-1b4d-4b10-871d-53950de686fb",
            new[] { "466ea637-0253-4100-8bb6-246895993971" });
        // destroy old plague heart bay wreck which was MOVED
        yield return ModifyToDestroyIfIdMatches("PlagueHeartBayWreck",
            new[] { "ac6ba091-7162-4056-83ac-9aa37ebcf91c" });
    }

    private static IEnumerator ModifyPlantInfection()
    {
        foreach (var plant in PlantInfectionData.InfectablePLants)
        {
            foreach (var classId in plant.PlantClassIDs)
            {
                var task = PrefabDatabase.GetPrefabAsync(classId);
                yield return task;
                if (!task.TryGetPrefab(out var plantPrefab))
                {
                    Plugin.Logger.LogError("Failed to load prefab by ClassID: " + classId);
                    continue;
                }
                var infect = plantPrefab.AddComponent<InfectAnything>();
                infect.infectChance = plant.NormalInfectionChance;
                infect.infectChanceWhenHiveMindIsReleased = plant.InfectionChanceWithHiveMind;
                infect.infectionAmount = 4;
                infect.infectionHeightStrength = plant.InfectionHeight;
                infect.infectionScale = plant.InfectionScale;
                infect.renderers = plantPrefab.GetComponentsInChildren<Renderer>();
                if (plant.OverrideGlowColor.HasValue)
                {
                    infect.overrideGlowColor = true;
                    infect.newGlowColor = plant.OverrideGlowColor.Value;
                }
            }
        }
    }

    private static IEnumerator ModifyToDestroyIfIdMatches(string classId, string[] ids)
    {
        var task = PrefabDatabase.GetPrefabAsync(classId);
        yield return task;
        if (!task.TryGetPrefab(out var prefab))
        {
            Plugin.Logger.LogError("Failed to load prefab by Class ID " + classId);
            yield break;
        }

        prefab.AddComponent<DestroyIfIdMatches>().ids = ids;
    }
}