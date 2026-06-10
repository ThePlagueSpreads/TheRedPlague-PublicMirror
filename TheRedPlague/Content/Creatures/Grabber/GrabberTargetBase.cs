using UnityEngine;

namespace TheRedPlague.Content.Creatures.Grabber;

public abstract class GrabberTargetBase : MonoBehaviour
{
    public abstract void StartGrab(GrabberCreature grabber);
    public abstract void EndGrab(GrabberCreature grabber);
    public abstract void Damage(GrabberCreature grabber, float damage, DamageType type);
    public abstract void SetTargetPositionAndRotation(Vector3 position, Quaternion rotation);
}