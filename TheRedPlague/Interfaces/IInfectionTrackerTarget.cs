using UnityEngine;

namespace TheRedPlague.Interfaces;

public interface IInfectionTrackerTarget
{
    public Vector3 GetTargetPosition();
    public int GetTrackingPriority();
}