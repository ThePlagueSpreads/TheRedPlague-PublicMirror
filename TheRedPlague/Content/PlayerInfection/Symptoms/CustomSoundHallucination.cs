using System.Collections;
using Nautilus.Utility;
using TheRedPlague.Content.PlayerInfection.Symptoms.Bases;
using TheRedPlague.Registration.Audio;
using UnityEngine;

namespace TheRedPlague.Content.PlayerInfection.Symptoms;

public class CustomSoundHallucination : TimedHallucinationSymptom
{
    private FMODAsset[] _sounds;

    private const float MinDistance = 15;
    private const float MaxDistance = 64;
    
    protected override float MinInterval => 110;
    protected override float MaxInterval => 210;
    protected override float MinInsanity => 30;
    protected override float MaxInsanity => 100;
    protected override float ChanceAtMinInsanity => 0.25f;
    protected override float ChanceAtMaxInsanity => 0.90f;

    protected override IEnumerator OnLoadAssets()
    {
        _sounds = new FMODAsset[ModAudio.HallucinationSoundCount];
        for (int i = 1; i <= ModAudio.HallucinationSoundCount; i++)
        {
            _sounds[i - 1] = AudioUtils.GetFmodAsset("SoundHallucination" + i);
        }
        yield break;
    }

    protected override void OnActivate()
    {
    }

    protected override void OnDeactivate()
    {
    }

    protected override void PerformTimedAction()
    {
        var sound = _sounds[Random.Range(0, _sounds.Length)];
        var pos = Player.main.transform.position + Random.onUnitSphere * Random.Range(MinDistance, MaxDistance);
        Utils.PlayFMODAsset(sound, pos);
    }
}