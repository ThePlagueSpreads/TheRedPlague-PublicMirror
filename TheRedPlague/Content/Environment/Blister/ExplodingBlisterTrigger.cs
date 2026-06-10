using UnityEngine;

namespace TheRedPlague.Content.Environment.Blister;

public class ExplodingBlisterTrigger : MonoBehaviour
{
    public ExplodingBlister blister;

    private void OnTriggerEnter(Collider other)
    {
        blister.HandleTriggerEnter(other);
    }
}