using System.Collections;
using TheRedPlague.Content.Creatures.Sucker;
using TheRedPlague.Content.PlayerInfection.Symptoms.Bases;
using TheRedPlague.Framework.Behaviour.Deletion;
using TheRedPlague.Utilities;
using UnityEngine;
using UWE;

namespace TheRedPlague.Content.PlayerInfection.Symptoms;

public class SpawnSuckersAroundVehicles : TimedHallucinationSymptom
{
    protected override float MinInterval => 46;
    protected override float MaxInterval => 64;
    protected override float MinInsanity => 18;
    protected override float MaxInsanity => 100;
    protected override float ChanceAtMinInsanity => 0.3f;
    protected override float ChanceAtMaxInsanity => 0.85f;
    protected override float FailDelayMultiplier => 0.6f;
    private const float MaxVehicleDistance = 30;
    private const float SpawnFromVehicleDistance = 3;
    private const float DespawnDelay = 100;

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
        var suckerTask = PrefabDatabase.GetPrefabAsync("SuckerController");
        yield return suckerTask;
        if (!suckerTask.TryGetPrefab(out var suckerPrefab))
            Plugin.Logger.LogError("Failed to load SuckerController prefab");
        if (!GenericTrpUtils.IsPositionOnScreen(position))
        {
            var sucker = Instantiate(suckerPrefab, position, Random.rotation);

            if (LargeWorldStreamer.main)
            {
                LargeWorldStreamer.main.MakeEntityTransient(sucker);
                var destroy = sucker.AddComponent<DestroyItemAfterDelay>();
                destroy.delay = DespawnDelay;
                destroy.useBloodFx = true;
                destroy.bloodFxCount = 3;
                destroy.bloodFxScale = 4;
                destroy.destroyDuration = 0.6f;
            }
        }
    }

    protected override void OnActivate()
    {
    }

    protected override void OnDeactivate()
    {
    }
}