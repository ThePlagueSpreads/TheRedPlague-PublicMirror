using UnityEngine;

namespace TheRedPlague.Mono.SFX;

public class PlayRandomSounds : MonoBehaviour, IScheduledUpdateBehaviour
{
    public int scheduledUpdateIndex { get; set; }

    public LiveMixin lm;
    
    public float minDelay;
    public float maxDelay;

    public FMOD_CustomEmitter emitter;

    private float _timePlayAgain;

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public void ScheduledUpdate()
    {
        if (lm != null && !lm.IsAlive())
            return;
        
        if (Time.time > _timePlayAgain)
        {
            emitter.Play();
            _timePlayAgain = Time.time + Random.Range(minDelay, maxDelay);
        }
    }

    public string GetProfileTag()
    {
        return "TRP:PlayRandomSounds";
    }
}