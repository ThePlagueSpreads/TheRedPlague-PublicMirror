using System.Collections;
using ECCLibrary;
using Nautilus.Assets;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Framework.Behaviour.Animation;
using UnityEngine;
using UWE;

namespace TheRedPlague.Content.Creatures.Sucker;

public class SuckerPrefab
{
    public PrefabInfo Info { get; }

    private bool Aurora { get; }

    private LiveMixinData _lmData;

    public static TechType ArmoredSuckerTechType = EnumHandler.AddEntry<TechType>("ArmoredSucker");

    public SuckerPrefab(PrefabInfo info, bool aurora)
    {
        Info = info;
        Aurora = aurora;
    }
    
    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
        
        _lmData = CreatureDataUtils.CreateLiveMixinData(100000);
        _lmData.broadcastKillOnDeath = true;
        
        if (Aurora)
        {
            CraftDataHandler.SetHarvestType(Info.TechType, HarvestType.DamageAlive);
        }
        
        PDAHandler.AddCustomScannerEntry(Info.TechType, 2, false, "Sucker");
    }

    private IEnumerator GetPrefab(IOut<GameObject> prefab)
    {
        var go = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("Sucker"));
        go.SetActive(false);
        PrefabUtils.AddBasicComponents(go, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);
        MaterialUtils.ApplySNShaders(go, 6);
        // var infect = go.AddComponent<InfectAnything>();
        // infect.infectionHeightStrength = 0.05f;

        var rb = go.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.mass = 20;
        var wf = go.AddComponent<WorldForces>();
        wf.useRigidbody = rb;
        wf.underwaterDrag = 1.5f;
        wf.underwaterGravity = 4f;
        
        if (Aurora)
        {
            var request = PrefabDatabase.GetPrefabAsync("98ac710d-5390-49fd-a850-dbea7bc07aef");
            yield return request;
            if (request.TryGetPrefab(out var controlRoomPrefab))
            {
                var skyApplier = go.EnsureComponent<SkyApplier>();
                skyApplier.customSkyPrefab = controlRoomPrefab.GetComponent<SkyApplier>().customSkyPrefab;
                skyApplier.dynamic = false;
                skyApplier.anchorSky = Skies.Custom;
            }
            
            // Set up armored variant
            var renderer = go.transform.Find("SuckerV2/Sucker2").GetComponent<Renderer>();
            
            Texture2D armorColor = AssetBundles.Core.LoadAsset<Texture2D>("ArmoredSuckerDiffuse");
            Texture2D armorNormal = AssetBundles.Core.LoadAsset<Texture2D>("ArmoredSuckerNormal");
            
            var armoredMaterial = new Material(renderer.sharedMaterial);
            armoredMaterial.mainTexture = armorColor;
            armoredMaterial.SetTexture("_SpecTex", armorColor);
            armoredMaterial.SetTexture("_BumpMap", armorNormal);
            
            var blocker = go.AddComponent<AuroraSuckerBreachBlocker>();
            blocker.mainBodyRenderer = renderer;
            blocker.armoredMaterial = armoredMaterial;
            blocker.techTag = go.GetComponent<TechTag>();
            
            // Set up interaction volume for hover text

            var interactionVolume = go.AddComponent<InteractionVolume>();
            var interactionVolumeCollider = new GameObject("InteractionVolume");
            interactionVolumeCollider.layer = LayerID.Useable;
            interactionVolumeCollider.transform.SetParent(go.transform);
            interactionVolumeCollider.transform.localPosition = Vector3.zero;
            var collider = interactionVolumeCollider.gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = 1.4f;
            interactionVolumeCollider.AddComponent<InteractionVolumeCollider>().owner = interactionVolume;
        }
        
        var look = go.transform.Find("SuckerV2/Sucker2Armature/Root/Eye").gameObject.AddComponent<GenericEyeLook>();
        look.dotLimit = 0;
        look.useLimits = true;

        go.AddComponent<SuckerDamageable>().animator = go.GetComponentInChildren<Animator>();

        var lm = go.AddComponent<LiveMixin>();
        lm.data = _lmData;

        prefab.Set(go);
    }
}