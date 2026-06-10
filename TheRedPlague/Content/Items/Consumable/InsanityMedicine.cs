using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Content.PlayerInfection;
using TheRedPlague.Framework.Inventory;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class InsanityMedicine
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("InsanityMedicine")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("InsanityMedicineIcon"));

    private static readonly FMODAsset UseSound = AudioUtils.GetFmodAsset("event:/player/drink_stillsuit");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.SetPdaGroupCategoryBefore(TechGroup.Personal, TechCategory.Equipment, TechType.FirstAidKit);
        prefab.SetRecipe(new RecipeData(
                new Ingredient(AmalgamatedBone.Info.TechType, 2))
            {
                craftAmount = 2
            })
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab(CraftTreeHandler.Paths.FabricatorEquipment);
        prefab.Register();
        KnownTechHandler.SetAnalysisTechEntry(Info.TechType, new[] { Info.TechType },
            KnownTechHandler.DefaultUnlockData.BasicUnlockSound,
            AssetBundles.Core.LoadAsset<Sprite>("InsanityMedicinePopup"));
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("AntipsychoticMedicinePrefab"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj, 6f);
        PrefabUtils.AddWorldForces(obj, 1f, isKinematic: true);
        PrefabUtils.AddVFXFabricating(obj, "AntipsychoticMedicine", -0.05f, 0.2f,
            new Vector3(0, 0.04f, 0), 5, new Vector3(-90, 0, 0));
        obj.AddComponent<UsableItem>().SetOnUseAction(Info.ClassID, OnUse);
        obj.AddComponent<Pickupable>();

        return obj;
    }

    private static void OnUse(GameObject obj)
    {
        PlagueDamageStat.main.Charge(10);
        Utils.PlayFMODAsset(UseSound, Player.main.transform.position);
    }
}