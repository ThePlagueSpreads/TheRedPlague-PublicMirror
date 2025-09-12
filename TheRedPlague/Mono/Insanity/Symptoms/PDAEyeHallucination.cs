using System.Collections;
using Nautilus.Utility;
using Story;
using UnityEngine;

namespace TheRedPlague.Mono.Insanity.Symptoms;

public class PdaEyeHallucination : InsanitySymptom
{
    private const float MinInsanity = 45;
    private const float RollInterval = 4;
    private const float RollChance = 0.25f;
    
    private GameObject _prefab;

    private GameObject _currentEye;
    
    private bool IsPdaOpen => Player.main.pda.isOpen;

    private float _timeRollAgain;
    private bool _rolledSuccessfully;

    private bool _storyProgressionAcquired;
    
    protected override IEnumerator OnLoadAssets()
    {
        var model = Plugin.CreaturesBundle.LoadAsset<GameObject>("PDACameraEyePrefab");
        if (model == null)
        {
            Plugin.Logger.LogError("Failed to find prefab for PDAEyeHallucination!");
            yield break;
        }
        _prefab = Instantiate(model);
        _prefab.SetActive(false);
        _prefab.AddComponent<SkyApplier>().renderers = _prefab.GetComponentsInChildren<Renderer>();
        MaterialUtils.ApplySNShaders(_prefab, 8);
    }

    protected override void OnActivate()
    {
        if (_currentEye == null)
            Spawn();
    }

    private void Spawn()
    {
        _currentEye = Instantiate(_prefab, Player.main.pda.transform);
        _currentEye.transform.localPosition = new Vector3(-0.074f, 0.099f, 0.010f);
        _currentEye.transform.localEulerAngles = new Vector3(72, 145, 180);
        _currentEye.transform.localScale = Vector3.one * 0.008f;
        _currentEye.SetActive(true);
    }

    protected override void OnDeactivate()
    {
        Destroy(_currentEye);
    }

    protected override bool ShouldDisplaySymptoms()
    {
        if (Player.main.pda == null)
            return false;
        
        if (!_storyProgressionAcquired)
        {
            // Check regularly
            _storyProgressionAcquired = IsStoryProgressCompleted();
            
            // If it STILL isn't complete
            if (!_storyProgressionAcquired)
                return false;
        }

        if (_storyProgressionAcquired && StoryGoalManager.main.IsGoalComplete(StoryUtils.ExplodePda.key))
        {
            return false;
        }
        
        UpdateRoll();
        
        // Effect lingers with previous value while the PDA is open:
        if (IsPdaOpen)
            return IsSymptomActive;

        // Primary preconditions:
        if (InsanityPercentage < MinInsanity)
            return false;
        
        if (_prefab == null)
            return false;
        
        // Balancing logic, etc:
        return _rolledSuccessfully;
    }

    private bool IsStoryProgressCompleted()
    {
        return StoryGoalManager.main.IsGoalComplete(StoryUtils.BennetIntroduction.key);
    }

    private void UpdateRoll()
    {
        if (Time.time < _timeRollAgain)
            return;
        _rolledSuccessfully = RollChance >= Random.value;
        _timeRollAgain = Time.time + RollInterval;
    }
}