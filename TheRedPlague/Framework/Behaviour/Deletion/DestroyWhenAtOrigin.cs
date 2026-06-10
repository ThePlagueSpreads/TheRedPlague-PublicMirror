using UnityEngine;

namespace TheRedPlague.Framework.Behaviour.Deletion;

public class DestroyWhenAtOrigin : MonoBehaviour
{
    public float maxSqrDistance = 0.001f;
    
    private void Start()
    {
        if (Vector3.SqrMagnitude(transform.position) <= maxSqrDistance)
        {
            Destroy(gameObject);
        }
    }
}