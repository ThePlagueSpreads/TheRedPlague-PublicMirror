using System;
using System.Linq;
using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Content.Environment.Flesh;
using TheRedPlague.Framework.MaterialModifiers;
using TheRedPlague.Framework.Migration;
using UnityEngine;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class FleshAndBoneRegistration
{
    [PrefabRegistration]
    private static void RegisterFleshAndBonePrefabs()
    {
        // Decals
        new FleshDecorationPrefab(PrefabInfo.WithTechType("FleshRoomDecal"), "FleshRoomDecalPrefab", false, true)
            .Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("FleshRoomDecal2"), "FleshRoomDecal2Prefab", false, true)
            .Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("MiscDecal1"), "MiscDecal1Prefab", false, false).Register();
        
        // Flesh props
        new SuperFleshDecorationPrefab("FleshMass", "FleshMass", false,
            new WavingEffectModifier(1) { Speed = new Vector4(0.1f, 0.2f) })
        {
            ModifyPrefab = obj =>
            {
                obj.AddComponent<DestroyIfIdMatches>().ids = ["49a6a66f-2291-4a17-ba64-6ce80812fefe"];
            }
        }.Register();
        new SuperFleshDecorationPrefab("FleshWall", "FleshWall", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();
        new SuperFleshDecorationPrefab("OrgansProp1", "OrgansProp1", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) },
            new FloatPropertyModifier("_Shininess", 2.5f)).Register();
        new SuperFleshDecorationPrefab("OrgansProp2", "OrgansProp2", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();
        new SuperFleshDecorationPrefab("OrgansProp3", "OrgansProp3", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();
        new SuperFleshDecorationPrefab("HangingFlesh", "HangingFlesh", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) })
        {
            HasGlobalVariant = true
        }.Register();

        new FleshDecorationPrefab(PrefabInfo.WithTechType("GorePile1"), "GorePile1Prefab", false, false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("GorePile2"), "GorePile2Prefab", false, false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("GorePile3"), "GorePile3Prefab", false, false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("CoreHolder"), "CoreHolderPrefab", false, true).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("CoreHolderGeneric"), "CoreHolderPrefab", false, false)
            .Register();
        new SuperFleshDecorationPrefab("Dangler", "DanglerPrefab", false, new WavingEffectModifier(1)).Register();
        new SuperFleshDecorationPrefab("Drooper", "Drooper", false, new WavingEffectModifier(1)).Register();
        new SuperFleshDecorationPrefab("Roofer", "Roofer", false, new WavingEffectModifier(1)).Register();

        new SuperFleshDecorationPrefab("FleshProp3", "FleshProp3Prefab", false,
            new WavingEffectModifier(0.1f)).Register();
        new SuperFleshDecorationPrefab("FleshProp4", "FleshProp4Prefab", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.06f, 0.03f, 0.06f, 0.05f) }).Register();
        new SuperFleshDecorationPrefab("FleshProp5", "FleshProp5", false,
            new WavingEffectModifier(1) { Scale = new Vector4(0.01f, 0.01f, 0.01f, 0.02f) }).Register();

        new SuperFleshDecorationPrefab("FuckYou", "FuckYouPrefab", false).Register();

        // later we can change this "cyclops island only" prop to remove the visuals and collisions, for future acts 
        new FleshDecorationPrefab(PrefabInfo.WithTechType("VineWall-CYCLOPSISLANDONLY"), "VineWallPrefab_ISLAND", false,
            false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("VineWall"), "VineWallPrefab", false,
            false, new DoubleSidedModifier(MaterialUtils.MaterialType.Transparent)).Register();

        // Veins
        new SuperFleshDecorationPrefab("Veins1", "Vein1_Prefab", false, new VeinsMaterialModifier()).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("DecoProps02Veins"), "DecoProps02Veins", false, false,
            new VeinsMaterialModifier()).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("PrecursorKeyTerminalVeins"), "PrecursorKeyTerminalVeins",
            false, false,
            new VeinsMaterialModifier()).Register();

        // Tentacles
        new SuperFleshDecorationPrefab("FleshTentacle", "DecorationalTentacle",
            false, new WavingEffectModifier(1))
        {
            ModifyPrefab = obj =>
            {
                obj.AddComponent<DestroyIfIdMatches>().ids = [
                    "10d33dc0-2e27-43b2-84db-812c0a6082c4",
                    "913b64f7-6e85-4d24-a691-ff4e26d6a3a6",
                    "b8a222b4-8936-4446-b19a-eee025f9aa74"
                ];
            }
        }.Register();

        // Bones
        for (var i = 1; i <= 4; i++)
        {
            new FleshDecorationPrefab(PrefabInfo.WithTechType("InfectedGhostSkeleton" + i),
                "GhostSkeletonP" + i, true, false)
            {
                CellLevel = LargeWorldEntity.CellLevel.VeryFar
            }.Register();
        }

        for (var i = 1; i <= 4; i++)
        {
            new FleshDecorationPrefab(PrefabInfo.WithTechType("InfectedReefbackSkeleton" + i),
                "ReefbackSkeletonP" + i, true, false)
            {
                CellLevel = LargeWorldEntity.CellLevel.Far
            }.Register();
        }

        var seaTreaderModelPrefix = "M_Seatreader_".ToLower();
        foreach (var seaTreaderBoneName in AssetBundles.Core.GetAllAssetNames()
                     .Where(name => name.Contains(seaTreaderModelPrefix)))
        {
            var fileName =
                seaTreaderBoneName.Substring(
                    seaTreaderBoneName.IndexOf(seaTreaderModelPrefix, StringComparison.Ordinal) +
                    seaTreaderModelPrefix.Length);
            new SeaTreaderBone("InfectedSeaTreaderSkeleton_" + fileName.Split('.')[0], seaTreaderBoneName).Register();
        }
        
        new SuperFleshDecorationPrefab("MeatwormDecoration", "MeatwormDecorationPrefab", false).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("OrnamentPlant1"), "OrnamentPlant1Prefab", false, false,
            new FloatPropertyModifier("_Shininess", 8)).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("OrnamentPlant2"), "OrnamentPlant2Prefab", false, false,
            new FloatPropertyModifier("_Shininess", 8)).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("SanguineBulb"), "SanguineBulbPrefab", false, false,
            new FloatPropertyModifier("_Shininess", 10)).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("MarrowVineLarge"), "MarrowVineLargePrefab", false, false,
            new FloatPropertyModifier("_Shininess", 7), new WavingEffectModifier(0.15f)
            {
                Scale = new Vector4(0.64f, 0, 0.6f, 0.2f),
                Speed = new Vector2(0.12f, 0.25f),
                Frequency = new Vector4(0.6f, 0.5f, 0.75f, 0.8f)
            }, new VectorPropertyModifier("_ObjectUp", new Vector4(0, 1, 0, 0))).Register();
        new FleshDecorationPrefab(PrefabInfo.WithTechType("MarrowVineSmall"), "MarrowVineSmallPrefab", false, false,
            new FloatPropertyModifier("_Shininess", 7), new WavingEffectModifier(0.06f)).Register();
        
        // Corpses

        new FleshDecorationPrefab(PrefabInfo.WithTechType("ChainBaseCorpse"), "ChainBaseCorpsePrefab", false, false)
            .Register();
    }
}