using TheRedPlague.Mono.Util;
using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.CrawlingFlesh;

public class CrawlingFleshMoveRandom : MonoBehaviour, IScheduledUpdateBehaviour
{
    public WalkerBehavior walker;
    public float interval;
    public float radius;

    private float _timeChangeDirection;
    
    public int scheduledUpdateIndex { get; set; }
    
    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public string GetProfileTag()
    {
        return "TRP:CrawlingFleshMoveRandom";
    }

    public void ScheduledUpdate()
    {
        if (Time.time < _timeChangeDirection)
            return;
        _timeChangeDirection = Time.time + interval;
        var angle = Random.value * 2f * Mathf.PI;
        walker.SetTargetPosition(transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius);
    }
}