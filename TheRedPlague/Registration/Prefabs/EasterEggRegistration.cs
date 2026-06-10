using System;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;
using TheRedPlague.Content.Items.Placeable;
using UnityEngine;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class EasterEggRegistration
{
    [PrefabRegistration]
    private static void Register()
    {
        var cigarretePrefab = new CustomPrefab(PrefabInfo.WithTechType("500Cigarettes")
            .WithFolderPath(TrpPrefabFolders.EasterEggs));
        var cigTemplate = new AssetBundleTemplate(AssetBundles.Core, "500Cigarettes", cigarretePrefab.Info);
        cigarretePrefab.SetGameObject(cigTemplate);
        PrefabUtils.AddBasicComponents(cigTemplate.Prefab, cigarretePrefab.Info.ClassID, cigarretePrefab.Info.TechType,
            LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(cigTemplate.Prefab);
        cigarretePrefab.Register();

        new Toy("Chrissy", "ChrissyPrefab", "TransFlagIcon")
        {
            IsBigoted = true
        }.Register();
        new Toy("PrecursorSkull", "PrecursorSkull", "PrecursorSkullIcon").Register();
        new Toy("BingBong", "BingBongPrefab", "GenericDecoIcon").Register();
        new Toy("TransDeskFlag", "TransFlagPrefab", "TransFlagIcon").Register();
        new PosterPrefab(PrefabInfo.WithTechType("XenaPoster")
                .WithFolderPath(TrpPrefabFolders.EasterEggs)
                .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("XenaPosterIcon")),
            () => AssetBundles.Core.LoadAsset<Texture2D>("XenaPoster")).Register();

        void RegisterClericalPropRetexture(string classId, string originalPropClassId, Func<Texture2D> texture)
        {
            var prefab = new CustomPrefab(PrefabInfo.WithTechType(classId)
                .WithFolderPath(TrpPrefabFolders.EasterEggs)
                .WithIcon(EssentialAssets.MiscDecoIcon));
            prefab.SetEquipment(EquipmentType.Hand);
            var template = new CloneTemplate(prefab.Info, originalPropClassId)
            {
                ModifyPrefab = obj =>
                {
                    var renderer = obj.GetComponentInChildren<Renderer>();
                    var material = renderer.material;
                    var loadedTexture = texture.Invoke();
                    material.mainTexture = loadedTexture;
                    material.SetTexture(ShaderPropertyID._SpecTex, loadedTexture);
                    obj.AddComponent<Pickupable>();
                    var placeTool = obj.AddComponent<PlaceTool>();
                    placeTool.allowedOnGround = true;
                    placeTool.allowedInBase = true;
                    placeTool.allowedOnConstructable = true;
                    placeTool.mainCollider = obj.GetComponentInChildren<Collider>();
                    placeTool.hasAnimations = false;
                    var viewModel = new GameObject("ViewModel");
                    viewModel.transform.parent = obj.transform;
                    var fpModel = obj.AddComponent<FPModel>();
                    fpModel.propModel = renderer.gameObject;
                    fpModel.viewModel = viewModel;
                    PrefabUtils.AddWorldForces(obj, 1, 0.5f, 0.5f, true);
                }
            };
            prefab.SetGameObject(template);
            prefab.Register();
        }

        RegisterClericalPropRetexture("SockDrawing", "a7519acf-6dec-429e-82ed-bbcf7a616c50",
            () => AssetBundles.Core.LoadAsset<Texture2D>("docking_clerical_trp_variants_1"));

        RegisterClericalPropRetexture("CrumpledMazieDrawing", "32e48451-8e81-428e-9011-baca82e9cd32",
            () => AssetBundles.Core.LoadAsset<Texture2D>("docking_clerical_trp_variants_1"));

        RegisterClericalPropRetexture("CrimxsenCard", "45af7cd6-36a9-4ced-a7b9-2b522022f2c8",
            () => AssetBundles.Core.LoadAsset<Texture2D>("docking_clerical_trp_variants_1"));
    }
}