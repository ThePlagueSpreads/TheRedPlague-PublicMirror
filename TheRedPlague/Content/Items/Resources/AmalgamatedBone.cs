using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Content.Infection;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Items.Resources;

[PrefabClass]
public static class AmalgamatedBone
{
    public static TechType HarvestableBoneTechType { get; } = EnumHandler.AddEntry<TechType>("HarvestableAmalgamatedBone");

    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AmalgamatedBone");

    [PrefabRegistration]
    private static void Register()
    {
        Info.WithIcon(AssetBundles.Core.LoadAsset<Sprite>("AmalgamatedBone"));
        var amalgamatedBonePrefab = new CustomPrefab(Info);
        var amalgamatedBoneTemplate = new CloneTemplate(Info, "42e1ac56-6fab-4a9f-95d9-eec5707fe62b");
        amalgamatedBoneTemplate.ModifyPrefab += (go) =>
        {
            foreach (Transform child in go.transform)
            {
                child.localScale *= 0.3f;
            }

            go.AddComponent<InfectAnything>().infectionHeightStrength = 0.2f;
            go.AddComponent<Pickupable>();
            go.AddComponent<TechTag>().type = Info.TechType;
            var rb = go.EnsureComponent<Rigidbody>();
            rb.mass = 13;
            rb.useGravity = false;
            rb.isKinematic = true;
            var wf = go.EnsureComponent<WorldForces>();
            wf.useRigidbody = rb;
            wf.underwaterDrag = 2;
            PrefabUtils.AddResourceTracker(go, Info.TechType);
        };
        amalgamatedBonePrefab.SetGameObject(amalgamatedBoneTemplate);
        amalgamatedBonePrefab.SetSpawns(new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_SandDune,
                count = 1,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Dunes_Grass,
                count = 1,
                probability = 0.4f
            });
        amalgamatedBonePrefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        amalgamatedBonePrefab.Register();
        CraftDataHandler.SetHarvestOutput(HarvestableBoneTechType, Info.TechType);
        CraftDataHandler.SetHarvestType(HarvestableBoneTechType, HarvestType.DamageAlive);
        PDAHandler.AddEncyclopediaEntry("HarvestableAmalgamatedBone", CustomPdaPaths.RedPlagueEnvironmentalData);
        PDAHandler.AddCustomScannerEntry(HarvestableBoneTechType, 4, false, "HarvestableAmalgamatedBone");
    }
}