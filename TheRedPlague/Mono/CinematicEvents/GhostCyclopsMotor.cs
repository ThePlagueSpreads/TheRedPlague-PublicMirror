using UnityEngine;

namespace TheRedPlague.Mono.CinematicEvents;

public class GhostCyclopsMotor : MonoBehaviour, IManagedUpdateBehaviour
{
    public CyclopsScrew screw;

    public float horizontalVelocity = 13;
    public float verticalVelocity = 7;

    private Vector2 _startPosition2D;
    private Vector2 _endPosition2D;
    private float _endY;
    private float _startTime;
    private float _duration;
    private bool _moving;
    
    public float GetApproximateDuration() => _duration;

    public void StartMovement(Vector3 startPosition, Vector3 endPosition)
    {
        _startPosition2D = new Vector2(startPosition.x, startPosition.z);
        _endPosition2D = new Vector2(endPosition.x, endPosition.z);
        _endY = endPosition.y;
        _moving = true;
        _startTime = Time.time;
        _duration = Vector2.Distance(new Vector2(startPosition.x, startPosition.z),
            new Vector2(endPosition.x, endPosition.z)) / horizontalVelocity;
    }

    public string GetProfileTag()
    {
        return "TRP:GhostCyclopsMotor";
    }

    private void OnEnable()
    {
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        BehaviourUpdateUtils.Deregister(this);
    }

    public void ManagedUpdate()
    {
        if (!_moving)
            return;
        var pos2D = Vector2.Lerp(_startPosition2D, _endPosition2D, (Time.time - _startTime) / _duration);
        var yPos = Mathf.MoveTowards(transform.position.y, _endY, Time.deltaTime * verticalVelocity);
        transform.position = new Vector3(pos2D.x, yPos, pos2D.y);
        if (screw)
            screw.OnSubAppliedThrottle();
    }

    public int managedUpdateIndex { get; set; }
}