using TheRedPlague.Content.PlayerInfection;

namespace TheRedPlague.Framework.CreatureBehaviours.Actions;

public class AvoidPlayerWhenSanityIsHigh : AvoidPlayerActionBase
{
    public float maxInsanityForAvoiding = 10f;
    
    protected override bool ShouldAvoidPlayer()
    {
        return InsanityManager.Main.Insanity < maxInsanityForAvoiding;
    }
}