using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour;

// Allows parasites to grow to their natural size over time
// Acts as a 'tag' so that creatures don't recursively amalgamate
// Also fixes scales in the case that a creature becomes massive for no apparent reason
public class AmalgamationParasite : MonoBehaviour, IScheduledUpdateBehaviour, IManagedUpdateBehaviour
{
    public float desiredLossyScale;
    public int scheduledUpdateIndex { get; set; }
    public int managedUpdateIndex { get; set; }

    private bool _growing;
    private float _growTimeStart;
    private float _growDuration;

    private const float MinScaleForGrowing = 0.01f;
    
    private void Start()
    {
        FixScale();
        UpdateSchedulerUtils.Register(this);
        if (desiredLossyScale == 0f)
        {
            Plugin.Logger.LogWarning("Please don't register a parasite with desired scale of 0!");
        }
    }

    public void StartGrowth(float duration)
    {
        if (!_growing)
        {
            BehaviourUpdateUtils.Register(this);
        }
        _growTimeStart = Time.time;
        _growDuration = duration;
        _growing = true;
        transform.localScale = GetMaxScale() * MinScaleForGrowing;
    }

    private void OnDestroy()
    {
        UpdateSchedulerUtils.Deregister(this);
        if (_growing)
        {
            BehaviourUpdateUtils.Deregister(this);
        }
    }
    
    public void ScheduledUpdate()
    {
        FixScale();
    }
    
    public void ManagedUpdate()
    {
        var scalePercent = GetScalePercentForGrowing();
        transform.localScale = GetMaxScale() * scalePercent;
        if (scalePercent >= 1f)
        {
            BehaviourUpdateUtils.Deregister(this);
            _growing = false;
        }
    }

    private float GetScalePercentForGrowing()
    {
        var scalePercent = Mathf.Clamp((Time.time - _growTimeStart) / _growDuration, MinScaleForGrowing, 1f);
        return scalePercent;
    }

    private void FixScale()
    {
        if (_growing)
            return;
        
        if (transform.lossyScale.x > desiredLossyScale)
        {
            transform.localScale = GetMaxScale();
        }
    }

    private Vector3 GetMaxScale()
    {
        return Vector3.one * (desiredLossyScale / GetParentScale());
    }

    private float GetParentScale()
    {
        if (transform.parent == null) return 1;
        var scale = transform.parent.lossyScale;
        return (scale.x + scale.y + scale.z) / 3;
    }

    public string GetProfileTag()
    {
        return "TRP:AmalgamationParasite";
    }
}