using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour;

public class ShoalDamageInRange : MonoBehaviour, IManagedUpdateBehaviour
{
    public float damageDistance = 3.3f;
    public float damageUpdateInterval = 2f;
    public float damage = 1f;

    private float _nextCheckTime;
    
    public int managedUpdateIndex { get; set; }

    private void OnEnable()
    {
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        BehaviourUpdateUtils.Deregister(this);
    }

    public void ManagedUpdate()
    {
        if (Time.time < _nextCheckTime) return;
        _nextCheckTime = Time.time + damageUpdateInterval;
        Check();
    }

    private void Check()
    {
        if (!Player.main.IsSwimming())
        {
            return;
        }

        if (Vector3.SqrMagnitude(Player.main.transform.position - transform.position) > damageDistance * damageDistance)
        {
            return;
        }

        Player.main.liveMixin.TakeDamage(damage, transform.position, DamageType.Normal, gameObject);
    }

    public string GetProfileTag()
    {
        return "TRP:ShoalDamageInRange";
    }
}