using UnityEngine;

namespace TheRedPlague.Content.Scares;

public class RepeatedSoundPlayer : MonoBehaviour, IScheduledUpdateBehaviour
{
    public DestroyMode destroyMode;
    public int minSoundPlays;
    public int maxSoundPlays;
    public float minInterval;
    public float maxInterval;
    public float maxDistanceRandomization;
    public FMODAsset[] sounds;
    public Vector3 playSoundPosition;

    private int _plays;
    private int _repeatLimit;
    private float _timeNextSound;
    private bool _ended;
    
    public int scheduledUpdateIndex { get; set; }

    private void Start()
    {
        _repeatLimit = Random.Range(minSoundPlays, maxSoundPlays);
    }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public string GetProfileTag()
    {
        return "TRP:StationaryDistantSound";
    }

    public void ScheduledUpdate()
    {
        if (_ended)
            return;

        if (Time.time > _timeNextSound)
            PlaySound();
    }

    private void PlaySound()
    {
        _timeNextSound = Time.time + Random.Range(minInterval, maxInterval);
            
        _plays++;
        
        FMODUWE.PlayOneShot(sounds[Random.Range(0, sounds.Length)], GetRandomPosition());
        
        if (_plays >= _repeatLimit)
        {
            OnReachedLimit();
        }
    }

    private Vector3 GetRandomPosition()
    {
        if (Mathf.Approximately(maxDistanceRandomization, 0))
            return playSoundPosition;

        return playSoundPosition + Random.onUnitSphere * (Random.value * maxDistanceRandomization);
    }

    private void OnReachedLimit()
    {
        if (destroyMode == DestroyMode.Component)
            Destroy(this);
        else if (destroyMode == DestroyMode.GameObject)
            Destroy(gameObject);
        _ended = true;
    }
    
    public enum DestroyMode
    {
        None,
        Component,
        GameObject
    }
}