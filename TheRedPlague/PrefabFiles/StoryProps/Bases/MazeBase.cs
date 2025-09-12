using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;
using UWE;

namespace TheRedPlague.PrefabFiles.StoryProps.Bases;

public static class MazeBase
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("MazeBase");

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
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoom.prefab");
        yield return request10;
        if (!request10.TryGetPrefab(out var baseLargeRoom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoom!");
            yield break;
        }

        var request11 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomAdjustableSupport.prefab");
        yield return request11;
        if (!request11.TryGetPrefab(out var baseLargeRoomAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomAdjustableSupport!");
            yield break;
        }

        var request12 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomExteriorBottom.prefab");
        yield return request12;
        if (!request12.TryGetPrefab(out var baseLargeRoomExteriorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomExteriorBottom!");
            yield break;
        }

        var request13 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomInteriorTop.prefab");
        yield return request13;
        if (!request13.TryGetPrefab(out var baseLargeRoomInteriorTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomInteriorTop!");
            yield break;
        }

        var request14 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomInteriorBottom.prefab");
        yield return request14;
        if (!request14.TryGetPrefab(out var baseLargeRoomInteriorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomInteriorBottom!");
            yield break;
        }

        var request15 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomCorridorConnectorShort.prefab");
        yield return request15;
        if (!request15.TryGetPrefab(out var baseLargeRoomCorridorConnectorShort))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomCorridorConnectorShort!");
            yield break;
        }

        var request16 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomReinforcementSide.prefab");
        yield return request16;
        if (!request16.TryGetPrefab(out var baseLargeRoomReinforcementSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomReinforcementSide!");
            yield break;
        }

        var request17 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomCoverSide.prefab");
        yield return request17;
        if (!request17.TryGetPrefab(out var baseLargeRoomCoverSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomCoverSide!");
            yield break;
        }

        var request18 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomCorridorConnector.prefab");
        yield return request18;
        if (!request18.TryGetPrefab(out var baseLargeRoomCorridorConnector))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomCorridorConnector!");
            yield break;
        }

        var request19 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomCoverBottom.prefab");
        yield return request19;
        if (!request19.TryGetPrefab(out var baseLargeRoomCoverBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomCoverBottom!");
            yield break;
        }

        var request20 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomExteriorTop.prefab");
        yield return request20;
        if (!request20.TryGetPrefab(out var baseLargeRoomExteriorTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomExteriorTop!");
            yield break;
        }

        var request21 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomCoverTop.prefab");
        yield return request21;
        if (!request21.TryGetPrefab(out var baseLargeRoomCoverTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomCoverTop!");
            yield break;
        }

        var request22 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorLShapeGlass.prefab");
        yield return request22;
        if (!request22.TryGetPrefab(out var baseCorridorLShapeGlass))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorLShapeGlass!");
            yield break;
        }

        var request23 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorIShapeGlass.prefab");
        yield return request23;
        if (!request23.TryGetPrefab(out var baseCorridorIShapeGlass))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorIShapeGlass!");
            yield break;
        }

        var request24 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomInteriorTopHole2.prefab");
        yield return request24;
        if (!request24.TryGetPrefab(out var baseLargeRoomInteriorTopHole2))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomInteriorTopHole2!");
            yield break;
        }

        var request25 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeRoomInteriorBottomHole2.prefab");
        yield return request25;
        if (!request25.TryGetPrefab(out var baseLargeRoomInteriorBottomHole2))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeRoomInteriorBottomHole2!");
            yield break;
        }

        var request26 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomBioReactor.prefab");
        yield return request26;
        if (!request26.TryGetPrefab(out var baseRoomBioReactor))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomBioReactor!");
            yield break;
        }

        var request27 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseWaterParkSide.prefab");
        yield return request27;
        if (!request27.TryGetPrefab(out var baseWaterParkSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseWaterParkSide!");
            yield break;
        }

        var request28 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeWaterParkWalls.prefab");
        yield return request28;
        if (!request28.TryGetPrefab(out var baseLargeWaterParkWalls))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeWaterParkWalls!");
            yield break;
        }

        var request29 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeWaterParkCeilingTop.prefab");
        yield return request29;
        if (!request29.TryGetPrefab(out var baseLargeWaterParkCeilingTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeWaterParkCeilingTop!");
            yield break;
        }

        var request30 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseLargeWaterParkFloorBottom.prefab");
        yield return request30;
        if (!request30.TryGetPrefab(out var baseLargeWaterParkFloorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseLargeWaterParkFloorBottom!");
            yield break;
        }

        var request31 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorTShape.prefab");
        yield return request31;
        if (!request31.TryGetPrefab(out var baseCorridorTShape))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorTShape!");
            yield break;
        }

        var request32 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorTShapeAdjustableSupport.prefab");
        yield return request32;
        if (!request32.TryGetPrefab(out var baseCorridorTShapeAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorTShapeAdjustableSupport!");
            yield break;
        }

        var request33 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopIntClosed.prefab");
        yield return request33;
        if (!request33.TryGetPrefab(out var baseCorridorCoverTShapeTopIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopIntClosed!");
            yield break;
        }

        var request34 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopExtClosed.prefab");
        yield return request34;
        if (!request34.TryGetPrefab(out var baseCorridorCoverTShapeTopExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopExtClosed!");
            yield break;
        }

        var request35 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomIntClosed.prefab");
        yield return request35;
        if (!request35.TryGetPrefab(out var baseCorridorCoverTShapeBottomIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomIntClosed!");
            yield break;
        }

        var request36 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomExtClosed.prefab");
        yield return request36;
        if (!request36.TryGetPrefab(out var baseCorridorCoverTShapeBottomExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomExtClosed!");
            yield break;
        }

        var request37 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeTopExtOpened.prefab");
        yield return request37;
        if (!request37.TryGetPrefab(out var baseCorridorCoverIShapeTopExtOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeTopExtOpened!");
            yield break;
        }

        var request38 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeTopIntOpened.prefab");
        yield return request38;
        if (!request38.TryGetPrefab(out var baseCorridorCoverIShapeTopIntOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeTopIntOpened!");
            yield break;
        }

        var request39 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorLadderTop.prefab");
        yield return request39;
        if (!request39.TryGetPrefab(out var baseCorridorLadderTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorLadderTop!");
            yield break;
        }

        var request40 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCap.prefab");
        yield return request40;
        if (!request40.TryGetPrefab(out var baseCorridorCap))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCap!");
            yield break;
        }

        var request41 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeBottomExtOpened.prefab");
        yield return request41;
        if (!request41.TryGetPrefab(out var baseCorridorCoverIShapeBottomExtOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeBottomExtOpened!");
            yield break;
        }

        var request42 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverIShapeBottomIntOpened.prefab");
        yield return request42;
        if (!request42.TryGetPrefab(out var baseCorridorCoverIShapeBottomIntOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverIShapeBottomIntOpened!");
            yield break;
        }

        var request43 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorLadderBottom.prefab");
        yield return request43;
        if (!request43.TryGetPrefab(out var baseCorridorLadderBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorLadderBottom!");
            yield break;
        }

        var request44 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseObservatory.prefab");
        yield return request44;
        if (!request44.TryGetPrefab(out var baseObservatory))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseObservatory!");
            yield break;
        }

        var request45 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseObservatoryCorridorConnector.prefab");
        yield return request45;
        if (!request45.TryGetPrefab(out var baseObservatoryCorridorConnector))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseObservatoryCorridorConnector!");
            yield break;
        }

        var request46 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorXShape.prefab");
        yield return request46;
        if (!request46.TryGetPrefab(out var baseCorridorXShape))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorXShape!");
            yield break;
        }

        var request47 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverXShapeTopIntClosed.prefab");
        yield return request47;
        if (!request47.TryGetPrefab(out var baseCorridorCoverXShapeTopIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverXShapeTopIntClosed!");
            yield break;
        }

        var request48 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverXShapeTopExtClosed.prefab");
        yield return request48;
        if (!request48.TryGetPrefab(out var baseCorridorCoverXShapeTopExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverXShapeTopExtClosed!");
            yield break;
        }

        var request49 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverXShapeBottomIntClosed.prefab");
        yield return request49;
        if (!request49.TryGetPrefab(out var baseCorridorCoverXShapeBottomIntClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverXShapeBottomIntClosed!");
            yield break;
        }

        var request50 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverXShapeBottomExtClosed.prefab");
        yield return request50;
        if (!request50.TryGetPrefab(out var baseCorridorCoverXShapeBottomExtClosed))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverXShapeBottomExtClosed!");
            yield break;
        }

        var request51 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseMoonpool.prefab");
        yield return request51;
        if (!request51.TryGetPrefab(out var baseMoonpool))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseMoonpool!");
            yield break;
        }

        var request52 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseMoonpoolCorridorConnector.prefab");
        yield return request52;
        if (!request52.TryGetPrefab(out var baseMoonpoolCorridorConnector))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseMoonpoolCorridorConnector!");
            yield break;
        }

        var request53 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseMoonpoolCoverSide.prefab");
        yield return request53;
        if (!request53.TryGetPrefab(out var baseMoonpoolCoverSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseMoonpoolCoverSide!");
            yield break;
        }

        var request54 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseMoonpoolCoverSideShort.prefab");
        yield return request54;
        if (!request54.TryGetPrefab(out var baseMoonpoolCoverSideShort))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseMoonpoolCoverSideShort!");
            yield break;
        }

        var request55 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseMoonpoolCorridorConnectorShort.prefab");
        yield return request55;
        if (!request55.TryGetPrefab(out var baseMoonpoolCorridorConnectorShort))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseMoonpoolCorridorConnectorShort!");
            yield break;
        }

        var request56 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorXShapeAdjustableSupport.prefab");
        yield return request56;
        if (!request56.TryGetPrefab(out var baseCorridorXShapeAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorXShapeAdjustableSupport!");
            yield break;
        }

        var request57 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverXShapeTopExtOpened.prefab");
        yield return request57;
        if (!request57.TryGetPrefab(out var baseCorridorCoverXShapeTopExtOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverXShapeTopExtOpened!");
            yield break;
        }

        var request58 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverXShapeTopIntOpened.prefab");
        yield return request58;
        if (!request58.TryGetPrefab(out var baseCorridorCoverXShapeTopIntOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverXShapeTopIntOpened!");
            yield break;
        }

        var request59 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomExtOpened.prefab");
        yield return request59;
        if (!request59.TryGetPrefab(out var baseCorridorCoverTShapeBottomExtOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomExtOpened!");
            yield break;
        }

        var request60 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeBottomIntOpened.prefab");
        yield return request60;
        if (!request60.TryGetPrefab(out var baseCorridorCoverTShapeBottomIntOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeBottomIntOpened!");
            yield break;
        }

        var request61 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorWindow.prefab");
        yield return request61;
        if (!request61.TryGetPrefab(out var baseCorridorWindow))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorWindow!");
            yield break;
        }

        var request62 = PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoom.prefab");
        yield return request62;
        if (!request62.TryGetPrefab(out var baseRoom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoom!");
            yield break;
        }

        var request63 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomAdjustableSupport.prefab");
        yield return request63;
        if (!request63.TryGetPrefab(out var baseRoomAdjustableSupport))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomAdjustableSupport!");
            yield break;
        }

        var request64 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomExteriorBottom.prefab");
        yield return request64;
        if (!request64.TryGetPrefab(out var baseRoomExteriorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomExteriorBottom!");
            yield break;
        }

        var request65 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomCorridorConnector.prefab");
        yield return request65;
        if (!request65.TryGetPrefab(out var baseRoomCorridorConnector))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCorridorConnector!");
            yield break;
        }

        var request66 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomCoverSideVariant.prefab");
        yield return request66;
        if (!request66.TryGetPrefab(out var baseRoomCoverSideVariant))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCoverSideVariant!");
            yield break;
        }

        var request67 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomCoverBottom.prefab");
        yield return request67;
        if (!request67.TryGetPrefab(out var baseRoomCoverBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCoverBottom!");
            yield break;
        }

        var request68 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomInteriorBottom.prefab");
        yield return request68;
        if (!request68.TryGetPrefab(out var baseRoomInteriorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomInteriorBottom!");
            yield break;
        }

        var request69 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseWaterParkBottom.prefab");
        yield return request69;
        if (!request69.TryGetPrefab(out var baseWaterParkBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseWaterParkBottom!");
            yield break;
        }

        var request70 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseWaterParkCeilingGlass.prefab");
        yield return request70;
        if (!request70.TryGetPrefab(out var baseWaterParkCeilingGlass))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseWaterParkCeilingGlass!");
            yield break;
        }

        var request71 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseWaterParkFloorBottom.prefab");
        yield return request71;
        if (!request71.TryGetPrefab(out var baseWaterParkFloorBottom))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseWaterParkFloorBottom!");
            yield break;
        }

        var request72 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomCoverTop.prefab");
        yield return request72;
        if (!request72.TryGetPrefab(out var baseRoomCoverTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCoverTop!");
            yield break;
        }

        var request73 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomInteriorTopHole.prefab");
        yield return request73;
        if (!request73.TryGetPrefab(out var baseRoomInteriorTopHole))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomInteriorTopHole!");
            yield break;
        }

        var request74 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseWaterParkTop.prefab");
        yield return request74;
        if (!request74.TryGetPrefab(out var baseWaterParkTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseWaterParkTop!");
            yield break;
        }

        var request75 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopExtOpened.prefab");
        yield return request75;
        if (!request75.TryGetPrefab(out var baseCorridorCoverTShapeTopExtOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopExtOpened!");
            yield break;
        }

        var request76 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseCorridorCoverTShapeTopIntOpened.prefab");
        yield return request76;
        if (!request76.TryGetPrefab(out var baseCorridorCoverTShapeTopIntOpened))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorCoverTShapeTopIntOpened!");
            yield break;
        }

        var request77 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomCoverSide.prefab");
        yield return request77;
        if (!request77.TryGetPrefab(out var baseRoomCoverSide))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomCoverSide!");
            yield break;
        }

        var request78 =
            PrefabDatabase.GetPrefabForFilenameAsync(
                "Assets/Prefabs/Base/GeneratorPieces/BaseRoomInteriorBottomHole.prefab");
        yield return request78;
        if (!request78.TryGetPrefab(out var baseRoomInteriorBottomHole))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomInteriorBottomHole!");
            yield break;
        }

        var request79 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomExteriorTop.prefab");
        yield return request79;
        if (!request79.TryGetPrefab(out var baseRoomExteriorTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomExteriorTop!");
            yield break;
        }

        var request80 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseRoomInteriorTop.prefab");
        yield return request80;
        if (!request80.TryGetPrefab(out var baseRoomInteriorTop))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseRoomInteriorTop!");
            yield break;
        }

        var request81 =
            PrefabDatabase.GetPrefabForFilenameAsync("Assets/Prefabs/Base/GeneratorPieces/BaseCorridorHatch.prefab");
        yield return request81;
        if (!request81.TryGetPrefab(out var baseCorridorHatch))
        {
            Plugin.Logger.LogError("Failed to load prefab with name BaseCorridorHatch!");
            yield break;
        }
        
        #endregion

        #region Spawning base pieces
        
        var child1 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child1.transform.localPosition = new Vector3(0f, 10.5f, -50f);
        child1.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child1.SetActive(true);
        StripComponents(child1);
        var child2 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child2.transform.localPosition = new Vector3(10f, 10.5f, -50f);
        child2.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child2.SetActive(true);
        StripComponents(child2);
        var child3 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child3.transform.localPosition = new Vector3(-30f, 0f, -40f);
        child3.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child3.SetActive(true);
        StripComponents(child3);
        var child4 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child4.transform.localPosition = new Vector3(10f, 0f, -40f);
        child4.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child4.SetActive(true);
        StripComponents(child4);
        var child5 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child5.transform.localPosition = new Vector3(-20f, 0f, -35f);
        child5.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child5.SetActive(true);
        StripComponents(child5);
        var child6 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child6.transform.localPosition = new Vector3(10f, 0f, -35f);
        child6.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child6.SetActive(true);
        StripComponents(child6);
        var child7 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child7.transform.localPosition = new Vector3(0f, 0f, -30f);
        child7.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child7.SetActive(true);
        StripComponents(child7);
        var child8 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child8.transform.localPosition = new Vector3(-5f, 3.5f, -30f);
        child8.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child8.SetActive(true);
        StripComponents(child8);
        var child9 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child9.transform.localPosition = new Vector3(5f, 3.5f, -30f);
        child9.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child9.SetActive(true);
        StripComponents(child9);
        var child10 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child10.transform.localPosition = new Vector3(10f, 0f, -25f);
        child10.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child10.SetActive(true);
        StripComponents(child10);
        var child11 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child11.transform.localPosition = new Vector3(-15f, 3.5f, -25f);
        child11.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child11.SetActive(true);
        StripComponents(child11);
        var child12 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child12.transform.localPosition = new Vector3(0f, 3.5f, -25f);
        child12.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child12.SetActive(true);
        StripComponents(child12);
        var child13 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child13.transform.localPosition = new Vector3(-5f, 3.5f, -20f);
        child13.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child13.SetActive(true);
        StripComponents(child13);
        var child14 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child14.transform.localPosition = new Vector3(5f, 3.5f, -20f);
        child14.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child14.SetActive(true);
        StripComponents(child14);
        var child15 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child15.transform.localPosition = new Vector3(-30f, 0f, -15f);
        child15.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child15.SetActive(true);
        StripComponents(child15);
        var child16 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child16.transform.localPosition = new Vector3(-10f, 10.5f, -15f);
        child16.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child16.SetActive(true);
        StripComponents(child16);
        var child17 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child17.transform.localPosition = new Vector3(10f, 10.5f, -15f);
        child17.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child17.SetActive(true);
        StripComponents(child17);
        var child18 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child18.transform.localPosition = new Vector3(10f, 0f, -10f);
        child18.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child18.SetActive(true);
        StripComponents(child18);
        var child19 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child19.transform.localPosition = new Vector3(-10f, 3.5f, -10f);
        child19.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child19.SetActive(true);
        StripComponents(child19);
        var child20 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child20.transform.localPosition = new Vector3(10f, 3.5f, -10f);
        child20.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child20.SetActive(true);
        StripComponents(child20);
        var child21 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child21.transform.localPosition = new Vector3(25f, 0f, -5f);
        child21.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child21.SetActive(true);
        StripComponents(child21);
        var child22 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child22.transform.localPosition = new Vector3(0f, 3.5f, 0f);
        child22.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child22.SetActive(true);
        StripComponents(child22);
        var child23 = Object.Instantiate(baseCorridorLShape, obj.transform);
        child23.transform.localPosition = new Vector3(10f, 3.5f, 0f);
        child23.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child23.SetActive(true);
        StripComponents(child23);
        var child24 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child24.transform.localPosition = new Vector3(0f, 10.5f, -50f);
        child24.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child24.SetActive(true);
        StripComponents(child24);
        var child25 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child25.transform.localPosition = new Vector3(10f, 10.5f, -50f);
        child25.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child25.SetActive(true);
        StripComponents(child25);
        var child26 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child26.transform.localPosition = new Vector3(-30f, 0f, -40f);
        child26.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child26.SetActive(true);
        StripComponents(child26);
        var child27 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child27.transform.localPosition = new Vector3(10f, 0f, -40f);
        child27.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child27.SetActive(true);
        StripComponents(child27);
        var child28 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child28.transform.localPosition = new Vector3(15f, 0f, -40f);
        child28.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child28.SetActive(true);
        StripComponents(child28);
        var child29 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child29.transform.localPosition = new Vector3(25f, 0f, -40f);
        child29.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child29.SetActive(true);
        StripComponents(child29);
        var child30 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child30.transform.localPosition = new Vector3(-20f, 0f, -35f);
        child30.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child30.SetActive(true);
        StripComponents(child30);
        var child31 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child31.transform.localPosition = new Vector3(10f, 0f, -35f);
        child31.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child31.SetActive(true);
        StripComponents(child31);
        var child32 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child32.transform.localPosition = new Vector3(-5f, 0f, -30f);
        child32.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child32.SetActive(true);
        StripComponents(child32);
        var child33 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child33.transform.localPosition = new Vector3(0f, 0f, -30f);
        child33.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child33.SetActive(true);
        StripComponents(child33);
        var child34 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child34.transform.localPosition = new Vector3(-5f, 0f, -25f);
        child34.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child34.SetActive(true);
        StripComponents(child34);
        var child35 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child35.transform.localPosition = new Vector3(10f, 0f, -25f);
        child35.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child35.SetActive(true);
        StripComponents(child35);
        var child36 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child36.transform.localPosition = new Vector3(-15f, 3.5f, -25f);
        child36.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child36.SetActive(true);
        StripComponents(child36);
        var child37 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child37.transform.localPosition = new Vector3(-10f, 0f, -20f);
        child37.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child37.SetActive(true);
        StripComponents(child37);
        var child38 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child38.transform.localPosition = new Vector3(5f, 3.5f, -20f);
        child38.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child38.SetActive(true);
        StripComponents(child38);
        var child39 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child39.transform.localPosition = new Vector3(-30f, 0f, -15f);
        child39.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child39.SetActive(true);
        StripComponents(child39);
        var child40 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child40.transform.localPosition = new Vector3(10f, 0f, -10f);
        child40.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child40.SetActive(true);
        StripComponents(child40);
        var child41 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child41.transform.localPosition = new Vector3(15f, 0f, -5f);
        child41.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child41.SetActive(true);
        StripComponents(child41);
        var child42 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child42.transform.localPosition = new Vector3(25f, 0f, -5f);
        child42.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child42.SetActive(true);
        StripComponents(child42);
        var child43 = Object.Instantiate(baseCorridorLShapeAdjustableSupport, obj.transform);
        child43.transform.localPosition = new Vector3(10f, 3.5f, 0f);
        child43.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child43.SetActive(true);
        StripComponents(child43);
        var child44 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child44.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child44.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child44.SetActive(true);
        StripComponents(child44);
        var child45 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child45.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child45.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child45.SetActive(true);
        StripComponents(child45);
        var child46 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child46.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child46.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child46.SetActive(true);
        StripComponents(child46);
        var child47 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child47.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child47.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child47.SetActive(true);
        StripComponents(child47);
        var child48 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child48.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child48.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child48.SetActive(true);
        StripComponents(child48);
        var child49 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child49.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child49.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child49.SetActive(true);
        StripComponents(child49);
        var child50 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child50.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child50.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child50.SetActive(true);
        StripComponents(child50);
        var child51 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child51.transform.localPosition = new Vector3(0f, 0f, -40f);
        child51.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child51.SetActive(true);
        StripComponents(child51);
        var child52 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child52.transform.localPosition = new Vector3(5f, 0f, -40f);
        child52.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child52.SetActive(true);
        StripComponents(child52);
        var child53 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child53.transform.localPosition = new Vector3(10f, 10.5f, -40f);
        child53.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child53.SetActive(true);
        StripComponents(child53);
        var child54 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child54.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child54.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child54.SetActive(true);
        StripComponents(child54);
        var child55 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child55.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child55.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child55.SetActive(true);
        StripComponents(child55);
        var child56 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child56.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child56.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child56.SetActive(true);
        StripComponents(child56);
        var child57 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child57.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child57.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child57.SetActive(true);
        StripComponents(child57);
        var child58 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child58.transform.localPosition = new Vector3(0f, 0f, -35f);
        child58.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child58.SetActive(true);
        StripComponents(child58);
        var child59 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child59.transform.localPosition = new Vector3(5f, 0f, -35f);
        child59.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child59.SetActive(true);
        StripComponents(child59);
        var child60 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child60.transform.localPosition = new Vector3(15f, 0f, -35f);
        child60.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child60.SetActive(true);
        StripComponents(child60);
        var child61 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child61.transform.localPosition = new Vector3(10f, 10.5f, -35f);
        child61.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child61.SetActive(true);
        StripComponents(child61);
        var child62 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child62.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child62.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child62.SetActive(true);
        StripComponents(child62);
        var child63 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child63.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child63.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child63.SetActive(true);
        StripComponents(child63);
        var child64 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child64.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child64.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child64.SetActive(true);
        StripComponents(child64);
        var child65 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child65.transform.localPosition = new Vector3(5f, 0f, -30f);
        child65.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child65.SetActive(true);
        StripComponents(child65);
        var child66 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child66.transform.localPosition = new Vector3(10f, 0f, -30f);
        child66.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child66.SetActive(true);
        StripComponents(child66);
        var child67 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child67.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child67.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child67.SetActive(true);
        StripComponents(child67);
        var child68 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child68.transform.localPosition = new Vector3(0f, 3.5f, -30f);
        child68.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child68.SetActive(true);
        StripComponents(child68);
        var child69 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child69.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child69.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child69.SetActive(true);
        StripComponents(child69);
        var child70 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child70.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child70.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child70.SetActive(true);
        StripComponents(child70);
        var child71 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child71.transform.localPosition = new Vector3(-15f, 10.5f, -30f);
        child71.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child71.SetActive(true);
        StripComponents(child71);
        var child72 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child72.transform.localPosition = new Vector3(10f, 10.5f, -30f);
        child72.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child72.SetActive(true);
        StripComponents(child72);
        var child73 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child73.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child73.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child73.SetActive(true);
        StripComponents(child73);
        var child74 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child74.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child74.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child74.SetActive(true);
        StripComponents(child74);
        var child75 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child75.transform.localPosition = new Vector3(5f, 0f, -25f);
        child75.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child75.SetActive(true);
        StripComponents(child75);
        var child76 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child76.transform.localPosition = new Vector3(15f, 0f, -25f);
        child76.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child76.SetActive(true);
        StripComponents(child76);
        var child77 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child77.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child77.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child77.SetActive(true);
        StripComponents(child77);
        var child78 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child78.transform.localPosition = new Vector3(5f, 3.5f, -25f);
        child78.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child78.SetActive(true);
        StripComponents(child78);
        var child79 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child79.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child79.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child79.SetActive(true);
        StripComponents(child79);
        var child80 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child80.transform.localPosition = new Vector3(-10f, 10.5f, -25f);
        child80.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child80.SetActive(true);
        StripComponents(child80);
        var child81 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child81.transform.localPosition = new Vector3(10f, 10.5f, -25f);
        child81.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child81.SetActive(true);
        StripComponents(child81);
        var child82 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child82.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child82.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child82.SetActive(true);
        StripComponents(child82);
        var child83 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child83.transform.localPosition = new Vector3(-10f, 10.5f, -20f);
        child83.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child83.SetActive(true);
        StripComponents(child83);
        var child84 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child84.transform.localPosition = new Vector3(10f, 10.5f, -20f);
        child84.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child84.SetActive(true);
        StripComponents(child84);
        var child85 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child85.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child85.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child85.SetActive(true);
        StripComponents(child85);
        var child86 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child86.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child86.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child86.SetActive(true);
        StripComponents(child86);
        var child87 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child87.transform.localPosition = new Vector3(-5f, 10.5f, -15f);
        child87.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child87.SetActive(true);
        StripComponents(child87);
        var child88 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child88.transform.localPosition = new Vector3(5f, 10.5f, -15f);
        child88.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child88.SetActive(true);
        StripComponents(child88);
        var child89 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child89.transform.localPosition = new Vector3(10f, 0f, -5f);
        child89.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child89.SetActive(true);
        StripComponents(child89);
        var child90 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child90.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child90.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child90.SetActive(true);
        StripComponents(child90);
        var child91 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child91.transform.localPosition = new Vector3(0f, 0f, 0f);
        child91.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child91.SetActive(true);
        StripComponents(child91);
        var child92 = Object.Instantiate(baseCorridorIShape, obj.transform);
        child92.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child92.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child92.SetActive(true);
        StripComponents(child92);
        var child93 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child93.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child93.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child93.SetActive(true);
        StripComponents(child93);
        var child94 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child94.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child94.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child94.SetActive(true);
        StripComponents(child94);
        var child95 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child95.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child95.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child95.SetActive(true);
        StripComponents(child95);
        var child96 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child96.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child96.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child96.SetActive(true);
        StripComponents(child96);
        var child97 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child97.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child97.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child97.SetActive(true);
        StripComponents(child97);
        var child98 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child98.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child98.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child98.SetActive(true);
        StripComponents(child98);
        var child99 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child99.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child99.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child99.SetActive(true);
        StripComponents(child99);
        var child100 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child100.transform.localPosition = new Vector3(0f, 0f, -40f);
        child100.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child100.SetActive(true);
        StripComponents(child100);
        var child101 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child101.transform.localPosition = new Vector3(5f, 0f, -40f);
        child101.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child101.SetActive(true);
        StripComponents(child101);
        var child102 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child102.transform.localPosition = new Vector3(20f, 0f, -40f);
        child102.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child102.SetActive(true);
        StripComponents(child102);
        var child103 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child103.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child103.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child103.SetActive(true);
        StripComponents(child103);
        var child104 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child104.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child104.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child104.SetActive(true);
        StripComponents(child104);
        var child105 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child105.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child105.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child105.SetActive(true);
        StripComponents(child105);
        var child106 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child106.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child106.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child106.SetActive(true);
        StripComponents(child106);
        var child107 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child107.transform.localPosition = new Vector3(0f, 0f, -35f);
        child107.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child107.SetActive(true);
        StripComponents(child107);
        var child108 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child108.transform.localPosition = new Vector3(5f, 0f, -35f);
        child108.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child108.SetActive(true);
        StripComponents(child108);
        var child109 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child109.transform.localPosition = new Vector3(15f, 0f, -35f);
        child109.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child109.SetActive(true);
        StripComponents(child109);
        var child110 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child110.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child110.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child110.SetActive(true);
        StripComponents(child110);
        var child111 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child111.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child111.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child111.SetActive(true);
        StripComponents(child111);
        var child112 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child112.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child112.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child112.SetActive(true);
        StripComponents(child112);
        var child113 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child113.transform.localPosition = new Vector3(5f, 0f, -30f);
        child113.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child113.SetActive(true);
        StripComponents(child113);
        var child114 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child114.transform.localPosition = new Vector3(10f, 0f, -30f);
        child114.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child114.SetActive(true);
        StripComponents(child114);
        var child115 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child115.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child115.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child115.SetActive(true);
        StripComponents(child115);
        var child116 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child116.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child116.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child116.SetActive(true);
        StripComponents(child116);
        var child117 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child117.transform.localPosition = new Vector3(5f, 0f, -25f);
        child117.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child117.SetActive(true);
        StripComponents(child117);
        var child118 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child118.transform.localPosition = new Vector3(15f, 0f, -25f);
        child118.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child118.SetActive(true);
        StripComponents(child118);
        var child119 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child119.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child119.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child119.SetActive(true);
        StripComponents(child119);
        var child120 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child120.transform.localPosition = new Vector3(-5f, 0f, -20f);
        child120.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child120.SetActive(true);
        StripComponents(child120);
        var child121 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child121.transform.localPosition = new Vector3(10f, 0f, -20f);
        child121.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child121.SetActive(true);
        StripComponents(child121);
        var child122 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child122.transform.localPosition = new Vector3(15f, 0f, -10f);
        child122.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child122.SetActive(true);
        StripComponents(child122);
        var child123 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child123.transform.localPosition = new Vector3(10f, 0f, -5f);
        child123.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child123.SetActive(true);
        StripComponents(child123);
        var child124 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child124.transform.localPosition = new Vector3(20f, 0f, -5f);
        child124.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child124.SetActive(true);
        StripComponents(child124);
        var child125 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child125.transform.localPosition = new Vector3(0f, 0f, 0f);
        child125.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child125.SetActive(true);
        StripComponents(child125);
        var child126 = Object.Instantiate(baseCorridorIShapeAdjustableSupport, obj.transform);
        child126.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child126.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child126.SetActive(true);
        StripComponents(child126);
        var child127 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child127.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child127.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child127.SetActive(true);
        StripComponents(child127);
        var child128 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child128.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child128.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child128.SetActive(true);
        StripComponents(child128);
        var child129 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child129.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child129.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child129.SetActive(true);
        StripComponents(child129);
        var child130 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child130.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child130.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child130.SetActive(true);
        StripComponents(child130);
        var child131 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child131.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child131.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child131.SetActive(true);
        StripComponents(child131);
        var child132 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child132.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child132.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child132.SetActive(true);
        StripComponents(child132);
        var child133 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child133.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child133.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child133.SetActive(true);
        StripComponents(child133);
        var child134 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child134.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child134.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child134.SetActive(true);
        StripComponents(child134);
        var child135 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child135.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child135.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child135.SetActive(true);
        StripComponents(child135);
        var child136 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child136.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child136.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child136.SetActive(true);
        StripComponents(child136);
        var child137 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child137.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child137.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child137.SetActive(true);
        StripComponents(child137);
        var child138 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child138.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child138.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child138.SetActive(true);
        StripComponents(child138);
        var child139 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child139.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child139.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child139.SetActive(true);
        StripComponents(child139);
        var child140 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child140.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child140.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child140.SetActive(true);
        StripComponents(child140);
        var child141 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child141.transform.localPosition = new Vector3(0f, 0f, -40f);
        child141.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child141.SetActive(true);
        StripComponents(child141);
        var child142 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child142.transform.localPosition = new Vector3(0f, 0f, -40f);
        child142.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child142.SetActive(true);
        StripComponents(child142);
        var child143 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child143.transform.localPosition = new Vector3(5f, 0f, -40f);
        child143.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child143.SetActive(true);
        StripComponents(child143);
        var child144 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child144.transform.localPosition = new Vector3(5f, 0f, -40f);
        child144.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child144.SetActive(true);
        StripComponents(child144);
        var child145 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child145.transform.localPosition = new Vector3(10f, 10.5f, -40f);
        child145.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child145.SetActive(true);
        StripComponents(child145);
        var child146 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child146.transform.localPosition = new Vector3(10f, 10.5f, -40f);
        child146.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child146.SetActive(true);
        StripComponents(child146);
        var child147 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child147.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child147.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child147.SetActive(true);
        StripComponents(child147);
        var child148 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child148.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child148.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child148.SetActive(true);
        StripComponents(child148);
        var child149 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child149.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child149.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child149.SetActive(true);
        StripComponents(child149);
        var child150 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child150.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child150.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child150.SetActive(true);
        StripComponents(child150);
        var child151 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child151.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child151.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child151.SetActive(true);
        StripComponents(child151);
        var child152 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child152.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child152.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child152.SetActive(true);
        StripComponents(child152);
        var child153 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child153.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child153.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child153.SetActive(true);
        StripComponents(child153);
        var child154 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child154.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child154.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child154.SetActive(true);
        StripComponents(child154);
        var child155 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child155.transform.localPosition = new Vector3(0f, 0f, -35f);
        child155.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child155.SetActive(true);
        StripComponents(child155);
        var child156 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child156.transform.localPosition = new Vector3(0f, 0f, -35f);
        child156.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child156.SetActive(true);
        StripComponents(child156);
        var child157 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child157.transform.localPosition = new Vector3(5f, 0f, -35f);
        child157.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child157.SetActive(true);
        StripComponents(child157);
        var child158 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child158.transform.localPosition = new Vector3(5f, 0f, -35f);
        child158.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child158.SetActive(true);
        StripComponents(child158);
        var child159 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child159.transform.localPosition = new Vector3(15f, 0f, -35f);
        child159.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child159.SetActive(true);
        StripComponents(child159);
        var child160 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child160.transform.localPosition = new Vector3(15f, 0f, -35f);
        child160.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child160.SetActive(true);
        StripComponents(child160);
        var child161 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child161.transform.localPosition = new Vector3(10f, 10.5f, -35f);
        child161.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child161.SetActive(true);
        StripComponents(child161);
        var child162 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child162.transform.localPosition = new Vector3(10f, 10.5f, -35f);
        child162.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child162.SetActive(true);
        StripComponents(child162);
        var child163 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child163.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child163.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child163.SetActive(true);
        StripComponents(child163);
        var child164 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child164.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child164.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child164.SetActive(true);
        StripComponents(child164);
        var child165 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child165.transform.localPosition = new Vector3(-20f, 0f, -30f);
        child165.transform.localRotation = new Quaternion(0f, 1f, 0f, 0f);
        child165.SetActive(true);
        StripComponents(child165);
        var child166 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child166.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child166.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child166.SetActive(true);
        StripComponents(child166);
        var child167 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child167.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child167.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child167.SetActive(true);
        StripComponents(child167);
        var child168 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child168.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child168.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child168.SetActive(true);
        StripComponents(child168);
        var child169 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child169.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child169.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child169.SetActive(true);
        StripComponents(child169);
        var child170 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child170.transform.localPosition = new Vector3(5f, 0f, -30f);
        child170.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child170.SetActive(true);
        StripComponents(child170);
        var child171 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child171.transform.localPosition = new Vector3(5f, 0f, -30f);
        child171.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child171.SetActive(true);
        StripComponents(child171);
        var child172 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child172.transform.localPosition = new Vector3(10f, 0f, -30f);
        child172.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child172.SetActive(true);
        StripComponents(child172);
        var child173 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child173.transform.localPosition = new Vector3(10f, 0f, -30f);
        child173.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child173.SetActive(true);
        StripComponents(child173);
        var child174 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child174.transform.localPosition = new Vector3(15f, 0f, -30f);
        child174.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child174.SetActive(true);
        StripComponents(child174);
        var child175 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child175.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child175.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child175.SetActive(true);
        StripComponents(child175);
        var child176 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child176.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child176.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child176.SetActive(true);
        StripComponents(child176);
        var child177 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child177.transform.localPosition = new Vector3(0f, 3.5f, -30f);
        child177.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child177.SetActive(true);
        StripComponents(child177);
        var child178 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child178.transform.localPosition = new Vector3(0f, 3.5f, -30f);
        child178.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child178.SetActive(true);
        StripComponents(child178);
        var child179 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child179.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child179.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child179.SetActive(true);
        StripComponents(child179);
        var child180 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child180.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child180.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child180.SetActive(true);
        StripComponents(child180);
        var child181 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child181.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child181.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child181.SetActive(true);
        StripComponents(child181);
        var child182 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child182.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child182.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child182.SetActive(true);
        StripComponents(child182);
        var child183 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child183.transform.localPosition = new Vector3(-15f, 10.5f, -30f);
        child183.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child183.SetActive(true);
        StripComponents(child183);
        var child184 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child184.transform.localPosition = new Vector3(-15f, 10.5f, -30f);
        child184.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child184.SetActive(true);
        StripComponents(child184);
        var child185 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child185.transform.localPosition = new Vector3(-10f, 10.5f, -30f);
        child185.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child185.SetActive(true);
        StripComponents(child185);
        var child186 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child186.transform.localPosition = new Vector3(10f, 10.5f, -30f);
        child186.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child186.SetActive(true);
        StripComponents(child186);
        var child187 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child187.transform.localPosition = new Vector3(10f, 10.5f, -30f);
        child187.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child187.SetActive(true);
        StripComponents(child187);
        var child188 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child188.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child188.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child188.SetActive(true);
        StripComponents(child188);
        var child189 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child189.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child189.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child189.SetActive(true);
        StripComponents(child189);
        var child190 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child190.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child190.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child190.SetActive(true);
        StripComponents(child190);
        var child191 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child191.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child191.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child191.SetActive(true);
        StripComponents(child191);
        var child192 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child192.transform.localPosition = new Vector3(0f, 0f, -25f);
        child192.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child192.SetActive(true);
        StripComponents(child192);
        var child193 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child193.transform.localPosition = new Vector3(5f, 0f, -25f);
        child193.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child193.SetActive(true);
        StripComponents(child193);
        var child194 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child194.transform.localPosition = new Vector3(5f, 0f, -25f);
        child194.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child194.SetActive(true);
        StripComponents(child194);
        var child195 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child195.transform.localPosition = new Vector3(15f, 0f, -25f);
        child195.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child195.SetActive(true);
        StripComponents(child195);
        var child196 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child196.transform.localPosition = new Vector3(15f, 0f, -25f);
        child196.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child196.SetActive(true);
        StripComponents(child196);
        var child197 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child197.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child197.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child197.SetActive(true);
        StripComponents(child197);
        var child198 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child198.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child198.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child198.SetActive(true);
        StripComponents(child198);
        var child199 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child199.transform.localPosition = new Vector3(5f, 3.5f, -25f);
        child199.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child199.SetActive(true);
        StripComponents(child199);
        var child200 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child200.transform.localPosition = new Vector3(5f, 3.5f, -25f);
        child200.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child200.SetActive(true);
        StripComponents(child200);
        var child201 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child201.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child201.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child201.SetActive(true);
        StripComponents(child201);
        var child202 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child202.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child202.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child202.SetActive(true);
        StripComponents(child202);
        var child203 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child203.transform.localPosition = new Vector3(-10f, 10.5f, -25f);
        child203.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child203.SetActive(true);
        StripComponents(child203);
        var child204 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child204.transform.localPosition = new Vector3(-10f, 10.5f, -25f);
        child204.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child204.SetActive(true);
        StripComponents(child204);
        var child205 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child205.transform.localPosition = new Vector3(10f, 10.5f, -25f);
        child205.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child205.SetActive(true);
        StripComponents(child205);
        var child206 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child206.transform.localPosition = new Vector3(10f, 10.5f, -25f);
        child206.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child206.SetActive(true);
        StripComponents(child206);
        var child207 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child207.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child207.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child207.SetActive(true);
        StripComponents(child207);
        var child208 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child208.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child208.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child208.SetActive(true);
        StripComponents(child208);
        var child209 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child209.transform.localPosition = new Vector3(15f, 0f, -20f);
        child209.transform.localRotation = new Quaternion(0f, 1f, 0f, 0f);
        child209.SetActive(true);
        StripComponents(child209);
        var child210 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child210.transform.localPosition = new Vector3(0f, 3.5f, -20f);
        child210.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child210.SetActive(true);
        StripComponents(child210);
        var child211 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child211.transform.localPosition = new Vector3(-10f, 10.5f, -20f);
        child211.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child211.SetActive(true);
        StripComponents(child211);
        var child212 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child212.transform.localPosition = new Vector3(-10f, 10.5f, -20f);
        child212.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child212.SetActive(true);
        StripComponents(child212);
        var child213 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child213.transform.localPosition = new Vector3(10f, 10.5f, -20f);
        child213.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child213.SetActive(true);
        StripComponents(child213);
        var child214 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child214.transform.localPosition = new Vector3(10f, 10.5f, -20f);
        child214.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child214.SetActive(true);
        StripComponents(child214);
        var child215 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child215.transform.localPosition = new Vector3(10f, 0f, -15f);
        child215.transform.localRotation = new Quaternion(0f, 1f, 0f, 0f);
        child215.SetActive(true);
        StripComponents(child215);
        var child216 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child216.transform.localPosition = new Vector3(15f, 0f, -15f);
        child216.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child216.SetActive(true);
        StripComponents(child216);
        var child217 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child217.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child217.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child217.SetActive(true);
        StripComponents(child217);
        var child218 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child218.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child218.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child218.SetActive(true);
        StripComponents(child218);
        var child219 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child219.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child219.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child219.SetActive(true);
        StripComponents(child219);
        var child220 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child220.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child220.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child220.SetActive(true);
        StripComponents(child220);
        var child221 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child221.transform.localPosition = new Vector3(-5f, 10.5f, -15f);
        child221.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child221.SetActive(true);
        StripComponents(child221);
        var child222 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child222.transform.localPosition = new Vector3(-5f, 10.5f, -15f);
        child222.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child222.SetActive(true);
        StripComponents(child222);
        var child223 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child223.transform.localPosition = new Vector3(0f, 10.5f, -15f);
        child223.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child223.SetActive(true);
        StripComponents(child223);
        var child224 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child224.transform.localPosition = new Vector3(5f, 10.5f, -15f);
        child224.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child224.SetActive(true);
        StripComponents(child224);
        var child225 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child225.transform.localPosition = new Vector3(5f, 10.5f, -15f);
        child225.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child225.SetActive(true);
        StripComponents(child225);
        var child226 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child226.transform.localPosition = new Vector3(10f, 0f, -5f);
        child226.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child226.SetActive(true);
        StripComponents(child226);
        var child227 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child227.transform.localPosition = new Vector3(10f, 0f, -5f);
        child227.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child227.SetActive(true);
        StripComponents(child227);
        var child228 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child228.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child228.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child228.SetActive(true);
        StripComponents(child228);
        var child229 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child229.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child229.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child229.SetActive(true);
        StripComponents(child229);
        var child230 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child230.transform.localPosition = new Vector3(0f, 0f, 0f);
        child230.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child230.SetActive(true);
        StripComponents(child230);
        var child231 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child231.transform.localPosition = new Vector3(0f, 0f, 0f);
        child231.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child231.SetActive(true);
        StripComponents(child231);
        var child232 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child232.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child232.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child232.SetActive(true);
        StripComponents(child232);
        var child233 = Object.Instantiate(baseCorridorIShapeCoverSide, obj.transform);
        child233.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child233.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child233.SetActive(true);
        StripComponents(child233);
        var child234 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child234.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child234.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child234.SetActive(true);
        StripComponents(child234);
        var child235 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child235.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child235.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child235.SetActive(true);
        StripComponents(child235);
        var child236 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child236.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child236.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child236.SetActive(true);
        StripComponents(child236);
        var child237 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child237.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child237.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child237.SetActive(true);
        StripComponents(child237);
        var child238 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child238.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child238.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child238.SetActive(true);
        StripComponents(child238);
        var child239 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child239.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child239.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child239.SetActive(true);
        StripComponents(child239);
        var child240 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child240.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child240.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child240.SetActive(true);
        StripComponents(child240);
        var child241 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child241.transform.localPosition = new Vector3(0f, 0f, -40f);
        child241.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child241.SetActive(true);
        StripComponents(child241);
        var child242 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child242.transform.localPosition = new Vector3(5f, 0f, -40f);
        child242.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child242.SetActive(true);
        StripComponents(child242);
        var child243 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child243.transform.localPosition = new Vector3(10f, 10.5f, -40f);
        child243.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child243.SetActive(true);
        StripComponents(child243);
        var child244 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child244.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child244.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child244.SetActive(true);
        StripComponents(child244);
        var child245 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child245.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child245.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child245.SetActive(true);
        StripComponents(child245);
        var child246 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child246.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child246.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child246.SetActive(true);
        StripComponents(child246);
        var child247 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child247.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child247.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child247.SetActive(true);
        StripComponents(child247);
        var child248 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child248.transform.localPosition = new Vector3(0f, 0f, -35f);
        child248.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child248.SetActive(true);
        StripComponents(child248);
        var child249 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child249.transform.localPosition = new Vector3(5f, 0f, -35f);
        child249.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child249.SetActive(true);
        StripComponents(child249);
        var child250 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child250.transform.localPosition = new Vector3(15f, 0f, -35f);
        child250.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child250.SetActive(true);
        StripComponents(child250);
        var child251 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child251.transform.localPosition = new Vector3(10f, 10.5f, -35f);
        child251.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child251.SetActive(true);
        StripComponents(child251);
        var child252 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child252.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child252.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child252.SetActive(true);
        StripComponents(child252);
        var child253 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child253.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child253.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child253.SetActive(true);
        StripComponents(child253);
        var child254 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child254.transform.localPosition = new Vector3(5f, 0f, -30f);
        child254.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child254.SetActive(true);
        StripComponents(child254);
        var child255 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child255.transform.localPosition = new Vector3(10f, 0f, -30f);
        child255.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child255.SetActive(true);
        StripComponents(child255);
        var child256 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child256.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child256.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child256.SetActive(true);
        StripComponents(child256);
        var child257 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child257.transform.localPosition = new Vector3(0f, 3.5f, -30f);
        child257.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child257.SetActive(true);
        StripComponents(child257);
        var child258 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child258.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child258.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child258.SetActive(true);
        StripComponents(child258);
        var child259 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child259.transform.localPosition = new Vector3(-15f, 10.5f, -30f);
        child259.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child259.SetActive(true);
        StripComponents(child259);
        var child260 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child260.transform.localPosition = new Vector3(10f, 10.5f, -30f);
        child260.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child260.SetActive(true);
        StripComponents(child260);
        var child261 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child261.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child261.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child261.SetActive(true);
        StripComponents(child261);
        var child262 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child262.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child262.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child262.SetActive(true);
        StripComponents(child262);
        var child263 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child263.transform.localPosition = new Vector3(5f, 0f, -25f);
        child263.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child263.SetActive(true);
        StripComponents(child263);
        var child264 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child264.transform.localPosition = new Vector3(15f, 0f, -25f);
        child264.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child264.SetActive(true);
        StripComponents(child264);
        var child265 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child265.transform.localPosition = new Vector3(5f, 3.5f, -25f);
        child265.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child265.SetActive(true);
        StripComponents(child265);
        var child266 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child266.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child266.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child266.SetActive(true);
        StripComponents(child266);
        var child267 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child267.transform.localPosition = new Vector3(-10f, 10.5f, -25f);
        child267.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child267.SetActive(true);
        StripComponents(child267);
        var child268 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child268.transform.localPosition = new Vector3(10f, 10.5f, -25f);
        child268.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child268.SetActive(true);
        StripComponents(child268);
        var child269 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child269.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child269.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child269.SetActive(true);
        StripComponents(child269);
        var child270 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child270.transform.localPosition = new Vector3(-10f, 10.5f, -20f);
        child270.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child270.SetActive(true);
        StripComponents(child270);
        var child271 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child271.transform.localPosition = new Vector3(10f, 10.5f, -20f);
        child271.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child271.SetActive(true);
        StripComponents(child271);
        var child272 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child272.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child272.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child272.SetActive(true);
        StripComponents(child272);
        var child273 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child273.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child273.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child273.SetActive(true);
        StripComponents(child273);
        var child274 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child274.transform.localPosition = new Vector3(-5f, 10.5f, -15f);
        child274.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child274.SetActive(true);
        StripComponents(child274);
        var child275 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child275.transform.localPosition = new Vector3(5f, 10.5f, -15f);
        child275.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child275.SetActive(true);
        StripComponents(child275);
        var child276 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child276.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child276.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child276.SetActive(true);
        StripComponents(child276);
        var child277 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child277.transform.localPosition = new Vector3(0f, 0f, 0f);
        child277.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child277.SetActive(true);
        StripComponents(child277);
        var child278 = Object.Instantiate(baseCorridorCoverIShapeTopIntClosed, obj.transform);
        child278.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child278.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child278.SetActive(true);
        StripComponents(child278);
        var child279 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child279.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child279.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child279.SetActive(true);
        StripComponents(child279);
        var child280 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child280.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child280.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child280.SetActive(true);
        StripComponents(child280);
        var child281 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child281.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child281.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child281.SetActive(true);
        StripComponents(child281);
        var child282 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child282.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child282.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child282.SetActive(true);
        StripComponents(child282);
        var child283 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child283.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child283.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child283.SetActive(true);
        StripComponents(child283);
        var child284 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child284.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child284.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child284.SetActive(true);
        StripComponents(child284);
        var child285 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child285.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child285.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child285.SetActive(true);
        StripComponents(child285);
        var child286 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child286.transform.localPosition = new Vector3(0f, 0f, -40f);
        child286.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child286.SetActive(true);
        StripComponents(child286);
        var child287 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child287.transform.localPosition = new Vector3(5f, 0f, -40f);
        child287.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child287.SetActive(true);
        StripComponents(child287);
        var child288 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child288.transform.localPosition = new Vector3(10f, 10.5f, -40f);
        child288.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child288.SetActive(true);
        StripComponents(child288);
        var child289 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child289.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child289.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child289.SetActive(true);
        StripComponents(child289);
        var child290 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child290.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child290.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child290.SetActive(true);
        StripComponents(child290);
        var child291 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child291.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child291.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child291.SetActive(true);
        StripComponents(child291);
        var child292 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child292.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child292.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child292.SetActive(true);
        StripComponents(child292);
        var child293 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child293.transform.localPosition = new Vector3(0f, 0f, -35f);
        child293.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child293.SetActive(true);
        StripComponents(child293);
        var child294 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child294.transform.localPosition = new Vector3(5f, 0f, -35f);
        child294.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child294.SetActive(true);
        StripComponents(child294);
        var child295 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child295.transform.localPosition = new Vector3(15f, 0f, -35f);
        child295.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child295.SetActive(true);
        StripComponents(child295);
        var child296 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child296.transform.localPosition = new Vector3(10f, 10.5f, -35f);
        child296.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child296.SetActive(true);
        StripComponents(child296);
        var child297 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child297.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child297.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child297.SetActive(true);
        StripComponents(child297);
        var child298 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child298.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child298.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child298.SetActive(true);
        StripComponents(child298);
        var child299 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child299.transform.localPosition = new Vector3(5f, 0f, -30f);
        child299.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child299.SetActive(true);
        StripComponents(child299);
        var child300 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child300.transform.localPosition = new Vector3(10f, 0f, -30f);
        child300.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child300.SetActive(true);
        StripComponents(child300);
        var child301 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child301.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child301.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child301.SetActive(true);
        StripComponents(child301);
        var child302 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child302.transform.localPosition = new Vector3(0f, 3.5f, -30f);
        child302.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child302.SetActive(true);
        StripComponents(child302);
        var child303 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child303.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child303.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child303.SetActive(true);
        StripComponents(child303);
        var child304 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child304.transform.localPosition = new Vector3(-15f, 10.5f, -30f);
        child304.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child304.SetActive(true);
        StripComponents(child304);
        var child305 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child305.transform.localPosition = new Vector3(10f, 10.5f, -30f);
        child305.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child305.SetActive(true);
        StripComponents(child305);
        var child306 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child306.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child306.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child306.SetActive(true);
        StripComponents(child306);
        var child307 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child307.transform.localPosition = new Vector3(15f, 0f, -25f);
        child307.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child307.SetActive(true);
        StripComponents(child307);
        var child308 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child308.transform.localPosition = new Vector3(5f, 3.5f, -25f);
        child308.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child308.SetActive(true);
        StripComponents(child308);
        var child309 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child309.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child309.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child309.SetActive(true);
        StripComponents(child309);
        var child310 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child310.transform.localPosition = new Vector3(-10f, 10.5f, -25f);
        child310.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child310.SetActive(true);
        StripComponents(child310);
        var child311 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child311.transform.localPosition = new Vector3(10f, 10.5f, -25f);
        child311.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child311.SetActive(true);
        StripComponents(child311);
        var child312 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child312.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child312.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child312.SetActive(true);
        StripComponents(child312);
        var child313 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child313.transform.localPosition = new Vector3(-10f, 10.5f, -20f);
        child313.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child313.SetActive(true);
        StripComponents(child313);
        var child314 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child314.transform.localPosition = new Vector3(10f, 10.5f, -20f);
        child314.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child314.SetActive(true);
        StripComponents(child314);
        var child315 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child315.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child315.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child315.SetActive(true);
        StripComponents(child315);
        var child316 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child316.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child316.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child316.SetActive(true);
        StripComponents(child316);
        var child317 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child317.transform.localPosition = new Vector3(-5f, 10.5f, -15f);
        child317.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child317.SetActive(true);
        StripComponents(child317);
        var child318 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child318.transform.localPosition = new Vector3(5f, 10.5f, -15f);
        child318.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child318.SetActive(true);
        StripComponents(child318);
        var child319 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child319.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child319.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child319.SetActive(true);
        StripComponents(child319);
        var child320 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child320.transform.localPosition = new Vector3(0f, 0f, 0f);
        child320.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child320.SetActive(true);
        StripComponents(child320);
        var child321 = Object.Instantiate(baseCorridorCoverIShapeTopExtClosed, obj.transform);
        child321.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child321.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child321.SetActive(true);
        StripComponents(child321);
        var child322 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child322.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child322.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child322.SetActive(true);
        StripComponents(child322);
        var child323 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child323.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child323.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child323.SetActive(true);
        StripComponents(child323);
        var child324 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child324.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child324.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child324.SetActive(true);
        StripComponents(child324);
        var child325 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child325.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child325.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child325.SetActive(true);
        StripComponents(child325);
        var child326 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child326.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child326.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child326.SetActive(true);
        StripComponents(child326);
        var child327 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child327.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child327.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child327.SetActive(true);
        StripComponents(child327);
        var child328 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child328.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child328.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child328.SetActive(true);
        StripComponents(child328);
        var child329 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child329.transform.localPosition = new Vector3(0f, 0f, -40f);
        child329.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child329.SetActive(true);
        StripComponents(child329);
        var child330 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child330.transform.localPosition = new Vector3(5f, 0f, -40f);
        child330.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child330.SetActive(true);
        StripComponents(child330);
        var child331 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child331.transform.localPosition = new Vector3(20f, 0f, -40f);
        child331.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child331.SetActive(true);
        StripComponents(child331);
        var child332 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child332.transform.localPosition = new Vector3(10f, 10.5f, -40f);
        child332.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child332.SetActive(true);
        StripComponents(child332);
        var child333 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child333.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child333.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child333.SetActive(true);
        StripComponents(child333);
        var child334 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child334.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child334.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child334.SetActive(true);
        StripComponents(child334);
        var child335 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child335.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child335.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child335.SetActive(true);
        StripComponents(child335);
        var child336 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child336.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child336.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child336.SetActive(true);
        StripComponents(child336);
        var child337 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child337.transform.localPosition = new Vector3(0f, 0f, -35f);
        child337.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child337.SetActive(true);
        StripComponents(child337);
        var child338 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child338.transform.localPosition = new Vector3(5f, 0f, -35f);
        child338.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child338.SetActive(true);
        StripComponents(child338);
        var child339 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child339.transform.localPosition = new Vector3(15f, 0f, -35f);
        child339.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child339.SetActive(true);
        StripComponents(child339);
        var child340 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child340.transform.localPosition = new Vector3(10f, 10.5f, -35f);
        child340.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child340.SetActive(true);
        StripComponents(child340);
        var child341 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child341.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child341.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child341.SetActive(true);
        StripComponents(child341);
        var child342 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child342.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child342.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child342.SetActive(true);
        StripComponents(child342);
        var child343 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child343.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child343.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child343.SetActive(true);
        StripComponents(child343);
        var child344 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child344.transform.localPosition = new Vector3(5f, 0f, -30f);
        child344.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child344.SetActive(true);
        StripComponents(child344);
        var child345 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child345.transform.localPosition = new Vector3(10f, 0f, -30f);
        child345.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child345.SetActive(true);
        StripComponents(child345);
        var child346 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child346.transform.localPosition = new Vector3(0f, 3.5f, -30f);
        child346.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child346.SetActive(true);
        StripComponents(child346);
        var child347 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child347.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child347.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child347.SetActive(true);
        StripComponents(child347);
        var child348 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child348.transform.localPosition = new Vector3(-15f, 10.5f, -30f);
        child348.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child348.SetActive(true);
        StripComponents(child348);
        var child349 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child349.transform.localPosition = new Vector3(10f, 10.5f, -30f);
        child349.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child349.SetActive(true);
        StripComponents(child349);
        var child350 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child350.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child350.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child350.SetActive(true);
        StripComponents(child350);
        var child351 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child351.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child351.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child351.SetActive(true);
        StripComponents(child351);
        var child352 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child352.transform.localPosition = new Vector3(5f, 0f, -25f);
        child352.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child352.SetActive(true);
        StripComponents(child352);
        var child353 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child353.transform.localPosition = new Vector3(15f, 0f, -25f);
        child353.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child353.SetActive(true);
        StripComponents(child353);
        var child354 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child354.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child354.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child354.SetActive(true);
        StripComponents(child354);
        var child355 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child355.transform.localPosition = new Vector3(5f, 3.5f, -25f);
        child355.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child355.SetActive(true);
        StripComponents(child355);
        var child356 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child356.transform.localPosition = new Vector3(-10f, 10.5f, -25f);
        child356.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child356.SetActive(true);
        StripComponents(child356);
        var child357 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child357.transform.localPosition = new Vector3(10f, 10.5f, -25f);
        child357.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child357.SetActive(true);
        StripComponents(child357);
        var child358 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child358.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child358.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child358.SetActive(true);
        StripComponents(child358);
        var child359 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child359.transform.localPosition = new Vector3(-5f, 0f, -20f);
        child359.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child359.SetActive(true);
        StripComponents(child359);
        var child360 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child360.transform.localPosition = new Vector3(10f, 0f, -20f);
        child360.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child360.SetActive(true);
        StripComponents(child360);
        var child361 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child361.transform.localPosition = new Vector3(-10f, 10.5f, -20f);
        child361.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child361.SetActive(true);
        StripComponents(child361);
        var child362 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child362.transform.localPosition = new Vector3(10f, 10.5f, -20f);
        child362.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child362.SetActive(true);
        StripComponents(child362);
        var child363 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child363.transform.localPosition = new Vector3(-5f, 10.5f, -15f);
        child363.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child363.SetActive(true);
        StripComponents(child363);
        var child364 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child364.transform.localPosition = new Vector3(5f, 10.5f, -15f);
        child364.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child364.SetActive(true);
        StripComponents(child364);
        var child365 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child365.transform.localPosition = new Vector3(15f, 0f, -10f);
        child365.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child365.SetActive(true);
        StripComponents(child365);
        var child366 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child366.transform.localPosition = new Vector3(10f, 0f, -5f);
        child366.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child366.SetActive(true);
        StripComponents(child366);
        var child367 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child367.transform.localPosition = new Vector3(20f, 0f, -5f);
        child367.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child367.SetActive(true);
        StripComponents(child367);
        var child368 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child368.transform.localPosition = new Vector3(0f, 0f, 0f);
        child368.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child368.SetActive(true);
        StripComponents(child368);
        var child369 = Object.Instantiate(baseCorridorCoverIShapeBottomIntClosed, obj.transform);
        child369.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child369.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child369.SetActive(true);
        StripComponents(child369);
        var child370 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child370.transform.localPosition = new Vector3(5f, 10.5f, -50f);
        child370.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child370.SetActive(true);
        StripComponents(child370);
        var child371 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child371.transform.localPosition = new Vector3(10f, 10.5f, -45f);
        child371.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child371.SetActive(true);
        StripComponents(child371);
        var child372 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child372.transform.localPosition = new Vector3(-25f, 0f, -40f);
        child372.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child372.SetActive(true);
        StripComponents(child372);
        var child373 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child373.transform.localPosition = new Vector3(-20f, 0f, -40f);
        child373.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child373.SetActive(true);
        StripComponents(child373);
        var child374 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child374.transform.localPosition = new Vector3(-15f, 0f, -40f);
        child374.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child374.SetActive(true);
        StripComponents(child374);
        var child375 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child375.transform.localPosition = new Vector3(-10f, 0f, -40f);
        child375.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child375.SetActive(true);
        StripComponents(child375);
        var child376 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child376.transform.localPosition = new Vector3(-5f, 0f, -40f);
        child376.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child376.SetActive(true);
        StripComponents(child376);
        var child377 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child377.transform.localPosition = new Vector3(0f, 0f, -40f);
        child377.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child377.SetActive(true);
        StripComponents(child377);
        var child378 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child378.transform.localPosition = new Vector3(5f, 0f, -40f);
        child378.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child378.SetActive(true);
        StripComponents(child378);
        var child379 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child379.transform.localPosition = new Vector3(20f, 0f, -40f);
        child379.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child379.SetActive(true);
        StripComponents(child379);
        var child380 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child380.transform.localPosition = new Vector3(10f, 10.5f, -40f);
        child380.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child380.SetActive(true);
        StripComponents(child380);
        var child381 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child381.transform.localPosition = new Vector3(-30f, 0f, -35f);
        child381.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child381.SetActive(true);
        StripComponents(child381);
        var child382 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child382.transform.localPosition = new Vector3(-15f, 0f, -35f);
        child382.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child382.SetActive(true);
        StripComponents(child382);
        var child383 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child383.transform.localPosition = new Vector3(-10f, 0f, -35f);
        child383.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child383.SetActive(true);
        StripComponents(child383);
        var child384 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child384.transform.localPosition = new Vector3(-5f, 0f, -35f);
        child384.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child384.SetActive(true);
        StripComponents(child384);
        var child385 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child385.transform.localPosition = new Vector3(0f, 0f, -35f);
        child385.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child385.SetActive(true);
        StripComponents(child385);
        var child386 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child386.transform.localPosition = new Vector3(5f, 0f, -35f);
        child386.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child386.SetActive(true);
        StripComponents(child386);
        var child387 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child387.transform.localPosition = new Vector3(15f, 0f, -35f);
        child387.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child387.SetActive(true);
        StripComponents(child387);
        var child388 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child388.transform.localPosition = new Vector3(10f, 10.5f, -35f);
        child388.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child388.SetActive(true);
        StripComponents(child388);
        var child389 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child389.transform.localPosition = new Vector3(-30f, 0f, -30f);
        child389.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child389.SetActive(true);
        StripComponents(child389);
        var child390 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child390.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child390.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child390.SetActive(true);
        StripComponents(child390);
        var child391 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child391.transform.localPosition = new Vector3(-10f, 0f, -30f);
        child391.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child391.SetActive(true);
        StripComponents(child391);
        var child392 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child392.transform.localPosition = new Vector3(5f, 0f, -30f);
        child392.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child392.SetActive(true);
        StripComponents(child392);
        var child393 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child393.transform.localPosition = new Vector3(10f, 0f, -30f);
        child393.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child393.SetActive(true);
        StripComponents(child393);
        var child394 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child394.transform.localPosition = new Vector3(0f, 3.5f, -30f);
        child394.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child394.SetActive(true);
        StripComponents(child394);
        var child395 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child395.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child395.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child395.SetActive(true);
        StripComponents(child395);
        var child396 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child396.transform.localPosition = new Vector3(-15f, 10.5f, -30f);
        child396.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child396.SetActive(true);
        StripComponents(child396);
        var child397 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child397.transform.localPosition = new Vector3(10f, 10.5f, -30f);
        child397.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child397.SetActive(true);
        StripComponents(child397);
        var child398 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child398.transform.localPosition = new Vector3(-30f, 0f, -25f);
        child398.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child398.SetActive(true);
        StripComponents(child398);
        var child399 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child399.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child399.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child399.SetActive(true);
        StripComponents(child399);
        var child400 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child400.transform.localPosition = new Vector3(5f, 0f, -25f);
        child400.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child400.SetActive(true);
        StripComponents(child400);
        var child401 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child401.transform.localPosition = new Vector3(15f, 0f, -25f);
        child401.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child401.SetActive(true);
        StripComponents(child401);
        var child402 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child402.transform.localPosition = new Vector3(-10f, 10.5f, -25f);
        child402.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child402.SetActive(true);
        StripComponents(child402);
        var child403 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child403.transform.localPosition = new Vector3(10f, 10.5f, -25f);
        child403.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child403.SetActive(true);
        StripComponents(child403);
        var child404 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child404.transform.localPosition = new Vector3(-30f, 0f, -20f);
        child404.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child404.SetActive(true);
        StripComponents(child404);
        var child405 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child405.transform.localPosition = new Vector3(-5f, 0f, -20f);
        child405.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child405.SetActive(true);
        StripComponents(child405);
        var child406 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child406.transform.localPosition = new Vector3(10f, 0f, -20f);
        child406.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child406.SetActive(true);
        StripComponents(child406);
        var child407 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child407.transform.localPosition = new Vector3(-10f, 10.5f, -20f);
        child407.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child407.SetActive(true);
        StripComponents(child407);
        var child408 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child408.transform.localPosition = new Vector3(10f, 10.5f, -20f);
        child408.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child408.SetActive(true);
        StripComponents(child408);
        var child409 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child409.transform.localPosition = new Vector3(-5f, 10.5f, -15f);
        child409.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child409.SetActive(true);
        StripComponents(child409);
        var child410 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child410.transform.localPosition = new Vector3(5f, 10.5f, -15f);
        child410.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child410.SetActive(true);
        StripComponents(child410);
        var child411 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child411.transform.localPosition = new Vector3(15f, 0f, -10f);
        child411.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child411.SetActive(true);
        StripComponents(child411);
        var child412 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child412.transform.localPosition = new Vector3(10f, 0f, -5f);
        child412.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child412.SetActive(true);
        StripComponents(child412);
        var child413 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child413.transform.localPosition = new Vector3(20f, 0f, -5f);
        child413.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child413.SetActive(true);
        StripComponents(child413);
        var child414 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child414.transform.localPosition = new Vector3(0f, 0f, 0f);
        child414.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child414.SetActive(true);
        StripComponents(child414);
        var child415 = Object.Instantiate(baseCorridorCoverIShapeBottomExtClosed, obj.transform);
        child415.transform.localPosition = new Vector3(5f, 3.5f, 0f);
        child415.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child415.SetActive(true);
        StripComponents(child415);
        var child416 = Object.Instantiate(baseLargeRoom, obj.transform);
        child416.transform.localPosition = new Vector3(0f, 10.5f, -32.5f);
        child416.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child416.SetActive(true);
        StripComponents(child416);
        var child417 = Object.Instantiate(baseLargeRoom, obj.transform);
        child417.transform.localPosition = new Vector3(25f, 0f, -22.5f);
        child417.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child417.SetActive(true);
        StripComponents(child417);
        var child418 = Object.Instantiate(baseLargeRoomAdjustableSupport, obj.transform);
        child418.transform.localPosition = new Vector3(0f, 10.5f, -32.5f);
        child418.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child418.SetActive(true);
        StripComponents(child418);
        var child419 = Object.Instantiate(baseLargeRoomAdjustableSupport, obj.transform);
        child419.transform.localPosition = new Vector3(25f, 0f, -22.5f);
        child419.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child419.SetActive(true);
        StripComponents(child419);
        var child420 = Object.Instantiate(baseLargeRoomExteriorBottom, obj.transform);
        child420.transform.localPosition = new Vector3(0f, 10.5f, -32.5f);
        child420.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child420.SetActive(true);
        StripComponents(child420);
        var child421 = Object.Instantiate(baseLargeRoomExteriorBottom, obj.transform);
        child421.transform.localPosition = new Vector3(25f, 0f, -22.5f);
        child421.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child421.SetActive(true);
        StripComponents(child421);
        var child422 = Object.Instantiate(baseLargeRoomInteriorTop, obj.transform);
        child422.transform.localPosition = new Vector3(0f, 10.5f, -32.5f);
        child422.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child422.SetActive(true);
        StripComponents(child422);
        var child423 = Object.Instantiate(baseLargeRoomInteriorBottom, obj.transform);
        child423.transform.localPosition = new Vector3(0f, 10.5f, -32.5f);
        child423.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child423.SetActive(true);
        StripComponents(child423);
        var child424 = Object.Instantiate(baseLargeRoomCorridorConnectorShort, obj.transform);
        child424.transform.localPosition = new Vector3(0f, 10.5f, -45f);
        child424.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child424.SetActive(true);
        StripComponents(child424);
        var child425 = Object.Instantiate(baseLargeRoomCorridorConnectorShort, obj.transform);
        child425.transform.localPosition = new Vector3(0f, 10.5f, -20f);
        child425.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child425.SetActive(true);
        StripComponents(child425);
        var child426 = Object.Instantiate(baseLargeRoomCorridorConnectorShort, obj.transform);
        child426.transform.localPosition = new Vector3(25f, 0f, -35f);
        child426.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child426.SetActive(true);
        StripComponents(child426);
        var child427 = Object.Instantiate(baseLargeRoomCorridorConnectorShort, obj.transform);
        child427.transform.localPosition = new Vector3(25f, 0f, -10f);
        child427.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child427.SetActive(true);
        StripComponents(child427);
        var child428 = Object.Instantiate(baseLargeRoomReinforcementSide, obj.transform);
        child428.transform.localPosition = new Vector3(-5f, 10.5f, -40f);
        child428.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child428.SetActive(true);
        StripComponents(child428);
        var child429 = Object.Instantiate(baseLargeRoomReinforcementSide, obj.transform);
        child429.transform.localPosition = new Vector3(-5f, 10.5f, -25f);
        child429.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child429.SetActive(true);
        StripComponents(child429);
        var child430 = Object.Instantiate(baseLargeRoomReinforcementSide, obj.transform);
        child430.transform.localPosition = new Vector3(5f, 10.5f, -40f);
        child430.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child430.SetActive(true);
        StripComponents(child430);
        var child431 = Object.Instantiate(baseLargeRoomReinforcementSide, obj.transform);
        child431.transform.localPosition = new Vector3(5f, 10.5f, -30f);
        child431.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child431.SetActive(true);
        StripComponents(child431);
        var child432 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child432.transform.localPosition = new Vector3(-5f, 10.5f, -35f);
        child432.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child432.SetActive(true);
        StripComponents(child432);
        var child433 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child433.transform.localPosition = new Vector3(5f, 10.5f, -35f);
        child433.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child433.SetActive(true);
        StripComponents(child433);
        var child434 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child434.transform.localPosition = new Vector3(5f, 10.5f, -25f);
        child434.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child434.SetActive(true);
        StripComponents(child434);
        var child435 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child435.transform.localPosition = new Vector3(20f, 0f, -30f);
        child435.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child435.SetActive(true);
        StripComponents(child435);
        var child436 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child436.transform.localPosition = new Vector3(20f, 0f, -25f);
        child436.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child436.SetActive(true);
        StripComponents(child436);
        var child437 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child437.transform.localPosition = new Vector3(20f, 0f, -15f);
        child437.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child437.SetActive(true);
        StripComponents(child437);
        var child438 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child438.transform.localPosition = new Vector3(30f, 0f, -30f);
        child438.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child438.SetActive(true);
        StripComponents(child438);
        var child439 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child439.transform.localPosition = new Vector3(30f, 0f, -25f);
        child439.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child439.SetActive(true);
        StripComponents(child439);
        var child440 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child440.transform.localPosition = new Vector3(30f, 0f, -20f);
        child440.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child440.SetActive(true);
        StripComponents(child440);
        var child441 = Object.Instantiate(baseLargeRoomCoverSide, obj.transform);
        child441.transform.localPosition = new Vector3(30f, 0f, -15f);
        child441.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child441.SetActive(true);
        StripComponents(child441);
        var child442 = Object.Instantiate(baseLargeRoomCorridorConnector, obj.transform);
        child442.transform.localPosition = new Vector3(-5f, 10.5f, -30f);
        child442.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child442.SetActive(true);
        StripComponents(child442);
        var child443 = Object.Instantiate(baseLargeRoomCorridorConnector, obj.transform);
        child443.transform.localPosition = new Vector3(20f, 0f, -20f);
        child443.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child443.SetActive(true);
        StripComponents(child443);
        var child444 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child444.transform.localPosition = new Vector3(0f, 10.5f, -43.4f);
        child444.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child444.SetActive(true);
        StripComponents(child444);
        var child445 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child445.transform.localPosition = new Vector3(-3.4f, 10.5f, -40f);
        child445.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child445.SetActive(true);
        StripComponents(child445);
        var child446 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child446.transform.localPosition = new Vector3(-3.4f, 10.5f, -35f);
        child446.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child446.SetActive(true);
        StripComponents(child446);
        var child447 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child447.transform.localPosition = new Vector3(-3.4f, 10.5f, -30f);
        child447.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child447.SetActive(true);
        StripComponents(child447);
        var child448 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child448.transform.localPosition = new Vector3(-3.4f, 10.5f, -25f);
        child448.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child448.SetActive(true);
        StripComponents(child448);
        var child449 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child449.transform.localPosition = new Vector3(0f, 10.5f, -40f);
        child449.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child449.SetActive(true);
        StripComponents(child449);
        var child450 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child450.transform.localPosition = new Vector3(0f, 10.5f, -35f);
        child450.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child450.SetActive(true);
        StripComponents(child450);
        var child451 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child451.transform.localPosition = new Vector3(0f, 10.5f, -30f);
        child451.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child451.SetActive(true);
        StripComponents(child451);
        var child452 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child452.transform.localPosition = new Vector3(0f, 10.5f, -25f);
        child452.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child452.SetActive(true);
        StripComponents(child452);
        var child453 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child453.transform.localPosition = new Vector3(3.4f, 10.5f, -40f);
        child453.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child453.SetActive(true);
        StripComponents(child453);
        var child454 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child454.transform.localPosition = new Vector3(3.4f, 10.5f, -35f);
        child454.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child454.SetActive(true);
        StripComponents(child454);
        var child455 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child455.transform.localPosition = new Vector3(3.4f, 10.5f, -30f);
        child455.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child455.SetActive(true);
        StripComponents(child455);
        var child456 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child456.transform.localPosition = new Vector3(3.4f, 10.5f, -25f);
        child456.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child456.SetActive(true);
        StripComponents(child456);
        var child457 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child457.transform.localPosition = new Vector3(0f, 10.5f, -21.6f);
        child457.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child457.SetActive(true);
        StripComponents(child457);
        var child458 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child458.transform.localPosition = new Vector3(25f, 0f, -33.4f);
        child458.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child458.SetActive(true);
        StripComponents(child458);
        var child459 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child459.transform.localPosition = new Vector3(21.6f, 0f, -30f);
        child459.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child459.SetActive(true);
        StripComponents(child459);
        var child460 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child460.transform.localPosition = new Vector3(21.6f, 0f, -25f);
        child460.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child460.SetActive(true);
        StripComponents(child460);
        var child461 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child461.transform.localPosition = new Vector3(21.6f, 0f, -20f);
        child461.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child461.SetActive(true);
        StripComponents(child461);
        var child462 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child462.transform.localPosition = new Vector3(21.6f, 0f, -15f);
        child462.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child462.SetActive(true);
        StripComponents(child462);
        var child463 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child463.transform.localPosition = new Vector3(25f, 0f, -30f);
        child463.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child463.SetActive(true);
        StripComponents(child463);
        var child464 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child464.transform.localPosition = new Vector3(25f, 0f, -15f);
        child464.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child464.SetActive(true);
        StripComponents(child464);
        var child465 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child465.transform.localPosition = new Vector3(28.4f, 0f, -30f);
        child465.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child465.SetActive(true);
        StripComponents(child465);
        var child466 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child466.transform.localPosition = new Vector3(28.4f, 0f, -25f);
        child466.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child466.SetActive(true);
        StripComponents(child466);
        var child467 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child467.transform.localPosition = new Vector3(28.4f, 0f, -20f);
        child467.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child467.SetActive(true);
        StripComponents(child467);
        var child468 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child468.transform.localPosition = new Vector3(28.4f, 0f, -15f);
        child468.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child468.SetActive(true);
        StripComponents(child468);
        var child469 = Object.Instantiate(baseLargeRoomCoverBottom, obj.transform);
        child469.transform.localPosition = new Vector3(25f, 0f, -11.6f);
        child469.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child469.SetActive(true);
        StripComponents(child469);
        var child470 = Object.Instantiate(baseLargeRoomExteriorTop, obj.transform);
        child470.transform.localPosition = new Vector3(0f, 10.5f, -32.5f);
        child470.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child470.SetActive(true);
        StripComponents(child470);
        var child471 = Object.Instantiate(baseLargeRoomExteriorTop, obj.transform);
        child471.transform.localPosition = new Vector3(25f, 0f, -22.5f);
        child471.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child471.SetActive(true);
        StripComponents(child471);
        var child472 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child472.transform.localPosition = new Vector3(0f, 10.5f, -43.4f);
        child472.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child472.SetActive(true);
        StripComponents(child472);
        var child473 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child473.transform.localPosition = new Vector3(-3.4f, 10.5f, -40f);
        child473.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child473.SetActive(true);
        StripComponents(child473);
        var child474 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child474.transform.localPosition = new Vector3(-3.4f, 10.5f, -35f);
        child474.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child474.SetActive(true);
        StripComponents(child474);
        var child475 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child475.transform.localPosition = new Vector3(-3.4f, 10.5f, -30f);
        child475.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child475.SetActive(true);
        StripComponents(child475);
        var child476 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child476.transform.localPosition = new Vector3(-3.4f, 10.5f, -25f);
        child476.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child476.SetActive(true);
        StripComponents(child476);
        var child477 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child477.transform.localPosition = new Vector3(0f, 10.5f, -40f);
        child477.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child477.SetActive(true);
        StripComponents(child477);
        var child478 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child478.transform.localPosition = new Vector3(0f, 10.5f, -35f);
        child478.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child478.SetActive(true);
        StripComponents(child478);
        var child479 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child479.transform.localPosition = new Vector3(0f, 10.5f, -30f);
        child479.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child479.SetActive(true);
        StripComponents(child479);
        var child480 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child480.transform.localPosition = new Vector3(0f, 10.5f, -25f);
        child480.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child480.SetActive(true);
        StripComponents(child480);
        var child481 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child481.transform.localPosition = new Vector3(3.4f, 10.5f, -40f);
        child481.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child481.SetActive(true);
        StripComponents(child481);
        var child482 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child482.transform.localPosition = new Vector3(3.4f, 10.5f, -35f);
        child482.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child482.SetActive(true);
        StripComponents(child482);
        var child483 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child483.transform.localPosition = new Vector3(3.4f, 10.5f, -30f);
        child483.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child483.SetActive(true);
        StripComponents(child483);
        var child484 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child484.transform.localPosition = new Vector3(3.4f, 10.5f, -25f);
        child484.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child484.SetActive(true);
        StripComponents(child484);
        var child485 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child485.transform.localPosition = new Vector3(0f, 10.5f, -21.6f);
        child485.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child485.SetActive(true);
        StripComponents(child485);
        var child486 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child486.transform.localPosition = new Vector3(25f, 0f, -33.4f);
        child486.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child486.SetActive(true);
        StripComponents(child486);
        var child487 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child487.transform.localPosition = new Vector3(21.6f, 0f, -30f);
        child487.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child487.SetActive(true);
        StripComponents(child487);
        var child488 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child488.transform.localPosition = new Vector3(21.6f, 0f, -25f);
        child488.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child488.SetActive(true);
        StripComponents(child488);
        var child489 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child489.transform.localPosition = new Vector3(21.6f, 0f, -20f);
        child489.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child489.SetActive(true);
        StripComponents(child489);
        var child490 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child490.transform.localPosition = new Vector3(21.6f, 0f, -15f);
        child490.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child490.SetActive(true);
        StripComponents(child490);
        var child491 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child491.transform.localPosition = new Vector3(25f, 0f, -30f);
        child491.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child491.SetActive(true);
        StripComponents(child491);
        var child492 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child492.transform.localPosition = new Vector3(25f, 0f, -15f);
        child492.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child492.SetActive(true);
        StripComponents(child492);
        var child493 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child493.transform.localPosition = new Vector3(28.4f, 0f, -30f);
        child493.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child493.SetActive(true);
        StripComponents(child493);
        var child494 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child494.transform.localPosition = new Vector3(28.4f, 0f, -25f);
        child494.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child494.SetActive(true);
        StripComponents(child494);
        var child495 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child495.transform.localPosition = new Vector3(28.4f, 0f, -20f);
        child495.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child495.SetActive(true);
        StripComponents(child495);
        var child496 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child496.transform.localPosition = new Vector3(28.4f, 0f, -15f);
        child496.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child496.SetActive(true);
        StripComponents(child496);
        var child497 = Object.Instantiate(baseLargeRoomCoverTop, obj.transform);
        child497.transform.localPosition = new Vector3(25f, 0f, -11.6f);
        child497.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child497.SetActive(true);
        StripComponents(child497);
        var child498 = Object.Instantiate(baseCorridorLShapeGlass, obj.transform);
        child498.transform.localPosition = new Vector3(15f, 0f, -40f);
        child498.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child498.SetActive(true);
        StripComponents(child498);
        var child499 = Object.Instantiate(baseCorridorLShapeGlass, obj.transform);
        child499.transform.localPosition = new Vector3(25f, 0f, -40f);
        child499.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child499.SetActive(true);
        StripComponents(child499);
        var child500 = Object.Instantiate(baseCorridorLShapeGlass, obj.transform);
        child500.transform.localPosition = new Vector3(-5f, 0f, -30f);
        child500.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child500.SetActive(true);
        StripComponents(child500);
        var child501 = Object.Instantiate(baseCorridorLShapeGlass, obj.transform);
        child501.transform.localPosition = new Vector3(-5f, 0f, -25f);
        child501.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child501.SetActive(true);
        StripComponents(child501);
        var child502 = Object.Instantiate(baseCorridorLShapeGlass, obj.transform);
        child502.transform.localPosition = new Vector3(-10f, 0f, -20f);
        child502.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child502.SetActive(true);
        StripComponents(child502);
        var child503 = Object.Instantiate(baseCorridorLShapeGlass, obj.transform);
        child503.transform.localPosition = new Vector3(15f, 0f, -5f);
        child503.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child503.SetActive(true);
        StripComponents(child503);
        var child504 = Object.Instantiate(baseCorridorIShapeGlass, obj.transform);
        child504.transform.localPosition = new Vector3(20f, 0f, -40f);
        child504.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child504.SetActive(true);
        StripComponents(child504);
        var child505 = Object.Instantiate(baseCorridorIShapeGlass, obj.transform);
        child505.transform.localPosition = new Vector3(-5f, 0f, -20f);
        child505.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child505.SetActive(true);
        StripComponents(child505);
        var child506 = Object.Instantiate(baseCorridorIShapeGlass, obj.transform);
        child506.transform.localPosition = new Vector3(10f, 0f, -20f);
        child506.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child506.SetActive(true);
        StripComponents(child506);
        var child507 = Object.Instantiate(baseCorridorIShapeGlass, obj.transform);
        child507.transform.localPosition = new Vector3(15f, 0f, -10f);
        child507.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child507.SetActive(true);
        StripComponents(child507);
        var child508 = Object.Instantiate(baseCorridorIShapeGlass, obj.transform);
        child508.transform.localPosition = new Vector3(20f, 0f, -5f);
        child508.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child508.SetActive(true);
        StripComponents(child508);
        var child509 = Object.Instantiate(baseLargeRoomInteriorTopHole2, obj.transform);
        child509.transform.localPosition = new Vector3(25f, 0f, -22.5f);
        child509.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child509.SetActive(true);
        StripComponents(child509);
        var child510 = Object.Instantiate(baseLargeRoomInteriorBottomHole2, obj.transform);
        child510.transform.localPosition = new Vector3(25f, 0f, -22.5f);
        child510.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child510.SetActive(true);
        StripComponents(child510);
        var child511 = Object.Instantiate(baseRoomBioReactor, obj.transform);
        child511.transform.localPosition = new Vector3(25f, 0f, -30f);
        child511.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child511.SetActive(true);
        StripComponents(child511);
        var child512 = Object.Instantiate(baseRoomBioReactor, obj.transform);
        child512.transform.localPosition = new Vector3(25f, 0f, -15f);
        child512.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child512.SetActive(true);
        StripComponents(child512);
        var child513 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child513.transform.localPosition = new Vector3(25f, 0f, -25f);
        child513.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child513.SetActive(true);
        StripComponents(child513);
        var child514 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child514.transform.localPosition = new Vector3(25f, 0f, -25f);
        child514.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child514.SetActive(true);
        StripComponents(child514);
        var child515 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child515.transform.localPosition = new Vector3(25f, 0f, -25f);
        child515.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child515.SetActive(true);
        StripComponents(child515);
        var child516 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child516.transform.localPosition = new Vector3(25f, 0f, -20f);
        child516.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child516.SetActive(true);
        StripComponents(child516);
        var child517 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child517.transform.localPosition = new Vector3(25f, 0f, -20f);
        child517.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child517.SetActive(true);
        StripComponents(child517);
        var child518 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child518.transform.localPosition = new Vector3(25f, 0f, -20f);
        child518.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child518.SetActive(true);
        StripComponents(child518);
        var child519 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child519.transform.localPosition = new Vector3(0f, 0f, -10f);
        child519.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child519.SetActive(true);
        StripComponents(child519);
        var child520 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child520.transform.localPosition = new Vector3(0f, 0f, -10f);
        child520.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child520.SetActive(true);
        StripComponents(child520);
        var child521 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child521.transform.localPosition = new Vector3(0f, 0f, -10f);
        child521.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child521.SetActive(true);
        StripComponents(child521);
        var child522 = Object.Instantiate(baseWaterParkSide, obj.transform);
        child522.transform.localPosition = new Vector3(0f, 0f, -10f);
        child522.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child522.SetActive(true);
        StripComponents(child522);
        var child523 = Object.Instantiate(baseLargeWaterParkWalls, obj.transform);
        child523.transform.localPosition = new Vector3(25f, 0f, -25f);
        child523.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child523.SetActive(true);
        StripComponents(child523);
        var child524 = Object.Instantiate(baseLargeWaterParkCeilingTop, obj.transform);
        child524.transform.localPosition = new Vector3(25f, 0f, -25f);
        child524.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child524.SetActive(true);
        StripComponents(child524);
        var child525 = Object.Instantiate(baseLargeWaterParkFloorBottom, obj.transform);
        child525.transform.localPosition = new Vector3(25f, 0f, -25f);
        child525.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child525.SetActive(true);
        StripComponents(child525);
        var child526 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child526.transform.localPosition = new Vector3(-20f, 0f, -30f);
        child526.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child526.SetActive(true);
        StripComponents(child526);
        var child527 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child527.transform.localPosition = new Vector3(15f, 0f, -30f);
        child527.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child527.SetActive(true);
        StripComponents(child527);
        var child528 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child528.transform.localPosition = new Vector3(-10f, 10.5f, -30f);
        child528.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child528.SetActive(true);
        StripComponents(child528);
        var child529 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child529.transform.localPosition = new Vector3(0f, 0f, -25f);
        child529.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child529.SetActive(true);
        StripComponents(child529);
        var child530 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child530.transform.localPosition = new Vector3(15f, 0f, -20f);
        child530.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child530.SetActive(true);
        StripComponents(child530);
        var child531 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child531.transform.localPosition = new Vector3(0f, 3.5f, -20f);
        child531.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child531.SetActive(true);
        StripComponents(child531);
        var child532 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child532.transform.localPosition = new Vector3(10f, 0f, -15f);
        child532.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child532.SetActive(true);
        StripComponents(child532);
        var child533 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child533.transform.localPosition = new Vector3(15f, 0f, -15f);
        child533.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child533.SetActive(true);
        StripComponents(child533);
        var child534 = Object.Instantiate(baseCorridorTShape, obj.transform);
        child534.transform.localPosition = new Vector3(0f, 10.5f, -15f);
        child534.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child534.SetActive(true);
        StripComponents(child534);
        var child535 = Object.Instantiate(baseCorridorTShapeAdjustableSupport, obj.transform);
        child535.transform.localPosition = new Vector3(-20f, 0f, -30f);
        child535.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child535.SetActive(true);
        StripComponents(child535);
        var child536 = Object.Instantiate(baseCorridorTShapeAdjustableSupport, obj.transform);
        child536.transform.localPosition = new Vector3(15f, 0f, -30f);
        child536.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child536.SetActive(true);
        StripComponents(child536);
        var child537 = Object.Instantiate(baseCorridorTShapeAdjustableSupport, obj.transform);
        child537.transform.localPosition = new Vector3(0f, 0f, -25f);
        child537.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child537.SetActive(true);
        StripComponents(child537);
        var child538 = Object.Instantiate(baseCorridorTShapeAdjustableSupport, obj.transform);
        child538.transform.localPosition = new Vector3(15f, 0f, -20f);
        child538.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child538.SetActive(true);
        StripComponents(child538);
        var child539 = Object.Instantiate(baseCorridorTShapeAdjustableSupport, obj.transform);
        child539.transform.localPosition = new Vector3(10f, 0f, -15f);
        child539.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child539.SetActive(true);
        StripComponents(child539);
        var child540 = Object.Instantiate(baseCorridorTShapeAdjustableSupport, obj.transform);
        child540.transform.localPosition = new Vector3(15f, 0f, -15f);
        child540.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child540.SetActive(true);
        StripComponents(child540);
        var child541 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child541.transform.localPosition = new Vector3(-20f, 0f, -30f);
        child541.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child541.SetActive(true);
        StripComponents(child541);
        var child542 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child542.transform.localPosition = new Vector3(15f, 0f, -30f);
        child542.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child542.SetActive(true);
        StripComponents(child542);
        var child543 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child543.transform.localPosition = new Vector3(-10f, 10.5f, -30f);
        child543.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child543.SetActive(true);
        StripComponents(child543);
        var child544 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child544.transform.localPosition = new Vector3(0f, 0f, -25f);
        child544.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child544.SetActive(true);
        StripComponents(child544);
        var child545 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child545.transform.localPosition = new Vector3(15f, 0f, -20f);
        child545.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child545.SetActive(true);
        StripComponents(child545);
        var child546 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child546.transform.localPosition = new Vector3(0f, 3.5f, -20f);
        child546.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child546.SetActive(true);
        StripComponents(child546);
        var child547 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child547.transform.localPosition = new Vector3(15f, 0f, -15f);
        child547.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child547.SetActive(true);
        StripComponents(child547);
        var child548 = Object.Instantiate(baseCorridorCoverTShapeTopIntClosed, obj.transform);
        child548.transform.localPosition = new Vector3(0f, 10.5f, -15f);
        child548.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child548.SetActive(true);
        StripComponents(child548);
        var child549 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child549.transform.localPosition = new Vector3(-20f, 0f, -30f);
        child549.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child549.SetActive(true);
        StripComponents(child549);
        var child550 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child550.transform.localPosition = new Vector3(15f, 0f, -30f);
        child550.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child550.SetActive(true);
        StripComponents(child550);
        var child551 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child551.transform.localPosition = new Vector3(-10f, 10.5f, -30f);
        child551.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child551.SetActive(true);
        StripComponents(child551);
        var child552 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child552.transform.localPosition = new Vector3(0f, 0f, -25f);
        child552.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child552.SetActive(true);
        StripComponents(child552);
        var child553 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child553.transform.localPosition = new Vector3(15f, 0f, -20f);
        child553.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child553.SetActive(true);
        StripComponents(child553);
        var child554 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child554.transform.localPosition = new Vector3(0f, 3.5f, -20f);
        child554.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child554.SetActive(true);
        StripComponents(child554);
        var child555 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child555.transform.localPosition = new Vector3(15f, 0f, -15f);
        child555.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child555.SetActive(true);
        StripComponents(child555);
        var child556 = Object.Instantiate(baseCorridorCoverTShapeTopExtClosed, obj.transform);
        child556.transform.localPosition = new Vector3(0f, 10.5f, -15f);
        child556.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child556.SetActive(true);
        StripComponents(child556);
        var child557 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child557.transform.localPosition = new Vector3(-20f, 0f, -30f);
        child557.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child557.SetActive(true);
        StripComponents(child557);
        var child558 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child558.transform.localPosition = new Vector3(15f, 0f, -30f);
        child558.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child558.SetActive(true);
        StripComponents(child558);
        var child559 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child559.transform.localPosition = new Vector3(-10f, 10.5f, -30f);
        child559.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child559.SetActive(true);
        StripComponents(child559);
        var child560 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child560.transform.localPosition = new Vector3(0f, 0f, -25f);
        child560.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child560.SetActive(true);
        StripComponents(child560);
        var child561 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child561.transform.localPosition = new Vector3(15f, 0f, -20f);
        child561.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child561.SetActive(true);
        StripComponents(child561);
        var child562 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child562.transform.localPosition = new Vector3(10f, 0f, -15f);
        child562.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child562.SetActive(true);
        StripComponents(child562);
        var child563 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child563.transform.localPosition = new Vector3(15f, 0f, -15f);
        child563.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child563.SetActive(true);
        StripComponents(child563);
        var child564 = Object.Instantiate(baseCorridorCoverTShapeBottomIntClosed, obj.transform);
        child564.transform.localPosition = new Vector3(0f, 10.5f, -15f);
        child564.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child564.SetActive(true);
        StripComponents(child564);
        var child565 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child565.transform.localPosition = new Vector3(-20f, 0f, -30f);
        child565.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child565.SetActive(true);
        StripComponents(child565);
        var child566 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child566.transform.localPosition = new Vector3(15f, 0f, -30f);
        child566.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child566.SetActive(true);
        StripComponents(child566);
        var child567 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child567.transform.localPosition = new Vector3(-10f, 10.5f, -30f);
        child567.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child567.SetActive(true);
        StripComponents(child567);
        var child568 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child568.transform.localPosition = new Vector3(0f, 0f, -25f);
        child568.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child568.SetActive(true);
        StripComponents(child568);
        var child569 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child569.transform.localPosition = new Vector3(15f, 0f, -20f);
        child569.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child569.SetActive(true);
        StripComponents(child569);
        var child570 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child570.transform.localPosition = new Vector3(10f, 0f, -15f);
        child570.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child570.SetActive(true);
        StripComponents(child570);
        var child571 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child571.transform.localPosition = new Vector3(15f, 0f, -15f);
        child571.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child571.SetActive(true);
        StripComponents(child571);
        var child572 = Object.Instantiate(baseCorridorCoverTShapeBottomExtClosed, obj.transform);
        child572.transform.localPosition = new Vector3(0f, 10.5f, -15f);
        child572.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child572.SetActive(true);
        StripComponents(child572);
        var child573 = Object.Instantiate(baseCorridorCoverIShapeTopExtOpened, obj.transform);
        child573.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child573.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child573.SetActive(true);
        StripComponents(child573);
        var child574 = Object.Instantiate(baseCorridorCoverIShapeTopExtOpened, obj.transform);
        child574.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child574.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child574.SetActive(true);
        StripComponents(child574);
        var child575 = Object.Instantiate(baseCorridorCoverIShapeTopExtOpened, obj.transform);
        child575.transform.localPosition = new Vector3(-20f, 0f, -25f);
        child575.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child575.SetActive(true);
        StripComponents(child575);
        var child576 = Object.Instantiate(baseCorridorCoverIShapeTopExtOpened, obj.transform);
        child576.transform.localPosition = new Vector3(5f, 0f, -25f);
        child576.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child576.SetActive(true);
        StripComponents(child576);
        var child577 = Object.Instantiate(baseCorridorCoverIShapeTopExtOpened, obj.transform);
        child577.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child577.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child577.SetActive(true);
        StripComponents(child577);
        var child578 = Object.Instantiate(baseCorridorCoverIShapeTopExtOpened, obj.transform);
        child578.transform.localPosition = new Vector3(10f, 0f, -5f);
        child578.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child578.SetActive(true);
        StripComponents(child578);
        var child579 = Object.Instantiate(baseCorridorCoverIShapeTopIntOpened, obj.transform);
        child579.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child579.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child579.SetActive(true);
        StripComponents(child579);
        var child580 = Object.Instantiate(baseCorridorCoverIShapeTopIntOpened, obj.transform);
        child580.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child580.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child580.SetActive(true);
        StripComponents(child580);
        var child581 = Object.Instantiate(baseCorridorCoverIShapeTopIntOpened, obj.transform);
        child581.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child581.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child581.SetActive(true);
        StripComponents(child581);
        var child582 = Object.Instantiate(baseCorridorCoverIShapeTopIntOpened, obj.transform);
        child582.transform.localPosition = new Vector3(10f, 0f, -5f);
        child582.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child582.SetActive(true);
        StripComponents(child582);
        var child583 = Object.Instantiate(baseCorridorLadderTop, obj.transform);
        child583.transform.localPosition = new Vector3(-15f, 0f, -30f);
        child583.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child583.SetActive(true);
        StripComponents(child583);
        var child584 = Object.Instantiate(baseCorridorLadderTop, obj.transform);
        child584.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child584.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child584.SetActive(true);
        StripComponents(child584);
        var child585 = Object.Instantiate(baseCorridorLadderTop, obj.transform);
        child585.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child585.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child585.SetActive(true);
        StripComponents(child585);
        var child586 = Object.Instantiate(baseCorridorLadderTop, obj.transform);
        child586.transform.localPosition = new Vector3(0f, 0f, -20f);
        child586.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child586.SetActive(true);
        StripComponents(child586);
        var child587 = Object.Instantiate(baseCorridorLadderTop, obj.transform);
        child587.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child587.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child587.SetActive(true);
        StripComponents(child587);
        var child588 = Object.Instantiate(baseCorridorLadderTop, obj.transform);
        child588.transform.localPosition = new Vector3(10f, 0f, -15f);
        child588.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child588.SetActive(true);
        StripComponents(child588);
        var child589 = Object.Instantiate(baseCorridorLadderTop, obj.transform);
        child589.transform.localPosition = new Vector3(10f, 0f, -5f);
        child589.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child589.SetActive(true);
        StripComponents(child589);
        var child590 = Object.Instantiate(baseCorridorCap, obj.transform);
        child590.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child590.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child590.SetActive(true);
        StripComponents(child590);
        var child591 = Object.Instantiate(baseCorridorCap, obj.transform);
        child591.transform.localPosition = new Vector3(-20f, 7f, -30f);
        child591.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child591.SetActive(true);
        StripComponents(child591);
        var child592 = Object.Instantiate(baseCorridorCap, obj.transform);
        child592.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child592.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child592.SetActive(true);
        StripComponents(child592);
        var child593 = Object.Instantiate(baseCorridorCap, obj.transform);
        child593.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child593.transform.localRotation = new Quaternion(0f, 0.7071067f, 0f, -0.7071068f);
        child593.SetActive(true);
        StripComponents(child593);
        var child594 = Object.Instantiate(baseCorridorCap, obj.transform);
        child594.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child594.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child594.SetActive(true);
        StripComponents(child594);
        var child595 = Object.Instantiate(baseCorridorCap, obj.transform);
        child595.transform.localPosition = new Vector3(0f, 0f, -20f);
        child595.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child595.SetActive(true);
        StripComponents(child595);
        var child596 = Object.Instantiate(baseCorridorCap, obj.transform);
        child596.transform.localPosition = new Vector3(0f, 0f, -20f);
        child596.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child596.SetActive(true);
        StripComponents(child596);
        var child597 = Object.Instantiate(baseCorridorCap, obj.transform);
        child597.transform.localPosition = new Vector3(10f, 0f, -15f);
        child597.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child597.SetActive(true);
        StripComponents(child597);
        var child598 = Object.Instantiate(baseCorridorCap, obj.transform);
        child598.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child598.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child598.SetActive(true);
        StripComponents(child598);
        var child599 = Object.Instantiate(baseCorridorCap, obj.transform);
        child599.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child599.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child599.SetActive(true);
        StripComponents(child599);
        var child600 = Object.Instantiate(baseCorridorCap, obj.transform);
        child600.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child600.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child600.SetActive(true);
        StripComponents(child600);
        var child601 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child601.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child601.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child601.SetActive(true);
        StripComponents(child601);
        var child602 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child602.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child602.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child602.SetActive(true);
        StripComponents(child602);
        var child603 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child603.transform.localPosition = new Vector3(-20f, 3.5f, -25f);
        child603.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child603.SetActive(true);
        StripComponents(child603);
        var child604 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child604.transform.localPosition = new Vector3(5f, 3.5f, -25f);
        child604.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child604.SetActive(true);
        StripComponents(child604);
        var child605 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child605.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child605.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child605.SetActive(true);
        StripComponents(child605);
        var child606 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child606.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child606.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child606.SetActive(true);
        StripComponents(child606);
        var child607 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child607.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child607.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child607.SetActive(true);
        StripComponents(child607);
        var child608 = Object.Instantiate(baseCorridorCoverIShapeBottomExtOpened, obj.transform);
        child608.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child608.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child608.SetActive(true);
        StripComponents(child608);
        var child609 = Object.Instantiate(baseCorridorCoverIShapeBottomIntOpened, obj.transform);
        child609.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child609.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child609.SetActive(true);
        StripComponents(child609);
        var child610 = Object.Instantiate(baseCorridorCoverIShapeBottomIntOpened, obj.transform);
        child610.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child610.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child610.SetActive(true);
        StripComponents(child610);
        var child611 = Object.Instantiate(baseCorridorCoverIShapeBottomIntOpened, obj.transform);
        child611.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child611.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child611.SetActive(true);
        StripComponents(child611);
        var child612 = Object.Instantiate(baseCorridorCoverIShapeBottomIntOpened, obj.transform);
        child612.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child612.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child612.SetActive(true);
        StripComponents(child612);
        var child613 = Object.Instantiate(baseCorridorCoverIShapeBottomIntOpened, obj.transform);
        child613.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child613.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child613.SetActive(true);
        StripComponents(child613);
        var child614 = Object.Instantiate(baseCorridorCoverIShapeBottomIntOpened, obj.transform);
        child614.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child614.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child614.SetActive(true);
        StripComponents(child614);
        var child615 = Object.Instantiate(baseCorridorLadderBottom, obj.transform);
        child615.transform.localPosition = new Vector3(-15f, 3.5f, -30f);
        child615.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child615.SetActive(true);
        StripComponents(child615);
        var child616 = Object.Instantiate(baseCorridorLadderBottom, obj.transform);
        child616.transform.localPosition = new Vector3(-20f, 10.5f, -30f);
        child616.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child616.SetActive(true);
        StripComponents(child616);
        var child617 = Object.Instantiate(baseCorridorLadderBottom, obj.transform);
        child617.transform.localPosition = new Vector3(-20f, 7f, -25f);
        child617.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child617.SetActive(true);
        StripComponents(child617);
        var child618 = Object.Instantiate(baseCorridorLadderBottom, obj.transform);
        child618.transform.localPosition = new Vector3(0f, 3.5f, -20f);
        child618.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child618.SetActive(true);
        StripComponents(child618);
        var child619 = Object.Instantiate(baseCorridorLadderBottom, obj.transform);
        child619.transform.localPosition = new Vector3(-10f, 3.5f, -15f);
        child619.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child619.SetActive(true);
        StripComponents(child619);
        var child620 = Object.Instantiate(baseCorridorLadderBottom, obj.transform);
        child620.transform.localPosition = new Vector3(10f, 3.5f, -15f);
        child620.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child620.SetActive(true);
        StripComponents(child620);
        var child621 = Object.Instantiate(baseCorridorLadderBottom, obj.transform);
        child621.transform.localPosition = new Vector3(10f, 3.5f, -5f);
        child621.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child621.SetActive(true);
        StripComponents(child621);
        var child622 = Object.Instantiate(baseObservatory, obj.transform);
        child622.transform.localPosition = new Vector3(-10f, 3.5f, -25f);
        child622.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child622.SetActive(true);
        StripComponents(child622);
        var child623 = Object.Instantiate(baseObservatoryCorridorConnector, obj.transform);
        child623.transform.localPosition = new Vector3(-10f, 3.5f, -25f);
        child623.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child623.SetActive(true);
        StripComponents(child623);
        var child624 = Object.Instantiate(baseCorridorXShape, obj.transform);
        child624.transform.localPosition = new Vector3(-5f, 3.5f, -25f);
        child624.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child624.SetActive(true);
        StripComponents(child624);
        var child625 = Object.Instantiate(baseCorridorXShape, obj.transform);
        child625.transform.localPosition = new Vector3(0f, 0f, -20f);
        child625.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child625.SetActive(true);
        StripComponents(child625);
        var child626 = Object.Instantiate(baseCorridorXShape, obj.transform);
        child626.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child626.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child626.SetActive(true);
        StripComponents(child626);
        var child627 = Object.Instantiate(baseCorridorXShape, obj.transform);
        child627.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child627.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child627.SetActive(true);
        StripComponents(child627);
        var child628 = Object.Instantiate(baseCorridorCoverXShapeTopIntClosed, obj.transform);
        child628.transform.localPosition = new Vector3(-5f, 3.5f, -25f);
        child628.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child628.SetActive(true);
        StripComponents(child628);
        var child629 = Object.Instantiate(baseCorridorCoverXShapeTopIntClosed, obj.transform);
        child629.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child629.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child629.SetActive(true);
        StripComponents(child629);
        var child630 = Object.Instantiate(baseCorridorCoverXShapeTopExtClosed, obj.transform);
        child630.transform.localPosition = new Vector3(-5f, 3.5f, -25f);
        child630.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child630.SetActive(true);
        StripComponents(child630);
        var child631 = Object.Instantiate(baseCorridorCoverXShapeTopExtClosed, obj.transform);
        child631.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child631.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child631.SetActive(true);
        StripComponents(child631);
        var child632 = Object.Instantiate(baseCorridorCoverXShapeBottomIntClosed, obj.transform);
        child632.transform.localPosition = new Vector3(-5f, 3.5f, -25f);
        child632.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child632.SetActive(true);
        StripComponents(child632);
        var child633 = Object.Instantiate(baseCorridorCoverXShapeBottomIntClosed, obj.transform);
        child633.transform.localPosition = new Vector3(0f, 0f, -20f);
        child633.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child633.SetActive(true);
        StripComponents(child633);
        var child634 = Object.Instantiate(baseCorridorCoverXShapeBottomIntClosed, obj.transform);
        child634.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child634.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child634.SetActive(true);
        StripComponents(child634);
        var child635 = Object.Instantiate(baseCorridorCoverXShapeBottomIntClosed, obj.transform);
        child635.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child635.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child635.SetActive(true);
        StripComponents(child635);
        var child636 = Object.Instantiate(baseCorridorCoverXShapeBottomExtClosed, obj.transform);
        child636.transform.localPosition = new Vector3(-5f, 3.5f, -25f);
        child636.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child636.SetActive(true);
        StripComponents(child636);
        var child637 = Object.Instantiate(baseCorridorCoverXShapeBottomExtClosed, obj.transform);
        child637.transform.localPosition = new Vector3(0f, 0f, -20f);
        child637.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child637.SetActive(true);
        StripComponents(child637);
        var child638 = Object.Instantiate(baseCorridorCoverXShapeBottomExtClosed, obj.transform);
        child638.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child638.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child638.SetActive(true);
        StripComponents(child638);
        var child639 = Object.Instantiate(baseCorridorCoverXShapeBottomExtClosed, obj.transform);
        child639.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child639.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child639.SetActive(true);
        StripComponents(child639);
        var child640 = Object.Instantiate(baseMoonpool, obj.transform);
        child640.transform.localPosition = new Vector3(-20f, 0f, -12.5f);
        child640.transform.localRotation = new Quaternion(0f, -0.7071068f, 0f, 0.7071068f);
        child640.SetActive(true);
        StripComponents(child640);
        var child641 = Object.Instantiate(baseMoonpoolCorridorConnector, obj.transform);
        child641.transform.localPosition = new Vector3(-25f, 0f, -15f);
        child641.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child641.SetActive(true);
        StripComponents(child641);
        var child642 = Object.Instantiate(baseMoonpoolCorridorConnector, obj.transform);
        child642.transform.localPosition = new Vector3(-15f, 0f, -15f);
        child642.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child642.SetActive(true);
        StripComponents(child642);
        var child643 = Object.Instantiate(baseMoonpoolCorridorConnector, obj.transform);
        child643.transform.localPosition = new Vector3(-15f, 0f, -10f);
        child643.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child643.SetActive(true);
        StripComponents(child643);
        var child644 = Object.Instantiate(baseMoonpoolCoverSide, obj.transform);
        child644.transform.localPosition = new Vector3(-25f, 0f, -10f);
        child644.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child644.SetActive(true);
        StripComponents(child644);
        var child645 = Object.Instantiate(baseMoonpoolCoverSideShort, obj.transform);
        child645.transform.localPosition = new Vector3(-20f, 0f, -5f);
        child645.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child645.SetActive(true);
        StripComponents(child645);
        var child646 = Object.Instantiate(baseMoonpoolCorridorConnectorShort, obj.transform);
        child646.transform.localPosition = new Vector3(-20f, 0f, -20f);
        child646.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child646.SetActive(true);
        StripComponents(child646);
        var child647 = Object.Instantiate(baseCorridorXShapeAdjustableSupport, obj.transform);
        child647.transform.localPosition = new Vector3(0f, 0f, -20f);
        child647.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child647.SetActive(true);
        StripComponents(child647);
        var child648 = Object.Instantiate(baseCorridorXShapeAdjustableSupport, obj.transform);
        child648.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child648.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child648.SetActive(true);
        StripComponents(child648);
        var child649 = Object.Instantiate(baseCorridorXShapeAdjustableSupport, obj.transform);
        child649.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child649.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child649.SetActive(true);
        StripComponents(child649);
        var child650 = Object.Instantiate(baseCorridorCoverXShapeTopExtOpened, obj.transform);
        child650.transform.localPosition = new Vector3(0f, 0f, -20f);
        child650.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child650.SetActive(true);
        StripComponents(child650);
        var child651 = Object.Instantiate(baseCorridorCoverXShapeTopExtOpened, obj.transform);
        child651.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child651.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child651.SetActive(true);
        StripComponents(child651);
        var child652 = Object.Instantiate(baseCorridorCoverXShapeTopIntOpened, obj.transform);
        child652.transform.localPosition = new Vector3(0f, 0f, -20f);
        child652.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child652.SetActive(true);
        StripComponents(child652);
        var child653 = Object.Instantiate(baseCorridorCoverXShapeTopIntOpened, obj.transform);
        child653.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child653.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child653.SetActive(true);
        StripComponents(child653);
        var child654 = Object.Instantiate(baseCorridorCoverTShapeBottomExtOpened, obj.transform);
        child654.transform.localPosition = new Vector3(0f, 3.5f, -20f);
        child654.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child654.SetActive(true);
        StripComponents(child654);
        var child655 = Object.Instantiate(baseCorridorCoverTShapeBottomIntOpened, obj.transform);
        child655.transform.localPosition = new Vector3(0f, 3.5f, -20f);
        child655.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child655.SetActive(true);
        StripComponents(child655);
        var child656 = Object.Instantiate(baseCorridorWindow, obj.transform);
        child656.transform.localPosition = new Vector3(-10f, 0f, -15f);
        child656.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child656.SetActive(true);
        StripComponents(child656);
        var child657 = Object.Instantiate(baseCorridorWindow, obj.transform);
        child657.transform.localPosition = new Vector3(-10f, 0f, -10f);
        child657.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child657.SetActive(true);
        StripComponents(child657);
        var child658 = Object.Instantiate(baseCorridorWindow, obj.transform);
        child658.transform.localPosition = new Vector3(10f, 0f, -5f);
        child658.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child658.SetActive(true);
        StripComponents(child658);
        var child659 = Object.Instantiate(baseRoom, obj.transform);
        child659.transform.localPosition = new Vector3(0f, 0f, -10f);
        child659.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child659.SetActive(true);
        StripComponents(child659);
        var child660 = Object.Instantiate(baseRoom, obj.transform);
        child660.transform.localPosition = new Vector3(0f, 3.5f, -10f);
        child660.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child660.SetActive(true);
        StripComponents(child660);
        var child661 = Object.Instantiate(baseRoomAdjustableSupport, obj.transform);
        child661.transform.localPosition = new Vector3(0f, 0f, -10f);
        child661.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child661.SetActive(true);
        StripComponents(child661);
        var child662 = Object.Instantiate(baseRoomExteriorBottom, obj.transform);
        child662.transform.localPosition = new Vector3(0f, 0f, -10f);
        child662.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child662.SetActive(true);
        StripComponents(child662);
        var child663 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child663.transform.localPosition = new Vector3(5f, 0f, -10f);
        child663.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child663.SetActive(true);
        StripComponents(child663);
        var child664 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child664.transform.localPosition = new Vector3(0f, 0f, -15f);
        child664.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child664.SetActive(true);
        StripComponents(child664);
        var child665 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child665.transform.localPosition = new Vector3(-5f, 0f, -10f);
        child665.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child665.SetActive(true);
        StripComponents(child665);
        var child666 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child666.transform.localPosition = new Vector3(0f, 0f, -5f);
        child666.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child666.SetActive(true);
        StripComponents(child666);
        var child667 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child667.transform.localPosition = new Vector3(5f, 3.5f, -10f);
        child667.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child667.SetActive(true);
        StripComponents(child667);
        var child668 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child668.transform.localPosition = new Vector3(-5f, 3.5f, -10f);
        child668.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child668.SetActive(true);
        StripComponents(child668);
        var child669 = Object.Instantiate(baseRoomCorridorConnector, obj.transform);
        child669.transform.localPosition = new Vector3(0f, 3.5f, -5f);
        child669.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child669.SetActive(true);
        StripComponents(child669);
        var child670 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child670.transform.localPosition = new Vector3(3.535534f, 0f, -13.53553f);
        child670.transform.localRotation = new Quaternion(0f, 0.3826835f, 0f, 0.9238795f);
        child670.SetActive(true);
        StripComponents(child670);
        var child671 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child671.transform.localPosition = new Vector3(-3.535534f, 0f, -13.53553f);
        child671.transform.localRotation = new Quaternion(0f, 0.9238796f, 0f, 0.3826835f);
        child671.SetActive(true);
        StripComponents(child671);
        var child672 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child672.transform.localPosition = new Vector3(-3.535534f, 0f, -6.464466f);
        child672.transform.localRotation = new Quaternion(0f, 0.9238796f, 0f, -0.3826834f);
        child672.SetActive(true);
        StripComponents(child672);
        var child673 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child673.transform.localPosition = new Vector3(3.535534f, 0f, -6.464466f);
        child673.transform.localRotation = new Quaternion(0f, 0.3826835f, 0f, -0.9238795f);
        child673.SetActive(true);
        StripComponents(child673);
        var child674 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child674.transform.localPosition = new Vector3(3.535534f, 3.5f, -13.53553f);
        child674.transform.localRotation = new Quaternion(0f, 0.3826835f, 0f, 0.9238795f);
        child674.SetActive(true);
        StripComponents(child674);
        var child675 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child675.transform.localPosition = new Vector3(-3.535534f, 3.5f, -13.53553f);
        child675.transform.localRotation = new Quaternion(0f, 0.9238796f, 0f, 0.3826835f);
        child675.SetActive(true);
        StripComponents(child675);
        var child676 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child676.transform.localPosition = new Vector3(-3.535534f, 3.5f, -6.464466f);
        child676.transform.localRotation = new Quaternion(0f, 0.9238796f, 0f, -0.3826834f);
        child676.SetActive(true);
        StripComponents(child676);
        var child677 = Object.Instantiate(baseRoomCoverSideVariant, obj.transform);
        child677.transform.localPosition = new Vector3(3.535534f, 3.5f, -6.464466f);
        child677.transform.localRotation = new Quaternion(0f, 0.3826835f, 0f, -0.9238795f);
        child677.SetActive(true);
        StripComponents(child677);
        var child678 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child678.transform.localPosition = new Vector3(0f, 0f, -13.423f);
        child678.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child678.SetActive(true);
        StripComponents(child678);
        var child679 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child679.transform.localPosition = new Vector3(-3.423f, 0f, -10f);
        child679.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child679.SetActive(true);
        StripComponents(child679);
        var child680 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child680.transform.localPosition = new Vector3(3.423f, 0f, -10f);
        child680.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child680.SetActive(true);
        StripComponents(child680);
        var child681 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child681.transform.localPosition = new Vector3(0f, 0f, -6.577f);
        child681.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child681.SetActive(true);
        StripComponents(child681);
        var child682 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child682.transform.localPosition = new Vector3(0f, 3.5f, -13.423f);
        child682.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child682.SetActive(true);
        StripComponents(child682);
        var child683 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child683.transform.localPosition = new Vector3(-3.423f, 3.5f, -10f);
        child683.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child683.SetActive(true);
        StripComponents(child683);
        var child684 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child684.transform.localPosition = new Vector3(3.423f, 3.5f, -10f);
        child684.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child684.SetActive(true);
        StripComponents(child684);
        var child685 = Object.Instantiate(baseRoomCoverBottom, obj.transform);
        child685.transform.localPosition = new Vector3(0f, 3.5f, -6.577f);
        child685.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child685.SetActive(true);
        StripComponents(child685);
        var child686 = Object.Instantiate(baseRoomInteriorBottom, obj.transform);
        child686.transform.localPosition = new Vector3(0f, 0f, -10f);
        child686.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child686.SetActive(true);
        StripComponents(child686);
        var child687 = Object.Instantiate(baseWaterParkBottom, obj.transform);
        child687.transform.localPosition = new Vector3(0f, 0f, -10f);
        child687.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child687.SetActive(true);
        StripComponents(child687);
        var child688 = Object.Instantiate(baseWaterParkCeilingGlass, obj.transform);
        child688.transform.localPosition = new Vector3(0f, 0f, -10f);
        child688.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child688.SetActive(true);
        StripComponents(child688);
        var child689 = Object.Instantiate(baseWaterParkFloorBottom, obj.transform);
        child689.transform.localPosition = new Vector3(0f, 0f, -10f);
        child689.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child689.SetActive(true);
        StripComponents(child689);
        var child690 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child690.transform.localPosition = new Vector3(0f, 0f, -13.423f);
        child690.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child690.SetActive(true);
        StripComponents(child690);
        var child691 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child691.transform.localPosition = new Vector3(-3.423f, 0f, -10f);
        child691.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child691.SetActive(true);
        StripComponents(child691);
        var child692 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child692.transform.localPosition = new Vector3(3.423f, 0f, -10f);
        child692.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child692.SetActive(true);
        StripComponents(child692);
        var child693 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child693.transform.localPosition = new Vector3(0f, 0f, -6.577f);
        child693.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child693.SetActive(true);
        StripComponents(child693);
        var child694 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child694.transform.localPosition = new Vector3(0f, 3.5f, -13.423f);
        child694.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child694.SetActive(true);
        StripComponents(child694);
        var child695 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child695.transform.localPosition = new Vector3(-3.423f, 3.5f, -10f);
        child695.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child695.SetActive(true);
        StripComponents(child695);
        var child696 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child696.transform.localPosition = new Vector3(0f, 3.5f, -10f);
        child696.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child696.SetActive(true);
        StripComponents(child696);
        var child697 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child697.transform.localPosition = new Vector3(3.423f, 3.5f, -10f);
        child697.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, -0.7071068f);
        child697.SetActive(true);
        StripComponents(child697);
        var child698 = Object.Instantiate(baseRoomCoverTop, obj.transform);
        child698.transform.localPosition = new Vector3(0f, 3.5f, -6.577f);
        child698.transform.localRotation = new Quaternion(0f, 1f, 0f, -4.371139E-08f);
        child698.SetActive(true);
        StripComponents(child698);
        var child699 = Object.Instantiate(baseRoomInteriorTopHole, obj.transform);
        child699.transform.localPosition = new Vector3(0f, 0f, -10f);
        child699.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child699.SetActive(true);
        StripComponents(child699);
        var child700 = Object.Instantiate(baseWaterParkTop, obj.transform);
        child700.transform.localPosition = new Vector3(0f, 0f, -10f);
        child700.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child700.SetActive(true);
        StripComponents(child700);
        var child701 = Object.Instantiate(baseCorridorCoverTShapeTopExtOpened, obj.transform);
        child701.transform.localPosition = new Vector3(10f, 0f, -15f);
        child701.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child701.SetActive(true);
        StripComponents(child701);
        var child702 = Object.Instantiate(baseCorridorCoverTShapeTopIntOpened, obj.transform);
        child702.transform.localPosition = new Vector3(10f, 0f, -15f);
        child702.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child702.SetActive(true);
        StripComponents(child702);
        var child703 = Object.Instantiate(baseRoomCoverSide, obj.transform);
        child703.transform.localPosition = new Vector3(0f, 3.5f, -15f);
        child703.transform.localRotation = new Quaternion(0f, 0.7071068f, 0f, 0.7071068f);
        child703.SetActive(true);
        StripComponents(child703);
        var child704 = Object.Instantiate(baseRoomInteriorBottomHole, obj.transform);
        child704.transform.localPosition = new Vector3(0f, 3.5f, -10f);
        child704.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child704.SetActive(true);
        StripComponents(child704);
        var child705 = Object.Instantiate(baseRoomExteriorTop, obj.transform);
        child705.transform.localPosition = new Vector3(0f, 3.5f, -10f);
        child705.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child705.SetActive(true);
        StripComponents(child705);
        var child706 = Object.Instantiate(baseRoomInteriorTop, obj.transform);
        child706.transform.localPosition = new Vector3(0f, 3.5f, -10f);
        child706.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child706.SetActive(true);
        StripComponents(child706);
        var child707 = Object.Instantiate(baseCorridorHatch, obj.transform);
        child707.transform.localPosition = new Vector3(0f, 0f, 0f);
        child707.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        child707.SetActive(true);
        StripComponents(child707);
        
        #endregion

        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.VeryFar);
        
        prefab.Set(obj);
    }

    private static void StripComponents(GameObject obj)
    {
        AbandonedBaseUtils.StripComponents(obj, new Color(0.5f, 0.05f, 0.05f), true);
    }
}