using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.PlagueAltar;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Equipment.BoneRegurgitator;

[PrefabClass]
public static class BoneCannonPrefab
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("BoneCannon")
        .WithSizeInInventory(new Vector2int(2, 2))
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("DrifterCannonIcon"));

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefabAsync);
        prefab.SetEquipment(EquipmentType.Hand);
        prefab.SetRecipe(new RecipeData(new Ingredient(PlagueIngot.Info.TechType, 3),
                new Ingredient(AmalgamatedBone.Info.TechType, 1),
                new Ingredient(TechType.Benzene, 1)))
            .WithCraftingTime(10)
            .WithFabricatorType(PlagueAltar.CraftTree)
            .WithStepsToFabricatorTab(PlagueAltar.EquipmentTab);
        prefab.SetPdaGroupCategory(CustomTechCategories.PlagueBiotechGroup, CustomTechCategories.PlagueBiotechCategory);
        prefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        prefab.Register();
    }

    private static IEnumerator GetPrefabAsync(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("DrifterCannonPrefab"));
        obj.SetActive(false);
        var rb = obj.EnsureComponent<Rigidbody>();
        rb.mass = 50;
        rb.useGravity = false;
        var wf = obj.EnsureComponent<WorldForces>();
        wf.useRigidbody = rb;
        wf.aboveWaterDrag = 0.15f;
        wf.underwaterDrag = 0.3f;
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj, 6f, 2f, 0.5f);
        var fpModel = obj.AddComponent<FPModel>();
        fpModel.propModel = obj.transform.Find("DrifterCannonAnimated").gameObject;
        fpModel.viewModel = obj.transform.Find("DrifterCannonAnimated_ViewModel").gameObject;
        var tool = obj.AddComponent<DrifterCannonTool>();
        tool.pickupable = obj.EnsureComponent<Pickupable>();
        tool.mainCollider = obj.GetComponent<Collider>();
        tool.animator = fpModel.viewModel.GetComponent<Animator>();
        tool.hasAnimations = true;
        tool.socket = PlayerTool.Socket.RightHand;
        tool.ikAimLeftArm = false;
        tool.ikAimRightArm = true;

        var projectile = obj.transform.Find("DrifterProjectilePrefab").gameObject;
        var projectileWf = projectile.AddComponent<WorldForces>();
        projectileWf.useRigidbody = projectile.GetComponent<Rigidbody>();

        tool.projectilePrefab = projectile;
        tool.projectileSpawnPoint = fpModel.viewModel.transform.Find("ProjectileSpawnPoint");
        tool.glowRenderer = fpModel.viewModel.transform.Find("DrifterCannon").gameObject.GetComponent<Renderer>();

        PrefabUtils.AddVFXFabricating(obj, "DrifterCannonAnimated", -0.3f, 0.25f, Vector3.down * 0.2f, 0.5f);

        prefab.Set(obj);
        yield break;
    }
}