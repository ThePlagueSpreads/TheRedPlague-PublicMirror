using Story;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent;

public class WatchPlayerLeavingFleshCave : MonoBehaviour, IScheduledUpdateBehaviour
{
    private const float FollowUpCheckDelay = 2f;
    private bool _triggered;
    private bool _doubleChecking;
    
    public static WatchPlayerLeavingFleshCave Instance { get; private set; }
    
    public StoryGoal goalToComplete;
    
    public int scheduledUpdateIndex { get; set; }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Plugin.Logger.LogWarning("More than one instance of WatchPlayerLeavingFleshCave found!");
            return;
        }

        Instance = this;
        
        if (StoryGoalManager.main != null && StoryGoalManager.main.IsGoalComplete(goalToComplete.key))
        {
            Destroy(gameObject);
        }
    }

    public string GetProfileTag()
    {
        return "TRP:WatchPlayerLeavingFleshCave";
    }

    public void ScheduledUpdate()
    {
        if (LeftFleshCave())
        {
            OnFleshCaveLeft();
        }
    }

    private void OnFleshCaveLeft()
    {
        if (!_doubleChecking)
        {
            Invoke(nameof(FollowupCheck), FollowUpCheckDelay);
            _doubleChecking = true;
        }
    }

    private void FollowupCheck()
    {
        _doubleChecking = false;
        if (LeftFleshCave())
        {
            CompleteGoal();
        }
    }

    private void CompleteGoal()
    {
        if (_triggered)
        {
            return;
        }
        goalToComplete.Trigger();
        Destroy(gameObject);
        _triggered = true;
    }

    private static bool LeftFleshCave()
    {
        if (WaitScreen.IsWaiting)
        {
            return false;
        }
        var player = Player.main;
        if (player == null)
            return false;
        var biomeString = player.GetBiomeString().ToLower();
        if (string.IsNullOrEmpty(biomeString)) return false;
        if (biomeString.StartsWith("precursor")) return false;
        if (biomeString.StartsWith("observatory")) return false;
        if (biomeString.StartsWith("fleshcave")) return false;
        if (biomeString.StartsWith("shrinebase")) return false;
        return true;
    }
}