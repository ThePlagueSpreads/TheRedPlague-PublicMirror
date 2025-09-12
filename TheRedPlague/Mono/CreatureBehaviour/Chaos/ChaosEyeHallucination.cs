using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

public class ChaosEyeHallucination : MonoBehaviour
{
    public Renderer renderer;
    public float fadeOutDuration = 2f;

    private float _clampedFadeOutDuration;
    private float _endTime;
    private float _startTime;

    private const float MinFade = 0.0001f;
    
    public bool IsExpired { get; private set; }

    public void Prepare(float maxDuration)
    {
        _endTime = Time.time + maxDuration;
        _clampedFadeOutDuration = Mathf.Min(maxDuration / 2f, fadeOutDuration);
        _startTime = Time.time;
        IsExpired = false;
    }
    
    public void DoManagedUpdate()
    {
        renderer.SetFadeAmount(GetFadeAmount());
        transform.LookAt(Player.main.transform.position);
        IsExpired = Time.time > _endTime;
    }

    private float GetFadeAmount()
    {
        if (Time.time < _startTime + _clampedFadeOutDuration)
        {
            return Mathf.Clamp(GenericTrpUtils.RemapValue(Time.time, _startTime,
                _startTime + _clampedFadeOutDuration, 0f, 1f), MinFade, 1f);
        }

        if (Time.time > _endTime - _clampedFadeOutDuration)
        {
            return Mathf.Clamp(GenericTrpUtils.RemapValue(Time.time, _endTime - _clampedFadeOutDuration,
                _endTime, 1f, 0), MinFade, 1f);
        }

        return 1f;
    }
}