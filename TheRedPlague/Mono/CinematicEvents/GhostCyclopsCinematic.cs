using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheRedPlague.Mono.CinematicEvents;

public class GhostCyclopsCinematic : MonoBehaviour
{
    private static GhostCyclopsCinematic _current;

    private bool _loaded;
    private PathData _pathData;
    private GameObject _activeSubmarine;

    private static readonly Dictionary<Path, PathData> Paths = new()
    {
        { Path.LostRiverBase, new PathData(true,
            new Vector3(-229, -715, 422), new Vector3(-147, -794, 250)) },
        { Path.LostRiverBaseCorridor, new PathData(false,
            new Vector3(-570, -774, 231), new Vector3(-461, -834, 326)) },
        { Path.LostRiverAncientSkeleton, new PathData(false,
            new Vector3(-948, -692, -543), new Vector3(-790, -681, -635)) },
        { Path.LavaZoneEntrance1, new PathData(false,
            new Vector3(312, -803, 752), new Vector3(284, -942, 645)) }
    };

    public enum Path
    {
        LostRiverBase,
        LostRiverBaseCorridor,
        LostRiverAncientSkeleton,
        // currently unused
        LavaZoneEntrance1
    }

    public static void StartCinematic(PathData pathData)
    {
        if (_current != null)
        {
            Plugin.Logger.LogWarning("GhostCyclopsCinematic is already active!");
            return;
        }

        var cinematicObject = new GameObject("GhostCyclopsCinematic");
        _current = cinematicObject.AddComponent<GhostCyclopsCinematic>();
        _current._pathData = pathData;
        _current.StartCoroutine(_current.CinematicCoroutine());
    }

    public static void StartCinematic(Path path)
    {
        StartCinematic(Paths[path]);
    }

    private IEnumerator CinematicCoroutine()
    {
        _loaded = false;

        yield return new WaitUntil(() => LightmappedPrefabs.main);

        LightmappedPrefabs.main.RequestScenePrefab("Cyclops", OnSubPrefabLoaded);

        yield return new WaitUntil(() => _loaded);

        _activeSubmarine.transform.position = _pathData.StartLocation;
        _activeSubmarine.transform.rotation = Quaternion.LookRotation(
            -(new Vector3(_pathData.EndLocation.x, 0, _pathData.EndLocation.z) -
             new Vector3(_pathData.StartLocation.x, 0, _pathData.StartLocation.z)).normalized);

        var motor = _activeSubmarine.AddComponent<GhostCyclopsMotor>();
        motor.screw = _activeSubmarine.GetComponentInChildren<CyclopsScrew>();
        motor.StartMovement(_pathData.StartLocation, _pathData.EndLocation);
        
        yield return new WaitForSeconds(motor.GetApproximateDuration() + 0.1f);
        Destroy(gameObject);
    }

    private void OnSubPrefabLoaded(GameObject subPrefab)
    {
        _activeSubmarine = Instantiate(subPrefab.transform
            .Find("CyclopsMeshStatic/" + (_pathData.Damaged ? "damaged" : "undamaged")).gameObject);
        _activeSubmarine.SetActive(true);
        if (!_pathData.Damaged)
        {
            _activeSubmarine.transform.Find("cyclops_LOD0").gameObject.SetActive(false);
            _activeSubmarine.transform.Find("Cyclops_Screw/Cyclops_submarine_exterior_engine_LOD3").gameObject
                .SetActive(false);
        }
        else
        {
            _activeSubmarine.transform.Find("CyclopsDamagedCollision").gameObject.SetActive(false);
        }

        var animated = Instantiate(subPrefab.transform.Find("CyclopsMeshAnimated").gameObject,
            _activeSubmarine.transform);
        animated.transform.localPosition = new Vector3(-74.8f, -0.3f, 29);

        var launchBay = Instantiate(subPrefab.transform.Find("LaunchBayBuilt").gameObject,
            _activeSubmarine.transform);
        launchBay.transform.localPosition = new Vector3(-1.091f, -1.417f, 6.810f);

        _activeSubmarine.name = "GhostCyclops";

        var handTargets = animated.GetComponentsInChildren<HandTarget>();
        foreach (var handTarget in handTargets)
        {
            DestroyImmediate(handTarget);
        }

        _loaded = true;
    }

    private void OnDestroy()
    {
        Destroy(_activeSubmarine);
    }

    public class PathData
    {
        public bool Damaged { get; }
        public Vector3 StartLocation { get; }
        public Vector3 EndLocation { get; }

        public PathData(bool damaged, Vector3 startLocation, Vector3 endLocation)
        {
            Damaged = damaged;
            StartLocation = startLocation;
            EndLocation = endLocation;
        }
    }
}