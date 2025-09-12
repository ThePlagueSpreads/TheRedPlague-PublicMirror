using System.Collections;
using System.Collections.Generic;
using ECCLibrary;
using Nautilus.Utility;
using TheRedPlague.Mono.CreatureBehaviour;
using TheRedPlague.Mono.CreatureBehaviour.Chaos;
using TheRedPlague.Mono.VFX;
using TheRedPlague.PrefabFiles.Creatures;
using TheRedPlague.Utilities;
using UnityEngine;
using UWE;
using Random = UnityEngine.Random;

namespace TheRedPlague.Mono.CinematicEvents;

public class ChaosLeviathanHatch : MonoBehaviour
{
    private static readonly MergePoint[] MergePoints =
    {
        new(new Vector3(-1107.930f, -369.539f, 1115.909f), new Vector3(-0.006f, 0.714f, 0.700f), 14f, 0.7f),
        new(new Vector3(-1109.385f, -345.635f, 1141.109f), new Vector3(-0.379f, 0.598f, 0.705f), 17.7f, 0.8f),
        new(new Vector3(-1098, -314.564f, 1115), new Vector3(0.737f, 0.205f, 0.643f), 26, 1.1f),
        new(new Vector3(-1136.003f, -277.509f, 1172.691f), new Vector3(-0.82105f, 0.4592f, -0.339f), 33f, 1.2f),
        new(new Vector3(-1172.740f, -193.247f, 1144.170f), new Vector3(-0.464f, 0.853f, -0.235f), 37.16f, 2f)
    };

    private static readonly string[] CreaturesToAssimilate =
    {
        "MimicPeeper",
        "MimicOculus",
        "FleshStalker",
        "PlagueBladderFish",
        "InvertedSpadefish",
        "MutantBoomerang",
        "MimicGasopod",
        "TeethTeeth",
        "EyeyeCaptain"
    };

    private GameObject _leviathan;

    private float _timeSequenceStarted;

    private ChaosTrailManagerManager _chaosTrailManager;

    private float TimePassed => Time.time - _timeSequenceStarted;

    private const float StartAssimilationAheadOfTimeSeconds = 6;
    private const float SpawnRingAheadOfTimeSeconds = 2;
    private const float FadeAheadOfTimeSeconds = 0.5f;
    private const float FadeDuration = 0.6f;
    private const float FadeInDuration = 0.4f;
    private const float FadeOutDuration = 0.3f;
    private const float RingLifetime = 4;
    private const float RingFadeDuration = 1f;
    private const float UpdateTrailManagerDelay = 0.5f;
    private const int MinAssimilationCreatures = 4;
    private const int MaxAssimilationCreatures = 6;

    private GameObject _ringPrefab;
    private GameObject _bloodPrefab;

    private static readonly Vector3 ChaosSpawnPosition = new Vector3(-1173, -175, 1139);
    private static readonly Vector3 ChaosSpawnForward = new Vector3(-0.7383f, 0.6578f, 0.1485f);

    public static IEnumerator PlaySequence(Transform plagueHeartTransform, GameObject chaosPrefab)
    {
        var obj = new GameObject("ChaosLeviathanHatch");
        var hatch = obj.AddComponent<ChaosLeviathanHatch>();

        var chaos = Instantiate(chaosPrefab,
            plagueHeartTransform.position, Quaternion.identity);
        chaos.SetActive(false);
        chaos.AddComponent<SkyApplier>().renderers = chaos.GetComponentsInChildren<Renderer>();
        ChaosLeviathanPrefab.ApplyChaosLeviathanMaterials(chaos);
        hatch._leviathan = chaos;

        var lod = chaos.AddComponent<BehaviourLOD>();
        lod.veryCloseThreshold = 200;
        lod.closeThreshold = 500;
        lod.farThreshold = 1000;

        var trails = new List<TrailManager>();

        var curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        var spineRoot = chaos.transform.Find("ChaosEndCinematic/ChaosArmature/Root/Spine");
        var mainTrailManager = new TrailManagerBuilder(chaos.transform, lod, spineRoot, 5.6f);
        mainTrailManager.SetTrailArrayToChildrenWithCondition(t => t.gameObject.name.StartsWith("Spine"));
        mainTrailManager.AllowDisableOnScreen = false;
        mainTrailManager.SetAllMultiplierAnimationCurves(curve);
        trails.Add(mainTrailManager.Apply());

        var pelvisRoot = spineRoot.transform.SearchChild("Pelvis");

        foreach (Transform child in pelvisRoot)
        {
            var trailManagerRoot = child.GetChild(0);
            if (!trailManagerRoot.gameObject.name.StartsWith("Tentacle"))
                continue;
            var tentacleTrail = new TrailManagerBuilder(chaos.transform, lod, trailManagerRoot, 1f);
            tentacleTrail.SetTrailArrayToAllChildren();
            tentacleTrail.AllowDisableOnScreen = false;
            tentacleTrail.MaxSegmentOffset = 2;
            tentacleTrail.SetAllMultiplierAnimationCurves(curve);
            trails.Add(tentacleTrail.Apply());
        }

        var eyeSpawner = chaos.AddComponent<ChaosSpawnRandomEyes>();
        var spineBones = mainTrailManager.Trails;
        eyeSpawner.bones = spineBones;
        eyeSpawner.eyesPerSpawnMin = 2;
        eyeSpawner.eyesPerSpawnMax = 4;

        var trailManagerManager = chaos.AddComponent<ChaosTrailManagerManager>();
        trailManagerManager.trails = trails.ToArray();
        trailManagerManager.dataAsset = Plugin.CreaturesBundle.LoadAsset<TextAsset>("ChaosTrailManagers");
        trailManagerManager.armatureRoot = chaos.transform.Find("ChaosEndCinematic/ChaosArmature/Root");
        trailManagerManager.modelObject = chaos.transform.Find("ChaosEndCinematic/Chaos-Low");
        hatch._chaosTrailManager = trailManagerManager;

        var ring = Instantiate(Plugin.CreaturesBundle.LoadAsset<GameObject>("ChaosLeviathanRingPrefab"),
            chaos.transform);
        MaterialUtils.ApplySNShaders(ring, 7);
        ring.AddComponent<SkyApplier>().renderers = ring.GetComponentsInChildren<Renderer>(true);
        hatch._ringPrefab = ring;
        ring.SetActive(false);

        foreach (var trail in trails)
        {
            trail.enabled = false;
        }
        
        var roar = chaos.AddComponent<ChaosLeviathanRoar>();
        roar.closeRoarLong = ChaosLeviathanPrefab.CloseRoarLong;
        roar.farRoarLong = ChaosLeviathanPrefab.FarRoarLong;
        roar.closeRoarShort = ChaosLeviathanPrefab.CloseRoarShort;
        roar.farRoarShort = ChaosLeviathanPrefab.FarRoarShort;
        roar.minInterval = 8;
        roar.maxInterval = 10;
        var roarEmitter = chaos.AddComponent<FMOD_CustomEmitter>();
        roarEmitter.followParent = true;
        roarEmitter.restartOnPlay = true;
        roarEmitter.playOnAwake = false;
        roar.emitter = roarEmitter;
        roar.animator = chaos.GetComponentInChildren<Animator>();
        roar.playSoundOnStart = false;

        chaos.SetActive(true);

        hatch.StartCoroutine(hatch.LoadBlood());

        yield return hatch.EventCoroutine();
    }

    private IEnumerator LoadBlood()
    {
        var taskResult = new TaskResult<GameObject>();
        yield return BloodFxUtils.GetRedBloodFx(taskResult);
        _bloodPrefab = taskResult.value;
    }

    private IEnumerator EventCoroutine()
    {
        _timeSequenceStarted = Time.time;
        var animator = _leviathan.transform.Find("ChaosEndCinematic").GetComponent<Animator>();
        animator.SetBool("hatch", true);

        yield return new WaitForSeconds(5);
        

        foreach (var mergePoint in MergePoints)
        {
            StartCoroutine(HandleMergePoint(mergePoint));
        }

        _chaosTrailManager.SetTrailsActive(true);
        _chaosTrailManager.UpdateTrails();

        yield return new WaitForSeconds(1);
        
        animator.SetBool("swimming", true);

        yield return new WaitForSeconds(31.5f);

        yield return SpawnRoamingChaosLeviathan();

        Destroy(gameObject);
    }

    private IEnumerator HandleMergePoint(MergePoint point)
    {
        yield return new WaitUntil(() => TimePassed > point.Time - StartAssimilationAheadOfTimeSeconds);
        var creatureCount = Random.Range(MinAssimilationCreatures, MaxAssimilationCreatures + 1);
        for (int i = 0; i < creatureCount; i++)
        {
            StartCoroutine(SpawnCreatureAroundMergePoint(
                CreaturesToAssimilate[Random.Range(0, CreaturesToAssimilate.Length)], point));
        }

        StartCoroutine(SpawnRing(point));

        yield return new WaitUntil(() => TimePassed > point.Time - FadeAheadOfTimeSeconds);
        FadingOverlay.PlayFX(Color.black, FadeInDuration, FadeDuration, FadeOutDuration);

        yield return new WaitUntil(() => TimePassed >= point.Time);
        yield return new WaitForSeconds(UpdateTrailManagerDelay);
        _chaosTrailManager.UpdateTrails();
    }

    private IEnumerator SpawnCreatureAroundMergePoint(string classId, MergePoint point)
    {
        var task = PrefabDatabase.GetPrefabAsync(classId);
        yield return task;
        if (task.TryGetPrefab(out var prefab))
        {
            var creature = UWE.Utils.InstantiateDeactivated(prefab);
            creature.transform.position = point.WorldPosition + Random.onUnitSphere * 10;
            creature.transform.LookAt(point.WorldPosition);
            var swimToPoint = creature.AddComponent<ForceSwimToPoint>();
            swimToPoint.evaluatePriority = 1000;
            swimToPoint.point = point.WorldPosition;
            var swimRandom = creature.GetComponent<SwimRandom>();
            swimToPoint.velocity = swimRandom ? swimRandom.swimVelocity + 2 : 5;
            var destroy = creature.AddComponent<CreatureDestroyWhenNearEpicenter>();
            destroy.point = point.WorldPosition;
            destroy.bloodPrefab = _bloodPrefab;
            creature.SetActive(true);
            Destroy(creature, 20);
        }
        else
        {
            Plugin.Logger.LogWarning("Could not find prefab for Class ID " + classId);
        }
    }

    private IEnumerator SpawnRing(MergePoint point)
    {
        yield return new WaitUntil(() => TimePassed > point.Time - SpawnRingAheadOfTimeSeconds);
        var ring = Instantiate(_ringPrefab, point.WorldPosition, Quaternion.identity);
        ring.transform.forward = point.ForwardDirection;
        ring.transform.localScale = Vector3.one * point.RingScale;
        var fade = ring.AddComponent<FadeInAndOut>();
        fade.renderers = ring.GetComponentsInChildren<MeshRenderer>(true);
        fade.lifetime = RingLifetime;
        fade.fadeTime = RingFadeDuration;
        ring.SetActive(true);
    }

    private IEnumerator SpawnRoamingChaosLeviathan()
    {
        var task = PrefabDatabase.GetPrefabAsync("RoamingChaosLeviathan");
        yield return task;
        if (!task.TryGetPrefab(out var prefab))
        {
            Plugin.Logger.LogWarning("Could not find RoamingChaosLeviathan Prefab");
        }
        else
        {
            var creature = Instantiate(prefab);
            creature.transform.position = ChaosSpawnPosition;
            creature.transform.forward = ChaosSpawnForward.normalized;
        }
    }

    private void OnDestroy()
    {
        Destroy(_leviathan);
    }

    private class MergePoint
    {
        public Vector3 WorldPosition { get; }
        public Vector3 ForwardDirection { get; }
        public float Time { get; }
        public float RingScale { get; }

        public MergePoint(Vector3 worldPosition, Vector3 forwardDirection, float time, float ringScale)
        {
            WorldPosition = worldPosition;
            ForwardDirection = forwardDirection.normalized;
            Time = time;
            RingScale = ringScale;
        }
    }

    private class CreatureDestroyWhenNearEpicenter : MonoBehaviour
    {
        public Vector3 point;
        public GameObject bloodPrefab;

        private bool _done;

        private void Update()
        {
            if (_done) return;
            if (Vector3.SqrMagnitude(point - transform.position) < 1f)
            {
                for (int i = 0; i < 30; i++)
                {
                    Instantiate(bloodPrefab, transform.position + Random.onUnitSphere, Quaternion.identity);
                }

                Destroy(gameObject, 0.5f);
                _done = true;
            }
        }
    }
}