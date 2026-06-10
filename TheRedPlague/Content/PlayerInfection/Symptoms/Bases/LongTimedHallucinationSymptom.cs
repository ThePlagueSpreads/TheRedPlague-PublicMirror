using System.Collections;
using System.Collections.Generic;
using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Content.PlayerInfection.Symptoms.Bases;

public abstract class LongTimedHallucinationSymptom : InsanitySymptom
{
    protected abstract float MinInterval { get; }
    protected abstract float MaxInterval { get; }
    protected abstract float MinInsanity { get; }
    protected abstract float MaxInsanity { get; }
    protected abstract float CheckInterval { get; }
    protected abstract float ChanceAtMinInsanity { get; }
    protected abstract float ChanceAtMaxInsanity { get; }
    protected abstract string UniqueId { get; }

    private static SaveData _saveData;

    protected override IEnumerator OnLoadAssets()
    {
        InitializeSaveData();
        
        if (!_saveData.DoesSaveDataExist(UniqueId))
        {
            SetTimePerformAgain(DayNightCycle.main.timePassedAsDouble + Random.Range(MinInterval, MaxInterval));
        }

        yield break;
    }

    private void InitializeSaveData()
    {
        if (_saveData == null)
        {
            _saveData = SaveDataHandler.RegisterSaveDataCache<SaveData>();
            _saveData.Load();
        }
    }

    protected override void PerformSymptoms(float dt)
    {
        if (DayNightCycle.main.timePassedAsDouble < GetTimePerformAgain())
            return;
        
        bool fail = Random.value > GetChance();

        if (fail)
        {
            SetTimePerformAgain(DayNightCycle.main.timePassedAsDouble + CheckInterval);
            return;
        }
        
        SetTimePerformAgain(DayNightCycle.main.timePassedAsDouble + Random.Range(MinInterval, MaxInterval));
        PerformTimedAction();
    }

    private double GetTimePerformAgain()
    {
        return _saveData.GetNextPlayTime(UniqueId);
    }

    private void SetTimePerformAgain(double time)
    {
        _saveData.SetNextPlayTime(UniqueId, time);
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

    [FileName("SavedHallucinationTimes")]
    private class SaveData : SaveDataCache
    {
        // Do not access publicly - only public for serialization purposes
        public Dictionary<string, double> NextPlayTimes { get; private set; }
        
        public bool DoesSaveDataExist(string id)
        {
            NextPlayTimes ??= new Dictionary<string, double>();
            return NextPlayTimes.ContainsKey(id);
        }

        public double GetNextPlayTime(string id)
        {
            if (NextPlayTimes.TryGetValue(id, out var value))
            {
                return value;
            }

            Plugin.Logger.LogError("No next play time exists for insanity symptom: " + id);
            return 0;
        }

        public void SetNextPlayTime(string id, double time)
        {
            NextPlayTimes[id] = time;
        }
    }
}