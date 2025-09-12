using UnityEngine;

namespace TheRedPlague.Mono.InfectionLogic;

public class InfectedVehicle : MonoBehaviour, IManagedUpdateBehaviour
{
    private const float MoveVelocity = 2f;
    public bool isExosuit;

    private Animator _animator;
    private Rigidbody _rb;

    private float _lastSpeedValue;
    
    public int managedUpdateIndex { get; set; }

    private void Awake()
    {
        var vehicle = GetComponent<Vehicle>();
        vehicle.enabled = false;

        // exosuit-only stuff
        if (!isExosuit) return;
        
        _animator = vehicle.mainAnimator;
        if (_animator == null)
        {
            Plugin.Logger.LogWarning("Possessed exosuit animator not found!");
        }

        _rb = vehicle.useRigidbody;
    }

    private void Start()
    {
        GetComponent<LiveMixin>().health = 0;
        var vehicle = GetComponent<Vehicle>();
        vehicle.constructionFallOverride = true;
        vehicle.enabled = false;
        vehicle.useRigidbody.isKinematic = false;

        if (isExosuit)
        {
            _animator.SetBool("onGround", true);
            var exosuit = vehicle as Exosuit;
            exosuit.UpdateExosuitArms();
        }
    }

    private void OnEnable()
    {
        if (isExosuit)
            BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        if (isExosuit)
            BehaviourUpdateUtils.Deregister(this);
    }

    public string GetProfileTag()
    {
        return "TRP:InfectedVehicle";
    }

    public void ManagedUpdate()
    {
        if (!_animator || !_rb)
            return;
        _lastSpeedValue = Mathf.MoveTowards(_lastSpeedValue, Mathf.Clamp01(new Vector2(_rb.velocity.x, _rb.velocity.z).magnitude / MoveVelocity), Time.deltaTime * 3f);
        _animator.SetFloat("move_speed_z", _lastSpeedValue);
    }
}