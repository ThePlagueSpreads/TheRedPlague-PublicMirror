using System;
using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Content.Infection;
using TheRedPlague.Framework.Environment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague.Utilities;

public static class TrpPrefabUtils
{
    public static GameObject CreateLootCubePrefab(PrefabInfo info)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.SetActive(false);
        PrefabUtils.AddBasicComponents(cube, info.ClassID, info.TechType, LargeWorldEntity.CellLevel.Near);
        PrefabUtils.AddWorldForces(cube, 5);
        cube.AddComponent<Pickupable>();
        return cube;
    }

    public static RedPlagueHost AddPlagueCreationComponents(GameObject creaturePrefab)
    {
        var host = creaturePrefab.EnsureComponent<RedPlagueHost>();
        host.mode = RedPlagueHost.Mode.PlagueCreation;
        
        var infectedMixin = host.gameObject.EnsureComponent<InfectedMixin>();
        infectedMixin.renderers = Array.Empty<Renderer>(); // NO NEED TO AFFECT THE RENDERERS
        host.gameObject.AddComponent<InfectOnStart>();
        
        return host;
    }
    
    public static RedPlagueHost AddLostReefCreatureComponents(GameObject creaturePrefab)
    {
        var host = creaturePrefab.EnsureComponent<RedPlagueHost>();
        host.mode = RedPlagueHost.Mode.LostReef;
        
        return host;
    }

    public static IEnumerator GenerateFleshCaveAtmosphereVolumes(GameObject gameObject)
    {
        var upperVolumeTask = UWE.PrefabDatabase.GetPrefabAsync("FleshCaveUpperVolume");
        yield return upperVolumeTask;
        var chamberVolumeTask = UWE.PrefabDatabase.GetPrefabAsync("FleshCaveChamberVolume");
        yield return chamberVolumeTask;
        var cacheVolumeTask = UWE.PrefabDatabase.GetPrefabAsync("FleshCaveCacheVolume");
        yield return cacheVolumeTask;

        // If we're too late...
        if (gameObject == null)
        {
            yield break;
        }

        upperVolumeTask.TryGetPrefab(out var upperVolume);
        chamberVolumeTask.TryGetPrefab(out var chamberVolume);
        cacheVolumeTask.TryGetPrefab(out var cacheVolume);

        var upperVolumesParent = gameObject.transform.Find("FleshCaveBiomeVolumes");
        var chamberVolumesParent = gameObject.transform.Find("FleshCavernBiomeVolumes");
        var cacheVolumesParent = gameObject.transform.Find("CacheBiomeVolumes");

        SpawnVolumesInPlaceOfChildren(gameObject.transform, upperVolumesParent, upperVolume);
        SpawnVolumesInPlaceOfChildren(gameObject.transform, chamberVolumesParent, chamberVolume);
        SpawnVolumesInPlaceOfChildren(gameObject.transform, cacheVolumesParent, cacheVolume);

        yield return null;

        gameObject.EnsureComponent<UpdateBiomeSkyAppliersOnStart>().updateEverySky = true;
    }

    public static IEnumerator GenerateShrineBaseAtmosphereVolumes(GameObject gameObject)
    {
        var hallwayTask = UWE.PrefabDatabase.GetPrefabAsync("ShrineBaseHallwayVolume");
        yield return hallwayTask;
        var mainRoomTask = UWE.PrefabDatabase.GetPrefabAsync("ShrineBaseMainRoomVolume");
        yield return mainRoomTask;

        // If we're too late...
        if (gameObject == null)
        {
            yield break;
        }

        hallwayTask.TryGetPrefab(out var hallway);
        mainRoomTask.TryGetPrefab(out var mainRoom);

        var hallwayParent = gameObject.transform.Find("AtmosphereVolumes-Hallway");
        var mainRoomParent = gameObject.transform.Find("AtmosphereVolumes-MainRoom");

        SpawnVolumesInPlaceOfChildren(gameObject.transform, hallwayParent, hallway);
        SpawnVolumesInPlaceOfChildren(gameObject.transform, mainRoomParent, mainRoom);

        yield return null;

        gameObject.EnsureComponent<UpdateBiomeSkyAppliersOnStart>().updateEverySky = true;
    }


    private static void SpawnVolumesInPlaceOfChildren(Transform root, Transform parent, GameObject volumePrefab)
    {
        foreach (Transform child in parent)
        {
            var volume = Object.Instantiate(volumePrefab, root);
            Object.DestroyImmediate(volume.GetComponent<LargeWorldEntity>());
            Object.DestroyImmediate(volume.GetComponent<PrefabIdentifier>());
            volume.transform.position = child.position;
            volume.transform.rotation = child.rotation;
            volume.transform.localScale = child.localScale;
        }
    }

    public static IEnumerator OnFleshCaveLoadAsync(GameObject root)
    {
        var obstructionRockTask = UWE.PrefabDatabase.GetPrefabAsync("fa986d5a-0cf8-4c63-af9f-8c36acd5bea4");
        yield return obstructionRockTask;
        if (!obstructionRockTask.TryGetPrefab(out var obstructionRock))
        {
            Plugin.Logger.LogError("Failed to find obstruction rock prefab for flesh cave!");
            yield break;
        }

        var material = new Material(obstructionRock.GetComponentInChildren<Renderer>().material);
        material.SetFloat("_CapBorderBlendRange", 0.285f);
        material.SetFloat("_CapBorderBlendOffset", -0.34f);
        material.SetFloat("_CapBorderBlendAngle", 2);
        material.SetFloat("_CapScale", 0.08f);
        material.SetFloat("_SideScale", 0.1f);
        material.SetFloat("_TriplanarBlendRange", 2f);
        material.SetFloat("_InnerBorderBlendRange", 0.76f);
        material.SetFloat("_InnerBorderBlendOffset", 1f);
        material.SetFloat("_Gloss", 0.3f);
        material.SetTexture("_CapSIGMap", AssetBundles.Core.LoadAsset<Texture2D>("fleshcache_ground_sig"));
        material.SetTexture("_CapBumpMap", AssetBundles.Core.LoadAsset<Texture2D>("fleshcache_ground_normal"));
        material.SetTexture("_CapTexture", AssetBundles.Core.LoadAsset<Texture2D>("fleshcache_ground_diffuse"));
        material.SetTexture("_SideSIGMap", AssetBundles.Core.LoadAsset<Texture2D>("fleshcache_wall_sig"));
        material.SetTexture("_SideBumpMap", AssetBundles.Core.LoadAsset<Texture2D>("fleshcache_wall_normal"));
        material.SetTexture("_SideTexture", AssetBundles.Core.LoadAsset<Texture2D>("fleshcache_wall_diffuse"));

        if (root == null)
            yield break;
        
        root.transform.Find("FleshCavePrefab/FleshCaveCache/FleshCaveCache").gameObject.GetComponent<Renderer>().material = material;
    }

    public static void ApplyBennetFleshFormMaterials(GameObject model)
    {
        MaterialUtils.ApplySNShaders(model, 7f, 1.3f, 1.3f);
    }
    
    /// <summary>
    /// Instantiates the prefab with the given Class ID as a child of <paramref name="parentObject"/>, removing the specified components.
    /// </summary>
    /// <param name="parentObject">The parent GameObject to instantiate the child under.</param>
    /// <param name="classId">The Class ID of the entity to instantiate as the child object.</param>
    /// <param name="result">If provided, receives a reference to the spawned child object. You may want to use an instance of the <see cref="TaskResult{T}"/> class.</param>
    /// <param name="stripComponents">If this parameter is true, this utility destroys any <see cref="LargeWorldEntity"/>, <see cref="PrefabIdentifier"/>, <see cref="Rigidbody"/>, <see cref="WorldForces"/>, and <see cref="TechTag"/> components on the root.</param>
    /// <param name="stripSkyAppliers">If this parameter is true, all <see cref="SkyApplier"/> components on the object and its children will be destroyed.</param>
    /// <remarks>
    /// <para>The instantiated child object will be given default values (0, 0, 0) for its local position and rotation.</para>
    /// <para>This method must be called asynchronously (using coroutines).</para>
    /// </remarks>
    public static IEnumerator AddChildPrefab(GameObject parentObject, string classId, IOut<GameObject> result = null, bool stripComponents = true, bool stripSkyAppliers = true)
    {
        var task = UWE.PrefabDatabase.GetPrefabAsync(classId);
        yield return task;

        if (!task.TryGetPrefab(out var prefab) || prefab == null)
        {
            Plugin.Logger.LogError(
                $"Could not load prefab by ClassId '{classId}' to add to parent '{parentObject}'");
            result?.Set(null);
            yield break;
        }

        AddPrefabAsChildInternal(parentObject, prefab, result, stripComponents, stripSkyAppliers);
    }
    
    /// <summary>
    /// Instantiates the prefab with the given Class ID as a child of <paramref name="parentObject"/>, removing the specified components.
    /// </summary>
    /// <param name="parentObject">The parent GameObject to instantiate the child under.</param>
    /// <param name="fileName">The full path to the child, e.g. "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir.prefab".</param>
    /// <param name="result">If provided, receives a reference to the spawned child object. You may want to use an instance of the <see cref="TaskResult{T}"/> class.</param>
    /// <param name="stripComponents">If this parameter is true, this utility destroys any <see cref="LargeWorldEntity"/>, <see cref="PrefabIdentifier"/>, <see cref="Rigidbody"/>, <see cref="WorldForces"/>, and <see cref="TechTag"/> components on the root.</param>
    /// <param name="stripSkyAppliers">If this parameter is true, all <see cref="SkyApplier"/> components on the object and its children will be destroyed.</param>
    /// <remarks>
    /// <para>The instantiated child object will be given default values (0, 0, 0) for its local position and rotation.</para>
    /// <para>This method must be called asynchronously (using coroutines).</para>
    /// </remarks>
    public static IEnumerator AddChildPrefabByFileName(GameObject parentObject, string fileName, IOut<GameObject> result = null, bool stripComponents = true, bool stripSkyAppliers = true)
    {
        var task = UWE.PrefabDatabase.GetPrefabForFilenameAsync(fileName);
        yield return task;

        if (!task.TryGetPrefab(out var prefab) || prefab == null)
        {
            Plugin.Logger.LogError(
                $"Could not load prefab by file name '{fileName}' to add to parent '{parentObject}'");
            result?.Set(null);
            yield break;
        }

        AddPrefabAsChildInternal(parentObject, prefab, result, stripComponents, stripSkyAppliers);
    }
    
    /// <summary>
    /// Instantiates the prefab with the given <see cref="TechType"/> as a child of <paramref name="parentObject"/>, removing the specified components.
    /// </summary>
    /// <param name="parentObject">The parent GameObject to instantiate the child under.</param>
    /// <param name="techType">The <see cref="TechType"/> of the entity to instantiate as the child object.</param>
    /// <param name="result">If provided, receives a reference to the spawned child object. You may want to use an instance of the <see cref="TaskResult{T}"/> class.</param>
    /// <param name="stripComponents">If this parameter is true, this utility destroys any <see cref="LargeWorldEntity"/>, <see cref="PrefabIdentifier"/>, <see cref="Rigidbody"/>, <see cref="WorldForces"/>, and <see cref="TechTag"/> components on the root.</param>
    /// <param name="stripSkyAppliers">If this parameter is true, all <see cref="SkyApplier"/> components on the object and its children will be destroyed.</param>
    /// <remarks>
    /// <para>The instantiated child object will be given default values (0, 0, 0) for its local position and rotation.</para>
    /// <para>This method must be called asynchronously (using coroutines).</para>
    /// </remarks>
    public static IEnumerator AddChildPrefab(GameObject parentObject, TechType techType, IOut<GameObject> result = null, bool stripComponents = true, bool stripSkyAppliers = true)
    {
        var task = CraftData.GetPrefabForTechTypeAsync(techType);
        yield return task;
        var prefab = task.GetResult();
        
        if (prefab == null)
        {
            Plugin.Logger.LogError(
                $"Could not load prefab by TechType '{techType}' to add to parent '{parentObject}'");
            result?.Set(null);
            yield break;
        }

        AddPrefabAsChildInternal(parentObject, prefab, result, stripComponents, stripSkyAppliers);
    }

    private static void AddPrefabAsChildInternal(GameObject parentObject, GameObject prefab, IOut<GameObject> result, bool stripComponents, bool stripSkyAppliers)
    {
        if (parentObject == null)
        {
            Plugin.Logger.LogError($"Failed to instantiate '{prefab}'; parent is null");
            return;
        }

        var child = UWE.Utils.InstantiateDeactivated(prefab);
        var childTransform = child.transform;

        childTransform.SetParent(parentObject.transform);
        childTransform.localPosition = Vector3.zero;
        childTransform.localRotation = Quaternion.identity;

        if (stripComponents)
        {
            Object.DestroyImmediate(child.GetComponent<LargeWorldEntity>());
            Object.DestroyImmediate(child.GetComponent<TechTag>());
            Object.DestroyImmediate(child.GetComponent<PrefabIdentifier>());
            Object.DestroyImmediate(child.GetComponent<WorldForces>());
            Object.DestroyImmediate(child.GetComponent<Rigidbody>());
            Object.DestroyImmediate(child.GetComponent<Pickupable>());
        }

        if (stripSkyAppliers)
        {
            foreach (var skyApplier in child.GetComponentsInChildren<SkyApplier>(true))
            {
                Object.DestroyImmediate(skyApplier);
            }
        }

        child.SetActive(true);
        
        result?.Set(child);
    }
}