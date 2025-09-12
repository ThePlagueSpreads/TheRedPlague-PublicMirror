using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheRedPlague.Mono.CreatureBehaviour;

public class FlinchingDisease : MonoBehaviour, IScheduledUpdateBehaviour
{
    public const float DiseaseChance = 0.14f;
    private const float MinInterval = 0.5f;
    private const float MaxInterval = 3;
    
    public Animator animator;
    public FlinchAnimationSettings settings;
    
    public int scheduledUpdateIndex { get; set; }

    private bool _registered;

    private float _timeNextFlinch;
    private float _flinchInterval;
    
    private void Start()
    {
        _flinchInterval = Random.Range(MinInterval, MaxInterval);
    }

    private void OnEnable()
    {
        if (!_registered)
        {
            UpdateSchedulerUtils.Register(this);
            _registered = true;
        }
    }

    private void OnDisable()
    {
        if (_registered)
        {
            _registered = false;
            UpdateSchedulerUtils.Deregister(this);
        }
    }

    public static FlinchAnimationSettings GetFlinchSettings(Animator animator)
    {
        bool hasFlinchTrigger = false;
        bool hasFlinchDamageValue = false;
        var parameters = animator.parameters;
        foreach (var param in parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger && param.nameHash == AnimatorHashID.flinch)
            {
                hasFlinchTrigger = true;
            }

            if (param.type == AnimatorControllerParameterType.Float && param.nameHash == AnimatorHashID.flinch_damage)
            {
                hasFlinchDamageValue = true;
            }
        }

        return new FlinchAnimationSettings(hasFlinchDamageValue, hasFlinchTrigger);
    }

    public string GetProfileTag()
    {
        return "TheRedPlague:ConstantFlinching";
    }

    public void ScheduledUpdate()
    {
        if (Time.time < _timeNextFlinch)
            return;
        
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger(AnimatorHashID.flinch);
        
        if (settings.HasFlinchDamageParameter)
        {
            animator.SetFloat(AnimatorHashID.flinch_damage, Random.value);
        }

        _timeNextFlinch = Time.time + _flinchInterval;
    }

    [Serializable]
    public struct FlinchAnimationSettings
    {
        public bool HasFlinchDamageParameter { get; }
        public bool HasFlinchTriggerParameter { get; }

        public FlinchAnimationSettings(bool hasFlinchDamageParameter, bool hasFlinchTriggerParameter)
        {
            HasFlinchDamageParameter = hasFlinchDamageParameter;
            HasFlinchTriggerParameter = hasFlinchTriggerParameter;
        }
    }
}