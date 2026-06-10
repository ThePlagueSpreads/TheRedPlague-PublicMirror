using System.Collections;
using Newtonsoft.Json;
using TheRedPlague.Framework.API.StructureLoading.StructureFormat;
using UnityEngine;
using UWE;

namespace TheRedPlague.Framework.API.StructureLoading;

public class ConglomerateStructureLoader : MonoBehaviour, IScheduledUpdateBehaviour
{
    public int scheduledUpdateIndex { get; set; }
    
    public Transform pivot;
    public float maxDistance;
    public Transform entityParent;
    public AssetBundle structureBundle;
    public string structureName;
    public Skies anchorSky;
    
    private Structure _structureData;
    
    private bool _busy;
    private bool _loaded;
    
    public string GetProfileTag()
    {
        return "TRP:ConglomerateStructureLoader";
    }

    private void Awake()
    {
        _structureData =
            JsonConvert.DeserializeObject<Structure>(structureBundle.LoadAsset<TextAsset>(structureName).text);
    }

    public void ScheduledUpdate()
    {
        if (_busy)
            return;
        
        var inLoadDistance = Vector3.SqrMagnitude(MainCamera.camera.transform.position - pivot.position) < maxDistance * maxDistance;

        if (!_loaded && inLoadDistance)
        {
            StartCoroutine(LoadProps());
        }
        else if (_loaded && !inLoadDistance)
        {
            StartCoroutine(UnloadProps());
        }
    }

    private IEnumerator LoadProps()
    {
        _busy = true;

        foreach (var entity in _structureData.Entities)
        {
            if (!PrefabDatabase.prefabFiles.ContainsKey(entity.classId))
            {
                Plugin.Logger.LogWarning($"No prefab found for Class ID: '{entity.classId}'s");
                continue;
            }
            var task = PrefabDatabase.GetPrefabAsync(entity.classId);
            yield return task;
            if (!task.TryGetPrefab(out var prefab))
            {
                Plugin.Logger.LogError("Failed to load prefab for ClassID: " + entity.classId);
                continue;
            }

            SpawnEntityFromPrefab(prefab, entity);
        }
        
        _busy = false;
        _loaded = true;
    }

    private void SpawnEntityFromPrefab(GameObject prefab, Entity entity)
    {
        var obj = UWE.Utils.InstantiateDeactivated(prefab);
        DestroyImmediate(obj.GetComponent<LargeWorldEntity>());
            
        obj.transform.localScale = entity.scale.ToVector3();
        obj.transform.SetParent(entityParent.transform);
        obj.transform.position = entity.position.ToVector3();
        obj.transform.rotation = entity.rotation.ToQuaternion();

        switch (anchorSky)
        {
            case Skies.Auto:
                break;
            case Skies.BaseInterior:
            case Skies.BaseGlass:
                var skyApplier = obj.GetComponent<SkyApplier>();
                if (skyApplier != null && skyApplier.anchorSky != Skies.BaseInterior && skyApplier.anchorSky != Skies.BaseGlass)
                {
                    skyApplier.anchorSky = Skies.BaseInterior;
                }
                break;
            default:
                Plugin.Logger.LogWarning($"Support for '{anchorSky}' anchor skies for conglomerate structures is not yet implemented.");
                break;
        }
        
        obj.SetActive(true);
    }

    private IEnumerator UnloadProps()
    {
        foreach (Transform child in entityParent)
        {
            Destroy(child.gameObject);
        }
        
        yield return null;
        
        _busy = false;
        _loaded = false;
    }

    private void OnEnable()
    {
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDisable()
    {
        UpdateSchedulerUtils.Deregister(this);
    }
}