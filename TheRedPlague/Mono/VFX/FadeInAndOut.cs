using UnityEngine;

namespace TheRedPlague.Mono.VFX;

public class FadeInAndOut : MonoBehaviour, IManagedUpdateBehaviour
{
    public float lifetime = 5f;
    
    public float fadeTime = 1f;

    public float destroyDelay;
    public MeshRenderer[] renderers;

    private float _startTime;
    
    public int managedUpdateIndex { get; set; }

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
        return "TRP:FadeInAndOut";
    }

    public void ManagedUpdate()
    {
        float elapsed = Time.time - _startTime;

        if (elapsed >= lifetime)
        {
            Destroy(gameObject, destroyDelay);

            BehaviourUpdateUtils.Deregister(this);
            return;
        }

        float alpha;

        if (elapsed < fadeTime) // Fade in
        {
            alpha = Mathf.Clamp01(elapsed / fadeTime);
        }
        else if (elapsed > lifetime - fadeTime) // Fade out
        {
            alpha = Mathf.Clamp01((lifetime - elapsed) / fadeTime);
        }
        else // Fully visible
        {
            alpha = 1f;
        }

        foreach (var renderer in renderers)
        {
            renderer.SetFadeAmount(Mathf.Clamp(alpha, 0.0001f, 1f));
        }
    }
}