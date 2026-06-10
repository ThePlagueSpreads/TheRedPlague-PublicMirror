using System.Collections;
using Nautilus.Utility;
using TheRedPlague.Content.PlayerInfection.Symptoms.Bases;
using UnityEngine;

namespace TheRedPlague.Content.PlayerInfection.Symptoms;

public class CrungusHallucination : LongTimedHallucinationSymptom
{
    protected override float MinInterval => 43 * 60;
    protected override float MaxInterval => 67 * 60;
    protected override float MinInsanity => 10;
    protected override float MaxInsanity => 70;
    protected override float CheckInterval => 15;
    protected override float ChanceAtMinInsanity => 0.8f;
    protected override float ChanceAtMaxInsanity => 1;
    protected override string UniqueId => "Crungus";

    private static readonly FMODAsset Sound = AudioUtils.GetFmodAsset("Crungus");
    private const int MinCalls = 3;
    private const int MaxCalls = 6;
    private const float MinCallDistanceHorizontal = 60;
    private const float MaxCallDistanceHorizontal = 90;
    private const float MinCallDistanceVertical = 10;
    private const float MaxCallDistanceVertical = 20;
    private const float MinCallInterval = 5;
    private const float MaxCallInterval = 10;
    
    protected override void OnActivate()
    {
        
    }

    protected override void OnDeactivate()
    {
        
    }
    
    protected override void PerformTimedAction()
    {
        StartCoroutine(PerformCoroutine());
    }

    private IEnumerator PerformCoroutine()
    {
        var random = Random.onUnitSphere;
        var horizontalDistance = Random.Range(MinCallDistanceHorizontal, MaxCallDistanceHorizontal);
        
        var position = MainCameraControl.main.transform.position + new Vector3(random.x * horizontalDistance,
            random.y * Random.Range(MinCallDistanceVertical, MaxCallDistanceVertical),
            random.z * horizontalDistance);

        var calls = Random.Range(MinCalls, MaxCalls + 1);

        for (int i = 0; i < calls; i++)
        {
            FMODUWE.PlayOneShot(Sound, position);
            yield return new WaitForSeconds(Random.Range(MinCallInterval, MaxCallInterval));
        }
    }
}