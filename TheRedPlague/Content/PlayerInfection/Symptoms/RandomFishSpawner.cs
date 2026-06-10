using System.Collections;
using TheRedPlague.Content.Infection;
using TheRedPlague.Framework.Behaviour.Horror;
using UnityEngine;
using UWE;

namespace TheRedPlague.Content.PlayerInfection.Symptoms;

public class RandomFishSpawner : InsanitySymptom
{
    private const float MinIntervalMinutes = 5;
    private const float MaxIntervalMinutes = 7;
    private const float MinDepthToSpawn = 10;
    private const float MinInsanity = 20;
    private const float Chance = 0.6f;
    private const float MutantDiversMinDepth = 300;
    private const float MutantDiversSpawnChance = 0.2f;
    private const float DeepFishMinDepth = 500;
    private const float RaycastReverseDistance = 1.5f;
    // relative to the raycast start
    private const float MinDistance = 7;
    private const float MaxDistance = 16;
    private const float RandomMaxRadius = 0.7f;
    private const float CheckSphereRadius = 1.5f;
    private const float DistanceFromWalls = 3;

    private float _timeJumpScareAgain;

    private readonly string[] _shallowFishClassIds =
    {
        "4c2808fe-e051-44d2-8e64-120ddcdc8abb", // Crabsquid
        "c7103527-f6fa-4d1e-a75d-146433851557", // Warper
        "66072588-f5aa-4a41-a8d4-bb7e8dffee51", // Boneshark
        "cf8794a1-5cd6-492e-8acf-7da7c940ef70", // Stalker
        "5e5f00b4-1531-45c0-8aca-84cbd3b580a4", // Sandshark
        "7d307502-46b7-4f86-afb0-65fe8867f893" // Crashfish
    };
    
    private readonly string[] _deepFishClassIds =
    {
        "e82d3c24-5a58-4307-a775-4741050c8a78", // River prowler
        "4c2808fe-e051-44d2-8e64-120ddcdc8abb", // Crabsquid
        "c7103527-f6fa-4d1e-a75d-146433851557", // Warper
        "c7103527-f6fa-4d1e-a75d-146433851557", // Warper
        "e69be2e8-a2e3-4c4c-a979-281fbf221729", // Ampeel
        "66072588-f5aa-4a41-a8d4-bb7e8dffee51" // Boneshark
    };

    private static bool CanJumpScare()
    {
        return Player.main.IsSwimming() && !Player.main.IsInside() && !Player.main.justSpawned &&
               Ocean.GetDepthOf(Player.main.gameObject) > MinDepthToSpawn;
    }

    private bool EvaluateRandom()
    {
        return Random.value < Chance;
    }

    protected override void PerformSymptoms(float dt)
    {
        if (Time.time < _timeJumpScareAgain)
        {
            return;
        }

        if (EvaluateRandom() && CanJumpScare())
        {
            JumpScare();
        }

        _timeJumpScareAgain = Time.time + Random.Range(MinIntervalMinutes * 60, MaxIntervalMinutes * 60);
    }

    private void JumpScare()
    {
        if (TryGetSpawnLocation(out var spawnLocation))
        {
            StartCoroutine(SpawnFishAsync(GetRandomFishClassId(), spawnLocation));
        }
    }

    private static bool TryGetSpawnLocation(out Vector3 spawnLocation)
    {
        var cameraTransform = MainCamera.camera.transform;
        var raycastCenter = cameraTransform.position - cameraTransform.forward * RaycastReverseDistance;
        float distance;
        if (Physics.Raycast(raycastCenter, - cameraTransform.forward, out var hit,
                MaxDistance, -1, QueryTriggerInteraction.Ignore))
        {
            distance = hit.distance - DistanceFromWalls;
            if (distance < MinDistance)
            {
                spawnLocation = default;
                return false;
            }
        }
        else
        {
            distance = MaxDistance;
        }
        spawnLocation = cameraTransform.position - cameraTransform.forward * distance + Random.insideUnitSphere * RandomMaxRadius;
        return !Physics.CheckSphere(spawnLocation, CheckSphereRadius, -1, QueryTriggerInteraction.Ignore);
    } 

    private string GetRandomFishClassId()
    {
        if (Player.main.transform.position.y < -MutantDiversMinDepth && Random.value < MutantDiversSpawnChance)
        {
            return Random.value > 0.5f ? "MutantDiver1" : "MutantDiver2";
        }

        var playerDepth = Ocean.GetDepthOf(Player.main.gameObject);
        var fishArray = playerDepth > DeepFishMinDepth ? _deepFishClassIds : _shallowFishClassIds;
        return fishArray[Random.Range(0, fishArray.Length)];
    }

    private static IEnumerator SpawnFishAsync(string classId, Vector3 location)
    {
        var task = PrefabDatabase.GetPrefabAsync(classId);
        yield return task;
        if (!task.TryGetPrefab(out var fishPrefab) || fishPrefab == null)
        {
            Plugin.Logger.LogError("Failed to load fish prefab for ClassID " + classId);
            yield break;
        }
        var fish = Instantiate(fishPrefab, location, Quaternion.identity);
        fish.SetActive(true);
        ZombieManager.Zombify(fish);
        var despawn = fish.AddComponent<DespawnWhenOffScreen>();
        despawn.initialDelay = 20f;
        // fish.AddComponent<PlaySoundWhenSeen>();
    }

    protected override IEnumerator OnLoadAssets()
    {
        yield break;
    }

    protected override void OnActivate()
    {
    }

    protected override void OnDeactivate()
    {
    }

    protected override bool ShouldDisplaySymptoms()
    {
        return InsanityPercentage >= MinInsanity;
    }
}