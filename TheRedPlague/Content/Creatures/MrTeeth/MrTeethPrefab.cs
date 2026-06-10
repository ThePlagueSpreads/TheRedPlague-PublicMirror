using System.Collections;
using ECCLibrary;
using ECCLibrary.Data;
using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.MrTeeth;

public class MrTeethPrefab : CreatureAsset
{
    private static readonly FMODAsset SpawnSound = AudioUtils.GetFmodAsset("MrTeethScream");

    public MrTeethPrefab(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(() => AssetBundles.Creatures.LoadAsset<GameObject>("MrTeethPrefab"),
            BehaviourType.Shark, EcoTargetType.Shark, 5000)
        {
            SwimRandomData = null,
            LocomotionData = new LocomotionData(40f),
            Mass = 1000,
            AvoidObstaclesData = new AvoidObstaclesData(0.4f, 6f, false, 5f, 6f, scanInterval: 0.2f)
        };
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        var hunt = prefab.AddComponent<MrTeethHuntBehaviour>();
        hunt.evaluatePriority = 0.3f;
        hunt.minActionCheckInterval = 0.1f;

        var bury = prefab.AddComponent<MrTeethBuryBehaviour>();
        bury.evaluatePriority = 0.8f;
        bury.minActionCheckInterval = 0.1f;

        var attackTrigger = prefab.transform.Find("AttackTrigger").gameObject.AddComponent<MrTeethAttackTrigger>();
        attackTrigger.animator = components.Animator;
        attackTrigger.rootObject = prefab;

        TrpPrefabUtils.AddPlagueCreationComponents(prefab);

        var emitter = prefab.AddComponent<FMOD_CustomEmitter>();
        emitter.followParent = true;
        emitter.playOnAwake = true;
        emitter.SetAsset(SpawnSound);

        // ADD TAIL
        var bonesharkTask = CraftData.GetPrefabForTechTypeAsync(TechType.BoneShark);
        yield return bonesharkTask;
        
        GameObject bonesharkPrefab = bonesharkTask.GetResult();
        
        Animator bonesharkMeshRef = bonesharkPrefab.GetComponentInChildren<Animator>();
        if (bonesharkMeshRef == null)
        {
            Plugin.Logger.LogError("Failed to find boneshark mesh");
            yield break;
        }
        
        // boneshark mesh = tail (lol, it's SO dumb)
        GameObject tail = Object.Instantiate(bonesharkMeshRef.gameObject, prefab.transform);
        Object.DestroyImmediate(tail.GetComponent<Animator>());
        Object.DestroyImmediate(tail.GetComponent<AnimateByVelocity>());
        tail.transform.localPosition = new Vector3(0, -1, -4);
        tail.transform.localRotation = Quaternion.identity;
        tail.transform.localScale = Vector3.one * 2;
        
        // Remove boneshark head and fins
        string[] bonesToHide =
        [
            "clav_left",
            "clav_right",
            "neck"
        ];
        
        Transform root = tail.transform.Find("root");
        foreach (var hide in bonesToHide)
        {
            root.Find(hide).localScale = Vector3.zero;
        }
        
        // fix tail LOD z-fighting
        tail.transform.Find("bone_shark_geo_LOD1").gameObject.SetActive(false);
        
        // update tail materials
        var material = tail.transform.Find("bone_shark_geo").GetComponent<Renderer>().material;
        material.color = new Color(0.666f, 0.52f, 0.43f);
        material.SetColor("_SpecColor", new Color(0.4f, 0.1f, 0.2f));
        material.SetFloat("_Shininess", 4);
        
        // set tail parent to existing rig bone
        var tailBone = prefab.SearchChild("Tail.001").transform;
        tail.transform.SetParent(tailBone);
    }

    protected override void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab, 5, modifiers:
            new DoubleSidedModifier(MaterialUtils.MaterialType.Opaque));
    }
}