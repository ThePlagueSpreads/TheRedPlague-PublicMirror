using System.Collections;
using ECCLibrary;
using ECCLibrary.Data;
using ECCLibrary.Mono;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Data;
using TheRedPlague.Mono.CreatureBehaviour;
using TheRedPlague.Mono.InfectionLogic;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Creatures;

public class Observer : CreatureAsset
{
    public Observer(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }

    protected override void PostRegister()
    {
        CreatureDataUtils.AddCreaturePDAEncyclopediaEntry(this, CustomPdaPaths.PlagueCreationsPath, null, null, 4, null, null);
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => Plugin.CreaturesBundle.LoadAsset<GameObject>("ObserverPrefab"),
            BehaviourType.Whale, EcoTargetType.Whale, 400)
        {
            CanBeInfected = false,
            LocomotionData = new LocomotionData(10, 0.4f, 3f, 0.3f),
            SwimRandomData = new SwimRandomData(0.2f, 4, new Vector3(14f, 5f, 14f), 8f),
            AvoidObstaclesData = new AvoidObstaclesData(0.67f, 4, false, 6f, 7f),
            AnimateByVelocityData = new AnimateByVelocityData(6f),
            FleeWhenScaredData = new FleeWhenScaredData(0.7f, 14f, 0.5f, 15, 0.02f, 10f, 0.1f),
            ScareableData = new ScareableData(EcoTargetType.Shark, 5f, 35f)
        };
        CreatureTemplateUtils.SetCreatureDataEssentials(template, LargeWorldEntity.CellLevel.Far, 140f);
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddPlagueCreationComponents(prefab);
        
        components.Scareable.daynightRangeMultiplier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));

        prefab.AddComponent<PlayerDistanceTracker>().maxDistance = 200f;
        var stare = prefab.AddComponent<StareAtPlayer>();
        stare.evaluatePriority = 0.6f;
        stare.detectionDistance = 48f;
        
        var stalk = prefab.AddComponent<StalkPlayer>();
        stalk.swimVelocity = 5;
        stalk.minDistance = 40f;
        stalk.distanceFromPlayerToSwimTo = 45f;
        stalk.evaluatePriority = 0.59f;

        var voice = prefab.AddComponent<CreatureVoice>();
        voice.closeIdleSound = AudioUtils.GetFmodAsset("ObserverCloseSounds");
        voice.farIdleSound = AudioUtils.GetFmodAsset("TrpObserverAmbience");
        voice.farThreshold = 30;
        var emitter = prefab.AddComponent<FMOD_CustomEmitter>();
        emitter.followParent = true;
        voice.emitter = emitter;
        voice.minInterval = 50;
        voice.maxInterval = 75;

        var avoidPlayer = prefab.AddComponent<AvoidPlayerWhenSanityIsHigh>();
        avoidPlayer.evaluatePriority = 0.65f;
        avoidPlayer.swimVelocity = 7;
        avoidPlayer.maxInsanityForAvoiding = 12;
        avoidPlayer.maxAvoidanceDistance = 80;
        
        yield break;
    }
}