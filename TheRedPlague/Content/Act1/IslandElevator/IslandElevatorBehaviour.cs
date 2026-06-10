using System;
using Story;
using UnityEngine;

namespace TheRedPlague.Content.Act1.IslandElevator;

public class IslandElevatorBehaviour : MonoBehaviour, IStoryGoalListener
{
    public static IslandElevatorBehaviour Main { get; private set; }
    
    private void Start()
    {
        Main = this;
        StoryGoalManager.main.AddListener(this);
        SetElevatorActive(StoryGoalManager.main.IsGoalComplete(Act1Story.IslandElevatorActivatedGoal.key));
    }

    public void NotifyGoalComplete(string key)
    {
        if (string.Equals(key, Act1Story.IslandElevatorActivatedGoal.key, StringComparison.OrdinalIgnoreCase))
        {
            SetElevatorActive(true);
        }
    }

    private void SetElevatorActive(bool state)
    {
        transform.Find("elevator_bot_trigger").gameObject.SetActive(state);
        transform.Find("FX").gameObject.SetActive(state);
    }

    private void OnDestroy()
    {
        StoryGoalManager.main.RemoveListener(this);
    }
}