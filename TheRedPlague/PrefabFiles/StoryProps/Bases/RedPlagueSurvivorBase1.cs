using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;
using UWE;

namespace TheRedPlague.PrefabFiles.StoryProps.Bases;

public static class RedPlagueSurvivorBase1
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("RedPlagueSurvivorBase1");

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
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorLShape.prefab");
        yield return request1;
        if (!request1.TryGetPrefab(out var baseCorridorLShape))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorLShape!");
            yield break;
        }

        var request2 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorLShapeAdjustableSupport.prefab");
        yield return request2;
        if (!request2.TryGetPrefab(out var baseCorridorLShapeAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorLShapeAdjustableSupport!");
            yield break;
        }

        var request3 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShape.prefab");
        yield return request3;
        if (!request3.TryGetPrefab(out var baseCorridorIShape))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShape!");
            yield break;
        }

        var request4 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeAdjustableSupport.prefab");
        yield return request4;
        if (!request4.TryGetPrefab(out var baseCorridorIShapeAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeAdjustableSupport!");
            yield break;
        }

        var request5 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeCoverSide.prefab");
        yield return request5;
        if (!request5.TryGetPrefab(out var baseCorridorIShapeCoverSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeCoverSide!");
            yield break;
        }

        var request6 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeTopIntClosed.prefab");
        yield return request6;
        if (!request6.TryGetPrefab(out var baseCorridorCoverIShapeTopIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeTopIntClosed!");
            yield break;
        }

        var request7 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeTopExtClosed.prefab");
        yield return request7;
        if (!request7.TryGetPrefab(out var baseCorridorCoverIShapeTopExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeTopExtClosed!");
            yield break;
        }

        var request8 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeBottomIntClosed.prefab");
        yield return request8;
        if (!request8.TryGetPrefab(out var baseCorridorCoverIShapeBottomIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeBottomIntClosed!");
            yield break;
        }

        var request9 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeBottomExtClosed.prefab");
        yield return request9;
        if (!request9.TryGetPrefab(out var baseCorridorCoverIShapeBottomExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeBottomExtClosed!");
            yield break;
        }

        var request10 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeWindowSide.prefab");
        yield return request10;
        if (!request10.TryGetPrefab(out var baseCorridorIShapeWindowSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeWindowSide!");
            yield break;
        }

        var request11 = PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoom.prefab");
        yield return request11;
        if (!request11.TryGetPrefab(out var baseRoom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoom!");
            yield break;
        }

        var request12 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomAdjustableSupport.prefab");
        yield return request12;
        if (!request12.TryGetPrefab(out var baseRoomAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomAdjustableSupport!");
            yield break;
        }

        var request13 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomExteriorBottom.prefab");
        yield return request13;
        if (!request13.TryGetPrefab(out var baseRoomExteriorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomExteriorBottom!");
            yield break;
        }

        var request14 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomCoverSideVariant.prefab");
        yield return request14;
        if (!request14.TryGetPrefab(out var baseRoomCoverSideVariant))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCoverSideVariant!");
            yield break;
        }

        var request15 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomCoverSide.prefab");
        yield return request15;
        if (!request15.TryGetPrefab(out var baseRoomCoverSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCoverSide!");
            yield break;
        }

        var request16 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomCoverBottom.prefab");
        yield return request16;
        if (!request16.TryGetPrefab(out var baseRoomCoverBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCoverBottom!");
            yield break;
        }

        var request17 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomInteriorBottom.prefab");
        yield return request17;
        if (!request17.TryGetPrefab(out var baseRoomInteriorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomInteriorBottom!");
            yield break;
        }

        var request18 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomCorridorConnector.prefab");
        yield return request18;
        if (!request18.TryGetPrefab(out var baseRoomCorridorConnector))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCorridorConnector!");
            yield break;
        }

        var request19 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomWindowSide.prefab");
        yield return request19;
        if (!request19.TryGetPrefab(out var baseRoomWindowSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomWindowSide!");
            yield break;
        }

        var request20 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomExteriorTopGlass.prefab");
        yield return request20;
        if (!request20.TryGetPrefab(out var baseRoomExteriorTopGlass))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomExteriorTopGlass!");
            yield break;
        }

        var request21 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomInteriorTopGlass.prefab");
        yield return request21;
        if (!request21.TryGetPrefab(out var baseRoomInteriorTopGlass))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomInteriorTopGlass!");
            yield break;
        }

        var request22 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomNuclearReactorUnderGlassDome.prefab");
        yield return request22;
        if (!request22.TryGetPrefab(out var baseRoomNuclearReactorUnderGlassDome))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomNuclearReactorUnderGlassDome!");
            yield break;
        }

        var request23 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeReinforcementSide.prefab");
        yield return request23;
        if (!request23.TryGetPrefab(out var baseCorridorIShapeReinforcementSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeReinforcementSide!");
            yield break;
        }

        var request24 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorTShape.prefab");
        yield return request24;
        if (!request24.TryGetPrefab(out var baseCorridorTShape))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorTShape!");
            yield break;
        }

        var request25 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorTShapeAdjustableSupport.prefab");
        yield return request25;
        if (!request25.TryGetPrefab(out var baseCorridorTShapeAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorTShapeAdjustableSupport!");
            yield break;
        }

        var request26 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopIntClosed.prefab");
        yield return request26;
        if (!request26.TryGetPrefab(out var baseCorridorCoverTShapeTopIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopIntClosed!");
            yield break;
        }

        var request27 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopExtClosed.prefab");
        yield return request27;
        if (!request27.TryGetPrefab(out var baseCorridorCoverTShapeTopExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopExtClosed!");
            yield break;
        }

        var request28 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomIntClosed.prefab");
        yield return request28;
        if (!request28.TryGetPrefab(out var baseCorridorCoverTShapeBottomIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomIntClosed!");
            yield break;
        }

        var request29 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomExtClosed.prefab");
        yield return request29;
        if (!request29.TryGetPrefab(out var baseCorridorCoverTShapeBottomExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomExtClosed!");
            yield break;
        }

        #endregion

        #region Spawning base pieces

        var child1 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child1.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child1.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child1.SetActive(true);
        StripComponents(child1);
        var child2 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child2.transform.localPosition = new Vector3(0f, 0f, -10f);
        child2.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child2.SetActive(true);
        StripComponents(child2);
        var child3 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child3.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child3.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child3.SetActive(true);
        StripComponents(child3);
        var child4 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child4.transform.localPosition = new Vector3(0f, 0f, -10f);
        child4.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child4.SetActive(true);
        StripComponents(child4);
        var child5 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child5.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child5.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child5.SetActive(true);
        StripComponents(child5);
        var child6 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child6.transform.localPosition = new Vector3(0f, 0f, -5f);
        child6.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child6.SetActive(true);
        StripComponents(child6);
        var child7 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child7.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child7.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child7.SetActive(true);
        StripComponents(child7);
        var child8 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child8.transform.localPosition = new Vector3(0f, 0f, -5f);
        child8.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child8.SetActive(true);
        StripComponents(child8);
        var child9 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child9.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child9.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child9.SetActive(true);
        StripComponents(child9);
        var child10 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child10.transform.localPosition = new Vector3(0f, 0f, -5f);
        child10.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child10.SetActive(true);
        StripComponents(child10);
        var child11 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child11.transform.localPosition = new Vector3(0f, 0f, 0f);
        child11.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child11.SetActive(true);
        StripComponents(child11);
        var child12 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child12.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child12.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child12.SetActive(true);
        StripComponents(child12);
        var child13 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child13.transform.localPosition = new Vector3(0f, 0f, -5f);
        child13.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child13.SetActive(true);
        StripComponents(child13);
        var child14 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child14.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child14.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child14.SetActive(true);
        StripComponents(child14);
        var child15 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child15.transform.localPosition = new Vector3(0f, 0f, -5f);
        child15.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child15.SetActive(true);
        StripComponents(child15);
        var child16 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child16.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child16.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child16.SetActive(true);
        StripComponents(child16);
        var child17 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child17.transform.localPosition = new Vector3(0f, 0f, -5f);
        child17.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child17.SetActive(true);
        StripComponents(child17);
        var child18 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child18.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child18.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child18.SetActive(true);
        StripComponents(child18);
        var child19 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child19.transform.localPosition = new Vector3(0f, 0f, -5f);
        child19.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child19.SetActive(true);
        StripComponents(child19);
        var child20 = Object.Instantiate(baseCorridorIShapeWindowSide, obj.transform);
        child20.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child20.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child20.SetActive(true);
        StripComponents(child20);
        var child21 = Object.Instantiate(baseRoom, obj.transform);
        child21.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child21.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child21.SetActive(true);
        StripComponents(child21);
        var child22 = Object.Instantiate(baseRoomAdjustableSupport, obj.transform);
        child22.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child22.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child22.SetActive(true);
        StripComponents(child22);
        var child23 = Object.Instantiate(baseRoomExteriorBottom, obj.transform);
        child23.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child23.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child23.SetActive(true);
        StripComponents(child23);
        var child24 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child24.transform.localPosition = new Vector3(-6.464466f, 0f, -3.535534f);
        child24.transform.localRotation = new Quaternion(0f, 0.3826835f, 0f, 0.9238795f);
        child24.SetActive(true);
        StripComponents(child24);
        var child25 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child25.transform.localPosition = new Vector3(-13.53553f, 0f, -3.535534f);
        child25.transform.localRotation = new Quaternion(0f, 0.9238796f, 0f, 0.3826835f);
        child25.SetActive(true);
        StripComponents(child25);
        var child26 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child26.transform.localPosition = new Vector3(-13.53553f, 0f, 3.535534f);
        child26.transform.localRotation = new Quaternion(0f, 0.9238796f, 0f, -0.3826834f);
        child26.SetActive(true);
        StripComponents(child26);
        var child27 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child27.transform.localPosition = new Vector3(-6.464466f, 0f, 3.535534f);
        child27.transform.localRotation = new Quaternion(0f, 0.3826835f, 0f, -0.9238795f);
        child27.SetActive(true);
        StripComponents(child27);
        var child28 = Object.Instantiate(baseRoomCoverSide, obj.transform);
        child28.transform.localPosition = new Vector3(-10f, 0f, 5f);
        child28.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child28.SetActive(true);
        StripComponents(child28);
        var child29 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child29.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child29.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child29.SetActive(true);
        StripComponents(child29);
        var child30 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child30.transform.localPosition = new Vector3(-13.423f, 0f, 0f);
        child30.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child30.SetActive(true);
        StripComponents(child30);
        var child31 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child31.transform.localPosition = new Vector3(-10f, 0f, -3.423f);
        child31.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child31.SetActive(true);
        StripComponents(child31);
        var child32 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child32.transform.localPosition = new Vector3(-6.577f, 0f, 0f);
        child32.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child32.SetActive(true);
        StripComponents(child32);
        var child33 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child33.transform.localPosition = new Vector3(-10f, 0f, 3.423f);
        child33.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child33.SetActive(true);
        StripComponents(child33);
        var child34 = Object.Instantiate(baseRoomInteriorBottom, obj.transform);
        child34.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child34.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child34.SetActive(true);
        StripComponents(child34);
        var child35 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child35.transform.localPosition = new Vector3(-10f, 0f, -5f);
        child35.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child35.SetActive(true);
        StripComponents(child35);
        var child36 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child36.transform.localPosition = new Vector3(-5f, 0f, 0f);
        child36.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child36.SetActive(true);
        StripComponents(child36);
        var child37 = Object.Instantiate(baseRoomWindowSide, obj.transform);
        child37.transform.localPosition = new Vector3(-15f, 0f, 0f);
        child37.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child37.SetActive(true);
        StripComponents(child37);
        var child38 = Object.Instantiate(baseRoomExteriorTopGlass, obj.transform);
        child38.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child38.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child38.SetActive(true);
        StripComponents(child38);
        var child39 = Object.Instantiate(baseRoomInteriorTopGlass, obj.transform);
        child39.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child39.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child39.SetActive(true);
        StripComponents(child39);
        var child40 = Object.Instantiate(baseRoomNuclearReactorUnderGlassDome, obj.transform);
        child40.transform.localPosition = new Vector3(-10f, 0f, 0f);
        child40.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child40.SetActive(true);
        StripComponents(child40);
        var child41 = Object.Instantiate(baseCorridorIShapeReinforcementSide, obj.transform);
        child41.transform.localPosition = new Vector3(0f, 0f, -5f);
        child41.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child41.SetActive(true);
        StripComponents(child41);
        var child42 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child42.transform.localPosition = new Vector3(0f, 0f, 0f);
        child42.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child42.SetActive(true);
        StripComponents(child42);
        var child43 = Object.Instantiate(baseCorridorTShapeAdjustableSupport, obj.transform);
        child43.transform.localPosition = new Vector3(0f, 0f, 0f);
        child43.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child43.SetActive(true);
        StripComponents(child43);
        var child44 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child44.transform.localPosition = new Vector3(0f, 0f, 0f);
        child44.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child44.SetActive(true);
        StripComponents(child44);
        var child45 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child45.transform.localPosition = new Vector3(0f, 0f, 0f);
        child45.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child45.SetActive(true);
        StripComponents(child45);
        var child46 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child46.transform.localPosition = new Vector3(0f, 0f, 0f);
        child46.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child46.SetActive(true);
        StripComponents(child46);
        var child47 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child47.transform.localPosition = new Vector3(0f, 0f, 0f);
        child47.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child47.SetActive(true);
        StripComponents(child47);

        #endregion

        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.VeryFar);

        prefab.Set(obj);
    }

    private static void StripComponents(GameObject obj)
    {
        AbandonedBaseUtils.StripComponents(obj, new Color(0.5f, 0.05f, 0.05f), true);
    }
}