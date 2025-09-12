using UnityEngine;

namespace TheRedPlague.Mono.StoryContent.B3NT;

public class BennetAnimations : MonoBehaviour, IScheduledUpdateBehaviour
{
    private const float SlerpSpeed = 1.4f;
    private const float LookAtPlayerDistanceSquared = 42 * 42;
    private const float ActiveDistanceSquared = 100 * 100;

    public Animator animator;
    public Transform rotationPivot;

    private static readonly int OpenParam = Animator.StringToHash("open");
    private static readonly int ScanParam = Animator.StringToHash("scan");
    private static readonly int FirstMeetParam = Animator.StringToHash("first_meet");

    private bool _firstMeetAnimEnabled;

    private float _glitchValue;
    private float _glitchTarget;

    private Transform _customLookTarget;
    private bool _customLookTargetInvalid = true;

    private bool _inLoadDistance;

    public void SetFirstMeet(bool hasNeverMet)
    {
        animator.SetBool(FirstMeetParam, hasNeverMet);
        _firstMeetAnimEnabled = hasNeverMet;
    }

    public void SetGlitchAmount(float amount)
    {
        _glitchTarget = amount;
    }

    public void SetOpen(bool open)
    {
        animator.SetBool(OpenParam, open);
    }

    public void SetOverrideLookTarget(Transform target)
    {
        _customLookTarget = target;
        _customLookTargetInvalid = _customLookTarget == null;
    }

    public void PlayScanAnimation()
    {
        Invoke(nameof(SetScanTrigger), 0.5f);
    }

    public void SetEmotionState(BennetEmotionFrame emotion)
    {
        SetOpen(emotion.Open);
        SetGlitchAmount(emotion.GlitchAmount);
    }

    private void SetScanTrigger()
    {
        animator.SetTrigger(ScanParam);
    }

    private bool ShouldLook()
    {
        if (!_customLookTargetInvalid && _customLookTarget != null)
            return true;

        return !_firstMeetAnimEnabled && Vector3.SqrMagnitude(Player.main.transform.position - rotationPivot.position) <
            LookAtPlayerDistanceSquared;
    }

    private void Update()
    {
        if (!_inLoadDistance)
            return;
        
        var lookPosition = !_customLookTargetInvalid && _customLookTarget != null
            ? _customLookTarget.position
            : Player.main.transform.position;
        var targetDirection = ShouldLook() ? lookPosition - rotationPivot.position : transform.forward;
        rotationPivot.rotation = Quaternion.Slerp(rotationPivot.rotation, Quaternion.LookRotation(targetDirection),
            SlerpSpeed * Time.deltaTime);

        if (Mathf.Abs(_glitchValue - _glitchTarget) < 0.001f) return;

        _glitchValue = Mathf.MoveTowards(_glitchValue, _glitchTarget, Time.deltaTime * 2f);
        animator.SetLayerWeight(1, _glitchValue);
    }

    public string GetProfileTag()
    {
        return "BennetAnimations";
    }

    public void ScheduledUpdate()
    {
        _inLoadDistance = (MainCamera.camera.transform.position - transform.position).sqrMagnitude <
                          ActiveDistanceSquared;
    }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
        ScheduledUpdate();
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    public int scheduledUpdateIndex { get; set; }
}