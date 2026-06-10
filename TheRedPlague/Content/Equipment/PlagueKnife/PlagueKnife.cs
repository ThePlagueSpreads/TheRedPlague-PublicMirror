using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using TheRedPlague.Content.Buildables.AdministratorFabricator;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.CommonPrefabs;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Equipment.PlagueKnife;

[PrefabClass]
public static class PlagueKnife
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PlagueKnife")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("PlagueKnifeIcon"));

    [PrefabRegistration]
    private static void Register()
    {
        var plagueKnife = new CustomPrefab(Info);
        var plagueKnifeTemplate = new CloneTemplate(Info, TechType.Knife);
        plagueKnifeTemplate.ModifyPrefab += ModifyPrefab;
        plagueKnife.SetGameObject(plagueKnifeTemplate);
        plagueKnife.SetEquipment(EquipmentType.Hand);
        plagueKnife.SetRecipe(new RecipeData(new Ingredient(TechType.Knife, 1),
                new Ingredient(AmalgamatedBone.Info.TechType, 3)))
            .WithCraftingTime(8)
            .WithFabricatorType(AdminFabricator.AdminCraftTree);
        plagueKnife.SetPdaGroupCategoryAfter(TechGroup.Personal, TechCategory.Tools, TechType.Knife);
        plagueKnife.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        plagueKnife.Register();
        
        new DataboxPrefab("PlagueKnifeDatabox", Info.TechType).Register();
    }

    private static void ModifyPrefab(GameObject prefab)
    {
        var renderer = prefab.GetComponentInChildren<Renderer>();
        Object.DestroyImmediate(renderer.gameObject.GetComponent<VFXFabricating>());
        renderer.enabled = false;

        var newModel = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("PlagueKnifeModel"), prefab.transform);
        newModel.transform.localPosition = new Vector3(0.03f, 0.05f, 0.001f);
        newModel.transform.localEulerAngles = new Vector3(85, 180, 180);
        newModel.transform.localScale = Vector3.one * 0.05f;
        MaterialUtils.ApplySNShaders(newModel, 7f, 2f, 10f);
        
        var skyApplier = prefab.GetComponentInChildren<SkyApplier>();
        skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>();

        var oldKnifeComponent = prefab.GetComponent<Knife>();

        var newKnifeComponent = prefab.AddComponent<PlagueKnifeTool>();
        newKnifeComponent.attackSound = oldKnifeComponent.attackSound;
        newKnifeComponent.underwaterMissSound = oldKnifeComponent.underwaterMissSound;
        var warperSwipeSound = AudioUtils.GetFmodAsset("event:/creature/warper/swipe");
        newKnifeComponent.attackSoundWarper = warperSwipeSound;
        newKnifeComponent.underwaterMissSoundWarper = warperSwipeSound;
        newKnifeComponent.surfaceMissSound = oldKnifeComponent.surfaceMissSound;
        newKnifeComponent.damageType = oldKnifeComponent.damageType;
        newKnifeComponent.damage = 20;
        newKnifeComponent.plagueDamage = 30;
        newKnifeComponent.attackDist = 4;
        newKnifeComponent.vfxEventType = VFXEventTypes.knife;
        newKnifeComponent.mainCollider = oldKnifeComponent.mainCollider;
        newKnifeComponent.drawSound = oldKnifeComponent.drawSound;
        newKnifeComponent.firstUseSound = oldKnifeComponent.firstUseSound;
        newKnifeComponent.hitBleederSound = oldKnifeComponent.hitBleederSound;
        newKnifeComponent.bleederDamage = 50;
        newKnifeComponent.socket = oldKnifeComponent.socket;
        newKnifeComponent.ikAimRightArm = true;
        newKnifeComponent.drawTime = 0;
        newKnifeComponent.holsterTime = 0.1f;
        newKnifeComponent.pickupable = oldKnifeComponent.pickupable;
        newKnifeComponent.hasFirstUseAnimation = true;
        newKnifeComponent.hasBashAnimation = true;
        Object.DestroyImmediate(oldKnifeComponent);
        
        PrefabUtils.AddVFXFabricating(prefab, newModel.gameObject.name, -0.05f, 0.05f, default, 0.03f, Vector3.up * 90);
    }
}