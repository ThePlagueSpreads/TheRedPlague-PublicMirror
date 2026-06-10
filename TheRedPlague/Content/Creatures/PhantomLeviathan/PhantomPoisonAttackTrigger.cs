using UnityEngine;

namespace TheRedPlague.Content.Creatures.PhantomLeviathan;

public class PhantomPoisonAttackTrigger : MonoBehaviour
{
    public PhantomPoisonAttack poisonAttack;
    
    public void OnTriggerEnter(Collider other)
    {
        poisonAttack.OnTouchTarget(other);
    }
}