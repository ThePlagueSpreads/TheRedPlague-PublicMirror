using TheRedPlague.Content.Act1.Dome;
using TheRedPlague.Content.Act2.Ending;
using UnityEngine;

namespace TheRedPlague.Content.Act3.DomeBase;

public class PlayerInsideDomeChecker : MonoBehaviour, IScheduledUpdateBehaviour
{
    public Transform center;
    public float maxDistance;
    
    public int scheduledUpdateIndex { get; set; }
    
    public string GetProfileTag()
    {
        return "TRP:PlayerDomeEnterer";
    }

    public void ScheduledUpdate()
    {
        var playerIsInDome = Vector3.SqrMagnitude(Player.main.transform.position - center.position) < maxDistance * maxDistance && !PreventPlayerFromBeingInDomeBase();
        if (playerIsInDome)
        {
            DomeBaseUtils.EnterDomeBase();
        }
        else
        {
            DomeBaseUtils.ExitDomeBase();
        }
    }

    private bool PreventPlayerFromBeingInDomeBase()
    {
        if (Player.main.cinematicModeActive)
            return true;

        if (PlagueHeartDestructionEvent.EventIsActive)
            return true;
        
        return false;
    }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }
}