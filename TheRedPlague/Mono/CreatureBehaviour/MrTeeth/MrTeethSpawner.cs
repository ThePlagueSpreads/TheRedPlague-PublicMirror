using System.Collections;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Mono.CreatureBehaviour.MrTeeth;

public class MrTeethSpawner : MonoBehaviour, IManagedUpdateBehaviour
{
    public float maxDistance = 6.5f;
    public float minInterval = 10f;
    
    private float _timeMrTeethCanSpawnAgain;
    private GameObject _mrTeethInstance;
    
    public int managedUpdateIndex { get; set; }
    
    private static readonly FMODAsset SpawnSound = AudioUtils.GetFmodAsset("MrTeethScream");

    private void OnEnable()
    {
        BehaviourUpdateUtils.Register(this);
    }

    private void OnDisable()
    {
        BehaviourUpdateUtils.Deregister(this);
    }
    
    public void ManagedUpdate()
    {
        if (!MrTeethCanSpawn()) return;
        var maxDistance = GetMaxDistance();
        foreach (var spawnPoint in MrTeethSpawnPoint.SpawnPoints)
        {
            if (Vector3.SqrMagnitude(spawnPoint.transform.position - Player.main.transform.position) <
                maxDistance * maxDistance)
            {
                SpawnMrTeeth(spawnPoint);
                break;
            }
        }
    }

    private float GetMaxDistance()
    {
        return maxDistance;
        
        /*
        if (PlagueArmorBehavior.IsPlagueArmorEquipped())
        {
            return maxDistancePlagueArmor;
        }

        return maxDistanceDefault;
        */
    }

    private bool MrTeethCanSpawn()
    {
        return Time.time > _timeMrTeethCanSpawnAgain && _mrTeethInstance == null;
    }
    
    private void SpawnMrTeeth(MrTeethSpawnPoint spawnPoint)
    {
        _timeMrTeethCanSpawnAgain = Time.time + minInterval;
        StartCoroutine(SpawnMrTeethCoroutine(spawnPoint.transform.position - spawnPoint.transform.forward * 2, spawnPoint.transform.forward));
    }

    private IEnumerator SpawnMrTeethCoroutine(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        var task = CraftData.GetPrefabForTechTypeAsync(ModPrefabs.MrTeeth.TechType);
        yield return task;
        var mrTeeth = UWE.Utils.InstantiateDeactivated(task.GetResult());
        DestroyImmediate(mrTeeth.GetComponent<LargeWorldEntity>());
        DestroyImmediate(mrTeeth.GetComponent<PrefabIdentifier>());
        var mrTeethTransform = mrTeeth.transform;
        mrTeethTransform.position = spawnPosition;
        // mrTeethTransform.forward = spawnDirection;
        mrTeethTransform.LookAt(MainCamera.camera.transform);
        mrTeeth.GetComponent<Rigidbody>().velocity = mrTeethTransform.forward * 4;
        mrTeeth.SetActive(true);
        _mrTeethInstance = mrTeeth;
        Utils.PlayFMODAsset(SpawnSound, transform.position);
    }

    public string GetProfileTag()
    {
        return "TRP:MrTeethSpawner";
    }
}