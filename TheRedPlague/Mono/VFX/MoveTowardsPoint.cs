using UnityEngine;

namespace TheRedPlague.Mono.VFX;

public class MoveTowardsPoint : MonoBehaviour, IManagedUpdateBehaviour
{
    public int managedUpdateIndex { get; set; }

    public Transform target;
    public float moveMetersPerSecond;
    public float rotateAnglesPerSecond;

    public void ManagedUpdate()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, target.position, moveMetersPerSecond * Time.deltaTime);
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, target.rotation, rotateAnglesPerSecond * Time.deltaTime);
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
        return "TRP:MoveTowardsPoint";
    }
}