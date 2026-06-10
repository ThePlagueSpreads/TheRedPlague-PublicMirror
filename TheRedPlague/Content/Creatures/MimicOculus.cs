using System.Collections;
using System.Collections.Generic;
using ECCLibrary;
using ECCLibrary.Data;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Content.Infection;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Creatures;

public class MimicOculus : CreatureAsset
{
    public MimicOculus(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }

    protected override void PostRegister()
    {
        CreatureDataUtils.AddCreaturePDAEncyclopediaEntry(this, CustomPdaPaths.PlagueCreationsPath,
            null, null, 3, null, null);
        ZombieManager.RegisterPlagueVariantConversion(TechType.Oculus, TechType);
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => AssetBundles.Creatures.LoadAsset<GameObject>("MimicOculus_Prefab"),
            BehaviourType.Shark, EcoTargetType.Shark, 500)
        {
            CanBeInfected = false,
            LocomotionData = new LocomotionData(),
            StayAtLeashData = new StayAtLeashData(0.4f, 5, 15f),
            SwimRandomData = new SwimRandomData(0.2f, 5f, new Vector3(10, 2, 10)),
            AvoidObstaclesData = new AvoidObstaclesData(0.5f, 5f, false, 5f, 10f),
            AttackLastTargetData = new AttackLastTargetData(0.6f, 5f, 0.5f, 9f),
            AggressiveWhenSeeTargetList = new List<AggressiveWhenSeeTargetData>
            {
                new(EcoTargetType.Shark, 1.1f, 17f, 1)
            },
            AnimateByVelocityData = new AnimateByVelocityData(5f),
            EyeFOV = -0.75f
        };
        CreatureTemplateUtils.SetCreatureDataEssentials(template, LargeWorldEntity.CellLevel.Near, 3.5f);
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddPlagueCreationComponents(prefab);

        var meleeAttack = CreaturePrefabUtils.AddMeleeAttack<MeleeAttack>(prefab, components,
            prefab.transform.Find("BiteTrigger").gameObject, true, 8, 10f, false);
        var biteSound = prefab.AddComponent<FMOD_StudioEventEmitter>();
        biteSound.path = "SmallZombieBite";
        biteSound.minInterval = 2;
        biteSound.startEventOnAwake = false;
        meleeAttack.attackSound = biteSound;

        yield break;
    }

    protected override void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab, 8f, 1.5f);
    }
}