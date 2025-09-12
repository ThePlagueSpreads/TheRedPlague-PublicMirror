using UnityEngine;

namespace TheRedPlague.Mono.VFX;

public class AdjustFarPlane : MonoBehaviour
{
    public float newFarClipPlane;
    public float transitionDuration;
    public float maxDepthToApply;

    private float _oldFarClipPlane;
    private Camera _camera;

    private float _changePerSecond;
    private float _currentFarClipPlane;
    
    public static AdjustFarPlane Main { get; private set; }

    private bool _overriding;
    private float _overrideDistance;

    private void Start()
    {
        Main = this;
        _camera = MainCamera.camera;
        _oldFarClipPlane = _camera.farClipPlane;
        _changePerSecond = Mathf.Abs(newFarClipPlane - _oldFarClipPlane) / transitionDuration;
    }

    private void OnDestroy()
    {
        if (_camera)
        {
            _camera.farClipPlane = _oldFarClipPlane;
        }
    }

    private void LateUpdate()
    {
        _currentFarClipPlane = Mathf.MoveTowards(_currentFarClipPlane,
            newFarClipPlane, Time.deltaTime * _changePerSecond);
        _camera.farClipPlane = GetActualFarClipPlane();
    }

    private float GetActualFarClipPlane()
    {
        if (_overriding)
        {
            return _overrideDistance;
        }
        return Mathf.Lerp(newFarClipPlane, _oldFarClipPlane,
            Mathf.InverseLerp(0, maxDepthToApply, Ocean.GetDepthOf(_camera.gameObject)));
    }

    public void OverrideFarClipPlane(float overrideDistance)
    {
        _overriding = true;
        _overrideDistance = overrideDistance;
    }
    
    public void StopOverridingFarClipPlane()
    {
        _overriding = false;
    }
}