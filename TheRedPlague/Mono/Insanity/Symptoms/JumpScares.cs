using System;
using System.Collections;
using Nautilus.Utility;
using TheRedPlague.Mono.Util;
using TheRedPlague.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Mono.Insanity.Symptoms;

// har har har har har har har har har har  har har har har har har har har
public class JumpScares : InsanitySymptom
{
    private const float MinDelay = 60 * 3;
    private const float MaxDelay = 60 * 7;
    private const float Radius = 34;
    private const float MinInsanity = 20;
    private float _timeSpawnAgain;

    private static readonly FMODAsset JumpscareSound = AudioUtils.GetFmodAsset("WarperJumpscare");
    private static readonly FMODAsset SkeletonAmbience = AudioUtils.GetFmodAsset("SkeletonAmbience");

    private readonly string[] _corpseClassIDs = {"InfectedCorpse", "SkeletonCorpse"};

    public static JumpScares main;

    private GameObject _warpOutEffect;

    private Vector3 _lastSpawnPos;

    private void Awake()
    {
        main = this;
    }

    public void JumpScareNow()
    {
        JumpScare();
    }

    private void Start()
    {
        _timeSpawnAgain = Time.time + Random.Range(MinDelay, MaxDelay);
    }

    private GameObject Spawn()
    {
        var model = Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("SharkJumpscare"));
        MaterialUtils.ApplySNShaders(model);
        var despawn = model.AddComponent<DespawnWhenOffScreen>();
        despawn.despawnIfTooClose = true;
        despawn.minDistance = 4;
        despawn.waitUntilSeen = true;
        despawn.moveInstead = true;
        despawn.moveRadius = 35f;
        despawn.disappearWhenLookedAtForTooLong = true;
        despawn.jumpscareWhenTooClose = true;
        despawn.OnDestroyCallback = OnMutantDestroyed;
        model.AddComponent<Util.LookAtPlayer>();
        if (Player.main.IsInBase())
        {
            model.transform.localScale *= 0.7f;
        }
        return model;
    }

    private void OnMutantDestroyed()
    {
        if (_warpOutEffect != null)
        {
            Instantiate(_warpOutEffect, _lastSpawnPos + Vector3.up, Quaternion.identity);
        }
    }

    private IEnumerator SpawnCorpse(Vector3 pos)
    {
        var classID = _corpseClassIDs[Random.Range(0, _corpseClassIDs.Length)];
        var task = UWE.PrefabDatabase.GetPrefabAsync(classID);
        yield return task;
        task.TryGetPrefab(out var prefab);
        var spawned = Instantiate(prefab, pos, Random.rotation);
        spawned.SetActive(true);
        var despawn = spawned.AddComponent<DespawnWhenOffScreen>();
        despawn.initialDelay = 20;
        despawn.waitUntilSeen = true;
        despawn.despawnIfTooClose = true;
        despawn.minDistance = 3;
        despawn.jumpscareWhenTooClose = true;
        despawn.rareJumpscare = true;
        Destroy(spawned.GetComponent<PrefabIdentifier>());
        var entity = spawned.GetComponent<LargeWorldEntity>();
        LargeWorld.main.streamer.cellManager.UnregisterEntity(entity);
        Destroy(entity);
        Destroy(spawned, 300);
    }

    private void Update()
    {
        if (Time.time > _timeSpawnAgain)
        {
            _timeSpawnAgain = Time.time + Random.Range(MinDelay, MaxDelay);
            JumpScare();
        }
    }

    private void JumpScare()
    {
        if (Plugin.Options.DisableJumpScares) return;
        if (!IsSymptomActive) return;
        var jumpscarePosition = MainCamera.camera.transform.position;
        if (GenericTrpUtils.TryGetSpawnPositionOnGround(out jumpscarePosition, Radius, 50, Radius / 2f))
        {
            var model = Spawn();
            model.transform.position = jumpscarePosition;
            _lastSpawnPos = jumpscarePosition;
            var diff = jumpscarePosition - Player.main.transform.position;
            diff.y = 0;
            model.transform.forward = diff.normalized;
            if (InsanityPercentage >= 45)
            {
                StartCoroutine(SpawnCorpse(jumpscarePosition + Vector3.up * 2));
            }
            
            if (Random.value < 0.4f && !StoryUtils.IsAct1Complete())
            {
                Utils.PlayFMODAsset(JumpscareSound, jumpscarePosition);
            }
            else
            {
                Utils.PlayFMODAsset(SkeletonAmbience, jumpscarePosition);
            }
        }
    }

    protected override IEnumerator OnLoadAssets()
    {
        var warperTask = CraftData.GetPrefabForTechTypeAsync(TechType.Warper);
        yield return warperTask;
        var warper = warperTask.GetResult();
        if (warper == null)
        {
            Plugin.Logger.LogWarning("Failed to load warper prefab");
            yield break;
        }

        try
        {
            var warperComponent = warper.GetComponent<Warper>();
            _warpOutEffect = warperComponent.warpOutEffectPrefab;
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError(e);
        }
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