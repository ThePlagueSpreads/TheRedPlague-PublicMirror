using System.Collections;

namespace TheRedPlague.Content.PlayerInfection.Symptoms;

public class InfectionWarnings : InsanitySymptom
{
    private const float MinInsanity = 30;

    protected override IEnumerator OnLoadAssets()
    {
        yield break;
    }

    protected override void OnActivate()
    {
        GeneralStory.InsanityWarning.Trigger();
    }

    protected override void OnDeactivate()
    {
        
    }

    protected override bool ShouldDisplaySymptoms()
    {
        return InsanityPercentage > MinInsanity;
    }
}