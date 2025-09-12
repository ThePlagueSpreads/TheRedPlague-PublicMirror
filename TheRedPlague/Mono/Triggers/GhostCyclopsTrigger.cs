using Story;
using TheRedPlague.Mono.CinematicEvents;

namespace TheRedPlague.Mono.Triggers;

public class GhostCyclopsTrigger : PlayerTrigger
{
    public GhostCyclopsCinematic.Path path;
    
    private string GetStoryGoalKeyName() => $"GhostCyclopsCinematic_{path}";
    
    protected override void OnTriggerActivated()
    {
        var goalName = GetStoryGoalKeyName();
        if (StoryGoalManager.main.IsGoalComplete(goalName))
        {
            Destroy(this);
            return;
        }

        StoryGoalManager.main.OnGoalComplete(goalName);
        GhostCyclopsCinematic.StartCinematic(path);
    }
}