using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

// This class is based on ECCLibrary.CreatureVoice
public class ChaosLeviathanRoar : MonoBehaviour, IScheduledUpdateBehaviour
{
    public FMODAsset closeRoarShort;
    public FMODAsset closeRoarLong;
    public FMODAsset farRoarShort;
    public FMODAsset farRoarLong;
    public FMOD_CustomEmitter emitter;

    public bool playSoundOnStart;

    public Animator animator;

    public float farThreshold = 160;
    public float minInterval = 18;
    public float maxInterval = 28;

    public float shortRoarScreenShakeDuration = 4;
    public float longRoarScreenShakeDuration = 5;
    public float maxScreenShakeStrength = 3f;

    public AnimationCurve screenShakeFallOffCurve = new(
        new Keyframe(0, 1),
        new Keyframe(35, 0.7f),
        new Keyframe(100, 0.5f),
        new Keyframe(300, 0));

    private float _timeCanPlaySoundAgain;
    private bool _schedulerRegistered;

    private bool _creatureDead;

    private bool _prepared;

    private static readonly int AnimatorTriggerParamShort = Animator.StringToHash("roar_short");
    private static readonly int AnimatorTriggerParamLong = Animator.StringToHash("roar_long");

    public float TimeLastPlayed { get; private set; }

    public int scheduledUpdateIndex { get; set; }

    public void BlockIdleSoundsForTime(float seconds)
    {
        _timeCanPlaySoundAgain = Mathf.Max(Time.time + seconds, _timeCanPlaySoundAgain);
    }

    private void Start()
    {
        Prepare();
    }

    private void Prepare()
    {
        if (_prepared)
            return;

        if (!playSoundOnStart)
        {
            _timeCanPlaySoundAgain = Time.time + Random.Range(minInterval, maxInterval);
        }

        _prepared = true;
    }

    private void OnEnable()
    {
        Prepare();
        if (!_creatureDead && !_schedulerRegistered)
        {
            UpdateSchedulerUtils.Register(this);
            _schedulerRegistered = true;
        }
    }

    private void OnDisable()
    {
        if (_schedulerRegistered)
        {
            UpdateSchedulerUtils.Deregister(this);
            _schedulerRegistered = false;
        }
    }

    void IScheduledUpdateBehaviour.ScheduledUpdate()
    {
        if (Time.time < _timeCanPlaySoundAgain) return;

        PlayIdleSound();
        _timeCanPlaySoundAgain = Time.time + Random.Range(minInterval, maxInterval);
    }

    private void PlayIdleSound()
    {
        if (!_prepared)
        {
            Plugin.Logger.LogError("Chaos leviathan is not ready to play audio!");
        }

        var shortRoar = Random.value < 0.5f;
        if (Vector3.Distance(MainCamera.camera.transform.position, transform.position) >= farThreshold)
        {
            emitter.SetAsset(shortRoar ? farRoarShort : farRoarLong);
        }
        else
        {
            emitter.SetAsset(shortRoar ? closeRoarShort : closeRoarLong);
        }

        emitter.Play();
        if (animator != null)
        {
            animator.SetTrigger(shortRoar ? AnimatorTriggerParamShort : AnimatorTriggerParamLong);
        }

        var screenShakeFallOffMultiplier =
            screenShakeFallOffCurve.Evaluate(Vector3.Distance(MainCamera.camera.transform.position,
                transform.position));
        if (screenShakeFallOffMultiplier > Mathf.Epsilon)
        {
            MainCameraControl.main.ShakeCamera(maxScreenShakeStrength * screenShakeFallOffMultiplier,
                shortRoar ? shortRoarScreenShakeDuration : longRoarScreenShakeDuration,
                MainCameraControl.ShakeMode.BuildUp, 1.1f);
        }

        TimeLastPlayed = Time.time;
    }

    string IManagedBehaviour.GetProfileTag()
    {
        return "TheRedPlague:ChaosVoice";
    }

    // This is a Unity message called by Subnautica's LiveMixin class
    private void OnKill()
    {
        _creatureDead = true;
        if (_schedulerRegistered)
        {
            UpdateSchedulerUtils.Deregister(this);
            _schedulerRegistered = false;
        }
    }
}