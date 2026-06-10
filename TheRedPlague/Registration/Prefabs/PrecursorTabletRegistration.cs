using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using TheRedPlague.Content.Buildables.AdministratorFabricator;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Content.Items.Story;
using TheRedPlague.Framework.CommonPrefabs;
using UnityEngine;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class PrecursorTabletRegistration
{
    public static PrefabInfo InfectionTabletInfo { get; } = PrefabInfo.WithTechType("InfectionTablet");

    public static PrefabInfo InfectionTabletFragmentInfo { get; } = PrefabInfo.WithTechType("InfectionTabletFragment");

    public static PrefabInfo GoldTabletInfo { get; } = PrefabInfo.WithTechType("GoldTablet");

    [PrefabRegistration]
    private static void Register()
    {
        // Infection tablet
        InfectionTabletInfo
            .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("InfectionTabletIcon"));
        new PrecursorTabletPrefab(InfectionTabletInfo,
            () => AssetBundles.Core.LoadAsset<Texture2D>("InfectionTabletTexture"),
            AssetBundles.Core.LoadAsset<Sprite>("InfectionTabletPopup"), new RecipeData(
                new Ingredient(RedPlagueSample.Info.TechType, 1),
                new Ingredient(TechType.Diamond, 1)),
            c => c.WithFabricatorType(AdminFabricator.AdminCraftTree),
            true).Register();

        new PrecursorTabletFragmentPrefab(InfectionTabletFragmentInfo,
            () => AssetBundles.Core.LoadAsset<Texture2D>("InfectionTabletTexture-Shattered"),
            InfectionTabletInfo.TechType, Color.red).Register();

        // Shrine base tablet

        GoldTabletInfo.WithIcon(AssetBundles.Core.LoadAsset<Sprite>("ShrineBaseTabletIcon"));
        new PrecursorTabletPrefab(GoldTabletInfo,
            () => AssetBundles.Core.LoadAsset<Texture2D>("ShrineBaseTabletTexture"),
            AssetBundles.Core.LoadAsset<Sprite>("ShrineBaseTabletPopup"), new RecipeData(
                new Ingredient(TechType.PrecursorIonCrystal, 1),
                new Ingredient(TechType.Gold, 2)),
            c => c.WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab(CraftTreeHandler.Paths.FabricatorEquipment),
            false).Register();

    }
}