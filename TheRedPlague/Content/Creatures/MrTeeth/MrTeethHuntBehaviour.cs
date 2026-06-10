using UnityEngine;

namespace TheRedPlague.Content.Creatures.MrTeeth;

public class MrTeethHuntBehaviour : CreatureAction
{
    public float swimVelocity = 40;

    private bool _performing;
    
    public override void StartPerform(Creature creature, float time)
    {
        _performing = true;
    }

    public override void StopPerform(Creature creature, float time)
    {
        _performing = false;
    }

    private void Update()
    {
        if (_performing || creature.GetBestAction() is not MrTeethBuryBehaviour)
        {
            if (Ocean.GetDepthOf(transform) <= 0)
            {
                swimBehaviour.splineFollowing.useRigidbody.AddForce(transform.forward * 3, ForceMode.Acceleration);
            }
            swimBehaviour.SwimTo(Player.main.transform.position, swimVelocity);
        }
    }
}