using TheRedPlague.Mono.CinematicEvents;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent;

public class TriggerThrusterEventAtSurface : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float maxDepth = 10;
    public float maxDistance = 1800;
    public Vector3 auroraCenter = new(945, 0, -265);
    
    public int scheduledUpdateIndex { get; set; }

    private bool _triggered;
    
    public string GetProfileTag()
    {
        return "TRP:TriggerThrusterEventAtSurface";
    }

    public void ScheduledUpdate()
    {
        if (_triggered)
            return;
        
        if (!Player.main.IsSwimming())
        {
            return;
        }
        var depth = Ocean.GetDepthOf(Player.main.gameObject);
        if (depth > maxDepth)
        {
            return;
        }

        if (Vector3.SqrMagnitude(Player.main.transform.position - auroraCenter) > maxDistance * maxDistance)
        {
            return;
        }

        _triggered = true;
        AuroraThrusterEvent.PlayCinematic();
        Destroy(gameObject);
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