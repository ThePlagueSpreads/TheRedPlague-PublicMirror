using System;
using System.Collections;
using System.Collections.Generic;
using TheRedPlague.Content.Buildables.PlagueAltar;
using TheRedPlague.Content.PlayerInfection.Symptoms.Bases;
using TheRedPlague.Framework.API.Saving;
using TheRedPlague.Framework.Behaviour.Horror;
using TheRedPlague.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Content.PlayerInfection.Symptoms;

public class PlagueAltarHallucination : TimedHallucinationSymptom
{
    protected override IEnumerator OnLoadAssets()
    {
        yield break;
    }
    
    protected override float MinInterval => 80;
    protected override float MaxInterval => 120;
    protected override float MinInsanity => 65;
    protected override float MaxInsanity => 90;
    protected override float ChanceAtMinInsanity => 0.20f;
    protected override float ChanceAtMaxInsanity => 0.50f;

    public int limitPerSave = 1;

    protected override void OnActivate()
    {
    }

    protected override void OnDeactivate()
    {
    }

    protected override bool ShouldDisplaySymptoms()
    {
        return CommonSaveCache.Data.PlagueAltarScareCount < limitPerSave && base.ShouldDisplaySymptoms() && Player.main.IsInBase();
    }

    protected override void PerformTimedAction()
    {
        if (CommonSaveCache.Data.PlagueAltarScareCount >= limitPerSave)
        {
            Plugin.Logger.LogWarning("Plague altar scare event happening too many times!");
            return;
        }
        
        if (AltarIntrusionEvent.TriggerTest(18))
        {
            CommonSaveCache.Data.IncrementPlagueAltarScareCount();
        }
    }
}