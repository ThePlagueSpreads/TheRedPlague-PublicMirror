using UnityEngine;

namespace TheRedPlague.Framework.Behaviour.Animation;

public class CopyObjectPosition : MonoBehaviour, IManagedUpdateBehaviour
{
    public Transform target;
    
    public string GetProfileTag()
    {
        return "TRP:CopyObjectPosition";
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
        if (target != null)
            transform.position = target.position;
    }

    public int managedUpdateIndex { get; set; }
}