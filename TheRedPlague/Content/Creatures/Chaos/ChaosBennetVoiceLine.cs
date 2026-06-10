using Story;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.Chaos;

public class ChaosBennetVoiceLine : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float maxDistance = 80;
    public float minExistenceTime = 30f;
    
    public int scheduledUpdateIndex { get; set; }

    private float _spawnTime;

    private void Start()
    {
        if (StoryGoalManager.main.IsGoalComplete(GetRelevantStoryGoal().key))
            return;
        
        UpdateSchedulerUtils.Register(this);
        
        _spawnTime = Time.time;
    }

    private void OnDestroy()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public string GetProfileTag()
    {
        return "TRP:ChaosBennetVoiceLine";
    }

    public void ScheduledUpdate()
    {
        if (Time.time < _spawnTime + minExistenceTime)
            return;
        
        if (!StoryGoalManager.main.IsGoalComplete(Act2Story.BennetChaosApproachPrecondition.key))
            return;

        if (StoryGoalManager.main.IsGoalComplete(Act2Story.BennetChaosApproach.key))
        {
            UpdateSchedulerUtils.Deregister(this);
            return;
        }
        
        var distance = Vector3.SqrMagnitude(Player.main.transform.position - transform.position);
        if (distance < maxDistance * maxDistance)
        {
            UpdateSchedulerUtils.Deregister(this);
            GetRelevantStoryGoal().Trigger();
        }
    }

    private StoryGoal GetRelevantStoryGoal()
    {
        return Act2Story.BennetChaosApproach;
    }
}