using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.Behaviour.Animation;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Precursor;

[PrefabClass]
public class RotatingRingPrefab
{
    private PrefabInfo Info { get; }
    private string ModelName { get; }
    private RotationAxis Axis { get; }
    private float RotateSpeed { get; }
    
    public RotatingRingPrefab(string classId, string modelName, RotationAxis axis, float rotateSpeed)
    {
        Info = PrefabInfo.WithTechType(classId)
            .WithFolderPath(TrpPrefabFolders.Precursor);
        ModelName = modelName;
        Axis = axis;
        RotateSpeed = rotateSpeed;
    }

    [PrefabRegistration]
    private static void RegisterAll()
    {
        // Floating cache rings
        for (var variants = 1; variants <= 2; variants++)
        {
            var modelName = $"B3NTCacheRing{variants}Prefab";

            // 0 = Perpendicular, 1 = Parallel
            for (var axis = 0; axis <= 1; axis++)
            {
                var rotationAxis = (RotatingRingPrefab.RotationAxis)axis;

                new RotatingRingPrefab($"B3NTCacheRing{variants}-{rotationAxis}-Slow", modelName,
                    rotationAxis, 35).Register();
                new RotatingRingPrefab($"B3NTCacheRing{variants}-{rotationAxis}-Fast", modelName,
                    rotationAxis, 60).Register();
            }
        }
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private GameObject GetPrefab()
    {
        var prefab = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>(ModelName));
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