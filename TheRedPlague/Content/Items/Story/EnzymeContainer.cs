using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Items.Story;

[PrefabClass]
public static class EnzymeContainer
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ConcentratedEnzymeContainer")
        .WithIcon(SpriteManager.Get(TechType.LabContainer3))
        .WithSizeInInventory(new Vector2int(2, 2));

    [PrefabRegistration]
    private static void Register()
    {
        var enzymeContainer = new CustomPrefab(Info);
        var enzymeContainerTemplate = new CloneTemplate(Info, TechType.LabContainer3);
        enzymeContainerTemplate.ModifyPrefab += (go) =>
        {
            var renderer = go.transform.Find("biodome_lab_containers_tube_01/biodome_lab_containers_tube_01_glass")
                .GetComponent<Renderer>();
            var material = renderer.material;
            material.color = new Color(1, 1, 1, 0.19f);
            material.SetColor(ShaderPropertyID._SpecColor, new Color(20, 12, 0, 1));
            material.SetFloat("_Shininess", 3);
            if (go.GetComponent<VFXFabricating>() == null)
            {
                PrefabUtils.AddVFXFabricating(go, null, -0.05f, 0.3f);
            }
        };
        enzymeContainer.SetGameObject(enzymeContainerTemplate);
        enzymeContainer.SetRecipe(new RecipeData(new Ingredient(Info.TechType, 16),
                new Ingredient(TechType.Titanium, 2), new Ingredient(TechType.Glass, 1)))
            .WithStepsToFabricatorTab("Resources", "AdvancedMaterials").WithCraftingTime(6)
            .WithFabricatorType(CraftTree.Type.Fabricator);
        enzymeContainer.AddGadget(new ScanningGadget(enzymeContainer, TechType.None))
            .WithPdaGroupCategory(TechGroup.Resources, TechCategory.AdvancedMaterials)
            .WithAnalysisTech(AssetBundles.Core.LoadAsset<Sprite>("InfectedEnzymeStorageContainer_Popup"),
                KnownTechHandler.DefaultUnlockData.BlueprintUnlockSound,
                KnownTechHandler.DefaultUnlockData.BlueprintUnlockMessage);
        enzymeContainer.Register();
    }
}