using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour;

[RequireComponent(typeof(PlayerDistanceTracker))]
public class StareAtPlayer : CreatureAction
{
    public float detectionDistance = 4f;

    private PlayerDistanceTracker _tracker;
    
    public override void Awake()
    {
        base.Awake();
        _tracker = GetComponent<PlayerDistanceTracker>();
    }

    public override float Evaluate(Creature creature, float time)
    {
        if (_tracker.distanceToPlayer < detectionDistance)
        {
            return GetEvaluatePriority();
        }

        return 0f;
    }

    public override void StartPerform(Creature creature, float time)
    {
        swimBehaviour.LookAt(Player.main.transform);
    }

    public override void Perform(Creature creature, float time, float deltaTime)
    {
        swimBehaviour.LookAt(Player.main.transform);
    }

    public override void StopPerform(Creature behaviour, float time)
    {
        swimBehaviour.LookAt(null);
    }
}