using System;
using Story;
using UnityEngine;

namespace TheRedPlague.Framework.Behaviour.Story;

public class CompleteGoalWhenInBiome : MonoBehaviour, IScheduledUpdateBehaviour, IStoryGoalListener
{
    public StoryGoal goal;
    public string preconditionGoalKey;
    public string biomeName;

    private bool _registered;
    
    public int scheduledUpdateIndex { get; set; }

    private bool _preconditionMet;
    private bool _completed;
    
    private void Start()
    {
        if (!_registered && !StoryGoalManager.main.IsGoalComplete(goal.key))
        {
            UpdateSchedulerUtils.Register(this);
            StoryGoalManager.main.AddListener(this);
            _registered = true;
            _preconditionMet = StoryGoalManager.main.IsGoalComplete(preconditionGoalKey);
        }
    }

    private void OnDestroy()
    {
        Unregister();
    }

    private void Unregister()
    {
        if (_registered)
        {
            UpdateSchedulerUtils.Deregister(this);
            StoryGoalManager.main.RemoveListener(this);
            _registered = false;
        }
    }

    public void NotifyGoalComplete(string key)
    {
        if (_preconditionMet || _completed)
            return;
        
        if (string.Equals(preconditionGoalKey, key, StringComparison.OrdinalIgnoreCase))
        {
            _preconditionMet = true;
        }
    }

    public string GetProfileTag()
    {
        return "TRP:CompleteGoalWhenInBiome";
    }

    public void ScheduledUpdate()
    {
        if (!_preconditionMet) return;
        
        var playerBiome = Player.main.GetBiomeString();
        if (string.Equals(playerBiome, biomeName, StringComparison.OrdinalIgnoreCase))
        {
            goal.Trigger();
            _completed = true;
            Unregister();
        }
    }
}