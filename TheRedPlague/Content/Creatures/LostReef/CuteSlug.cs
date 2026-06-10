using System;
using System.Collections;
using ECCLibrary;
using ECCLibrary.Data;
using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Framework.CreatureBehaviours;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.LostReef;

public class CuteSlug : CreatureAsset
{
    private const float SwimVelocity = 0.4f;

    public CuteSlug(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }
    
    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() =>
            {
                var prefab = AssetBundles.Creatures.LoadAsset<GameObject>("CuteSlug.prefab");
                prefab.SetActive(false);
                // Required to prevent multiple component errors from components requiring SwimBehaviour or a derived component
                prefab.EnsureComponent<WalkBehaviour>();
                return prefab;
            },
            BehaviourType.SmallFish, EcoTargetType.SmallFish, 100)
        {
            CanBeInfected = false,
            LocomotionData = new LocomotionData(5f, 0.5f, 2f, 0.3f, false, true, true),
            StayAtLeashData = new StayAtLeashData(0.48f, SwimVelocity, 20),
            SwimRandomData = new SwimRandomData(0.4f, SwimVelocity, new Vector3(10, 4, 10)),
            AvoidObstaclesData = new AvoidObstaclesData(0.45f, SwimVelocity, false, 5, 5),
            AnimateByVelocityData = new AnimateByVelocityData(0.1f),
            SurfaceType = VFXSurfaceTypes.vegetation,
            PhysicMaterial = new PhysicMaterial(),
            PickupableFishData = new PickupableFishData(TechType.Hoverfish, "WorldModel", "ViewModel"),
            SwimBehaviourData = null
        };
        CreatureTemplateUtils.SetCreatureDataEssentials(template, LargeWorldEntity.CellLevel.Near, 10);
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddLostReefCreatureComponents(prefab);
        
        var surfaceTracker = prefab.EnsureComponent<OnSurfaceTracker>();
        surfaceTracker.minSurfaceCos = 0f;
        surfaceTracker.maxSurfaceAngle = 180f;
        var surfaceMovement = prefab.EnsureComponent<OnSurfaceMovement>();
        surfaceMovement.locomotion = components.Locomotion;
        surfaceMovement.onSurfaceTracker = surfaceTracker;

        var wb = prefab.EnsureComponent<WalkBehaviour>();
        wb.allowSwimming = true;
        wb.onSurfaceTracker = surfaceTracker;
        wb.onSurfaceMovement = surfaceMovement;
        wb.splineFollowing = components.SplineFollowing;
        wb.turnSpeed = 0.8f;
        
        var moveOnSurface = prefab.EnsureComponent<MoveOnSurface>();
        moveOnSurface.walkBehaviour = wb;
        moveOnSurface.moveVelocity = SwimVelocity;
        moveOnSurface.moveRadius = 8f;
        moveOnSurface.creature = components.Creature;
        moveOnSurface.onSurfaceTracker = surfaceTracker;
        moveOnSurface.evaluatePriority = 0.6f;

        var avoidEdges = prefab.EnsureComponent<CrawlerAvoidEdges>();
        avoidEdges.onSurfaceTracker = surfaceTracker;
        avoidEdges.walkBehaviour = wb;
        avoidEdges.rgbody = components.Rigidbody;
        avoidEdges.moveVelocity = SwimVelocity;

        var descend = prefab.EnsureComponent<DescendToGround>();
        descend.evaluatePriority = 0.5f;
        descend.checkGroundLoaded = false;
        descend.actionInterval = 1f;
        descend.forceValue = 3;
        var constantForce = prefab.AddComponent<ConstantForce>();
        descend.descendForce = constantForce;
        descend.maxDuration = 7;
        descend.onGroundAction = moveOnSurface;
        descend.onGroundTracker = surfaceTracker;

        components.WorldForces.handleDrag = false;
        components.WorldForces.handleGravity = false;

        var gravity = prefab.AddComponent<WalkerGravity>();
        gravity.onSurface = surfaceTracker;
        gravity.rigidbody = components.Rigidbody;
        gravity.liveMixin = components.LiveMixin;

        components.Rigidbody.drag = 1.9f;
        components.Rigidbody.angularDrag = 0.05f;
        components.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        
        yield break;
    }

    protected override void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab, 5, 4f, 1f,
            new DoubleSidedModifier(MaterialUtils.MaterialType.Transparent),
            new FresnelModifier(0.5f));
    }
}