using UnityEngine;

namespace TheRedPlague.Content.PlayerInfection.Symptoms.Bases;

public abstract class TimedHallucinationSymptom : InsanitySymptom
{
    protected abstract float MinInterval { get; }
    protected abstract float MaxInterval { get; }
    protected abstract float MinInsanity { get; }
    protected abstract float MaxInsanity { get; }
    protected abstract float ChanceAtMinInsanity { get; }
    protected abstract float ChanceAtMaxInsanity { get; }
    protected virtual float FailDelayMultiplier => 1f;
    
    private float _timePerformAgain;

    private void Start()
    {
        ResetTimer(false);
    }

    protected override void PerformSymptoms(float dt)
    {
        if (Time.time < _timePerformAgain)
            return;
        
        bool fail = Random.value > GetChance();

        if (fail)
        {
            ResetTimer(true);
            return;
        }
        
        ResetTimer(false);
        PerformTimedAction();
    }

    private void ResetTimer(bool onFail)
    {
        var delay = Random.Range(MinInterval, MaxInterval);
        
        if (onFail)
        {
            delay *= FailDelayMultiplier;
        }

        _timePerformAgain = Time.time + delay;
    }

    protected abstract void PerformTimedAction();

    private float GetChance()
    {
        var percent = Mathf.InverseLerp(MinInsanity, MaxInsanity, InsanityPercentage);
        return Mathf.Lerp(ChanceAtMinInsanity, ChanceAtMaxInsanity, percent);
    }

    protected override bool ShouldDisplaySymptoms()
    {
        return InsanityPercentage >= MinInsanity;
    }
}