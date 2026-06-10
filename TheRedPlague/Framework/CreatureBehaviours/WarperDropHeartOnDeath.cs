using System.Collections;
using TheRedPlague.Content.Items.Resources;
using UnityEngine;

namespace TheRedPlague.Framework.CreatureBehaviours;

public class WarperDropHeartOnDeath : MonoBehaviour
{
    public void OnKill()
    {
        UWE.CoroutineHost.StartCoroutine(SpawnHeartCoroutine(transform.position));
    }

    private IEnumerator SpawnHeartCoroutine(Vector3 position)
    {
        var task = CraftData.GetPrefabForTechTypeAsync(WarperHeart.Info.TechType);
        yield return task;
        var obj = Instantiate(task.GetResult(), position, Quaternion.identity);
        obj.SetActive(true);
        var rb = obj.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        LargeWorld.main.streamer.cellManager.RegisterEntity(obj);
    }
}