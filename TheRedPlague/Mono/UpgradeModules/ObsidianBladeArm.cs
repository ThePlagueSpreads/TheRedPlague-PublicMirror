using System.Diagnostics.CodeAnalysis;
using Nautilus.Utility;
using TheRedPlague.Data;
using UnityEngine;

namespace TheRedPlague.Mono.UpgradeModules;

public class ObsidianBladeArm : MonoBehaviour, IExosuitArm
{
    public Animator animator;

    private static readonly FMODAsset HitTerrainSound = AudioUtils.GetFmodAsset("event:/sub/exo/claw_hit_terain");
    private static readonly FMODAsset HitFishSound = AudioUtils.GetFmodAsset("event:/sub/exo/claw_hit_fish");
    private static readonly FMODAsset AttackSound = AudioUtils.GetFmodAsset("event:/sub/exo/claw_punch");

    private static readonly int BashParam = Animator.StringToHash("bash");

    public Transform front;
    public VFXController fxControl;

    public float attackCooldown = 1.9f;
    public float attackDistance = 7.5f;
    public float attackDamage = 60;
    public float attackPlagueCuttingDamage = 40;
    public float knockBackForce = 200f;
    public int hitsToBreakDrillables = 2;
    public int hitsPerBreak = 2;

    private Exosuit _exosuit;

    private int _hitCounter;

    private float _timeCanAttackAgain;

    private static readonly VFXEventTypes[] HitEvents = {
        VFXEventTypes.impact,
        VFXEventTypes.diamondBlade
    };

    GameObject IExosuitArm.GetGameObject()
    {
        return gameObject;
    }

    GameObject IExosuitArm.GetInteractableRoot(GameObject target)
    {
        var drillable = target.GetComponent<Drillable>();
        if (drillable != null)
        {
            return drillable.gameObject;
        }

        var breakableResource = target.GetComponentInParent<BreakableResource>();
        if (breakableResource != null)
        {
            return breakableResource.gameObject;
        }

        return null;
    }

    void IExosuitArm.SetSide(Exosuit.Arm arm)
    {
        _exosuit = GetComponentInParent<Exosuit>();
        transform.localScale = new Vector3(arm == Exosuit.Arm.Right ? -1 : 1, 1, 1);
    }

    bool IExosuitArm.OnUseDown([UnscopedRef] out float cooldownDuration)
    {
        return TryUse(out cooldownDuration);
    }

    bool IExosuitArm.OnUseHeld([UnscopedRef] out float cooldownDuration)
    {
        return TryUse(out cooldownDuration);
    }

    bool IExosuitArm.OnUseUp([UnscopedRef] out float cooldownDuration)
    {
        cooldownDuration = 0f;
        return true;
    }

    // block anim later?
    bool IExosuitArm.OnAltDown()
    {
        return false;
    }

    //  ReSharper disable once Unity.IncorrectMethodSignature
    void IExosuitArm.Update(ref Quaternion aimDirection)
    {
    }

    void IExosuitArm.ResetArm()
    {
    }

    private bool TryUse(out float cooldownDuration)
    {
        if (Time.time < _timeCanAttackAgain)
        {
            cooldownDuration = 0;
            return false;
        }
        animator.SetTrigger(BashParam);
        fxControl.Play(0);
        cooldownDuration = attackCooldown;
        Utils.PlayFMODAsset(AttackSound, _exosuit.transform.position);
        _timeCanAttackAgain = Time.time + attackCooldown;
        return true;
    }

    // This is automatically called by the Animator apparently...
    public void OnHit()
    {
        if (!_exosuit.CanPilot() || !_exosuit.GetPilotingMode())
        {
            return;
        }

        Vector3 hitPosition = default;
        GameObject target = null;
        UWE.Utils.TraceFPSTargetPosition(_exosuit.gameObject, attackDistance, ref target, ref hitPosition, out _);
        if (target == null)
        {
            var volume = Player.main.gameObject.GetComponent<InteractionVolumeUser>();
            if (volume != null && volume.GetMostRecent() != null)
            {
                target = volume.GetMostRecent().gameObject;
            }
        }

        if (target == null)
        {
            return;
        }

        var liveMixin = target.FindAncestor<LiveMixin>();
        if (liveMixin)
        {
            liveMixin.TakeDamage(attackDamage, hitPosition);
            liveMixin.TakeDamage(attackPlagueCuttingDamage, hitPosition, CustomDamageTypes.PlagueCutting);
        }
        
        var drillable = target.FindAncestor<Drillable>();
        if (drillable)
        {
            drillable.OnDrill(front.position, _exosuit, out _);
            DrillPunch(drillable, front.position);
        }

        if (target.FindAncestor<Creature>())
        {
            Utils.PlayFMODAsset(HitFishSound, front);
        }
        else
        {
            Utils.PlayFMODAsset(HitTerrainSound, front);
        }

        var vfxSurface = target.GetComponent<VFXSurface>();
        var rotation = MainCameraControl.main.transform.eulerAngles + new Vector3(300f, 90f, 0f);
        foreach (var hitFx in HitEvents)
        {
            VFXSurfaceTypeManager.main.Play(vfxSurface, hitFx, hitPosition, Quaternion.Euler(rotation),
                _exosuit.gameObject.transform);
        }
        target.SendMessage("BashHit", this, SendMessageOptions.DontRequireReceiver);
        TryKnockBack(target);
    }

    private void DrillPunch(Drillable drillable, Vector3 position)
    {
        _hitCounter++;
        if (hitsToBreakDrillables > _hitCounter)
        {
            return;
        }
        _hitCounter = 0;
        
        for (int i = 0; i < hitsPerBreak; i++)
        {
            if (!DamageDeposit(drillable, position))
                break;
        }
    }

    // returns false if destroyed
    private bool DamageDeposit(Drillable drillable, Vector3 position)
    {
        // Check if it has been destroyed
        var totalHealth = 0f;
        foreach (var health in drillable.health)
        {
            totalHealth += health;
        }

        if (totalHealth <= 0f) return false;
        
        var closestIndex = drillable.FindClosestMesh(position, out var center);
        totalHealth -= drillable.health[closestIndex];
        drillable.health[closestIndex] = 0;
        
        drillable.renderers[closestIndex].gameObject.SetActive(value: false);
        drillable.SpawnFX(drillable.breakFX, center);
        if (drillable.resources.Length != 0)
        {
            drillable.StartCoroutine(drillable.SpawnLootAsync(center));
        }

        if (totalHealth <= 0f)
        {
            drillable.SpawnFX(drillable.breakAllFX, center);
            // Replaces the raising of the Drillable.onDrilled event
            var antechamber = drillable.gameObject.GetComponentInParent<AnteChamber>();
            if (antechamber != null)
            {
                antechamber.OnDrilled(drillable);
            }
            if (drillable.deleteWhenDrilled)
            {
                var resourceTracker = GetComponent<ResourceTracker>();
                if (resourceTracker)
                {
                    resourceTracker.OnBreakResource();
                }
                var destroyTime = drillable.lootPinataOnSpawn ? 6f : 0f;
                drillable.Invoke(nameof(Drillable.DestroySelf), destroyTime);
            }

            return false;
        }

        return true;
    }

    private void TryKnockBack(GameObject target)
    {
        var worldForces = target.GetComponentInParent<WorldForces>();
        
        if (worldForces == null)
            return;
        
        var rb = worldForces.useRigidbody;
        if (rb == null)
            return;
        
        var propulsionCannonTarget = target.GetComponentInParent<IPropulsionCannonAmmo>();
        if (propulsionCannonTarget != null && !propulsionCannonTarget.GetAllowedToGrab())
        {
            return;
        }
        
        if (rb.isKinematic)
            rb.isKinematic = false;
        
        // actually apply knock back now
        var force = knockBackForce;
        
        if (rb.mass <= 1)
        {
            force *= rb.mass;
        }
        
        rb.AddForce(MainCamera.camera.transform.forward * force, ForceMode.Impulse);
    }
}