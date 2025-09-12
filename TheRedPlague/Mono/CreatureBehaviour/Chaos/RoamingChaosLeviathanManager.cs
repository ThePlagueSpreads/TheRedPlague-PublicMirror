using System.Collections;
using System.Collections.Generic;
using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using TheRedPlague.Interfaces;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using UnityEngine;
using UWE;
using Random = UnityEngine.Random;

namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

public class RoamingChaosLeviathanManager : MonoBehaviour, IScheduledUpdateBehaviour, IInfectionTrackerTarget
{
    public static RoamingChaosLeviathanManager Main { get; private set; }

    private static SaveData _data;

    public int scheduledUpdateIndex { get; set; }

    private const float MetersPerSecond = 15f;
    private const float PatrolRadius = 1600f;
    private const float MinDepth = 80f;
    private const float MaxDepth = 140f;
    private const float SpawnDistance = 210f;
    private const float UnloadDistance = 370f;

    private const string ChaosLeviathanClassId = "RoamingChaosLeviathan";

    private static readonly HashSet<RoamingChaos> _leviathans = new();

    private bool _spawningChaosLeviathan;

    private float _timeOfLastFrame;
    
    public static void RegisterSaveData()
    {
        _data = SaveDataHandler.RegisterSaveDataCache<SaveData>();
    }

    public static void RegisterLeviathan(RoamingChaos chaos)
    {
        _leviathans.Add(chaos);
    }

    public static void UnregisterLeviathan(RoamingChaos chaos)
    {
        _leviathans.Remove(chaos);
    }
    
    public static void CreateManagerIfNoneExists()
    {
        if (Main != null)
            return;
        
        new GameObject("RoamingChaosLeviathanManager").AddComponent<RoamingChaosLeviathanManager>();
    }

    private void Start()
    {
        if (Main != null)
        {
            Plugin.Logger.LogWarning("Multiple RoamingChaosLeviathanManagers in scene!");
            Destroy(gameObject);
            return;
        }
        
        Main = this;
        _timeOfLastFrame = Time.time;
        UpdateSchedulerUtils.Register(this);
        InfectionTargetRegistry.RegisterTarget(this);
    }

    private void OnDestroy()
    {
        UpdateSchedulerUtils.Deregister(this);
        InfectionTargetRegistry.UnregisterTarget(this);
    }

    public void ScheduledUpdate()
    {
        var deltaTime = Time.time - _timeOfLastFrame;
        _timeOfLastFrame = Time.time;

        if (_spawningChaosLeviathan)
            return;

        if (!IsChaosLeviathanLoaded())
        {
            _data.LastAngle += GetAngleRateOfChange() * deltaTime;

            if (InSpawningRange())
            {
                StartCoroutine(RecreateChaosLeviathan());
            }

            return;
        }

        _data.LastAngle = GetAngleFromPosition(GetChaosLeviathan().transform.position);

        if (ShouldUnloadChaosLeviathan())
        {
            var chaos = GetChaosLeviathan();
            _leviathans.Remove(chaos);
            if (chaos != null)
            {
                Destroy(chaos.gameObject);   
            }
        }
    }

    private IEnumerator RecreateChaosLeviathan()
    {
        _spawningChaosLeviathan = true;
        
        var task = PrefabDatabase.GetPrefabAsync(ChaosLeviathanClassId);
        yield return task;
        
        if (task.TryGetPrefab(out var chaosLeviathanPrefab))
        {
            var spawnAngle = _data.LastAngle;
            var chaos = Instantiate(chaosLeviathanPrefab, GetRandomSpawnPosition(spawnAngle), Quaternion.identity);
            chaos.transform.forward = GetSpawnDirection(spawnAngle);
        }
        else
        {
            Plugin.Logger.LogWarning("Prefab is null for ClassID: " + ChaosLeviathanClassId);
        }

        _spawningChaosLeviathan = false;
    }

    private static bool InSpawningRange()
    {
        var expectedSpawnPosition = GetAssumedPosition();
        return Vector3.SqrMagnitude(MainCamera.camera.transform.position - expectedSpawnPosition) <
               SpawnDistance * SpawnDistance;
    }

    private static bool ShouldUnloadChaosLeviathan()
    {
        var chaosLeviathan = GetChaosLeviathan();
        return chaosLeviathan == null ||
               Vector3.SqrMagnitude(chaosLeviathan.transform.position - MainCamera.camera.transform.position) >
               UnloadDistance * UnloadDistance;
    }

    private static RoamingChaos GetChaosLeviathan()
    {
        return _leviathans.FirstOrFallback(null);
    }

    private static float GetAngleRateOfChange()
    {
        return MetersPerSecond / PatrolRadius;
    }

    private static bool IsChaosLeviathanLoaded()
    {
        return _leviathans.Count > 0 && _leviathans.FirstOrFallback(null) != null;
    }

    private static Vector3 GetPositionFromAngle(float radians, float y)
    {
        return new Vector3(Mathf.Cos(radians) * PatrolRadius, y, Mathf.Sin(radians) * PatrolRadius);
    }

    private static Vector3 GetRandomSpawnPosition(float angle)
    {
        return GetPositionFromAngle(angle, -Random.Range(MinDepth, MaxDepth));
    }

    private static Vector3 GetSpawnDirection(float angle)
    {
        return new Vector3(-Mathf.Sin(angle), 0, Mathf.Cos(angle)).normalized;
    }

    private static Vector3 GetAssumedPosition()
    {
        return GetPositionFromAngle(_data.LastAngle, -(MaxDepth + MinDepth) / 2f);
    }

    public static Vector3 GetFutureSwimPosition(Vector3 currentPosition, float metersAhead)
    {
        var angle = GetAngleFromPosition(currentPosition) + metersAhead / PatrolRadius;
        return GetPositionFromAngle(angle, currentPosition.y);
    }

    private static float GetAngleFromPosition(Vector3 position)
    {
        return Mathf.Atan2(position.z, position.x);
    }

    public string GetProfileTag()
    {
        return "TRP:RoamingChaosLeviathanManager";
    }

    [FileName("RoamingChaosLeviathanManager")]
    private class SaveData : SaveDataCache
    {
        public float LastAngle;
    }

    public Vector3 GetTargetPosition()
    {
        return GetAssumedPosition();
    }

    public int GetTrackingPriority()
    {
        if (IsChaosLeviathanLoaded())
            return 0;

        return 5;
    }
}