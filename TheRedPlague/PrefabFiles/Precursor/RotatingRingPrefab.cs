using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Mono.VFX;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Precursor;

public class RotatingRingPrefab
{
    private PrefabInfo Info { get; }
    private string ModelName { get; }
    private RotationAxis Axis { get; }
    private float RotateSpeed { get; }

    public RotatingRingPrefab(string classId, string modelName, RotationAxis axis, float rotateSpeed)
    {
        Info = PrefabInfo.WithTechType(classId);
        ModelName = modelName;
        Axis = axis;
        RotateSpeed = rotateSpeed;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private GameObject GetPrefab()
    {
        var prefab = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>(ModelName));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        prefab.AddComponent<ConstructionObstacle>();
        MaterialUtils.ApplySNShaders(prefab, 8f, 5f);
        var rotation = prefab.AddComponent<ConstantRotation>();
        rotation.target = prefab.transform.GetChild(0);
        rotation.eulerRotation = (Axis == RotationAxis.Parallel ? Vector3.up : Vector3.right) * RotateSpeed;
        rotation.space = Space.Self;
        return prefab;
    }

    public enum RotationAxis
    {
        Perpendicular,
        Parallel
    }
}