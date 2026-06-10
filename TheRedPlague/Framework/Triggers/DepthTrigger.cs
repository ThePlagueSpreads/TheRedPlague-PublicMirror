using Story;
using UnityEngine;

namespace TheRedPlague.Framework.Triggers;

public class DepthTrigger : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float depthThresholdCoordinate; // compared with the Y value of the player
    public bool checkAbove;
    
    public StoryGoal goal;
    public StoryGoal prerequisite;
    
    private bool _hasPrerequisite;
    private bool _prerequisiteWasMetCache;
    private bool _completed;
    private bool _ready;
    
    public int scheduledUpdateIndex { get; set; }

    private void Start()
    {
        _completed = StoryGoalManager.main.IsGoalComplete(goal.key);

        if (_completed)
            return;
        
        _hasPrerequisite = prerequisite != null;
        if (_hasPrerequisite && string.IsNullOrEmpty(prerequisite.key))
        {
            Plugin.Logger.LogWarning($"Prerequisite goal for trigger '{this}' exists but is null.");
        }

        _ready = true;
    }

    public string GetProfileTag()
    {
        return "TheRedPlague.Framework.Triggers:DepthTrigger";
    }

    public void ScheduledUpdate()
    {
        if (_completed || !_ready)
            return;

        var goalManager = StoryGoalManager.main;

        if (!CheckPlayerInRange())
            return;
        
        if (!GetPrerequisiteMet(goalManager))
            return;
        
        CompleteGoal(); 
    }

    private void CompleteGoal()
    {
        goal.Trigger();
        _completed = true;
        UpdateSchedulerUtils.Deregister(this);
    }

    private bool GetPrerequisiteMet(StoryGoalManager manager)
    {
        if (!_hasPrerequisite)
            return true;
        if (_prerequisiteWasMetCache)
            return true;
        _prerequisiteWasMetCache = manager.IsGoalComplete(prerequisite.key);
        return _prerequisiteWasMetCache;
    }

    private bool CheckPlayerInRange()
    {
        var yPosition = Player.main.transform.position.y;
        if (checkAbove)
        {
            return yPosition > depthThresholdCoordinate;
        }

        return yPosition < depthThresholdCoordinate;
    }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }
}