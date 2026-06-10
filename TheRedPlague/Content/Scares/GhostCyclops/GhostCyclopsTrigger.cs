using Story;
using TheRedPlague.Framework.Triggers;

namespace TheRedPlague.Content.Scares.GhostCyclops;

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