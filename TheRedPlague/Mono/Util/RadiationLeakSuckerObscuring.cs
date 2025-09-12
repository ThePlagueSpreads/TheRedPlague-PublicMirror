using System.Collections.Generic;
using Story;
using UnityEngine;

namespace TheRedPlague.Mono.Util;

public class RadiationLeakSuckerObscuring : MonoBehaviour, IManagedUpdateBehaviour
{
    public static RadiationLeakSuckerObscuring main;
    private const float MaxDistanceFromRoom = 70f;
    private const float SuckerBlockDistanceSqr = 4f;
    private const float UpdateInterval = 0.2f;
    private const int MinNumberOfBreaches = 11;
    private static readonly Vector3 RoomCenter = new Vector3(870, 10, -2);
    
    private static readonly HashSet<Transform> SuckerTransforms = new();

    private bool _registeredUpdate;
    
    public int managedUpdateIndex { get; set; }

    private Collider[] _leakColliders;

    private float _timeUpdateAgain;

    private bool _auroraRepaired;

    private void Awake()
    {
        main = this;
    }

    private Collider[] GetLeakColliders()
    {
        if (_leakColliders == null || _leakColliders.Length < MinNumberOfBreaches)
        {
            _leakColliders = GetComponentsInChildren<Collider>();
        }

        return _leakColliders;
    }

    public void ManagedUpdate()
    {
        if (Time.time < _timeUpdateAgain)
            return;

        _timeUpdateAgain = Time.time + UpdateInterval;
        
        foreach (var collider in GetLeakColliders())
        {
            if (collider == null)
                continue;
            collider.enabled = !IsBreachBlocked(collider);
        }
    }

    private bool IsBreachBlocked(Collider breachCollider)
    {
        var center = breachCollider.transform.position;

        foreach (var sucker in SuckerTransforms)
        {
            if (sucker == null)
                continue;
            
            if (Vector3.SqrMagnitude(sucker.position - center) < SuckerBlockDistanceSqr)
            {
                return true;
            }
        }

        return false;
    }
    
    public static void RegisterSucker(Transform transform)
    {
        if (Vector3.SqrMagnitude(transform.position - RoomCenter) > MaxDistanceFromRoom * MaxDistanceFromRoom)
        {
            return;
        }
        
        if (main)
        {
            if (main._auroraRepaired)
                return;
            if (StoryGoalManager.main != null && StoryGoalManager.main.IsGoalComplete("AuroraRadiationFixed"))
            {
                main._auroraRepaired = true;
                main.UnregisterUpdate();
                return;
            }
        }
        
        SuckerTransforms.Add(transform);
        if (main)
            main.RegisterUpdate();
    }

    public static void UnregisterSucker(Transform transform)
    {
        SuckerTransforms.Remove(transform);
        if (SuckerTransforms.Count == 0 && main)
            main.UnregisterUpdate();
    }

    public static void DamageSuckersInRange(Vector3 position, float radius, float damage, DamageType type)
    {
        if (main == null)
        {
            Plugin.Logger.LogWarning("Sucker radiation leak obscuring manager not found!");
            return;
        }

        foreach (var sucker in SuckerTransforms)
        {
            if (Vector3.SqrMagnitude(position - sucker.position) > radius * radius)
                continue;
            
            if (sucker != null && sucker.TryGetComponent<LiveMixin>(out var lm))
            {
                lm.TakeDamage(damage, position, type);
            }
        }
    }

    private void RegisterUpdate()
    {
        if (!_registeredUpdate)
        {
            BehaviourUpdateUtils.Register(this);
            _registeredUpdate = true;
        }
    }

    private void UnregisterUpdate()
    {
        if (_registeredUpdate)
        {
            BehaviourUpdateUtils.Deregister(this);
            _registeredUpdate = false;
        }
    }

    public string GetProfileTag()
    {
        return "TRP:RadiationLeakSuckerObscuring";
    }

    private void OnDestroy()
    {
        UnregisterUpdate();
    }
}