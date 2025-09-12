using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;
using UWE;

namespace TheRedPlague.PrefabFiles.StoryProps.Bases;

public static class RedPlagueSurvivorBase2
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("RedPlagueSurvivorBase2");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GeneratePrefab);
        prefab.Register();
    }

    private static IEnumerator GeneratePrefab(IOut<GameObject> prefab)
    {
        var obj = new GameObject(Info.ClassID);
        obj.SetActive(false);

        #region Prefab requests

        var request1 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseFoundationPiece.prefab");
        yield return request1;
        if (!request1.TryGetPrefab(out var baseFoundationPiece))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseFoundationPiece!");
            yield break;
        }

        var request2 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorTShape.prefab");
        yield return request2;
        if (!request2.TryGetPrefab(out var baseCorridorTShape))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorTShape!");
            yield break;
        }

        var request3 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorTShapeSupport.prefab");
        yield return request3;
        if (!request3.TryGetPrefab(out var baseCorridorTShapeSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorTShapeSupport!");
            yield break;
        }

        var request4 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopIntClosed.prefab");
        yield return request4;
        if (!request4.TryGetPrefab(out var baseCorridorCoverTShapeTopIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopIntClosed!");
            yield break;
        }

        var request5 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomIntClosed.prefab");
        yield return request5;
        if (!request5.TryGetPrefab(out var baseCorridorCoverTShapeBottomIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomIntClosed!");
            yield break;
        }

        var request6 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomExtClosed.prefab");
        yield return request6;
        if (!request6.TryGetPrefab(out var baseCorridorCoverTShapeBottomExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomExtClosed!");
            yield break;
        }

        var request7 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorWindow.prefab");
        yield return request7;
        if (!request7.TryGetPrefab(out var baseCorridorWindow))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorWindow!");
            yield break;
        }

        var request8 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeReinforcementSide.prefab");
        yield return request8;
        if (!request8.TryGetPrefab(out var baseCorridorIShapeReinforcementSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeReinforcementSide!");
            yield break;
        }

        var request9 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopExtClosed.prefab");
        yield return request9;
        if (!request9.TryGetPrefab(out var baseCorridorCoverTShapeTopExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopExtClosed!");
            yield break;
        }

        var request10 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeGlass.prefab");
        yield return request10;
        if (!request10.TryGetPrefab(out var baseCorridorIShapeGlass))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeGlass!");
            yield break;
        }

        var request11 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeSupport.prefab");
        yield return request11;
        if (!request11.TryGetPrefab(out var baseCorridorIShapeSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeSupport!");
            yield break;
        }

        var request12 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeBottomIntClosed.prefab");
        yield return request12;
        if (!request12.TryGetPrefab(out var baseCorridorCoverIShapeBottomIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeBottomIntClosed!");
            yield break;
        }

        var request13 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeBottomExtClosed.prefab");
        yield return request13;
        if (!request13.TryGetPrefab(out var baseCorridorCoverIShapeBottomExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeBottomExtClosed!");
            yield break;
        }
        
        /*

        var request14 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorHatch.prefab");
        yield return request14;
        if (!request14.TryGetPrefab(out var baseCorridorHatch))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorHatch!");
            yield break;
        }
        */

        var request15 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShape.prefab");
        yield return request15;
        if (!request15.TryGetPrefab(out var baseCorridorIShape))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShape!");
            yield break;
        }

        var request16 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeCoverSide.prefab");
        yield return request16;
        if (!request16.TryGetPrefab(out var baseCorridorIShapeCoverSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeCoverSide!");
            yield break;
        }

        var request17 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeTopIntClosed.prefab");
        yield return request17;
        if (!request17.TryGetPrefab(out var baseCorridorCoverIShapeTopIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeTopIntClosed!");
            yield break;
        }

        var request18 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeTopExtClosed.prefab");
        yield return request18;
        if (!request18.TryGetPrefab(out var baseCorridorCoverIShapeTopExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeTopExtClosed!");
            yield break;
        }

        #endregion

        #region Spawning base pieces

        var child1 = Object.Instantiate(baseFoundationPiece, obj.transform);
        child1.transform.localPosition = new Vector3(2.5f, 0f, 2.5f);
        child1.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child1.SetActive(true);
        StripComponents(child1);
        var child2 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child2.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child2.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child2.SetActive(true);
        StripComponents(child2);
        var child3 = Object.Instantiate(baseCorridorTShapeSupport, obj.transform);
        child3.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child3.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child3.SetActive(true);
        StripComponents(child3);
        var child4 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child4.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child4.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child4.SetActive(true);
        StripComponents(child4);
        var child5 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child5.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child5.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child5.SetActive(true);
        StripComponents(child5);
        var child6 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child6.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child6.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child6.SetActive(true);
        StripComponents(child6);
        var child7 = Object.Instantiate(baseCorridorWindow, obj.transform);
        child7.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child7.transform.localRotation = new Quaternion(0f, 1f, 0f, 0f);
        child7.SetActive(true);
        StripComponents(child7);
        var child8 = Object.Instantiate(baseCorridorWindow, obj.transform);
        child8.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child8.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child8.SetActive(true);
        StripComponents(child8);
        var child9 = Object.Instantiate(baseCorridorIShapeReinforcementSide, obj.transform);
        child9.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child9.transform.localRotation = new Quaternion(0f, 1f, 0f, 0f);
        child9.SetActive(true);
        StripComponents(child9);
        var child10 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child10.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child10.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child10.SetActive(true);
        StripComponents(child10);
        var child11 = Object.Instantiate(baseCorridorIShapeGlass, obj.transform);
        child11.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child11.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child11.SetActive(true);
        StripComponents(child11);
        var child12 = Object.Instantiate(baseCorridorIShapeSupport, obj.transform);
        child12.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child12.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child12.SetActive(true);
        StripComponents(child12);
        var child13 = Object.Instantiate(baseCorridorIShapeSupport, obj.transform);
        child13.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child13.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child13.SetActive(true);
        StripComponents(child13);
        var child14 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child14.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child14.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child14.SetActive(true);
        StripComponents(child14);
        var child15 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child15.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child15.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child15.SetActive(true);
        StripComponents(child15);
        var child16 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child16.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child16.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child16.SetActive(true);
        StripComponents(child16);
        var child17 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child17.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child17.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child17.SetActive(true);
        StripComponents(child17);
        /*
        var child18 = Object.Instantiate(baseCorridorHatch, obj.transform);
        child18.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child18.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child18.SetActive(true);
        StripComponents(child18);
        */
        var child19 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child19.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child19.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child19.SetActive(true);
        StripComponents(child19);
        var child20 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child20.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child20.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child20.SetActive(true);
        StripComponents(child20);
        var child21 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child21.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child21.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child21.SetActive(true);
        StripComponents(child21);
        var child22 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child22.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child22.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child22.SetActive(true);
        StripComponents(child22);
        var child23 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child23.transform.localPosition = new Vector3(0f, 3.5f, 5f);
        child23.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child23.SetActive(true);
        StripComponents(child23);

        #endregion

        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.VeryFar);

        prefab.Set(obj);
    }

    private static void StripComponents(GameObject obj)
    {
        AbandonedBaseUtils.StripComponents(obj, new Color(0.5f, 0.05f, 0.05f), true);
    }
}