using TheRedPlague.Interfaces;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Mono.Equipment;

public class InfectionTrackerTool : PlayerTool
{
    private const float MinPingDuration = 0.3f;
    private const float MaxPingDuration = 1.5f;
    private const float MinPingInterval = 0.75f;
    private const float MaxPingInterval = 4f;
    private const float DistanceForMinInterval = 50;
    private const float DistanceForMaxInterval = 600f;
    private const float DistanceForCloseSound = 50;

    public override string animToolName => "flashlight";

    public GameObject arrowPrefab;
    public GameObject viewModel;
    public FMOD_CustomEmitter pingEmitter;
    public FMOD_CustomLoopingEmitter closeSoundEmitter;
    public Renderer fpModelRenderer;

    private GameObject _arrowModel;

    private Transform _arrow;

    private Material _fpModelMaterial;
    private Material _arrowMaterial;

    private Color _defaultFpModelColor;
    private Color _defaultColor;
    private float _timeNextPing;
    private bool _pinging;
    private float _pingPower;
    private float _currentPingDuration = MinPingDuration;
    
    private void Start()
    {
        _fpModelMaterial = new Material(fpModelRenderer.sharedMaterial);
        fpModelRenderer.sharedMaterial = _fpModelMaterial;
        _defaultFpModelColor = _fpModelMaterial.GetColor(ShaderPropertyID._GlowColor);
    }

    private void LateUpdate()
    {
        if (Player.main == null)
            return;
        var bestTarget = InfectionTargetRegistry.GetBest();
        if (bestTarget == null || !viewModel.activeSelf)
        {
            _arrowModel.SetActive(false);
            _pinging = false;
            return;
        }

        if (_pinging)
        {
            _pingPower -= Time.deltaTime / _currentPingDuration;
            if (_pingPower <= 0f)
            {
                _pinging = false;
            }

            UpdateMaterial();
        }
        else if (Time.time > _timeNextPing)
        {
            Ping(bestTarget);
        }

        _arrowModel.SetActive(true);
        var camTrans = MainCamera.camera.transform;
        _arrow.transform.position = camTrans.position + camTrans.forward * 1.3f + camTrans.up * -0.05f;
        _arrow.LookAt(bestTarget.GetTargetPosition());
    }

    private void Ping(IInfectionTrackerTarget target)
    {
        pingEmitter.Play();
        _pingPower = 1;
        _pinging = true;
        float distance = Vector3.Distance(target.GetTargetPosition(), transform.position);
        var interval = GetPingInterval(distance);
        if (distance < DistanceForCloseSound)
        {
            closeSoundEmitter.Play();
        }
        else
        {
            closeSoundEmitter.Stop();
        }
        _timeNextPing = Time.time + Mathf.Max(MinPingDuration, interval);
        _currentPingDuration = Mathf.Clamp(interval, MinPingDuration, MaxPingDuration);
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        if (!_pinging)
        {
            _arrowMaterial.color = _defaultColor;
            _fpModelMaterial.SetColor(ShaderPropertyID._GlowColor, _defaultFpModelColor);
            return;
        }

        _arrowMaterial.color = new Color(_defaultColor.r * (1f + _pingPower), _defaultColor.g, _defaultColor.b,
            _defaultColor.a);
        _fpModelMaterial.SetColor(ShaderPropertyID._GlowColor, _defaultFpModelColor * (0.5f + _pingPower));
    }

    private float GetPingInterval(float distance)
    {
        return GenericTrpUtils.RemapValue(distance, DistanceForMinInterval,
            DistanceForMaxInterval, MinPingInterval, MaxPingInterval);
    }

    private void OnEnable()
    {
        _arrow = Instantiate(arrowPrefab).transform;
        _arrow.gameObject.SetActive(true);
        _arrow.transform.localScale = Vector3.one * 0.5f;
        _arrowModel = _arrow.GetChild(0).gameObject;
        var arrowRenderer = _arrowModel.GetComponentInChildren<Renderer>();
        _arrowMaterial = new Material(arrowRenderer.sharedMaterial);
        arrowRenderer.sharedMaterial = _arrowMaterial;
        _defaultColor = _arrowMaterial.color;
    }

    private void OnDisable()
    {
        if (_arrow)
        {
            Destroy(_arrow.gameObject);
        }

        Destroy(_arrowMaterial);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Destroy(_fpModelMaterial);
    }
}