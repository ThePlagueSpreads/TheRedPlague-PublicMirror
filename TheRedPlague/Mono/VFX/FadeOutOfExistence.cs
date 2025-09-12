using UnityEngine;

namespace TheRedPlague.Mono.VFX;

public class FadeOutOfExistence : MonoBehaviour, IManagedUpdateBehaviour
{
    public float duration;
    public float disableDelay;
    public MeshRenderer[] renderers;

    private float _startTime;
    
    private void Start()
    {
        _startTime = Time.time;
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDestroy()
    {
        BehaviourUpdateUtils.Deregister(this);
    }

    public string GetProfileTag()
    {
        return "TRP:FadeOutOfExistence";
    }

    public void ManagedUpdate()
    {
        if (Time.time > _startTime + duration)
        {
            if (disableDelay <= 0)
                gameObject.SetActive(false);
            else
                Invoke(nameof(Disable), disableDelay);
            BehaviourUpdateUtils.Deregister(this);
            return;
        }
        foreach (var renderer in renderers)
        {
            renderer.SetFadeAmount(Mathf.Clamp(1f - (Time.time - _startTime) / duration, 0.0001f, 1f));
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    public int managedUpdateIndex { get; set; }
}