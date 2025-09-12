using UnityEngine;

namespace TheRedPlague.Mono.VFX;

public class LerpToPoint : MonoBehaviour, IManagedUpdateBehaviour
{
    public int managedUpdateIndex { get; set; }

    public Transform target;
    public float duration;
    
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    public void ManagedUpdate()
    {
        transform.position =
            Vector3.Lerp(transform.position, target.position, (Time.time - _startTime) / duration);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, target.rotation, (Time.time - _startTime) / duration);
    }

    private void OnEnable()
    {
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        BehaviourUpdateUtils.Deregister(this);
    }

    public string GetProfileTag()
    {
        return "TRP:LerpToPoint";
    }
}