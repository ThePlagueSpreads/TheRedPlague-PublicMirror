using System.Collections;
using ECCLibrary;
using ECCLibrary.Data;
using Nautilus.Assets;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Framework.CreatureBehaviours;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.HoverPet;

public class Hippopenomenon : CreatureAsset
{
    private static PingType Ping { get; set; }
    
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Hippopenomenon")
        .WithFolderPath(TrpPrefabFolders.Creatures.Pets);
    
    public Hippopenomenon() : base(Info)
    {
        CreatureDataUtils.AddCreaturePDAEncyclopediaEntry(this, CustomPdaPaths.PlagueCreationsPath,
            null, null, 5, null, null);
    }

    protected override void PostRegister()
    {
        Ping = EnumHandler.AddEntry<PingType>("HoverPetSignal")
            .WithIcon(AssetBundles.Creatures.LoadAsset<Sprite>("HoverPetSignal"));
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => AssetBundles.Creatures.LoadAsset<GameObject>("HoverPetPrefab"),
            BehaviourType.MediumFish, EcoTargetType.None, 10000)
        {
            CanBeInfected = false,
            LocomotionData = new LocomotionData(10, 0.6f, 3f, 0.3f),
            SwimRandomData = new SwimRandomData(0.2f, 4, new Vector3(10, 2, 10)),
            AvoidObstaclesData = new AvoidObstaclesData(0.8f, 4, false, 10f, 7f, 2f, 1f, 3),
            AnimateByVelocityData = new AnimateByVelocityData(2f)
        };
        CreatureTemplateUtils.SetCreatureDataEssentials(template, LargeWorldEntity.CellLevel.Global, 80f);
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddPlagueCreationComponents(prefab);
        
        var followPlayer = prefab.AddComponent<CreatureFollowPlayer>();
        followPlayer.creature = components.Creature;
        followPlayer.distanceToPlayer = 14;

        var warpWhenFar = prefab.AddComponent<WarpToPlayerWhenFar>();
        warpWhenFar.warpDistance = 60f;

        prefab.AddComponent<HoverPetBehavior>();
        prefab.AddComponent<HoverPetSwimToMazeBase>();

        var ping = prefab.AddComponent<PingInstance>();
        ping.pingType = Ping;
        ping.origin = prefab.transform;
        ping.displayPingInManager = false;
        
        yield break;
    }

    protected override void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab, 6f);
    }
}