using System.Collections;
using Nautilus.Utility;
using TheRedPlague.Content.PlayerInfection;
using UnityEngine;

namespace TheRedPlague.Content.Buildables.PlagueAltar;

public class PlagueAltarCrafter : GhostCrafter
{
    private const float FabricatorPowerConsumption = 5f;
    private const float InfectionDamage = 9;

    private static readonly int CraftingParamId = Animator.StringToHash("crafting");

    public Animator animator;
    public FMODAsset interactSound;
    public FMOD_CustomLoopingEmitter craftSoundEmitter;
    public GameObject sparksPrefab;
    public Transform[] beamEndPoints;
    public Renderer[] beams;

    private ParticleSystem[] _craftingParticles;
    private bool _craftingIntruder;

    public float extraPowerConsumption = 25f;

    private int _disableSources;

    public override void Start()
    {
        base.Start();
        spawnAnimationDuration = 6f;
        _craftingParticles = new ParticleSystem[beamEndPoints.Length];
        for (var i = 0; i < _craftingParticles.Length; i++)
        {
            var particle = Instantiate(sparksPrefab, beamEndPoints[i], true);
            particle.transform.localPosition = Vector3.up * 0.006f;
            particle.transform.localRotation = Quaternion.identity;
            particle.SetActive(true);
            _craftingParticles[i] = particle.GetComponent<ParticleSystem>();
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        if (logic == null || !logic.inProgress)
            return;
        var highestTransform = ghost != null && ghost.itemSpawnPoint != null ? ghost.itemSpawnPoint : transform;
        Shader.SetGlobalFloat(ShaderPropertyID._FabricatorPosY, highestTransform.position.y - 0.3f);
    }

    public override void OnOpenedChanged(bool opened)
    {
        base.OnOpenedChanged(opened);
        if (opened)
        {
            Utils.PlayFMODAsset(interactSound, transform.position);
        }
    }

    public override void OnStateChanged(bool crafting)
    {
        base.OnStateChanged(crafting);
        SetCraftingVisualsActive(crafting);
    }

    private void SetCraftingVisualsActive(bool crafting)
    {
        animator.SetBool(CraftingParamId, crafting);
        if (crafting)
        {
            craftSoundEmitter.Play();
            PlagueDamageStat.main.TakeInfectionDamage(InfectionDamage);
            Act2Story.PlagueAltarFirstUse.Trigger();
        }
        else
        {
            craftSoundEmitter.Stop();
            Utils.PlayFMODAsset(interactSound, transform.position);
        }

        foreach (var particle in _craftingParticles)
        {
            particle.SetPlaying(crafting && MiscSettings.flashes);
        }

        foreach (var beam in beams)
        {
            beam.enabled = crafting;
        }
    }

    public override void Craft(TechType techType, float duration)
    {
        var totalRequiredPower = extraPowerConsumption + FabricatorPowerConsumption;

        if (GameModeUtils.RequiresPower() && powerRelay.GetPower() < totalRequiredPower)
        {
            ErrorMessage.AddMessage(Language.main.GetFormat("TrpMachineInsufficientPower", totalRequiredPower));
            return;
        }

        powerRelay.ConsumeEnergy(extraPowerConsumption, out _);

        base.Craft(techType, duration);
    }

    public void AddInteractionLock()
    {
        _disableSources++;
        enabled = false;
    }

    public void RemoveInteractionLock()
    {
        _disableSources--;
        if (_disableSources <= 0)
            enabled = true;
    }

    public bool CanCraftIntruder()
    {
        return !_craftingIntruder && !state;
    }
    
    public bool CraftIntruder()
    {
        if (!CanCraftIntruder())
        {
            Plugin.Logger.LogMessage("Plague Altar is busy. CraftIntruder cannot be called.");
            return false;
        }
        StartCoroutine(CraftIntruderCoroutine());
        return true;
    }

    private IEnumerator CraftIntruderCoroutine()
    {
        _craftingIntruder = true;
        AddInteractionLock();
        
        var skeleton = Instantiate(AssetBundles.Core.LoadAsset<GameObject>("SkeletonRagdoll_Bloody"));
        MaterialUtils.ApplySNShaders(skeleton, 3);
        var rigidbody = skeleton.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbody) rb.isKinematic = true;
        SetUpFakeGhostModel(skeleton);
        SetCraftingVisualsActive(true);

        var craftDuration = 20f;
        float startTime = Time.time;
        var endTime = startTime + craftDuration;
        
        while (Time.time < endTime)
        {
            ghost.UpdateProgress((Time.time - startTime) / craftDuration);
            yield return null;
        }
        
        SetCraftingVisualsActive(false);

        var collider = GetComponent<Collider>();
        bool hasCollider = collider != null;
        foreach (var rb in rigidbody)
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.isKinematic = false;
            if (hasCollider && rb.TryGetComponent<Collider>(out var boneCollider))
            {
                Physics.IgnoreCollision(boneCollider, collider);
            }
        }
        
        // dissociate corpse model before clearing
        ghost.ghostModel = null;
        foreach (var material in ghost.ghostMaterials)
        {
            if (material)
                material.DisableKeyword("FX_BUILDING");
        }
        ghost.ghostMaterials.Clear();
        ghost.UpdateModel(TechType.None);

        skeleton.transform.parent = null;
        
        RemoveInteractionLock();
    }

    // THIS CODE NEEDS TO GO LATER
    private void SetUpFakeGhostModel(GameObject newGhostModel)
    {
        for (int num = ghost.ghostMaterials.Count - 1; num >= 0; num--)
        {
            Material material = ghost.ghostMaterials[num];
            if (material != null)
            {
                Destroy(material);
            }
        }
        ghost.ghostMaterials.Clear();
        ghost.boundsToVFX = null;
        if (ghost.ghostModel != null)
        {
            Destroy(ghost.ghostModel);
            ghost.ghostModel = null;
        }
        
        ghost.ghostModel = newGhostModel;
        SkyApplier skyApplier = newGhostModel.AddComponent<SkyApplier>();
        skyApplier.anchorSky = Skies.BaseInterior;
        newGhostModel.SetActive(value: true);
        newGhostModel.transform.parent = ghost.itemSpawnPoint;
        UWE.Utils.SetIsKinematicAndUpdateInterpolation(newGhostModel, isKinematic: true);
        newGhostModel.transform.localPosition = new Vector3(0.95f, -0.2f, 0);
        newGhostModel.transform.localEulerAngles = new Vector3(270, 90, 0);
        // LATER, ALLOW THIS TO BE DEFINED ELSEWHERE
        var vfxFabricating = newGhostModel.AddComponent<VFXFabricating>();
        vfxFabricating.localMinY = -0.2f;
        vfxFabricating.localMaxY = 0.19f;
        ghost.boundsToVFX = vfxFabricating;
        newGhostModel.GetComponentsInChildren(includeInactive: true, CrafterGhostModel.sGhostRenderers);
        skyApplier.renderers = CrafterGhostModel.sGhostRenderers.ToArray();
        for (int i = 0; i < CrafterGhostModel.sGhostRenderers.Count; i++)
        {
            Material[] materials = CrafterGhostModel.sGhostRenderers[i].materials;
            foreach (Material material in materials)
            {
                if (material != null)
                {
                    ghost.ghostMaterials.Add(material);
                    if (material.shader != null && material.shader.name != "DontRender")
                    {
                        material.EnableKeyword("FX_BUILDING");
                        material.SetTexture(ShaderPropertyID._EmissiveTex, ghost._EmissiveTex);
                        material.SetFloat(ShaderPropertyID._Cutoff, 0.4f);
                        material.SetColor(ShaderPropertyID._BorderColor, new Color(0.7f, 0.7f, 1f, 1f));
                        material.SetFloat(ShaderPropertyID._Built, 0f);
                        material.SetFloat(ShaderPropertyID._Cutoff, 0.42f);
                        material.SetVector(ShaderPropertyID._BuildParams, new Vector4(2f, 0.7f, 3f, -0.25f));
                        material.SetFloat(ShaderPropertyID._NoiseStr, 0.25f);
                        material.SetFloat(ShaderPropertyID._NoiseThickness, 0.49f);
                        material.SetFloat(ShaderPropertyID._BuildLinear, 1f);
                        material.SetFloat(ShaderPropertyID._MyCullVariable, 0f);
                    }
                }
            }
        }
        CrafterGhostModel.sGhostRenderers.Clear();
        Shader.SetGlobalFloat(ShaderPropertyID._SubConstructProgress, 0f);
    }
}