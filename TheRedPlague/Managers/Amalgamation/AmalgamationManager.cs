using System;
using System.Collections;
using System.Collections.Generic;
using ECCLibrary;
using TheRedPlague.Mono.CreatureBehaviour;
using TheRedPlague.Mono.InfectionLogic;
using TheRedPlague.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TheRedPlague.Managers.Amalgamation;

public static class AmalgamationManager
{
    private static readonly int InfectionHeightStrength = Shader.PropertyToID("_InfectionHeightStrength");

    private static readonly List<TechType> LeviathanTechTypes = new()
    {
        TechType.ReaperLeviathan,
        TechType.GhostLeviathan,
        TechType.GhostLeviathanJuvenile,
        TechType.SeaDragon
    };

    private const float LeviathanProbabilityScale = 8;
    private const float AmalgamationParasiteSpawnDuration = 4;
    
    public static void AmalgamateCreature(RedPlagueHost host)
    {
        UWE.CoroutineHost.StartCoroutine(AmalgamateCreatureInternal(host));
    }

    private static TechType GetTechTypeOfCreatureSafe(GameObject creatureGameObject)
    {
        var techTag = creatureGameObject.GetComponent<TechTag>();
        var prefabIdentifier = creatureGameObject.GetComponent<PrefabIdentifier>();

        if (techTag != null)
        {
            return techTag.type;
        }

        if (prefabIdentifier != null)
        {
            CraftData.PreparePrefabIDCache();
            return CraftData.entClassTechTable.GetOrDefault(prefabIdentifier.ClassId, TechType.None);
        }

        return TechType.None;
    }

    private static IEnumerator AmalgamateCreatureInternal(RedPlagueHost host)
    {
        yield return null;

        if (host == null || host.gameObject == null)
        {
            yield break;
        }

        // Necessary in the case that the parent creature overrides the actual TechType of a child
        var techType = GetTechTypeOfCreatureSafe(host.gameObject);

        if (techType == TechType.None)
        {
            yield break;
        }

        if (AmalgamationSettingsDatabase.CustomModificationsList.TryGetValue(techType, out var customModification))
        {
            yield return customModification(host.gameObject);
        }

        if (!AmalgamationSettingsDatabase.SettingsList.TryGetValue(techType, out var settings))
            yield break;

        if (host.transform.parent != null && host.transform.parent.gameObject.GetComponentInParent<Creature>() != null)
            yield break;

        if (host.GetComponent<AmalgamationParasite>() != null || host.GetComponent<AmalgamatedCreature>() != null)
            yield break;

        // Mark the creature as already amalgamated to prevent double amalgamations
        host.gameObject.AddComponent<AmalgamatedCreature>();

        // Get blood FX
        var bloodFxResult = new TaskResult<GameObject>();
        yield return BloodFxUtils.GetBloodFx(bloodFxResult);
        var bloodFxPrefab = bloodFxResult.value;

        if (bloodFxPrefab == null)
        {
            Plugin.Logger.LogWarning("Failed to find Blood FX!");
        }

        // Get the probability multiplier
        var probabilityScale =
            Mathf.Clamp(ZombieManager.GetInfectionStrengthAtPosition(host.transform.position), 0.05f, 1f);
        if (LeviathanTechTypes.Contains(techType))
        {
            probabilityScale = Mathf.Clamp01(probabilityScale * LeviathanProbabilityScale);
            if (ZombieManager.IsBiomeHeavilyInfected(WaterBiomeManager.main.GetBiome(host.transform.position)))
            {
                probabilityScale = 1f;
            }
        }

        var bloodFxScale = AmalgamationSettingsDatabase.BloodFxScales.GetOrDefault(techType, 1f);

        // Spawn parasites
        foreach (var attachPoint in settings.AttachPoints)
        {
            foreach (var bone in attachPoint.PathToAffectedBone)
            {
                if (Random.value <= attachPoint.Probability * probabilityScale)
                {
                    yield return AttachCreatureToHost(host.gameObject, attachPoint, bone, bloodFxPrefab,
                        AmalgamationParasiteSpawnDuration, bloodFxScale);
                    if (host == null || host.gameObject == null)
                    {
                        Plugin.Logger.LogWarning("Host was deleted before amalgamation finished!");
                        yield break;
                    }
                }
            }
        }
    }

    private static void SpawnBloodFx(GameObject bloodFx, Transform bone, float scale)
    {
        var fx = Object.Instantiate(bloodFx);
        var fxTransform = fx.transform;
        fxTransform.parent = bone;
        fxTransform.ZeroTransform(scl:false);
        fxTransform.ApplyToAllChildrenRecursive(transform => transform.localScale = Vector3.one * scale);
        fx.SetActive(true);
    }

    private static IEnumerator AttachCreatureToHost(GameObject host, ParasiteAttachPoint parasiteAttachPoint,
        string chosenBone, GameObject bloodFxPrefab, float spawnDelay, float bloodFxScale)
    {
        // Shrink the bone
        var attachmentBone = host.transform.Find(chosenBone);
        if (attachmentBone == null)
        {
            Plugin.Logger.LogWarning($"Could not find attachment bone of path '{chosenBone}' on host {host}!");
            yield break;
        }

        SpawnBloodFx(bloodFxPrefab, attachmentBone, bloodFxScale);

        foreach (var unaffectedChild in parasiteAttachPoint.UnaffectedChildObjects)
        {
            var childObj = attachmentBone.Find(unaffectedChild);
            if (childObj != null) childObj.parent = attachmentBone.parent;
        }

        if (parasiteAttachPoint.RemoveBodyPart)
            attachmentBone.transform.localScale *= 0.01f;

        // Create parasite
        var chosenParasite =
            parasiteAttachPoint.AttachableCreatures[Random.Range(0, parasiteAttachPoint.AttachableCreatures.Length)];
        var parasitePrefabTask = CraftData.GetPrefabForTechTypeAsync(chosenParasite.Type);
        yield return parasitePrefabTask;
        if (attachmentBone == null)
        {
            Plugin.Logger.LogWarning($"Attachment bone became null! The host might've died.");
            yield break;
        }

        var parasitePrefab = parasitePrefabTask.GetResult();
        if (parasitePrefab == null)
        {
            Plugin.Logger.LogWarning($"Parasite prefab for TechType {chosenParasite.Type} not found!");
            yield break;
        }

        var parasite = UWE.Utils.InstantiateDeactivated(parasitePrefab);
        parasite.transform.SetParent(attachmentBone, true);
        Object.Destroy(parasite.GetComponent<LargeWorldEntity>());
        Object.Destroy(parasite.GetComponent<PrefabIdentifier>());

        // Position the parasite properly
        var parasiteTransform = parasite.transform;
        parasiteTransform.localPosition = Vector3.zero;
        parasiteTransform.localEulerAngles = parasiteAttachPoint.LocalEulerAngles;

        // Scale the parasite properly
        var lossyBoneScale = attachmentBone.lossyScale;
        var parasiteScale = new Vector3(chosenParasite.Scale / lossyBoneScale.x,
            chosenParasite.Scale / lossyBoneScale.y, chosenParasite.Scale / lossyBoneScale.z);
        // var parasiteAverageScale = (parasiteScale.x + parasiteScale.y + parasiteScale.z) / 3f;
        var parasiteComponent = parasite.AddComponent<AmalgamationParasite>();
        parasiteComponent.desiredLossyScale = chosenParasite.Scale;

        if (!string.IsNullOrEmpty(chosenParasite.DecapitationPoint))
        {
            var decapitationPoint = parasiteTransform.Find(chosenParasite.DecapitationPoint);
            if (decapitationPoint) decapitationPoint.localScale = Vector3.one * 0.001f;
            else Plugin.Logger.LogWarning($"Failed to find point at path '{chosenParasite.DecapitationPoint}'");
        }

        // Disable the parasite's movement
        foreach (var collider in parasite.GetComponentsInChildren<Collider>(true))
        {
            if (collider.isTrigger) continue;
            collider.enabled = false;
        }

        var creature = parasite.GetComponent<Creature>();
        if (creature != null)
        {
            creature.enabled = false;
            creature.SetSize((parasiteScale.x + parasiteScale.y + parasiteScale.z) / 3);
        }

        parasiteTransform.localScale = parasiteScale;

        var parasiteRb = parasite.GetComponent<Rigidbody>();
        if (parasiteRb != null) parasiteRb.isKinematic = true;

        try
        {
            ZombieManager.Zombify(parasite);
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError(
                $"Error while zombifying parasite on '{host.name}' (parasite: '{parasite.name}'): {e.Message}");
        }

        var parasiteRenderers = parasite.GetComponentsInChildren<Renderer>(true);

        FixHeightStrength();

        parasite.SetActive(true);
        parasiteComponent.StartGrowth(AmalgamationParasiteSpawnDuration);

        yield return null;

        if (parasite == null)
        {
            Plugin.Logger.LogWarning("Parasite was destroyed!");
            yield break;
        }

        FixHeightStrength();

        void FixHeightStrength()
        {
            foreach (var renderer in parasiteRenderers)
            {
                if (renderer == null)
                    continue;
                foreach (var material in renderer.materials)
                {
                    if (!material) continue;
                    if (material.HasProperty(InfectionHeightStrength))
                        material.SetFloat(InfectionHeightStrength, 0);
                }
            }
        }
    }
}