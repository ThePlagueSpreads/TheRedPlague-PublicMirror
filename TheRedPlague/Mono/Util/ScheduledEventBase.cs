using UnityEngine;

namespace TheRedPlague.Mono.Util;

public abstract class ScheduledEventBase : MonoBehaviour, IScheduledUpdateBehaviour
{
    public bool performImmediately;
    
    private float _timeNextSpawn;

    public int scheduledUpdateIndex { get; set; }
    
    protected abstract void PerformAction();
    protected abstract float GetDelay();

    private bool _started;
    
    private void OnEnable()
    {
        if (!_started)
        {
            if (!performImmediately)
            {
                _timeNextSpawn = Time.time + GetDelay();
            }
            _started = true;
        }
        
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }
    
    public string GetProfileTag()
    {
        return "TRP:ScheduledEventBase";
    }

    public void ScheduledUpdate()
    {
        if (Time.time < _timeNextSpawn) return;
        PerformAction();
        _timeNextSpawn = Time.time + GetDelay();
    }
}