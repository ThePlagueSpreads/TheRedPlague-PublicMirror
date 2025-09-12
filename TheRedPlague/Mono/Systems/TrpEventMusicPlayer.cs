using System;
using UnityEngine;

namespace TheRedPlague.Mono.Systems;

public class TrpEventMusicPlayer : MonoBehaviour
{
    private const float MaxExpectedFadeOutTime = 10f;
    
    private static TrpEventMusicPlayer _instance;

    private float _timeCanPlayMusicAgain;
    
    private FMOD_CustomEmitter _currentEmitter;

    private bool _currentTrackImmuneToCancels;

    private void Awake()
    {
        _instance = this;
    }

    public static void PlayMusic(FMODAsset asset, float duration, bool highPriorityTrack, bool immuneToCancel = false)
    {
        try
        {
            var currentTrackStillPlaying = Time.time < _instance._timeCanPlayMusicAgain;
            if (currentTrackStillPlaying && _instance._currentTrackImmuneToCancels)
            {
                return;
            }
            if (highPriorityTrack)
            {
                if (_instance._currentEmitter != null)
                {
                    _instance._currentEmitter.Stop();
                    Destroy(_instance._currentEmitter.gameObject, MaxExpectedFadeOutTime);
                }
            }
            else if (currentTrackStillPlaying)
            {
                return;
            }
            var musicPlayer = new GameObject("MusicPlayer").AddComponent<FMOD_CustomEmitter>();
            musicPlayer.SetAsset(asset);
            musicPlayer.Play();
            Destroy(musicPlayer.gameObject, duration * 2);
            _instance._timeCanPlayMusicAgain = Time.time + duration;
            _instance._currentEmitter = musicPlayer;
            _instance._currentTrackImmuneToCancels = immuneToCancel;
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError("Error while attempting to play " + asset + ": " + e);
        }
    }
}