using UnityEngine;

namespace TheRedPlague.Content.Creatures.Drifter;

public class DrifterAnimationController : MonoBehaviour, IScheduledUpdateBehaviour
{
    private static readonly int Flying = Animator.StringToHash("flying");
    
    public Animator animator;
    
    public int scheduledUpdateIndex { get; set; }

    public string GetProfileTag()
    {
        return "TheRedPlague.Content.Creatures.Drifter:DrifterAnimationController";
    }

    public void ScheduledUpdate()
    {
        animator.SetBool(Flying, transform.position.y > Ocean.GetOceanLevel());
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