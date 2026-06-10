using TheRedPlague.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Content.Creatures.Mutants;

public class MoveTowardsPlayerWhenOffScreen : MonoBehaviour, IScheduledUpdateBehaviour
{
    public float maxDist = 10f;
    public float interval = 1f;
    
    private float _timeCheckAgain;
    
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
        return "TRP:MoveTowardsPlayerWhenOffScreen";
    }

    public void ScheduledUpdate()
    {
        if (Time.time > _timeCheckAgain)
        {
            if (Vector3.SqrMagnitude(MainCamera.camera.transform.position - transform.position) < maxDist * maxDist && !GenericTrpUtils.IsPositionOnScreen(transform.position))
            {
                transform.position = Vector3.MoveTowards(transform.position, MainCamera.camera.transform.position, Random.Range(1, 2));
            }
            _timeCheckAgain = Time.time + interval;
        }
    }
}