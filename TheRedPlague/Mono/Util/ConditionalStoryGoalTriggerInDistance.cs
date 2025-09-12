using Story;
using UnityEngine;

namespace TheRedPlague.Mono.Util;

public class ConditionalStoryGoalTriggerInDistance : MonoBehaviour, IScheduledUpdateBehaviour, IStoryGoalListener
{
    public float activationRadius;
    public string requiredGoal;
    public StoryGoal goalToComplete;

    private bool _registered;
    private bool _waitingOnStoryGoal;
    private bool _requirementMet;

    private float _radiusSquared;
    
    public int scheduledUpdateIndex { get; set; }
    
    private void Start()
    {
        if (StoryGoalManager.main.IsGoalComplete(goalToComplete.key))
        {
            return;
        }
        
        _requirementMet = StoryGoalManager.main.IsGoalComplete(requiredGoal);
        _radiusSquared = activationRadius * activationRadius;

        if (_requirementMet)
        {
            _registered = true;
            UpdateSchedulerUtils.Register(this);
        }
        else
        {
            _waitingOnStoryGoal = true;
            StoryGoalManager.main.AddListener(this);
        }
    }

    private void OnDestroy()
    {
        if (_registered)
        {
            UpdateSchedulerUtils.Deregister(this);
        }

        if (_waitingOnStoryGoal)
        {
            StoryGoalManager.main.RemoveListener(this);
        }
    }

    public string GetProfileTag()
    {
        return "TRP:ConditionalStoryGoalTriggerInDistance";
    }

    public void ScheduledUpdate()
    {
        CheckPlayerInRange();
    }

    private void CheckPlayerInRange()
    {
        if (Vector3.SqrMagnitude(Player.main.transform.position - transform.position) < _radiusSquared)
        {
            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        UpdateSchedulerUtils.Deregister(this);
        _registered = false;
        goalToComplete.Trigger();
    }

    public void NotifyGoalComplete(string key)
    {
        if (key == requiredGoal)
        {
            _waitingOnStoryGoal = false;
            StoryGoalManager.main.RemoveListener(this);
            _registered = true;
            UpdateSchedulerUtils.Register(this);
        }
    }
}