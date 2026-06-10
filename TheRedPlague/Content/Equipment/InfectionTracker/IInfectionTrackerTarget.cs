using UnityEngine;

namespace TheRedPlague.Content.Equipment.InfectionTracker;

public interface IInfectionTrackerTarget
{
    public Vector3 GetTargetPosition();
    public int GetTrackingPriority();
}