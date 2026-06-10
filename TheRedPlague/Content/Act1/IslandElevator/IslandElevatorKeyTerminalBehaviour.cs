using Nautilus.Utility;
using Story;
using TheRedPlague.Framework.Behaviour.Precursor;

namespace TheRedPlague.Content.Act1.IslandElevator;

public class IslandElevatorKeyTerminalBehaviour : CustomTabletTerminalBehaviour
{
    protected override void OnCinematicModeStarted()
    {
        if (IslandElevatorBehaviour.Main)
        {
            Utils.PlayFMODAsset(AudioUtils.GetFmodAsset("IslandElevatorActivation"),
                IslandElevatorBehaviour.Main.transform.position);
        }
    }

    protected override void OnActivation()
    {
        Act1Story.IslandElevatorActivatedGoal.Trigger();
    }

    protected override bool LoadSavedSlottedState()
    {
        return StoryGoalManager.main.IsGoalComplete(Act1Story.IslandElevatorActivatedGoal.key);
    }

    protected override void SaveStateAsSlotted()
    {
        
    }
}