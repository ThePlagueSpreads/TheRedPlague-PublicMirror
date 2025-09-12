using UnityEngine;

namespace TheRedPlague.Mono.Util;

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