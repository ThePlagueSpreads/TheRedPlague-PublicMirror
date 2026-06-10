using UnityEngine;

namespace TheRedPlague.Framework.Behaviour.Deletion;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 1f;
    
    private void Start()
    {
        Destroy(gameObject, delay);
    }
}