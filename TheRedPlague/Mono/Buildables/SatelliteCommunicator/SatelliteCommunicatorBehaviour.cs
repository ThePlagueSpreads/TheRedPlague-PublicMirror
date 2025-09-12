using System.Collections;
using Nautilus.Utility;
using Story;
using UnityEngine;

namespace TheRedPlague.Mono.Buildables.SatelliteCommunicator;

public class SatelliteCommunicatorBehaviour : MonoBehaviour, IStoryGoalListener, IManagedUpdateBehaviour
{
    public int managedUpdateIndex { get; set; }
    
    public SatelliteCommunicatorBeacon beacon;
    public VoiceNotification failVoiceNotification;
    public Animator animator;
    public ParticleSystem beaconEnableParticleSystem;

    public Renderer mainRenderer;
    
    private Material _glowMaterial;

    public float maxDepth = 500;
    public float raycastCheckDistance = 600;
    public float raycastCheckStartHeight = 6;
    public float powerUpDuration = 10f;

    private float _powerUpPercent;

    private static readonly int Instant = Animator.StringToHash("instant");
    private static readonly int Activated = Animator.StringToHash("activated");
    private static readonly FMODAsset ActivateSound = AudioUtils.GetFmodAsset("SatelliteCommunicationDeviceActivate");
    
    private IEnumerator Start()
    {
        _glowMaterial = mainRenderer.materials[2];
        UpdateGlowMaterial();
        
        if (StoryGoalManager.main.IsGoalComplete(StoryUtils.TeleportToSpaceEvent.key))
        {
            SetStateDisabled();
            yield break;
        }
        
        yield return new WaitForSeconds(1);

        var isValid = IsAreaValid();

        if (!isValid)
        {
            failVoiceNotification.Play();
            yield break;
        }
        
        StoryGoalManager.main.AddListener(this);
        
        // INITIATE THE EVENT!
        
        StoryUtils.SatelliteCommunicatorSuccess.Trigger();

        yield return new WaitForSeconds(1);
        
        Utils.PlayFMODAsset(ActivateSound, transform.position);

        BehaviourUpdateUtils.Register(this);
        
        animator.SetBool(Activated, true);
        animator.SetBool(Instant, false);
        
        yield return new WaitForSeconds(11);
        
        beaconEnableParticleSystem.Play();
        beacon.gameObject.SetActive(true);
        beacon.SetNewBrightness(1f);
        
        StoryUtils.ObeyBennetEvent.Trigger();
    }
    
    public void ManagedUpdate()
    {
        if (_powerUpPercent >= 1f)
        {
            BehaviourUpdateUtils.Deregister(this);
        }

        _powerUpPercent = Mathf.Clamp01(_powerUpPercent + Time.deltaTime / powerUpDuration);
        UpdateGlowMaterial();
    }

    private void UpdateGlowMaterial()
    {
        _glowMaterial.SetFloat(ShaderPropertyID._GlowStrength, _powerUpPercent);
        _glowMaterial.SetFloat(ShaderPropertyID._GlowStrengthNight, _powerUpPercent);
    }

    private void SetStateDisabled()
    {
        animator.SetBool(Activated, false);
        beacon.gameObject.SetActive(false);
    }

    private bool IsAreaValid()
    {
        if (transform.position.y < -maxDepth)
        {
            return false;
        }

        if (Physics.Raycast(transform.position + Vector3.up * raycastCheckStartHeight,
                Vector3.up, raycastCheckDistance, -1, QueryTriggerInteraction.Ignore))
        {
            return false;
        }

        return true;
    }

    public void NotifyGoalComplete(string key)
    {
        if (key == StoryUtils.TeleportToSpaceEvent.key)
        {
            SetStateDisabled();
        }
    }

    private void OnDestroy()
    {
        StoryGoalManager.main.RemoveListener(this);
        Destroy(_glowMaterial);
    }

    public string GetProfileTag()
    {
        return "TRP:SatelliteCommunicatorBehaviour";
    }
}