using ECCLibrary;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Creatures;

public class InsectoidPrefab
{
    public PrefabInfo Info { get; }
    private float ScaleMultipler { get; }
    private string OriginalClassId { get; }

    private static readonly FMODAsset WalkSound = AudioUtils.GetFmodAsset("InsectoidWalk");
    private static readonly FMODAsset ScreechSound = AudioUtils.GetFmodAsset("InsectoidScreech");

    public InsectoidPrefab(PrefabInfo info, float scaleMultiplier,
        string originalClassId = "3e0a11f1-e2b2-4c4f-9a8e-0b0a77dcc065")
    {
        Info = info;
        ScaleMultipler = scaleMultiplier;
        OriginalClassId = originalClassId;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info, OriginalClassId)
        {
            ModifyPrefab = ModifyPrefab
        });
        prefab.Register();
        CreatureDataUtils.SetAcidImmune(Info.TechType);
    }

    private void ModifyPrefab(GameObject prefab)
    {
        var insectoidModel = Object.Instantiate(Plugin.CreaturesBundle.LoadAsset<GameObject>("InsectoidModel"),
            prefab.transform);
        
        MaterialUtils.ApplySNShaders(insectoidModel, 6);

        insectoidModel.transform.localPosition = new Vector3(0, -0.2f * ScaleMultipler, 0);
        insectoidModel.transform.localScale = Vector3.one * ScaleMultipler;
        insectoidModel.transform.localRotation = Quaternion.identity;
        
        var creatureComponent = prefab.GetComponent<Creature>();
        creatureComponent.traitsAnimator.gameObject.SetActive(false);
        
        var animator = insectoidModel.GetComponent<Animator>();
        
        creatureComponent.traitsAnimator = animator;
        
        var jumpRandom = prefab.GetComponent<CrawlerJumpRandom>();
        
        if (jumpRandom)
        {
            jumpRandom.evaluatePriority = 0;
        }
        
        var moveOnSurface = prefab.GetComponent<MoveOnSurface>();
        if (moveOnSurface == null)
        {
            Plugin.Logger.LogWarning("Prefab has no MoveOnSurface component for Insectoid!");
        }
        else
        {
            moveOnSurface.updateTargetInterval = 0.35f;
            moveOnSurface.moveRadius = 20;
        }
        
        prefab.GetComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();

        var screechEmitter = prefab.AddComponent<FMOD_CustomEmitter>();
        screechEmitter.playOnAwake = true;
        screechEmitter.SetAsset(ScreechSound);
        screechEmitter.followParent = true;

        if (creatureComponent is CaveCrawler crawler)
        {
            crawler.walkingSound.SetAsset(WalkSound);
        }

        prefab.GetComponent<WalkBehaviour>().turnSpeed *= 1.3f;
    }
}