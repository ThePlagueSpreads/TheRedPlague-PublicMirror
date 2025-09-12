using System.Collections;
using TheRedPlague.Managers;
using TheRedPlague.Mono.Util;
using UnityEngine;

namespace TheRedPlague.Mono.Insanity.Symptoms;

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

    private readonly TechType[] _shallowFishTechTypes =
    {
        TechType.CrabSquid,
        TechType.Warper,
        TechType.Warper,
        TechType.BoneShark,
        TechType.Stalker,
        TechType.Sandshark,
        TechType.Crash
    };
    
    private readonly TechType[] _deepFishTechTypes =
    {
        TechType.SpineEel,
        TechType.CrabSquid,
        TechType.Warper,
        TechType.Warper,
        TechType.Shocker,
        TechType.BoneShark
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
            StartCoroutine(SpawnFishAsync(GetRandomFishTechType(), spawnLocation));
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

    private TechType GetRandomFishTechType()
    {
        if (Player.main.transform.position.y < -MutantDiversMinDepth && Random.value < MutantDiversSpawnChance)
        {
            return Random.value > 0.5f ? ModPrefabs.MutantDiver1.TechType : ModPrefabs.MutantDiver2.TechType;
        }

        var playerDepth = Ocean.GetDepthOf(Player.main.gameObject);
        var fishArray = playerDepth > DeepFishMinDepth ? _deepFishTechTypes : _shallowFishTechTypes;
        return fishArray[Random.Range(0, fishArray.Length)];
    }

    private static IEnumerator SpawnFishAsync(TechType techType, Vector3 location)
    {
        var task = CraftData.GetPrefabForTechTypeAsync(techType);
        yield return task;
        var result = task.GetResult();
        if (!result) yield break;
        var fish = Instantiate(result, location, Quaternion.identity);
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