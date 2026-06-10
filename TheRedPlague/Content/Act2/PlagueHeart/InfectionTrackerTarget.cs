using TheRedPlague.Content.Equipment.InfectionTracker;
using UnityEngine;

namespace TheRedPlague.Content.Act2.PlagueHeart;

public class InfectionTrackerTarget : MonoBehaviour, IInfectionTrackerTarget
{
    public int priority;

    private void OnEnable()
    {
        InfectionTargetRegistry.RegisterTarget(this);
    }

    private void OnDisable()
    {
        InfectionTargetRegistry.UnregisterTarget(this);
    }

    public Vector3 GetTargetPosition()
    {
        return transform.position;
    }

    public int GetTrackingPriority()
    {
        return priority;
    }
}