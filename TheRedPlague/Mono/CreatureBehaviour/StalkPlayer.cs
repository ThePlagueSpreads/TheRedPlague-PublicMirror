using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour;

[RequireComponent(typeof(PlayerDistanceTracker))]
public class StalkPlayer : CreatureAction
{
    public float minDistance = 50;
    public float distanceFromPlayerToSwimTo = 25f;
    public float swimVelocity = 3f;

    private PlayerDistanceTracker _tracker;

    public override void Awake()
    {
        base.Awake();
        _tracker = GetComponent<PlayerDistanceTracker>();
    }

    public override float Evaluate(Creature creature, float time)
    {
        if (_tracker.distanceToPlayer > minDistance)
        {
            return GetEvaluatePriority();
        }

        return 0f;
    }

    public override void Perform(Creature creature, float time, float deltaTime)
    {
        swimBehaviour.SwimTo(GetTargetPosition(), swimVelocity);
    }

    private Vector3 GetTargetPosition()
    {
        if ((Player.main.transform.position - transform.position).sqrMagnitude < 0.1f)
            return transform.position;

        return Player.main.transform.position + (transform.position - Player.main.transform.position).normalized *
            distanceFromPlayerToSwimTo;
    }
}