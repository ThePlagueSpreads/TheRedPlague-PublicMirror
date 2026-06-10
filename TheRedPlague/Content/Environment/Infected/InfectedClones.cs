using System;
using ECCLibrary;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using TheRedPlague.Content.Infection;
using TheRedPlague.Content.Items.Resources;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Infected;

[PrefabClass]
public static class InfectedClones
{
    private static LiveMixinData HarvestableBoneLiveMixinData
    {
        get
        {
            if (field == null)
            {
                field = CreatureDataUtils.CreateLiveMixinData(300);
            }

            return field;
        }
    }
    
    [PrefabRegistration]
    private static void Register()
    {
        var infectedReaperSkeleton = MakeInfectedClone(
            new PrefabInfo("InfectedReaperSkeleton", "InfectedReaperSkeletonPrefab", AmalgamatedBone.HarvestableBoneTechType),
            "8fe779a5-e907-4e9e-b748-1eee25589b34", 4f, true);
        infectedReaperSkeleton.Register();

        var reaperWithoutSkullModification = (GameObject go) =>
        {
            foreach (Transform child in go.transform)
            {
                var name = child.gameObject.name;
                if (name.Contains("bone") || name.Contains("skull"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        };

        // InfectedReaperSkeletonNoSkull
        var infectedReaperSkeletonNoSkull = MakeInfectedClone(
            new PrefabInfo("InfectedReaperSkeletonNoSkull", "InfectedReaperSkeletonNoSkullPrefab",
                AmalgamatedBone.HarvestableBoneTechType),
            "8fe779a5-e907-4e9e-b748-1eee25589b34", 4f, true, reaperWithoutSkullModification);
        infectedReaperSkeletonNoSkull.Register();

        // InfectedSkeleton1
        var infectedGenericSkeleton1 = MakeInfectedClone(
            new PrefabInfo("InfectedSkeleton1", "InfectedSkeleton1Prefab", AmalgamatedBone.HarvestableBoneTechType),
            "0b6ea118-1c0b-4039-afdb-2d9b26401ad2", 7f, true);
        infectedGenericSkeleton1.Register();

        // InfectedSkeleton2
        var infectedGenericSkeleton2 = MakeInfectedClone(
            new PrefabInfo("InfectedSkeleton2", "InfectedSkeleton2Prefab", AmalgamatedBone.HarvestableBoneTechType),
            "e10ff9a1-5f1e-4c4d-bf5f-170dba9e321b", 8f, true);
        infectedGenericSkeleton2.Register();

        // InfectedRib
        var infectedRib2 = MakeInfectedClone(
            new PrefabInfo("InfectedRib", "InfectedRibPrefab", AmalgamatedBone.HarvestableBoneTechType),
            "33c31a89-9d3b-4717-ad26-4cc8106a1f24", 2f, true);
        infectedRib2.Register();
        
        var infectedHangingPlant = MakeInfectedClone(PrefabInfo.WithTechType("InfectedHangingPlant"),
            "8d7f308a-21db-4d1f-99c7-38860e5132e7", 1f, false,
            obj =>
            {
                obj.GetComponentInChildren<Renderer>().material.color = new Color(3, 0.3f, 0.3f);
                obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Far;
            });
        infectedHangingPlant.Register();
    }
    
    private static CustomPrefab MakeInfectedClone(PrefabInfo info, string cloneClassID, float scale, bool isBone,
        Action<GameObject> modifyPrefab = null)
    {
        var prefab = new CustomPrefab(info);
        var template = new CloneTemplate(prefab.Info, cloneClassID);
        if (modifyPrefab != null)
        {
            template.ModifyPrefab += modifyPrefab;
        }

        template.ModifyPrefab += go =>
        {
            var infect = go.AddComponent<InfectAnything>();
            infect.infectionScale = Vector3.one * 2;
            infect.infectionAmount = 1;
            if (Math.Abs(scale - 1f) > 0.001f)
            {
                var scaler = new GameObject("Scaler").transform;
                scaler.parent = go.transform;
                scaler.localPosition = Vector3.zero;
                while (go.transform.childCount > 1)
                {
                    go.transform.GetChild(0).parent = scaler;
                }

                scaler.transform.localScale = Vector3.one * scale;
            }

            if (isBone)
            {
                go.EnsureComponent<TechTag>().type = AmalgamatedBone.HarvestableBoneTechType;
                go.AddComponent<LiveMixin>().data = HarvestableBoneLiveMixinData;
            }
        };

        prefab.SetGameObject(template);
        return prefab;
    }
}