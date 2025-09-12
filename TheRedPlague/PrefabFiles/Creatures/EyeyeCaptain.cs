using System.Collections;
using System.Collections.Generic;
using ECCLibrary;
using ECCLibrary.Data;
using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Data;
using TheRedPlague.Managers;
using TheRedPlague.Mono.CreatureBehaviour.Mimics;
using TheRedPlague.Mono.InfectionLogic;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Creatures;

public class EyeyeCaptain : CreatureAsset
{
    public EyeyeCaptain(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }

    protected override void PostRegister()
    {
        CreatureDataUtils.AddCreaturePDAEncyclopediaEntry(this, CustomPdaPaths.PlagueCreationsPath,
            null, null, 3, null, null);
        ZombieManager.RegisterPlagueVariantConversion(TechType.LavaEyeye, TechType);
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => Plugin.CreaturesBundle.LoadAsset<GameObject>("EyeyeCaptainPrefab"),
            BehaviourType.Shark, EcoTargetType.Shark, 200)
        {
            CanBeInfected = false,
            LocomotionData = new LocomotionData(),
            StayAtLeashData = new StayAtLeashData(0.4f, 4, 27),
            SwimRandomData = new SwimRandomData(0.2f, 4, new Vector3(10, 2, 10)),
            AvoidObstaclesData = new AvoidObstaclesData(0.7f, 4, true, 5, 4f),
            AggressiveWhenSeeTargetList = new List<AggressiveWhenSeeTargetData>
            {
                new(EcoTargetType.Shark, 0.7f, 20, 1)
            },
            AnimateByVelocityData = new AnimateByVelocityData(4)
        };
        CreatureTemplateUtils.SetCreatureDataEssentials(template, LargeWorldEntity.CellLevel.Near, 5f);
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddPlagueCreationComponents(prefab);
        
        var attack = prefab.AddComponent<CaptainEyeyeAttack>();
        attack.swimVelocity = 6;
        attack.swimInterval = 0.6f;
        attack.aggressionThreshold = 0.5f;
        attack.minAttackDuration = 4f;
        attack.maxAttackDuration = 7f;
        attack.pauseInterval = 14;
        attack.rememberTargetTime = 8;
        attack.lastTarget = components.LastTarget;
        attack.minDistanceToTarget = 1;
        attack.maxCastingDistance = 13;
        attack.lookDirectionTransform = prefab.transform;
        attack.attackTypes = new[]
        {
            new RangedAttackLastTarget.RangedAttackType
            {
                ammoPrefab = null,
                ammoVelocity = 0,
                animChargeParameter = null,
                animParameter = null,
                attackChance = 1,
                attackSound = null,
                castDelay = 1,
                chargeTime = 1,
                castProjectileInterval = 15,
                maxProjectiles = 1,
                projectilesSpread = 0
            }
        };
        attack.creature = components.Creature;
        attack.model = prefab.transform.Find("EyeyeCaptain").gameObject;
        var eyeTransform = prefab.transform.Find("EyeyeCaptain/EyeyeCaptainArmature/BodyRoot/Spine/Neck/Eye");
        attack.eyeTransform = eyeTransform;
        attack.ammoSpawnPoint = eyeTransform;
        attack.eyePhysicsMaterial = Plugin.CreaturesBundle.LoadAsset<PhysicMaterial>("BouncyEyeMaterial");
        
        yield break;
    }

    protected override void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab, 6, 3f, 0.5f,
            new DoubleSidedModifier(MaterialUtils.MaterialType.Transparent));
    }
}