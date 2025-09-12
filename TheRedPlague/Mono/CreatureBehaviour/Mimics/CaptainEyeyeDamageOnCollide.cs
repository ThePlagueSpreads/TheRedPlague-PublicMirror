using TheRedPlague.Data;
using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Mimics;

public class CaptainEyeyeDamageOnCollide : MonoBehaviour
{
    public Collider collider;
    public float normalDamage = 7f;
    public float plagueDamage = 4f;
    
    private bool _damaged;

    public void OnCollisionEnter(Collision other)
    {
        if (_damaged)
            return;
        
        if (other.gameObject.TryGetComponent<LiveMixin>(out var lm))
        {
            lm.TakeDamage(normalDamage, transform.position);
            lm.TakeDamage(plagueDamage, transform.position, CustomDamageTypes.PenetrativePlagueDamage);
            _damaged = true;
        }
    }

    private void Start()
    {
        Invoke(nameof(EnableCollider), 0.2f);
    }

    private void EnableCollider()
    {
        collider.enabled = true;
    }
}