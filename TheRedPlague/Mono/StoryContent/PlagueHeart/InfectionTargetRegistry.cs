using System.Collections.Generic;
using TheRedPlague.Interfaces;

namespace TheRedPlague.Mono.StoryContent.PlagueHeart;

public static class InfectionTargetRegistry
{
    private static readonly HashSet<IInfectionTrackerTarget> Targets = new();

    public static void RegisterTarget(IInfectionTrackerTarget target)
    {
        Targets.Add(target);
    }

    public static void UnregisterTarget(IInfectionTrackerTarget target)
    {
        Targets.Remove(target);
    }
    
    public static IInfectionTrackerTarget GetBest()
    {
        IInfectionTrackerTarget best = null;
        var highestPriority = int.MinValue;
        
        foreach (var target in Targets)
        {
            var priority = target.GetTrackingPriority();
            if (priority > highestPriority)
            {
                highestPriority = priority;
                best = target;
            }
        }
        
        return best;
    }
}