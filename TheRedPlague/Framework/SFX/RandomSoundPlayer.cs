using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Framework.SFX;

public class RandomSoundPlayer : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float minDelay;
    public float maxDelay;
    public float maxDistance;
    public RepeatMode repeatMode;
    public bool useScreenShake;
    public float screenShakeDuration;

    public FMOD_CustomEmitter emitter;

    private float _timePlayAgain;
    private int _timesPlayed;
    private int _maxPlays;
    
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
        _maxPlays = CalculateMaxPlays();
    }

    public void ScheduledUpdate()
    {
        if (Time.time < _timePlayAgain) return;
        bool canPlayAgain  = GetCanPlayAgain();
        if (!canPlayAgain) return;
        var distance = Vector3.SqrMagnitude(MainCamera.camera.transform.position - transform.position);
        var inRange = distance < maxDistance * maxDistance;
        if (inRange)
        {
            emitter.Play();
            _timesPlayed++;
            if (useScreenShake && distance < 100)
            {
                MainCameraControl.main.ShakeCamera(0.3f, screenShakeDuration);
            }
        }
        if (GetCanPlayAgain() && inRange)
            _timePlayAgain = Time.time + Random.Range(minDelay, maxDelay);
    }

    private bool GetCanPlayAgain()
    {
        if (repeatMode == RepeatMode.Endless)
            return true;
        return _maxPlays < 0 || _timesPlayed < _maxPlays;
    }
    
    private int CalculateMaxPlays()
    {
        switch (repeatMode)
        {
            case RepeatMode.Endless:
                return -1;
            case RepeatMode.Once:
                return 1;
            case RepeatMode.FewTimes:
                return Random.Range(3, 4 + 1);
        }

        Plugin.Logger.LogError($"Undefined RepeatMode: {repeatMode} ({this})");
        
        return -1;
    }

    public string GetProfileTag()
    {
        return "TRP:RandomSoundPlayer";
    }
}