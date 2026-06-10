using System.Collections;
using Nautilus.Utility;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.Behaviour.Deletion;
using TheRedPlague.Framework.CreatureBehaviours;
using TheRedPlague.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Content.Environment.Blister;

public class ExplodingBlister : MonoBehaviour, IScheduledUpdateBehaviour
{
    public string explodeTriggerParameterName = "explode";
    public string respawnTriggerParameterName = "respawn";

    public float spawnReadyChance;
    public float respawnDelay;
    public float minExplodeDelay;
    public float maxExplodeDelay;
    public bool respawn;

    public float formationDuration = 5f;
    public float popDuration = 1.958f;
    public float deflationDuration = 4.375f;

    public float spawnMysteriousRemainsChance = 0.25f;
    public float spawnMysteriousRemainsMaxDistance = 40f;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Renderer[] renderers;
    [HideInInspector] public DamageInRange damager;

    private float _timeNextEvent;
    private State _state;
    private bool _killed;
    private bool _initializedState;
    private int _lastExplosionFrame;

    private GameObject _explodeVfx;

    public int scheduledUpdateIndex { get; set; }

    private static FMODAsset _sound = AudioUtils.GetFmodAsset("PustuleExplosion");

    public static ExplodingBlister SetUpBlister(GameObject root, GameObject prefab, float spawnReadyChance,
        float damageRadius,
        float normalDamage, float plagueDamage, float respawnDelay, float minExplodeDelay, float maxExplodeDelay,
        bool respawn)
    {
        var component = prefab.AddComponent<ExplodingBlister>();
        component.spawnReadyChance = spawnReadyChance;
        component.respawnDelay = respawnDelay;
        component.minExplodeDelay = minExplodeDelay;
        component.maxExplodeDelay = maxExplodeDelay;
        component.respawn = respawn;

        component.animator = prefab.GetComponentInChildren<Animator>();
        component.renderers = prefab.GetComponentsInChildren<Renderer>();
        component.SetRenderersActive(false);

        var damageInRange = prefab.transform.Find("DamageCenter").gameObject.AddComponent<DamageInRange>();
        damageInRange.dealerRoot = root;
        damageInRange.normalDamage = normalDamage;
        damageInRange.plagueDamage = plagueDamage;
        damageInRange.damageRadius = damageRadius;
        damageInRange.canDamageSubmarines = false;
        component.damager = damageInRange;

        prefab.transform.Find("ExplodeTrigger").gameObject.AddComponent<ExplodingBlisterTrigger>().blister = component;

        return component;
    }

    private void Start()
    {
        var spawnActive = spawnReadyChance > 0 && Random.value < spawnReadyChance;
        if (spawnActive)
        {
            SetState(State.Forming);
        }
        else
        {
            SetState(State.Dead);
        }

        UpdateSchedulerUtils.Register(this);

        StartCoroutine(LoadExplodeVfx());
    }

    private IEnumerator LoadExplodeVfx()
    {
        var taskResult = new TaskResult<GameObject>();
        yield return BloodFxUtils.GetRedBloodFx(taskResult);
        var obj = Instantiate(taskResult.Get(), transform, true);
        var systems = obj.GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in systems)
        {
            var emit = ps.emission;
            emit.rateOverTime = 0;
            emit.burstCount = 1;
            emit.SetBurst(0, new ParticleSystem.Burst
            {
                time = 0,
                cycleCount = 1,
                probability = 1,
                maxCount = 10,
                count = 10,
                minCount = 10
            });
            var main = ps.main;
            main.scalingMode = ParticleSystemScalingMode.Hierarchy;
        }

        _explodeVfx = obj;
        _explodeVfx.transform.localScale = Vector3.one * 15f;
    }

    public void ScheduledUpdate()
    {
        if (_killed)
            return;

        if (Time.time > _timeNextEvent)
        {
            SetState(GetNextState(_state));
        }
    }

    private void OnDestroy()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    private void SetRenderersActive(bool active)
    {
        foreach (var r in renderers)
            r.enabled = active;
    }

    private static State GetNextState(State previous)
    {
        return previous switch
        {
            State.Dead => State.Forming,
            State.Forming => State.Idle,
            State.Idle => State.Exploding,
            State.Exploding => State.Deflating,
            State.Deflating => State.Dead,
            // Should not occur
            _ => State.Dead
        };
    }

    private void SetState(State state)
    {
        float delay;
        switch (state)
        {
            case State.Dead:
                if (!_initializedState)
                {
                    delay = respawnDelay * Random.value;
                }
                else
                {
                    delay = respawnDelay;
                    if (!respawn)
                    {
                        _killed = true;
                    }
                }

                SetRenderersActive(false);
                break;
            case State.Forming:
                delay = formationDuration;
                animator.SetTrigger(respawnTriggerParameterName);
                SetRenderersActive(true);
                break;
            case State.Idle:
                delay = Random.Range(minExplodeDelay, maxExplodeDelay);
                break;
            case State.Exploding:
                delay = popDuration;
                animator.SetTrigger(explodeTriggerParameterName);
                StartCoroutine(ExplodeCoroutine());
                break;
            case State.Deflating:
                delay = deflationDuration;
                if (!respawn)
                {
                    _killed = true;
                }

                break;
            // Should not occur
            default:
                Plugin.Logger.LogWarning("Unknown state: " + _state);
                delay = 1f;
                break;
        }

        _timeNextEvent = Time.time + delay;
        _initializedState = true;
        _state = state;
    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(popDuration);
        ExplodeNow();
    }

    private void ExplodeNow()
    {
        if (_lastExplosionFrame == Time.frameCount)
            return;

        _lastExplosionFrame = Time.frameCount;
        DamageInRange();
        CreateExplosionFX();
        if (spawnMysteriousRemainsChance > 0
            && Vector3.SqrMagnitude(MainCamera.camera.transform.position - transform.position) <
            spawnMysteriousRemainsMaxDistance * spawnMysteriousRemainsMaxDistance
            && Random.value < spawnMysteriousRemainsChance)
            StartCoroutine(SpawnMysteriousRemains());
    }

    private IEnumerator SpawnMysteriousRemains()
    {
        var task = CraftData.GetPrefabForTechTypeAsync(MysteriousRemains.Info.TechType);
        yield return task;
        var mysteriousRemains = Instantiate(task.GetResult(), transform.position, Random.rotation);
        var destroy = mysteriousRemains.AddComponent<DestroyItemAfterDelay>();
        destroy.delay = 30;
        destroy.useBloodFx = true;
        destroy.bloodFxCount = 3;
        destroy.bloodFxScale = 3;
        destroy.destroyDuration = 0.6f;
        var rb = mysteriousRemains.GetComponent<Rigidbody>();
        if (rb != null)
            UWE.Utils.SetIsKinematic(rb, false);
        LargeWorldStreamer.main.MakeEntityTransient(mysteriousRemains);
    }

    private void CreateExplosionFX()
    {
        if (_explodeVfx == null)
        {
            Plugin.Logger.LogWarning("Explode VFX not found!");
            return;
        }

        var effects = new[]
        {
            Instantiate(_explodeVfx, transform.position, transform.rotation),
            Instantiate(_explodeVfx, transform.position, transform.rotation)
        };
        effects[1].transform.localScale *= 0.5f;
        foreach (var particleSystem in effects[1].GetComponentsInChildren<ParticleSystem>())
        {
            var main = particleSystem.main;
            main.startColor = new Color(0.3f, 0.2f, 0.17f);
        }

        foreach (var vfx in effects)
        {
            vfx.SetActive(true);
            Destroy(vfx, 10);
        }

        FMODUWE.PlayOneShot(_sound, transform.position);
    }

    private void DamageInRange()
    {
        damager.DealDamageToTargetsInRange();
    }

    public void HandleTriggerEnter(Collider collider)
    {
        if (_state == State.Idle && collider.gameObject.GetComponents<Player>() != null)
        {
            ExplodeNow();
            SetState(State.Exploding);
        }
    }

    private enum State
    {
        Dead,
        Forming,
        Idle,
        Exploding,
        Deflating
    }

    public string GetProfileTag()
    {
        return "TRP:ExplodingBlister";
    }
}