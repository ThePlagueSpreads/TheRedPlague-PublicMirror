using TheRedPlague.Mono.Insanity;

namespace TheRedPlague.Mono.CreatureBehaviour;

public class AvoidPlayerWhenSanityIsHigh : AvoidPlayerActionBase
{
    public float maxInsanityForAvoiding = 10f;
    
    protected override bool ShouldAvoidPlayer()
    {
        return InsanityManager.Main.Insanity < maxInsanityForAvoiding;
    }
}