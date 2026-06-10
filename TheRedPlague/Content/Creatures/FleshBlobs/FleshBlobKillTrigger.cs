using UnityEngine;

namespace TheRedPlague.Content.Creatures.FleshBlobs;

public class FleshBlobKillTrigger : MonoBehaviour
{
    public FleshBlobKillTriggerManager manager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.main.gameObject)
        {
            manager.KillPlayer(this.gameObject);
        }
    }
}