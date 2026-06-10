using System.Collections;
using System.Collections.Generic;
using ECCLibrary;
using ECCLibrary.Data;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.PlagueAltar;
using TheRedPlague.Content.Infection;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public class Meatball : CreatureAsset
{
    [PrefabRegistration]
    private static void AutoRegister()
    {
        new Meatball(PrefabInfo.WithTechType("Meatball")
                .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("MeatballIcon"))).Register();
    }
    
    public Meatball(PrefabInfo prefabInfo) : base(prefabInfo)
    {
        CustomPrefab.SetRecipe(new RecipeData(new Ingredient(RedPlagueSample.Info.TechType, 2)))
            .WithCraftingTime(3.5f)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab(CraftTreeHandler.Paths.FabricatorMachines);
        CustomPrefab.SetPdaGroupCategory(CustomTechCategories.PlagueBiotechGroup, CustomTechCategories.PlagueBiotechCategory);
        CustomPrefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);

        KnownTechHandler.SetAnalysisTechEntry(RedPlagueSample.Info.TechType, new[] { prefabInfo.TechType },
            KnownTechHandler.DefaultUnlockData.BasicUnlockSound,
            AssetBundles.Core.LoadAsset<Sprite>("MeatballPopup"));

        // Meatball pack (for plague altar)
        var meatballPack = new CustomPrefab(PrefabInfo.WithTechType("MeatballPack", true)
            .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("MeatballPackIcon")));
        var meatballPackTemplate = new AssetBundleTemplate(AssetBundles.Core, "MeatballPackPrefab", meatballPack.Info);
        meatballPack.SetGameObject(meatballPackTemplate);
        MaterialUtils.ApplySNShaders(meatballPackTemplate.Prefab);
        PrefabUtils.AddBasicComponents(meatballPackTemplate.Prefab, meatballPack.Info.ClassID, meatballPack.Info.TechType, LargeWorldEntity.CellLevel.Near);
        PrefabUtils.AddVFXFabricating(meatballPackTemplate.Prefab,
            "CraftModel", -0.25f, 0.5f, default, 0.5f);
        meatballPack.SetRecipe(new RecipeData(new Ingredient(RedPlagueSample.Info.TechType, 2),
                new Ingredient(PlagueCatalyst.Info.TechType, 1))
            {
                LinkedItems = new List<TechType> { prefabInfo.TechType, prefabInfo.TechType, prefabInfo.TechType },
                craftAmount = 0
            }).WithCraftingTime(6)
            .WithFabricatorType(PlagueAltar.CraftTree)
            .WithStepsToFabricatorTab(PlagueAltar.ConsumableTab);
        meatballPack.Register();
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(AssetBundles.Core.LoadAsset<GameObject>("MeatballPrefab"),
            BehaviourType.Shark,
            EcoTargetType.Shark, 1000)
        {
            CellLevel = LargeWorldEntity.CellLevel.Near,
            SwimRandomData = null,
            LocomotionData = new LocomotionData(0, 0, 0),
            Mass = 150f,
            AcidImmune = true,
            RespawnData = new RespawnData(false),
            PickupableFishData = new PickupableFishData(TechType.Peeper, "Meatball", "MeatballFirstPerson"),
            LiveMixinData =
            {
                destroyOnDeath = true
            }
        };
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        components.WorldForces.underwaterGravity = 0;
        components.WorldForces.underwaterDrag = 2.34f;
        prefab.EnsureComponent<RedPlagueHost>().mode = RedPlagueHost.Mode.Immune;
        PrefabUtils.AddVFXFabricating(prefab, "Meatball", -0.15f, 0.24f, Vector3.up * 0.1f, 20f);
        yield break;
    }
}