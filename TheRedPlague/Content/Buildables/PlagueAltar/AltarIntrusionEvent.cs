using System.Collections;
using System.Collections.Generic;
using Nautilus.Utility;
using TheRedPlague.Framework.VFX.Flickering;
using UnityEngine;
using UWE;

namespace TheRedPlague.Content.Buildables.PlagueAltar;

public class AltarIntrusionEvent : MonoBehaviour
{
    private static readonly List<AltarIntrusionEvent> Altars = new();
    
    private static readonly int PulsatingAnimParam = Animator.StringToHash("pulsating");

    public Animator animator;
    public PlagueAltarEye[] eyes;
    public PlagueAltarCrafter crafter;

    public ParticleSystem[] vomitParticles;
    public ParticleSystem[] hemorrhageParticles;
    public Transform sparksParent;

    private static readonly FMODAsset Sound = AudioUtils.GetFmodAsset("PlagueAltarCorpseCreation");
    
    // settings
    public float enableSparksInterval = 0.5f;

    private GameObject[] _sparks;
    
    public static bool TriggerTest(float maxDistance)
    {
        if (Altars.Count == 0)
        {
            return false;
        }
        
        var playerPos = Player.main.transform.position;
        int closest = -1;
        float closestDistance = maxDistance * maxDistance;
        for (int i = 0; i < Altars.Count; i++)
        {
            var distance = Vector3.SqrMagnitude(playerPos - Altars[i].transform.position);
            if (distance < closestDistance)
            {
                closest = i;
                closestDistance = distance;
            }
        }

        if (closest < 0)
            return false;

        var closestAltar = Altars[closest];
        
        if (closestAltar != null)
        {
            closestAltar.StartIntrusion();
            return true;
        }

        return false;
    }

    private void Start()
    {
        StartCoroutine(LoadAssetsCoroutine());
    }

    private IEnumerator LoadAssetsCoroutine()
    {
        var task = PrefabDatabase.GetPrefabAsync("78afcc32-7963-4939-a894-52a69a8faa9b");
        yield return task;
        if (!task.TryGetPrefab(out var prefab))
        {
            Plugin.Logger.LogError("Failed to load sparks prefab for Plague Altar");
            yield break;
        }

        _sparks = new GameObject[sparksParent.childCount];
        for (int i = 0; i < sparksParent.childCount; i++)
        {
            var spark = UWE.Utils.InstantiateDeactivated(prefab, sparksParent.GetChild(i), Vector3.zero, Quaternion.identity);
            spark.transform.localScale = Vector3.one * 0.3f;
            DestroyImmediate(spark.GetComponent<LargeWorldEntity>());
            DestroyImmediate(spark.GetComponent<PrefabIdentifier>());
            
            foreach (var particleSystem in spark.GetComponentsInChildren<ParticleSystem>(true))
            {
                var main = particleSystem.main;
                main.scalingMode = ParticleSystemScalingMode.Hierarchy;
            }
            
            _sparks[i] = spark.gameObject;
        }
    }

    private void OnEnable()
    {
        Altars.Add(this);
    }

    private void OnDisable()
    {
        Altars.Remove(this);
    }

    public bool StartIntrusion()
    {
        if (!crafter.CanCraftIntruder())
        {
            Plugin.Logger.LogWarning("PlagueAltar is busy! Can't start intrusion");
            return false;
        }
        StartCoroutine(BasicIntrusionCoroutine());
        return true;
    }

    private IEnumerator BasicIntrusionCoroutine()
    {
        crafter.AddInteractionLock();

        yield return new WaitForSeconds(3);
        FMODUWE.PlayOneShot(Sound, transform.position);
        SetEyesCrazy(true);
        yield return new WaitForSeconds(2f);
        SetEyesCrazy(false);
        FlickerLights(1);
        yield return new WaitForSeconds(2f);
        SetParticlesActive(hemorrhageParticles, true);
        yield return new WaitForSeconds(2f);
        SetParticlesActive(hemorrhageParticles, false);
        yield return new WaitForSeconds(1f);
        SetEyesCrazy(true);
        FlickerLights(1);
        yield return new WaitForSeconds(3);
        SetEyesCrazy(false);
        FlickerLights(2);
        SetPulsating(true);
        yield return new WaitForSeconds(1.5f);
        SetParticlesActive(vomitParticles, true);
        SetEyesCrazy(true);
        yield return new WaitForSeconds(3f);
        SetParticlesActive(hemorrhageParticles, true);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(EnableSparksCoroutine());
        
        yield return new WaitForSeconds(5);
        
        crafter.CraftIntruder();
        
        yield return new WaitForSeconds(23);
        
        CleanUp();
    }

    private IEnumerator EnableSparksCoroutine()
    {
        if (_sparks == null)
        {
            Plugin.Logger.LogError("Plague Altar: Sparks not loaded.");
            yield break;
        }
        
        foreach (var spark in _sparks)
        {
            yield return new WaitForSeconds(enableSparksInterval);
            if (spark) spark.SetActive(true);
        }
    }

    private void CleanUp()
    {
        crafter.RemoveInteractionLock();
        SetParticlesActive(hemorrhageParticles, false);
        SetParticlesActive(vomitParticles, false);
        SetEyesCrazy(false);
        SetPulsating(false);
        
        StopCoroutine(nameof(EnableSparksCoroutine));

        if (_sparks != null)
        {
            foreach (var spark in _sparks)
            {
                spark.SetActive(false);
            }
        }
    }

    private void FlickerLights(float duration)
    {
        if (TryGetCurrentSubRoot(out var sub))
            sub.gameObject.AddComponent<LightFlickerEvent>().SetUp(sub.gameObject, duration);
    }

    private void SetEyesCrazy(bool crazy)
    {
        foreach (var eye in eyes)
        {
            eye.SetCrazy(crazy);
        }
    }
    
    private void SetPulsating(bool pulsating)
    {
        animator.SetBool(PulsatingAnimParam, pulsating);
    }

    private void SetParticlesActive(ParticleSystem[] particles, bool active)
    {
        foreach (var ps in particles)
        {
            ps.SetPlaying(active);
        }
    }

    private bool TryGetCurrentSubRoot(out SubRoot sub)
    {
        sub = null;
        if (gameObject.GetComponentInParent<SubRoot>() != null)
        {
            sub = gameObject.GetComponentInParent<SubRoot>();
        }
        return sub != null;
    }
}