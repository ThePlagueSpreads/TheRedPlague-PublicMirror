using System.Collections;
using TheRedPlague.Mono.CreatureBehaviour.Sucker;
using TheRedPlague.Mono.Insanity.Symptoms.Bases;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Mono.Insanity.Symptoms;

public class SpawnSuckersAroundVehicles : TimedHallucinationSymptom
{
    protected override float MinInterval => 23;
    protected override float MaxInterval => 32;
    protected override float MinInsanity => 18;
    protected override float MaxInsanity => 100;
    protected override float ChanceAtMinInsanity => 0.3f;
    protected override float ChanceAtMaxInsanity => 0.85f;
    private const float MaxVehicleDistance = 30;
    private const float SpawnFromVehicleDistance = 3;

    protected override IEnumerator OnLoadAssets()
    {
        yield break;
    }

    protected override void PerformTimedAction()
    {
        if (Player.main.GetVehicle() != null)
            return;

        if (Player.main.precursorOutOfWater)
            return;
        
        if (!SuckerControllerTarget.TryGetClosest(out var result, Player.main.transform.position, MaxVehicleDistance))
            return;
        
        var onScreen = GenericTrpUtils.IsPositionOnScreen(result.transform.position);
        if (onScreen) return;

        var rb = result.GetComponent<Rigidbody>();
        if (rb == null || rb.isKinematic) return;

        StartCoroutine(SpawnSuckerAsync(result.transform.position + Random.onUnitSphere * SpawnFromVehicleDistance));
    }

    private IEnumerator SpawnSuckerAsync(Vector3 position)
    {
        var suckerTask = CraftData.GetPrefabForTechTypeAsync(ModPrefabs.SuckerController.TechType);
        yield return suckerTask;
        if (!GenericTrpUtils.IsPositionOnScreen(position))
            Instantiate(suckerTask.GetResult(), position, Random.rotation);
    }

    protected override void OnActivate()
    {
    }

    protected override void OnDeactivate()
    {
    }
}