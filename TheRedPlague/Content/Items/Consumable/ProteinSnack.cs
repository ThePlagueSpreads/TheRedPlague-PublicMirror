using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.PlagueAltar;
using TheRedPlague.Content.Environment.Plants;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Content.PlayerInfection;
using TheRedPlague.Framework.Gadgets;
using TheRedPlague.Framework.Inventory;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class ProteinSnack
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("ProteinSnack", true);
    private static FMODAsset EatSound { get; } = AudioUtils.GetFmodAsset("event:/player/eat");
    private static FMODAsset VomitSound { get; } = AudioUtils.GetFmodAsset("event:/player/Puke");
    private static FMODAsset VomitSoundUnderwater { get; } = AudioUtils.GetFmodAsset("event:/player/Puke_underwater");

    private const float FoodValue = 52;
    private const float WaterValue = -2;

    [PrefabRegistration]
    private static void Register()
    {
        var proteinSnackObject = AssetBundles.Creatures.LoadAsset<GameObject>("ProteinSnackPrefab");
        PrefabUtils.AddBasicComponents(proteinSnackObject, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(proteinSnackObject);
        var proteinSnackEatable = proteinSnackObject.EnsureComponent<Eatable>();
        proteinSnackEatable.foodValue = FoodValue;
        proteinSnackEatable.waterValue = WaterValue;
        proteinSnackObject.EnsureComponent<Pickupable>();
        PrefabUtils.AddWorldForces(proteinSnackObject, 1.3f, 0.8f);
        PrefabUtils.AddVFXFabricating(proteinSnackObject, "Model", -0.2f, 0.1f, new Vector3(0, 0.024f, 0), 0.1f,
            new Vector3(0, 0, 90));
        proteinSnackObject.AddComponent<UsableItem>().SetOnUseAction(Info.ClassID,
            go =>
            {
                PlagueDamageStat.main.TakeInfectionDamage(40);
                FMODUWE.PlayOneShot(EatSound, Player.main.transform.position);
                FMODUWE.PlayOneShot(Player.main.IsUnderwaterForSwimming() ? VomitSoundUnderwater : VomitSound, Player.main.transform.position);
                Player.main.GetComponent<Survival>().Eat(go);
            });
        Info.WithIcon(AssetBundles.Creatures.LoadAsset<Sprite>("ProteinSnackIcon"));
        var proteinSnackPrefab = new CustomPrefab(Info);
        proteinSnackPrefab.SetGameObject(proteinSnackObject);
        proteinSnackPrefab.SetRecipe(new RecipeData(new Ingredient(PlagueIngot.Info.TechType, 1),
                new Ingredient(MeatShroom.Info.TechType, 1)))
            .WithFabricatorType(PlagueAltar.CraftTree)
            .WithStepsToFabricatorTab(PlagueAltar.ConsumableTab);
        proteinSnackPrefab.SetPdaGroupCategory(TechGroup.Survival, TechCategory.CookedFood);
        proteinSnackPrefab.SetUnlock(MeatShroom.Info.TechType);
        proteinSnackPrefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        proteinSnackPrefab.Register();
    }
}