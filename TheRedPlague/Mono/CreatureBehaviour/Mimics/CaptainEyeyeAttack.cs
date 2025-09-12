using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Mimics;

public class CaptainEyeyeAttack : RangedAttackLastTarget, IManagedLateUpdateBehaviour
{
    public float regrowDuration = 18f;
    public float despawnEyeDelay = 20f;
    public float launchVelocity = 40;
    public PhysicMaterial eyePhysicsMaterial;

    public Transform eyeTransform;
    public GameObject model;

    private bool _eyeGrowingBack;

    private float _timeStartGrowing;

    public int managedLateUpdateIndex { get; set; }

    public override void Cast(RangedAttackType attackType, Vector3 directionToTarget)
    {
        ShootEye();
    }

    private void ShootEye()
    {
        creature.GetAnimator().SetTrigger("attack");

        _eyeGrowingBack = true;
        BehaviourUpdateUtils.Register(this);

        _timeStartGrowing = Time.time;

        var clonedModel = Instantiate(model, model.transform.position, model.transform.rotation);
        clonedModel.GetComponent<Animator>().enabled = false;
        clonedModel.transform.Find("EyeyeCaptainBody").gameObject.SetActive(false);
        clonedModel.transform.Find("EyeyeCaptainEye").gameObject.GetComponent<SkinnedMeshRenderer>().localBounds =
            new Bounds(Vector3.zero, Vector3.one * 100000);
        var newEye = clonedModel.transform.Find("EyeyeCaptainArmature/BodyRoot/Spine/Neck/Eye").gameObject;
        var eyeCollider = newEye.AddComponent<SphereCollider>();
        eyeCollider.enabled = false;
        eyeCollider.radius = 0.0045f;
        eyeCollider.sharedMaterial = eyePhysicsMaterial;
        var newEyeRb = newEye.gameObject.AddComponent<Rigidbody>();
        newEyeRb.mass = 2;
        newEyeRb.AddForce(transform.forward * launchVelocity, ForceMode.VelocityChange);
        var worldForces = newEye.AddComponent<WorldForces>();
        worldForces.useRigidbody = newEyeRb;
        newEye.AddComponent<CaptainEyeyeDamageOnCollide>().collider = eyeCollider;

        Destroy(clonedModel, despawnEyeDelay);
    }

    private bool CanAttack() => !_eyeGrowingBack;

    public override float Evaluate(Creature creature, float time)
    {
        if (!CanAttack())
            return 0;

        return base.Evaluate(creature, time);
    }

    public string GetProfileTag()
    {
        return "TRP:CaptainEyeyeAttack";
    }

    public void ManagedLateUpdate()
    {
        if (Time.time > _timeStartGrowing + regrowDuration)
        {
            OnEyeFinishGrowing();
            return;
        }

        eyeTransform.localScale = Vector3.one * Mathf.Clamp01((Time.time - _timeStartGrowing) / regrowDuration);
    }

    public override void Perform(Creature creature, float time, float deltaTime)
    {
        base.Perform(creature, time, deltaTime);
        if (currentTarget != null)
            swimBehaviour.LookAt(currentTarget.transform);
    }

    public override void StopPerform(Creature creature, float time)
    {
        base.StopPerform(creature, time);
        swimBehaviour.LookAt(null);
    }

    private void OnEyeFinishGrowing()
    {
        if (_eyeGrowingBack)
        {
            BehaviourUpdateUtils.Deregister(this);
        }

        _eyeGrowingBack = false;
        eyeTransform.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        if (_eyeGrowingBack)
        {
            BehaviourUpdateUtils.Deregister(this);
            _eyeGrowingBack = false;
        }
    }
}