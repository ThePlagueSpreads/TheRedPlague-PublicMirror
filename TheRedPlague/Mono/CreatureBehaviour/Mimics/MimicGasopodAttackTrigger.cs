using System.Collections;
using TheRedPlague.PrefabFiles.Creatures.Misc;
using TheRedPlague.Utilities;
using UnityEngine;
using UWE;

namespace TheRedPlague.Mono.CreatureBehaviour.Mimics;

public class MimicGasopodAttackTrigger : MonoBehaviour
{
    public Creature creature;
    public DamageInRange rangeDamage;
    public FMOD_CustomEmitter emitter;
    public FMODAsset damageSound;
    public Transform infectionPodPosition;
    public float damageDelay = 1.8f;
    public float spawnPodsDelay = 0.2f;
    public float attackInterval = 7f;
    public float spawnPodDistance = 0.3f;

    private float _timeCanAttackAgain;
    private static readonly int Attack = Animator.StringToHash("attack");

    public void OnTriggerEnter(Collider other)
    {
        if (Time.time < _timeCanAttackAgain)
            return;
        if (!creature.liveMixin.IsAlive())
            return;
        if (!rangeDamage.IsValidTarget(GenericTrpUtils.GetTargetRoot(other)))
            return;
        StartAttack();
    }

    private void StartAttack()
    {
        _timeCanAttackAgain = Time.time + attackInterval;
        Invoke(nameof(OnDamage), damageDelay);
        Invoke(nameof(SpawnInfectionPods), spawnPodsDelay);
        creature.GetAnimator().SetTrigger(Attack);
    }

    private void OnDamage()
    {
        emitter.SetAsset(damageSound);
        emitter.Play();
        rangeDamage.DealDamageToTargetsInRange();
    }
    
    private void SpawnInfectionPods()
    {
        var spawns = Random.Range(4, 8);
        for (var i = 0; i < spawns; i++)
        {
            StartCoroutine(SpawnGasPodAsync());
        }
    }

    private IEnumerator SpawnGasPodAsync()
    {
        var direction = Random.onUnitSphere;
        direction.z = -Mathf.Abs(direction.z) * 3f;
        var position = infectionPodPosition.TransformPoint(direction * spawnPodDistance);
        var task = PrefabDatabase.GetPrefabAsync(InfectionPod.Info.ClassID);
        yield return task;
        if (!task.TryGetPrefab(out var prefab))
        {
            Plugin.Logger.LogWarning("Failed to find infection pod prefab for MimicGasopod!");
            yield break;
        }

        var obj = Instantiate(prefab, position, Random.rotation);
        direction = infectionPodPosition.TransformDirection(direction);
        obj.GetComponent<Rigidbody>().AddForce(direction * (1.1f + Random.value * 1.3f), ForceMode.VelocityChange);
        
        if (LargeWorldStreamer.main)
        {
            LargeWorldStreamer.main.MakeEntityTransient(obj);
        }
    }
}