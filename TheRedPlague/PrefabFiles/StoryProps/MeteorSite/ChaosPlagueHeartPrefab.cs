using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using UnityEngine;
using UWE;

namespace TheRedPlague.PrefabFiles.StoryProps.MeteorSite;

public static class ChaosPlagueHeartPrefab
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ChaosPlagueHeart");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.Register();
    }

    private static IEnumerator CreatePrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PlagueHeartPrefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(prefab, 8, 1f, 1f,
            new IgnoreParticleSystemsModifier(), new PlagueHeartModifier());
        prefab.AddComponent<InfectionTrackerTarget>().priority = 1;
        prefab.AddComponent<ConstructionObstacle>();

        var behaviour = prefab.AddComponent<PlagueHeartBehaviour>();

        // Set up meteorite explosion FX
        
        var containerParent = prefab.transform.Find("PlagueHeartContainer");
        var meteoriteRigidbodies = new Rigidbody[13];
        for (int i = 0; i < meteoriteRigidbodies.Length; i++)
        {
            var rb = containerParent.Find("MeteorFragment." + i.ToString("000")).GetComponent<Rigidbody>();
            var worldForces = rb.gameObject.AddComponent<WorldForces>();
            worldForces.underwaterGravity = 3;
            worldForces.underwaterDrag = 0.4f;
            meteoriteRigidbodies[i] = rb;
            rb.gameObject.AddComponent<ImmuneToPropulsioncannon>();
        }

        behaviour.meteoriteFragments = meteoriteRigidbodies;
        
        // Set up meteor crumble FX
        var fallingRockTask = PrefabDatabase.GetPrefabAsync("e019dd4a-88e3-49c8-93a4-fe909b9b6391");
        yield return fallingRockTask;
        if (!fallingRockTask.TryGetPrefab(out var fallingRockPrefab))
        {
            Plugin.Logger.LogError("FallingRockPrefab is null");
        }

        var smokeMaterial = new Material(fallingRockPrefab.GetComponent<VFXFallingRocks>().startPrefab.transform
            .Find("Smk")
            .GetComponent<ParticleSystemRenderer>().sharedMaterial);
        
        var meteoriteCrumbleParticles = new ParticleSystem[meteoriteRigidbodies.Length];
        for (int i = 0; i < meteoriteCrumbleParticles.Length; i++)
        {
            var ps = meteoriteRigidbodies[i].transform.GetChild(1).GetComponent<ParticleSystem>();
            meteoriteCrumbleParticles[i] = ps;
            ps.GetComponent<Renderer>().sharedMaterial = smokeMaterial;
        }

        behaviour.meteoriteParticles = meteoriteCrumbleParticles;

        // Handle particle systems
        
        behaviour.outerLeakParticles =
            containerParent.Find("MeteorLeakParticleEffects").GetComponentsInChildren<ParticleSystem>();

        // Set up force field FX

        var forceFieldRenderer =
            prefab.transform.Find("PlagueHeartContainer/ContainmentField").GetComponent<Renderer>();
        forceFieldRenderer.material = new Material(MaterialUtils.ForceFieldMaterial);

        var referenceForceField = PrefabDatabase.GetPrefabAsync("18f2fbaa-78df-46a9-805a-79ac4d942125");
        yield return referenceForceField;
        if (!referenceForceField.TryGetPrefab(out var referencePrefab))
        {
            Plugin.Logger.LogWarning("Failed to find reference force field prefab!");
            yield break;
        }

        var lerpColor = forceFieldRenderer.gameObject.AddComponent<VFXLerpColor>();
        lerpColor.PlayOnAwake = false;
        lerpColor.destroyMaterial = true;
        lerpColor.looping = false;
        lerpColor.reverse = false;
        lerpColor.duration = 5;
        lerpColor.randomAmount = 0;
        var referenceLerpColor = referencePrefab.GetComponentInChildren<VFXLerpColor>();
        lerpColor.blendCurve = referenceLerpColor.blendCurve;
        behaviour.forceFieldColorControl = lerpColor;

        var forcefieldLoopingEmitter = prefab.AddComponent<FMOD_CustomLoopingEmitter>();
        forcefieldLoopingEmitter.SetAsset(AudioUtils.GetFmodAsset("PlagueHeartForcefieldLoop"));
        forcefieldLoopingEmitter.followParent = true;
        forcefieldLoopingEmitter.playOnAwake = false;
        forcefieldLoopingEmitter.restartOnPlay = false;
        behaviour.forceFieldEmitter = forcefieldLoopingEmitter;
        behaviour.forceFieldDisableSound = AudioUtils.GetFmodAsset("PlagueHeartForcefieldPowerDown");
        
        // Set up hatch
        
        behaviour.hatchAnimator = prefab.transform.Find("PlagueHeartContainer").GetComponent<Animator>();
        behaviour.hatchOpenSound = AudioUtils.GetFmodAsset("PlagueHeartHatchOpen");
        
        // Set up egg

        behaviour.eggAnimator = prefab.transform.Find("ChaosEgg").GetComponent<Animator>();
        
        var interiorBlood = prefab.transform.Find("InteriorBlood");
        interiorBlood.GetComponent<ParticleSystemRenderer>().sharedMaterial = smokeMaterial;
        behaviour.interiorBloodParticles = interiorBlood.GetComponent<ParticleSystem>();

        // Return prefab
        result.Set(prefab);
    }

    private class PlagueHeartModifier : MaterialModifier
    {
        public override void EditMaterial(Material material, Renderer renderer,
            int materialIndex, MaterialUtils.MaterialType materialType)
        {
            if (renderer is not ParticleSystemRenderer)
            {
                return;
            }

            material.shader = MaterialUtils.Shaders.ParticlesUBER;
        }
    }
}