using System;
using UnityEngine;

namespace TheRedPlague.Content.PlayerInfection;

public class HealSanityNearby : MonoBehaviour, IScheduledUpdateBehaviour
{
    private const float MaxDeltaTime = 3f;

    public float radius;
    public string biomeFilter;
    public float sanityPerSecond;
    
    private float _lastUpdateTime;
    
    public int scheduledUpdateIndex { get; set; }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public void ScheduledUpdate()
    {
        if (CanHeal())
        {
            var deltaTime = Mathf.Min(Time.time - _lastUpdateTime, MaxDeltaTime);
            var healAmount = sanityPerSecond * deltaTime;
            PlagueDamageStat.main.HealInfectionDamage(healAmount);
        }
        _lastUpdateTime = Time.time;
    }

    private bool CanHeal()
    {
        if (Vector3.SqrMagnitude(Player.main.transform.position - transform.position) > radius * radius)
            return false;
        if (!string.IsNullOrEmpty(biomeFilter))
            return Player.main.GetBiomeString().Equals(biomeFilter, StringComparison.OrdinalIgnoreCase);
        return true;
    }
    
    public string GetProfileTag()
    {
        return "TRP:HealSanityNearby";
    }
}