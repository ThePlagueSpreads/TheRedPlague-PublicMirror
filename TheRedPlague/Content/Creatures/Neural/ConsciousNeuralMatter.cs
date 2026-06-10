using System.Collections;
using ECCLibrary;
using ECCLibrary.Data;
using ECCLibrary.Mono;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Extensions;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.PlagueAltar;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.Gadgets;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.Neural;

public class ConsciousNeuralMatter : CreatureAsset
{
    // Not in the creatures bundle because it shares space with the dormant neural matter
    // And it's technically a resource
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ConsciousNeuralMatter")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("ConsciousPlagueMatterIcon"))
        .WithSizeInInventory(new Vector2int(2, 2))
        .WithFolderPath(TrpPrefabFolders.Creatures.Pets);

    public ConsciousNeuralMatter() : base(Info)
    {
        CustomPrefab.SetPdaGroupCategory(CustomTechCategories.PlagueBiotechGroup,
            CustomTechCategories.PlagueBiotechCategory);
        CustomPrefab.SetRecipe(new RecipeData(new Ingredient(DormantNeuralMatter.Info.TechType, 1),
                new Ingredient(TechType.Battery, 1), new Ingredient(TechType.Diamond, 1)))
            .WithCraftingTime(20)
            .WithFabricatorType(PlagueAltar.CraftTree)
            .WithStepsToFabricatorTab(PlagueAltar.PetsTab);
        CustomPrefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
    }

    protected override void PostRegister()
    {
        KnownTechHandler.SetAnalysisTechEntry(Info.TechType, System.Array.Empty<TechType>(),
            KnownTechHandler.DefaultUnlockData.NewCreatureDiscoveredSound,
            AssetBundles.Core.LoadAsset<Sprite>("ConsciousPlagueMatterPopup"));
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => AssetBundles.Core.LoadAsset<GameObject>("ConsciousNeuralMatter"),
            BehaviourType.Shark, EcoTargetType.MediumFish, 1000)
        {
            SwimRandomData = new SwimRandomData(0.1f, 2f, new Vector3(12, 3, 12)),
            LocomotionData = new LocomotionData(8f, 0.3f, 2f, 0.4f),
            AvoidObstaclesData = new AvoidObstaclesData(0.4f, 2f, false, 7f, 5f),
            PickupableFishData = new PickupableFishData(TechType.MapRoomCamera, "WorldModel", "ViewModel"),
            FleeWhenScaredData = new FleeWhenScaredData(0.3f, 4f),
            RespawnData = new RespawnData(false),
            AnimateByVelocityData = new AnimateByVelocityData(0.5f),
            CanBeInfected = false
        };
        CreatureTemplateUtils.SetCreatureDataEssentials(template, LargeWorldEntity.CellLevel.Medium, 14.2f,
            0, new BehaviourLODData(10f, 30f, 100f), 1800);
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        TrpPrefabUtils.AddPlagueCreationComponents(prefab);
        
        var heldFish = prefab.GetComponent<HeldFish>();
        heldFish.ikAimRightArm = false;

        var gift = prefab.AddComponent<NeuralGifting>();
        gift.giftParent = prefab.transform.SearchChild("HoldingPosition");
        gift.colliders = new[] { prefab.GetComponent<Collider>() };
        gift.animator = components.Animator;
        gift.pickupable = components.Pickupable;
        
        PrefabUtils.AddVFXFabricating(prefab, "WorldModel", -0.5f, 0.6f, Vector3.zero, 0.65f);
        
        yield break;
    }
}