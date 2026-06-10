using Nautilus.Utility;
using TheRedPlague.Framework.SFX;
using UnityEngine;

namespace TheRedPlague.Content.Act3.DomeBase;

public class DomeBaseAmbienceController : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float shakeDelayMin = 5;
    public float shakeDelayMax = 14;
    public float shakeIntensityMin = 0.3f;
    public float shakeIntensityMax = 0.8f;
    public float shakeDurationMin = 2;
    public float shakeDurationMax = 5;
    public float shakeFrequencyMin = 0.3f;
    public float shakeFrequencyMax = 1;

    public MainCameraControl.ShakeMode[] shakeModes =
    {
        MainCameraControl.ShakeMode.BuildUp,
        MainCameraControl.ShakeMode.Quadratic
    };

    public GenericMusicPlayer ambientSoundPlayer;

    private static DomeBaseAmbienceController _instance;

    private float _timeNextShake;

    private void Awake()
    {
        _instance = this;
    }

    public void ScheduledUpdate()
    {
        if (Time.time < _timeNextShake) return;

        var ambientSoundsActive = ambientSoundPlayer.emitter.playing;
        if (!ambientSoundsActive)
        {
            _timeNextShake = Time.time + shakeDurationMin;
            return;
        }

        _timeNextShake = Time.time + Random.Range(shakeDelayMin, shakeDelayMax);
        MainCameraControl.main.ShakeCamera(Random.Range(shakeIntensityMin, shakeIntensityMax),
            Random.Range(shakeDurationMin, shakeDurationMax), shakeModes[Random.Range(0, shakeModes.Length)],
            Random.Range(shakeFrequencyMin, shakeFrequencyMax));
    }

    private void OnEnable() => UpdateSchedulerUtils.Register(this);
    private void OnDisable() => UpdateSchedulerUtils.Deregister(this);

    public static void PlayFredRevealMusic()
    {
        if (!_instance) return;
        TrpEventMusicPlayer.PlayMusic(AudioUtils.GetFmodAsset("InTheVeins"), 210, false);
    }

    public string GetProfileTag()
    {
        return "TRP:DomeBaseAmbienceController";
    }

    public int scheduledUpdateIndex { get; set; }
}