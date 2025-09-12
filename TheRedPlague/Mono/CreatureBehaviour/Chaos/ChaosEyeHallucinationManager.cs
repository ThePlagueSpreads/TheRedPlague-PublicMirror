using System.Collections.Generic;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.Chaos;

public class ChaosEyeHallucinationManager : MonoBehaviour, IManagedUpdateBehaviour
{
    private const float MinEyeLifetime = 4f;
    private const float MaxEyeLifetime = 7f;
    
    public static ChaosEyeHallucinationManager instance;

    public int managedUpdateIndex { get; set; }

    private GameObject _prefab;

    private bool _assetsLoaded;

    private readonly List<ChaosEyeHallucination> _eyes = new();

    private void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        BehaviourUpdateUtils.Deregister(this);
    }

    public void CreateEye(Vector3 position)
    {
        LoadAssetsIfApplicable();
        var eyeObj = Instantiate(_prefab, position, Random.rotation);
        eyeObj.SetActive(true);
        var eye = eyeObj.GetComponent<ChaosEyeHallucination>();
        eye.Prepare(Random.Range(MinEyeLifetime, MaxEyeLifetime));
        _eyes.Add(eye);
    }

    public void ManagedUpdate()
    {
        foreach (var eye in _eyes)
        {
            eye.DoManagedUpdate();
        }

        for (int i = 0; i < _eyes.Count; i++)
        {
            if (_eyes[i].IsExpired)
            {
                Destroy(_eyes[i].gameObject);
                _eyes.RemoveAt(i);
                i--;
            }
        }
    }
    
    private void LoadAssetsIfApplicable()
    {
        if (_assetsLoaded)
            return;

        _prefab = Instantiate(Plugin.CreaturesBundle.LoadAsset<GameObject>("ChaosFloatingEyePrefab"),
            transform, false);
        _prefab.SetActive(false);
        MaterialUtils.ApplySNShaders(_prefab);
        _prefab.AddComponent<SkyApplier>().renderers = _prefab.GetComponentsInChildren<Renderer>();
        var hallucination = _prefab.AddComponent<ChaosEyeHallucination>();
        hallucination.renderer = _prefab.GetComponentInChildren<Renderer>();

        _assetsLoaded = true;
    }
    
    public string GetProfileTag()
    {
        return "TRP:ChaosEyeHallucinationManager";
    }
}