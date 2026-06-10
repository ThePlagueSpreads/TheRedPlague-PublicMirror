using UnityEngine;

namespace TheRedPlague.Content.Buildables.PlagueAltar;

public class PlagueAltarEye : MonoBehaviour, IManagedUpdateBehaviour, IScheduledUpdateBehaviour
{
    private const float EnableDistanceSqr = 2500;
    
    public bool flip;
    public float crazyEyesMaxRotationDegrees = 3;
    public float crazyEyesRotationSpeedDegrees = 100;
    public float crazyEyesMoveMinInterval = 0.05f;
    public float crazyEyesMoveMaxInterval = 0.1f;
    
    public int managedUpdateIndex { get; set; }
    public int scheduledUpdateIndex { get; set; }

    private bool _updateRegistered;

    private bool _crazy;
    private float _timeCrazyMoveAgain;
    private Vector3 _crazyEyesOffsetTarget;
    private Vector3 _realCrazyEyesOffset;

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

    public void SetCrazy(bool crazy)
    {
        _crazy = crazy;
    }

    public void ManagedUpdate()
    {
        var vector = (MainCamera.camera.transform.position - transform.position).normalized;
        if (flip) vector *= -1;
        transform.up = vector;
        
        if (_crazy)
        {
            if (Time.time > _timeCrazyMoveAgain)
            {
                _timeCrazyMoveAgain = Time.time + Random.Range(crazyEyesMoveMinInterval, crazyEyesMoveMaxInterval);
                _crazyEyesOffsetTarget = new Vector3(Random.value, Random.value, Random.value) * crazyEyesMaxRotationDegrees;
            }
            _realCrazyEyesOffset = Vector3.MoveTowards(_realCrazyEyesOffset, _crazyEyesOffsetTarget,
                Time.deltaTime * crazyEyesRotationSpeedDegrees);
            transform.Rotate(_realCrazyEyesOffset);
        }
    }
    
    public string GetProfileTag()
    {
        return "TRP:PlagueAltarEye";
    }
}