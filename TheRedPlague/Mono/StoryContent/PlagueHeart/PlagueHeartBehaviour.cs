using Story;
using TheRedPlague.Mono.VFX;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent.PlagueHeart;

public class PlagueHeartBehaviour : MonoBehaviour, IScheduledUpdateBehaviour
{
    public static PlagueHeartBehaviour Main { get; private set; }
    
    public int scheduledUpdateIndex { get; set; }

    public ParticleSystem[] outerLeakParticles;

    // Meteorite
    public Rigidbody[] meteoriteFragments;
    public ParticleSystem[] meteoriteParticles;
    public float explosionRadius = 30;
    public float explosionVelocity = 10;
    public float fragmentFadeOutDelay = 4;
    public float fragmentFadeOutDuration = 6;
    public float fragmentDisableDelay = 2;

    // Force field
    public FMOD_CustomLoopingEmitter forceFieldEmitter;
    public FMODAsset forceFieldDisableSound;
    public VFXLerpColor forceFieldColorControl;
    public float maxDistanceForForceFieldSound = 80f;

    // Hatch
    public FMODAsset hatchOpenSound;
    public Animator hatchAnimator;
    
    // Egg
    public Animator eggAnimator;
    public ParticleSystem interiorBloodParticles;

    private static readonly int AnimParamInstant = Animator.StringToHash("instant");
    private static readonly int AnimParamOpen = Animator.StringToHash("open");

    private bool _forcefieldActive;

    private void Start()
    {
        Main = this;
        
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.MeteorExplodeGoal.key))
        {
            DisableMeteorAfterReload();
        }

        var forcefieldDisabled = StoryGoalManager.main.IsGoalComplete(StoryUtils.PlagueHeartForceFieldDisableGoal.key);
        if (forcefieldDisabled)
        {
            DisableForceFieldAfterReload();
        }

        _forcefieldActive = !forcefieldDisabled;

        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.OpenPlagueHeartHatchGoal.key))
        {
            OpenHatchAfterReload();
        }
        
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.MeteorEggHatchGoal.key))
        {
            HatchEggAfterReload();
        }

        UpdateSchedulerUtils.Register(this);
    }

    public void ScheduledUpdate()
    {
        if (!isActiveAndEnabled)
            return;

        if (_forcefieldActive && Vector3.SqrMagnitude(MainCamera.camera.transform.position - transform.position) <
            maxDistanceForForceFieldSound * maxDistanceForForceFieldSound)
        {
            forceFieldEmitter.Play();
        }
        else
        {
            forceFieldEmitter.Stop();
        }
    }

    private void OnDestroy()
    {
        UpdateSchedulerUtils.Deregister(this);
    }

    private void DisableMeteorAfterReload()
    {
        foreach (var fragment in meteoriteFragments)
        {
            fragment.gameObject.SetActive(false);
        }

        SetParticlesActive(outerLeakParticles, false);
    }

    private void DisableForceFieldAfterReload()
    {
        forceFieldColorControl.gameObject.SetActive(false);
    }

    private void OpenHatchAfterReload()
    {
        hatchAnimator.SetBool(AnimParamInstant, true);
        hatchAnimator.SetBool(AnimParamOpen, true);
    }

    private void HatchEggAfterReload()
    {
        eggAnimator.SetBool(AnimParamInstant, true);
        eggAnimator.SetBool(AnimParamOpen, true);
    }

    public void ExplodeMeteor()
    {
        foreach (var fragment in meteoriteFragments)
        {
            if (fragment == null)
            {
                Plugin.Logger.LogWarning("Nonexistent meteorite fragment found in plague heart!");
                continue;
            }

            fragment.isKinematic = false;
            fragment.AddExplosionForce(explosionVelocity, transform.position,
                explosionRadius, 0, ForceMode.VelocityChange);
        }

        foreach (var particle in meteoriteParticles)
        {
            particle.Play();
        }

        SetParticlesActive(outerLeakParticles, false);
        Invoke(nameof(FadeOutMeteorFragments), fragmentFadeOutDelay);

        StoryUtils.MeteorExplodeGoal.Trigger();
        
        interiorBloodParticles.Play();
    }

    private void FadeOutMeteorFragments()
    {
        foreach (var fragment in meteoriteFragments)
        {
            var fadeOut = fragment.gameObject.AddComponent<FadeOutOfExistence>();
            fadeOut.duration = fragmentFadeOutDuration;
            fadeOut.disableDelay = fragmentDisableDelay;
            fadeOut.renderers = fadeOut.GetComponentsInChildren<MeshRenderer>();
        }
    }

    public void DisableForceField()
    {
        forceFieldEmitter.Stop();
        Utils.PlayFMODAsset(forceFieldDisableSound, transform.position);
        forceFieldColorControl.Play();
        Invoke(nameof(DisableForceFieldModel), forceFieldColorControl.duration + 0.001f);

        StoryUtils.PlagueHeartForceFieldDisableGoal.Trigger();
        _forcefieldActive = false;
    }

    public void OpenHatch()
    {
        hatchAnimator.SetBool(AnimParamInstant, false);
        hatchAnimator.SetBool(AnimParamOpen, true);
        Utils.PlayFMODAsset(hatchOpenSound, transform.position);

        StoryUtils.OpenPlagueHeartHatchGoal.Trigger();
        interiorBloodParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        var main = interiorBloodParticles.main;
        main.gravityModifier = -0.05f;

        PlagueScreenFXController.TryStartMeteorSiteEffect();
    }

    public void HatchEgg()
    {
        eggAnimator.SetBool(AnimParamInstant, false);
        eggAnimator.SetBool(AnimParamOpen, true);
        StoryUtils.MeteorEggHatchGoal.Trigger();
    }

    private void DisableForceFieldModel()
    {
        forceFieldColorControl.gameObject.SetActive(false);
    }

    private void SetParticlesActive(ParticleSystem[] particles, bool active)
    {
        foreach (var ps in particles)
        {
            if (active)
                ps.Play();
            else
                ps.Stop();
        }
    }

    public string GetProfileTag()
    {
        return "TRP:PlagueHeartBehaviour";
    }
}