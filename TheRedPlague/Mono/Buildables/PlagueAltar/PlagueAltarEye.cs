using UnityEngine;

namespace TheRedPlague.Mono.Buildables.PlagueAltar;

public class PlagueAltarEye : MonoBehaviour, IManagedUpdateBehaviour, IScheduledUpdateBehaviour
{
    private const float EnableDistanceSqr = 2500;
    
    public bool flip;
    
    public int managedUpdateIndex { get; set; }
    public int scheduledUpdateIndex { get; set; }

    private bool _updateRegistered;

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
        if (_updateRegistered)
        {
            BehaviourUpdateUtils.Deregister(this);
            _updateRegistered = false;
        }
    }

    public void ScheduledUpdate()
    {
        if (Vector3.SqrMagnitude(Player.main.transform.position - transform.position) < EnableDistanceSqr)
        {
            if (_updateRegistered) return;
            BehaviourUpdateUtils.Register(this);
            _updateRegistered = true;
        }
        else if (_updateRegistered)
        {
            BehaviourUpdateUtils.Deregister(this);
            _updateRegistered = false;
        }
    }

    public void ManagedUpdate()
    {
        var vector = (MainCamera.camera.transform.position - transform.position).normalized;
        if (flip) vector *= -1;
        transform.up = vector;
    }
    
    public string GetProfileTag()
    {
        return "TRP:PlagueAltarEye";
    }
}