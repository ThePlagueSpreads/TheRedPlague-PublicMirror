using System.Collections;
using UnityEngine;
using UWE;

namespace TheRedPlague.Framework.Migration;

public class ReplacePrefabOnStart : MonoBehaviour
{
    public string newClassId;
    public bool techTypeIsSameAsClassId;

    private void Start()
    {
        CoroutineHost.StartCoroutine(Replace(gameObject, newClassId, techTypeIsSameAsClassId));
    }

    private static IEnumerator Replace(GameObject gameObject, string newClassId, bool techTypeIsSameAsClassId)
    {
        if (techTypeIsSameAsClassId && (gameObject.transform.parent == global::Inventory.main.container.tr ||
                                        gameObject.GetComponentInParent<Player>() != null))
        {
            if (TechTypeExtensions.FromString(newClassId, out var techType, true))
            {
                CraftData.AddToInventory(techType);
                Destroy(gameObject);
                yield break;
            }
        }

        var task = PrefabDatabase.GetPrefabAsync(newClassId);
        yield return task;
        if (gameObject == null)
            yield break;
        if (!task.TryGetPrefab(out var prefab))
        {
            Plugin.Logger.LogWarning("Failed to load prefab with ClassID " + newClassId);
            yield break;
        }

        var wasActive = gameObject.activeSelf;
        var newObject = Instantiate(prefab, gameObject.transform.parent, false);
        newObject.transform.localPosition = gameObject.transform.localPosition;
        newObject.transform.localEulerAngles = gameObject.transform.localEulerAngles;
        newObject.SetActive(wasActive);
        if (gameObject.TryGetComponent<Rigidbody>(out var oldRigidbody) &&
            newObject.TryGetComponent<Rigidbody>(out var newRigidbody))
        {
            newRigidbody.isKinematic = oldRigidbody.isKinematic;
        }

        Destroy(gameObject);
    }
}