using System;
using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Mono.CreatureBehaviour.CrawlingFlesh;
using TheRedPlague.Mono.Util;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Creatures;

public class CrawlingFleshPrefab
{
    public PrefabInfo Info { get; }
    private Func<GameObject> Model { get; }
    private MaterialModifier[] MaterialModifiers { get; }
    private LargeWorldEntity.CellLevel CellLevel { get; }
    private MovementSettings Settings { get; }

    public CrawlingFleshPrefab(PrefabInfo info, Func<GameObject> model, LargeWorldEntity.CellLevel cellLevel,
        MovementSettings settings, params MaterialModifier[] materialModifiers)
    {
        Info = info;
        Model = model;
        CellLevel = cellLevel;
        MaterialModifiers = materialModifiers;
        Settings = settings;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private GameObject GetPrefab()
    {
        var prefab = UWE.Utils.InstantiateDeactivated(Model.Invoke());
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, CellLevel);
        MaterialUtils.ApplySNShaders(prefab, 6, 1, 1, MaterialModifiers);
        var walker = prefab.AddComponent<WalkerBehavior>();
        walker.maxHeightUpdateDelay = 0.2f;
        walker.horizontalMoveSpeed = Settings.HorizontalSpeed;
        walker.maxVerticalMoveSpeed = Settings.VerticalSpeed;
        walker.upwardsNormalFactor = Settings.UpwardsFactor;
        walker.rotateSpeed = Settings.RotateSpeed;
        walker.depth = Settings.Depth;
        var random = prefab.AddComponent<CrawlingFleshMoveRandom>();
        random.interval = Settings.MovementInterval;
        random.radius = Settings.MovementRadius;
        random.walker = walker;
        return prefab;
    }

    public struct MovementSettings
    {
        public float HorizontalSpeed { get; }
        public float VerticalSpeed { get; }
        public float RotateSpeed { get; }
        public float Depth { get; }
        public float UpwardsFactor { get; }
        public float MovementInterval { get; }
        public float MovementRadius { get; }

        public MovementSettings(float horizontalSpeed, float verticalSpeed, float rotateSpeed, float depth,
            float upwardsFactor, float movementInterval, float movementRadius)
        {
            HorizontalSpeed = horizontalSpeed;
            VerticalSpeed = verticalSpeed;
            RotateSpeed = rotateSpeed;
            Depth = depth;
            UpwardsFactor = upwardsFactor;
            MovementInterval = movementInterval;
            MovementRadius = movementRadius;
        }
    }
}