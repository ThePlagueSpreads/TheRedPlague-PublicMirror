using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Sucker;

public class SuckerTargetTechnology : CreatureAction, IOnTakeDamage
{
    public float minDamageForReacting = 5f;
    public float cooldownAfterTakingDamage = 24;
    
    public float swimVelocity;
    public float radius;
    
    private SuckerControllerTarget _target;

    private float _timeCanTargetAgain;
    
    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), Random.value, 2f);
    }

    public override float Evaluate(Creature creature, float time)
    {
        if (Time.time < _timeCanTargetAgain)
            return 0f;
        if (_target != null) return evaluatePriority;
        return 0;
    }
    
    public override void Perform(Creature creature, float time, float deltaTime)
    {
        if (_target == null) return;
        swimBehaviour.SwimTo(_target.transform.position, swimVelocity);
    }

    private void UpdateTarget()
    {
        SuckerControllerTarget.TryGetClosest(out _target, transform.position, radius);
    }

    public void OnTakeDamage(DamageInfo damageInfo)
    {
        if (damageInfo.damage > minDamageForReacting)
        {
            _timeCanTargetAgain = Time.time + cooldownAfterTakingDamage;
        }
    }
}