using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Mono.Buildables.SatelliteCommunicator;

public class SatelliteCommunicatorBeacon : MonoBehaviour, IManagedUpdateBehaviour
{
    public int managedUpdateIndex { get; set; }

    public Renderer[] renderers;

    public float[] brightnessPerRenderer =
    {
        0.5f,
        1f,
        2f,
        3f
    };

    public float transitionDuration = 1f;

    public float minWidth = 1f;
    public float maxWidth = 10f;
    public float minDistanceForWidthChange = 100f;
    public float maxDistanceForWidthChange = 2000f;

    private float _displayedBrightness;
    private float _targetBrightness;
    private Material[] _materials;
    private bool _transitioning;

    private void OnEnable()
    {
        if (_materials == null)
        {
            _materials = new Material[renderers.Length];
            for (var i = 0; i < renderers.Length; i++)
            {
                var material = renderers[i].material;
                material.SetFloat(ShaderPropertyID._GlowStrength, brightnessPerRenderer[i]);
                material.SetFloat(ShaderPropertyID._GlowStrengthNight, brightnessPerRenderer[i]);
                _materials[i] = material;
            }
        }

        UpdateMaterials();
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        BehaviourUpdateUtils.Deregister(this);
    }

    public string GetProfileTag()
    {
        return "TRP:SatelliteCommunicator";
    }

    public void SetNewBrightness(float newBrightness)
    {
        _targetBrightness = newBrightness;
        _transitioning = true;
    }

    private void UpdateMaterials()
    {
        foreach (var material in _materials)
        {
            material.color = new Color(1, 1, 1, _displayedBrightness);
        }
    }

    public void ManagedUpdate()
    {
        var distanceToCamera = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(MainCamera.camera.transform.position.x, MainCamera.camera.transform.position.z));
        var scale = GenericTrpUtils.RemapValue(distanceToCamera, minDistanceForWidthChange, maxDistanceForWidthChange,
            minWidth, maxWidth);
        transform.localScale = new Vector3(scale, 1, scale);

        if (!_transitioning)
            return;

        if (Mathf.Approximately(transitionDuration, 0f))
        {
            _displayedBrightness = _targetBrightness;
        }
        else
        {
            _displayedBrightness = Mathf.MoveTowards(_displayedBrightness, _targetBrightness,
                Time.deltaTime / transitionDuration);
        }

        UpdateMaterials();

        if (Mathf.Approximately(_displayedBrightness, _targetBrightness))
        {
            _transitioning = false;
        }
    }

    private void OnDestroy()
    {
        if (_materials == null) return;
        foreach (var material in _materials)
        {
            Destroy(material);
        }
    }
}