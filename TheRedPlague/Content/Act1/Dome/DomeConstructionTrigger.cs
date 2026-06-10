using System.Collections;
using Story;
using UnityEngine;

namespace TheRedPlague.Content.Act1.Dome;

public class DomeConstructionTrigger : MonoBehaviour
{
    private bool _triggered;
    
    private void Update()
    {
        if (!_triggered && Vector3.Distance(transform.position, Player.main.transform.position) < 20)
        {
            Trigger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        
        if (other.gameObject != Player.main.gameObject)
        {
            return;
        }
        
        Plugin.Logger.LogInfo("Dome construction trigger entered...");

        var story = StoryGoalManager.main;
        
        if (story == null)
        {
            Plugin.Logger.LogError("StoryGoalManager is not initialized!");
            return;
        }

        if (story.IsGoalComplete(Act1Story.DomeConstructionEvent.key))
        {
            Plugin.Logger.LogError("DomeConstructionEvent was already triggered!");
            return;
        }

        Trigger();
    }

    private void Trigger()
    {
        UWE.CoroutineHost.StartCoroutine(SpawnDome());
        Destroy(gameObject);
        _triggered = true;
    }

    public static IEnumerator SpawnDome()
    {
        yield return new WaitForSeconds(6);
        Plugin.Logger.LogInfo("Beginning dome construction.");
        var domeTask = CraftData.GetPrefabForTechTypeAsync(DomePrefab.Info.TechType);
        yield return domeTask;
        var dome = Instantiate(domeTask.GetResult(), Vector3.up * 50, Quaternion.identity);
        dome.SetActive(true);
        dome.transform.localScale = Vector3.one * 900;
    }
}