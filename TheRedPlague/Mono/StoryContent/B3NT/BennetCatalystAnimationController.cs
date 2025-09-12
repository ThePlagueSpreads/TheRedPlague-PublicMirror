using System;
using System.Collections;
using Nautilus.Utility;
using TheRedPlague.Mono.StoryContent.Precursor;
using TheRedPlague.Mono.VFX;
using UnityEngine;
using UWE;

namespace TheRedPlague.Mono.StoryContent.B3NT;

public class BennetCatalystAnimationController : MonoBehaviour
{
    private const float CatalystMoveSpeed = 0.4f;
    private const float CatalystRotateSpeed = 150f;

    private Transform _plagueCatalystModel;
    private Transform _bennetEye;

    private static readonly Vector3 TargetPosition = new(-1517, -855.1f, 961.6f);
    private static readonly Quaternion TargetRotation = Quaternion.Euler(0, 270, 0);

    private bool _plagueCatalystRotating;
    private bool _plagueCatalystMoving;

    private BuildFX _catalystDeconstructFX;
    private GameObject _beamVfxPrefab;
    private ParticleSystem _beamVfx;

    private static readonly FMODAsset CatalystAnimationSound = AudioUtils.GetFmodAsset("B3NTCatalystCinematic");

    public static void CreateInstance(Transform plagueCatalyst, Transform bennetEye)
    {
        var instance = new GameObject("BennetCatalystAnimationController");
        var controller = instance.AddComponent<BennetCatalystAnimationController>();
        controller._plagueCatalystModel = plagueCatalyst;
        controller._bennetEye = bennetEye;
    }

    private IEnumerator Start()
    {
        StartCoroutine(LoadBeamVfx());

        // Set up Build FX
        _catalystDeconstructFX = _plagueCatalystModel.gameObject.AddComponent<BuildFX>();
        _catalystDeconstructFX.centerTransform = _plagueCatalystModel.transform;
        _catalystDeconstructFX.renderers = _plagueCatalystModel.GetComponentsInChildren<Renderer>();
        // red color: (1.3f, 0.2f, 0.2f)
        _catalystDeconstructFX.StartSetUp(new BuildFX.Settings(new Color(0.5f, 1f, 0.4f),
            new Vector4(0.3f, 0.2f, 0.2f, -0.12f), 1f, 1.55f, 0.42f, 0.4f, 0.4f));

        // Set plague catalyst loose
        yield return new WaitForSeconds(2.3f);
        _plagueCatalystModel.parent = null;

        _plagueCatalystRotating = true;
        yield return new WaitForSeconds(0.5f);

        // Start moving the plague catalyst into the floating position
        _plagueCatalystMoving = true;
        if (BennetController.Main)
            BennetController.Main.animations.SetOverrideLookTarget(_plagueCatalystModel);
        Utils.PlayFMODAsset(CatalystAnimationSound, _plagueCatalystModel.position);
        yield return new WaitForSeconds(2);

        _catalystDeconstructFX.StartFx(1f);
        _catalystDeconstructFX.StartConstructionEffectInterpolation(0f, 9f);

        if (BennetController.Main)
            BennetController.Main.animations.PlayScanAnimation();

        yield return new WaitForSeconds(1f);

        FlickerShrineBaseLights(0.5f);
        
        SpawnBeam();

        yield return new WaitForSeconds(8);
        
        if (_beamVfx)
        {
            _beamVfx.Stop();
            _beamVfx.GetComponentInChildren<LightAnimator>().gameObject.SetActive(false);
        }
        
        yield return new WaitForSeconds(2);

        FlickerShrineBaseLights(4f);

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    private IEnumerator LoadBeamVfx()
    {
        var antechamberTask = PrefabDatabase.GetPrefabAsync("e8143977-448e-4202-b780-83485fa5f31a");
        yield return antechamberTask;
        if (!antechamberTask.TryGetPrefab(out var antechamberPrefab))
        {
            Plugin.Logger.LogError("Failed to load antechamber prefab!");
            yield break;
        }

        _beamVfxPrefab = antechamberPrefab.GetComponent<VFXController>().emitters[1].fx;
    }

    private void SpawnBeam()
    {
        if (_bennetEye == null)
        {
            Plugin.Logger.LogWarning("Eye reference is null! Skipping beam creation!");
        }

        if (_beamVfxPrefab == null)
        {
            Plugin.Logger.LogWarning("Beam VFX prefab is null! Skipping beam creation!");
        }
        var beam = Instantiate(_beamVfxPrefab, _bennetEye);
        beam.transform.localPosition = new Vector3(-0.014f, 0.020f, 0.045f);
        beam.transform.localEulerAngles = new Vector3(90, 0, 0);
        _bennetEye.transform.localScale = Vector3.one * 0.4f;
        
        beam.transform.Find("xElecCubeTopUpper").gameObject.SetActive(false);
        
        foreach (Transform child in beam.transform)
        {
            if (child.TryGetComponent<ParticleSystem>(out var system))
            {
                var main = system.main;
                main.scalingMode = ParticleSystemScalingMode.Hierarchy;
            }
        }
        _beamVfx = beam.GetComponent<ParticleSystem>();
        _beamVfx.Play();
    }

    private void FlickerShrineBaseLights(float duration)
    {
        if (ShrineBaseController.Main != null)
        {
            try
            {
                ShrineBaseController.Main.FlickerLights(duration);
            }
            catch (Exception e)
            {
                Plugin.Logger.LogError("Failed to flicker lights: " + e);
            }
        }
    }

    private void Update()
    {
        if (_plagueCatalystMoving)
        {
            _plagueCatalystModel.transform.position = Vector3.Lerp(_plagueCatalystModel.position, TargetPosition,
                Time.deltaTime * CatalystMoveSpeed);
        }

        if (_plagueCatalystRotating)
        {
            _plagueCatalystModel.rotation = Quaternion.RotateTowards(_plagueCatalystModel.rotation, TargetRotation,
                CatalystRotateSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        if (BennetController.Main)
            BennetController.Main.animations.SetOverrideLookTarget(null);

        if (_plagueCatalystModel)
            Destroy(_plagueCatalystModel.gameObject);
        
        if (_beamVfx != null)
            Destroy(_beamVfx.gameObject);
    }
}