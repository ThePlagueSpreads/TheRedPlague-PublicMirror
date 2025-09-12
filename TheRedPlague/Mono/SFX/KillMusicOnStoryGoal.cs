using Story;
using UnityEngine;

namespace TheRedPlague.Mono.SFX;

public class KillMusicOnStoryGoal : MonoBehaviour, IStoryGoalListener
{
    public string goalKey;
    public bool stayDisabledAfter;

    public GenericMusicPlayer musicPlayer;

    private bool _musicStopped;
    
    private void Start()
    {
        StoryGoalManager.main.AddListener(this);
        if (stayDisabledAfter && StoryGoalManager.main.IsGoalComplete(goalKey))
        {
            musicPlayer.enabled = false;
            _musicStopped = true;
        }
    }

    private void OnDestroy()
    {
        StoryGoalManager.main.RemoveListener(this);
    }

    public void NotifyGoalComplete(string key)
    {
        if (_musicStopped) return;
        
        if (string.Equals(key, goalKey))
        {
            musicPlayer.enabled = false;
            _musicStopped = true;
            StoryGoalManager.main.RemoveListener(this);
        }
    }
}