using Nautilus.Utility;
using TheRedPlague.Mono.Util;
using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

public class ChaosSpawnRandomEyes : ScheduledEventBase
{
    public Transform[] bones;
    public float minRadiusAroundBones = 15;
    public float maxRadiusAroundBones = 40;
    public float maxDistanceForCollisionCheck = 55;
    public float collisionCheckRadius = 1f;
    public float maxDistanceToSpawn = 100;

    public int eyesPerSpawnMin = 1;
    public int eyesPerSpawnMax = 2;
    public float spawnIntervalMin = 2;
    public float spawnIntervalMax = 5;

    public float minTimeBetweenSounds = 8f;
    private static readonly FMODAsset SpawnSound = AudioUtils.GetFmodAsset("TrpCreepySing");

    private float _timeCanSpawnAgain;

    protected override void PerformAction()
    {
        var manager = ChaosEyeHallucinationManager.instance;
        if (manager == null)
        {
            Plugin.Logger.LogWarning("ChaosEyeHallucinationManager instance is null!");
            return;
        }

        var amount = Random.Range(eyesPerSpawnMin, eyesPerSpawnMax + 1);
        for (var i = 0; i < amount; i++)
        {
            var position = bones[Random.Range(0, bones.Length)].position +
                           Random.onUnitSphere * Random.Range(minRadiusAroundBones, maxRadiusAroundBones);
            
            var sqrDistanceToPlayer = Vector3.SqrMagnitude(Player.main.transform.position - position);
            
            if (sqrDistanceToPlayer > maxDistanceToSpawn * maxDistanceToSpawn)
            {
                continue;
            }

            if (sqrDistanceToPlayer < maxDistanceForCollisionCheck * maxDistanceForCollisionCheck &&
                Physics.CheckSphere(position, collisionCheckRadius, -1, QueryTriggerInteraction.Ignore))
            {
                continue;
            }

            manager.CreateEye(position);
            TryPlaySound(position);
        }
    }

    protected override float GetDelay()
    {
        return Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    private void TryPlaySound(Vector3 position)
    {
        if (Time.time < _timeCanSpawnAgain)
            return;
        _timeCanSpawnAgain = Time.time + minTimeBetweenSounds;
        Utils.PlayFMODAsset(SpawnSound, position);
    }
}