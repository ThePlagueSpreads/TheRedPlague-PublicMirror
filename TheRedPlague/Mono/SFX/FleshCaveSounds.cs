using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Mono.SFX;

public class FleshCaveSounds : MonoBehaviour, IScheduledUpdateBehaviour
{
    public int scheduledUpdateIndex { get; set; }

    public float minInterval = 17;
    public float maxInterval = 28;

    public float minDistance = 27;
    public float maxDistance = 38;
    
    private float _timeCheckAgain;

    private static FMODAsset Sound { get; } = AudioUtils.GetFmodAsset("TrpCaveSounds");

    public string GetProfileTag()
    {
        return "TRP:FleshCaveSounds";
    }

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
        if (Time.time < _timeCheckAgain)
            return;
        
        _timeCheckAgain = Time.time + Random.Range(minInterval, maxInterval);

        if (IsPlayerInFleshCave())
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        FMODUWE.PlayOneShot(Sound, MainCameraControl.main.transform.position + Random.onUnitSphere * Random.Range(minDistance, maxDistance));
    }

    private bool IsPlayerInFleshCave()
    {
        if (Player.main == null)
            return false;
        var biomeString = Player.main.GetBiomeString();
        return biomeString.StartsWith("fleshcave");
    }
}