using TheRedPlague.Interfaces;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent.PlagueHeart;

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