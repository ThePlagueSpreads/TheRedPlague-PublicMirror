using UnityEngine;

namespace TheRedPlague.Content.Creatures.MrTeeth;

public class MrTeethRagdollFix : MonoBehaviour
{
    public Collider collider;
    public Collider[] corpseColliders;

    private void Start()
    {
        foreach (var corpseCollider in corpseColliders)
        {
            if (corpseCollider == null)
            {
                Plugin.Logger.LogWarning("Null corpse collider found on Mr. Teeth - how did this happen?");
                continue;
            }
            Physics.IgnoreCollision(collider, corpseCollider);
        }
    }
}