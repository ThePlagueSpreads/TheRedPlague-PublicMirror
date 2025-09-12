using UnityEngine;

namespace TheRedPlague.Mono.SFX;

public class RandomSoundPlayer : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float minDelay;
    public float maxDelay;
    public float maxDistance;
    public bool playOnce;
    public bool useScreenShake;
    public float screenShakeDuration;

    public FMOD_CustomEmitter emitter;

    private float _timePlayAgain;
    
    public int scheduledUpdateIndex { get; set; }

    private bool _playedAlready;

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
        if (Time.time < _timePlayAgain) return;
        if (playOnce && _playedAlready) return;
        var distance = Vector3.SqrMagnitude(MainCamera.camera.transform.position - transform.position);
        var inRange = distance < maxDistance * maxDistance;
        if (inRange)
        {
            emitter.Play();
            _playedAlready = true;
            if (useScreenShake && distance < 100)
            {
                MainCameraControl.main.ShakeCamera(0.3f, screenShakeDuration);
            }
        }
        if (!playOnce || inRange)
            _timePlayAgain = Time.time + Random.Range(minDelay, maxDelay);
    }

    public string GetProfileTag()
    {
        return "TRP:RandomSoundPlayer";
    }
}