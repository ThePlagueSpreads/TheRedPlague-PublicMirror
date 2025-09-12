using TheRedPlague.Data;
using TheRedPlague.Mono.InfectionLogic;
using UnityEngine;
using UWE;

namespace TheRedPlague.Mono.CreatureBehaviour.Mimics;

public class InfectionPodBehavior : MonoBehaviour, IPropulsionCannonAmmo
{
    public float detonateDelay = 6f;

    public GameObject model;
    public Collider mainCollider;
    public GameObject gasEffectPrefab;

    public float damageRadius = 5;
    public float damagePerSecond = 4;
    public float damageInterval = 0.4f;
    public float smokeDuration = 10;
    // Affected by the damage interval
    public float infectPercentPerSecond = 0.3f;
    public bool isArtificial;

    public FMOD_StudioEventEmitter releaseSound;
    public FMOD_StudioEventEmitter burstSound;

    private SphereCollider _damageTrigger;
    private TriggerStayTracker _tracker;
    private GameObject _gasEffect;

    private float _timeLastDamageTick;
    private bool _grabbedByPropCannon;
    private bool _wasShot;
    private float _detonateAtTime;
    private float _detonateTime;
    private bool _detonated;

    private void Start()
    {
        if (releaseSound != null)
        {
            Utils.PlayEnvSound(releaseSound, base.transform.position, 5f);
        }

        _tracker = GetComponent<TriggerStayTracker>();
        if (detonateDelay > 0f)
        {
            PrepareDetonationTime();
        }
        else
        {
            Detonate();
        }
    }

    private void PrepareDetonationTime()
    {
        _detonateAtTime = Time.time + detonateDelay * 0.6f + Random.value * detonateDelay * 0.4f;
    }

    void IPropulsionCannonAmmo.OnGrab()
    {
        _grabbedByPropCannon = true;
    }

    void IPropulsionCannonAmmo.OnShoot()
    {
        _wasShot = true;
        PrepareDetonationTime();
    }

    void IPropulsionCannonAmmo.OnImpact()
    {
        Detonate();
    }

    void IPropulsionCannonAmmo.OnRelease()
    {
        if (!_wasShot)
        {
            PrepareDetonationTime();
        }

        _grabbedByPropCannon = false;
    }

    bool IPropulsionCannonAmmo.GetAllowedToGrab()
    {
        return !_detonated;
    }

    bool IPropulsionCannonAmmo.GetAllowedToShoot()
    {
        return true;
    }

    // Unity message (probably)
    private void OnDrop()
    {
        PrepareDetonationTime();
        if (LargeWorldStreamer.main)
        {
            LargeWorldStreamer.main.MakeEntityTransient(gameObject);
        }
    }

    private void Detonate()
    {
        if (_detonated) return;
        
        _detonated = true;
        _detonateTime = Time.time;
        model.SetActive(false);
        mainCollider.enabled = false;
        _gasEffect = Instantiate(gasEffectPrefab, transform, true);
        UWE.Utils.ZeroTransform(_gasEffect);
        _damageTrigger = gameObject.AddComponent<SphereCollider>();
        _damageTrigger.radius = damageRadius;
        _damageTrigger.isTrigger = true;
        if (burstSound != null)
        {
            Utils.PlayEnvSound(burstSound, transform.position, 13f);
        }
        var renderers = _gasEffect.GetComponentsInChildren<Renderer>(true);

        foreach (var renderer in renderers)
        {
            var materials = renderer.materials;
            foreach (var material in materials)
            {
                material.color = Color.red * 0.3f;
            }

            renderer.materials = materials;
        }
    }

    private void Update()
    {
        if (_detonated)
        {
            if (_timeLastDamageTick + damageInterval <= Time.time)
            {
                foreach (var item in _tracker.Get())
                {
                    if (!item)
                    {
                        continue;
                    }

                    var liveMixin = item.GetComponent<LiveMixin>();
                    if (liveMixin == null || !liveMixin.IsAlive())
                    {
                        continue;
                    }

                    var player = item.GetComponent<Player>();
                    if ((player != null && player.IsInside()) ||
                        (player == null && item.GetComponent<Living>() == null))
                    {
                        continue;
                    }

                    var waterParkItem = item.GetComponent<WaterParkItem>();
                    if (waterParkItem != null && waterParkItem.IsInsideWaterPark()) continue;

                    bool dealDamage = true;
                    
                    // Try to infect
                    if (Random.value < infectPercentPerSecond / damageInterval)
                    {
                        var host = item.GetComponent<RedPlagueHost>();
                        if (host)
                        {
                            dealDamage = host.CanBeInfected();
                            if (dealDamage)
                                host.Infect();
                        }
                    }

                    if (dealDamage)
                    {
                        liveMixin.TakeDamage(damagePerSecond * damageInterval, item.transform.position, CustomDamageTypes.PenetrativePlagueDamage);
                    }
                    
                    if (!isArtificial)
                    {
                        liveMixin.NotifyCreatureDeathsOfCreatureAttack();
                    }
                }

                _timeLastDamageTick = Time.time;
            }

            if (_detonateTime + smokeDuration <= Time.time)
            {
                Destroy(gameObject);
            }
        }
        else if (!_grabbedByPropCannon && _detonateAtTime <= Time.time)
        {
            Detonate();
        }
    }
}