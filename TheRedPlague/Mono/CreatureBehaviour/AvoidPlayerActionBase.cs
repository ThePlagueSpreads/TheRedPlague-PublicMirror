using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour;

public abstract class AvoidPlayerActionBase : CreatureAction
{
    public float swimVelocity = 5f;
    public float swimDistance = 5f;
    public float swimInterval = 0.5f;
    public float maxAvoidanceDistance = 30f;

    private float _timeSwimAgain;
    
    public override float Evaluate(Creature creature, float time)
    {
        var shouldAvoid = ShouldAvoidPlayer();
        if (!shouldAvoid) return 0;
        if (Vector3.SqrMagnitude(transform.position - Player.main.transform.position) >
            maxAvoidanceDistance * maxAvoidanceDistance)
            return 0;
        return GetEvaluatePriority();
    }

    public override void Perform(Creature creature, float time, float deltaTime)
    {
        if (Time.time < _timeSwimAgain)
            return;
        _timeSwimAgain = Time.time + swimInterval;
        swimBehaviour.SwimTo(transform.position + (transform.position - Player.main.transform.position).normalized * swimDistance, swimVelocity);
    }

    protected abstract bool ShouldAvoidPlayer();
}