using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour;

public class ForceSwimToPoint : CreatureAction
{
    public float velocity = 3;
    public Vector3 point;
    
    public override float Evaluate(Creature creature, float time)
    {
        return evaluatePriority;
    }

    public override void Perform(Creature creature, float time, float deltaTime)
    {
        swimBehaviour.SwimTo(point, velocity);
    }

    private void Update()
    {
        swimBehaviour.SwimTo(point, velocity);
    }
}