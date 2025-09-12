using Story;

namespace TheRedPlague.Mono.StoryContent.Precursor;

public class StoryGoalTabletTerminal : CustomTabletTerminalBehaviour
{
    public string associatedStoryGoal;
    
    protected override void OnCinematicModeStarted()
    {
    }

    protected override void OnActivation()
    {
        StoryGoalManager.main.OnGoalComplete(associatedStoryGoal);
    }

    protected override bool LoadSavedSlottedState()
    {
        return StoryGoalManager.main.IsGoalComplete(associatedStoryGoal);
    }

    protected override void SaveStateAsSlotted()
    {
    }
}