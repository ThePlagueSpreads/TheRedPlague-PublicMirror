using UnityEngine;

namespace TheRedPlague.Mono.CinematicEvents;

public class SatelliteLaser : MonoBehaviour
{
    public Transform root;
    public Transform startPosition;
    public Transform endPosition;
    public LineRenderer lineRenderer;
    public float brightnessTransitionDuration = 3f;
    
    public bool isGroundLaser;

    private float _renderedBrightness = 1f;
    private float _targetBrightness = 1f;

    private void Start()
    {
        if (!isGroundLaser)
        {
            lineRenderer.SetPosition(0, root.InverseTransformPoint(startPosition.position));
        }
    }

    public void SetBrightness(float newBrightness)
    {
        _renderedBrightness = newBrightness;
        _targetBrightness = newBrightness;
        UpdateColor();
    }
    
    public void TransitionToNewBrightness(float newBrightness)
    {
        _targetBrightness = newBrightness;
    }

    private void LateUpdate()
    {
        if (!isGroundLaser)
        {
            lineRenderer.SetPosition(1, root.InverseTransformPoint(endPosition.position));
        }

        if (Mathf.Approximately(_renderedBrightness, _targetBrightness)) return;
        
        _renderedBrightness = Mathf.MoveTowards(_renderedBrightness, _targetBrightness,
            Time.deltaTime / brightnessTransitionDuration);
        UpdateColor();
    }
    
    private void UpdateColor()
    {
        var color = new Color(1, 1, 1, _renderedBrightness);
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}