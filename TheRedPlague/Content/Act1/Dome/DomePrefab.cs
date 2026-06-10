using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nautilus.Assets;
using Nautilus.Extensions;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Content.Act3.DomeBase;
using TheRedPlague.Framework.API.Lighting;
using TheRedPlague.Framework.API.StructureLoading;
using UnityEngine;

namespace TheRedPlague.Content.Act1.Dome;

[PrefabClass]
public static class DomePrefab
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("NewInfectionDome");

    private static GameObject _cyclopsPrefab;
    private static GameObject _shieldPrefab;
    private static bool _cyclopsLoaded;

    [PrefabRegistration]
    private static void Register()
    {
        var infectionDome = new CustomPrefab(Info);
        infectionDome.SetGameObject(GetPrefab);
        infectionDome.Register();
        infectionDome.RemoveFromCache();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("NewInfectionDome"));
        obj.SetActive(false);
        MaterialUtils.ApplySNShaders(obj, 7, 1f, 1f, new DomeMaterialModifier());
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Global);

        // Load Cyclops

        _cyclopsLoaded = false;

        yield return new WaitUntil(() => LightmappedPrefabs.main);

        LightmappedPrefabs.main.RequestScenePrefab("Cyclops", OnCyclopsReferenceLoaded);

        yield return new WaitUntil(() => _cyclopsLoaded);

        var interiorRenderers = new[]
        {
            obj.transform.SearchChild("DomeBaseInterior").GetComponent<Renderer>(),
            obj.transform.SearchChild("DomeBaseInteriorAdditions").GetComponent<Renderer>()
        };

        var exteriorSkyApplier = obj.EnsureComponent<SkyApplier>();
        exteriorSkyApplier.anchorSky = Skies.SafeShallow;

        var exteriorRenderers = exteriorSkyApplier.renderers == null
            ? new List<Renderer>()
            : new List<Renderer>(exteriorSkyApplier.renderers);
        exteriorRenderers.RemoveAll(interiorRenderers.Contains);

        var lightControl = LightingControllerUtils.AddCustomLightingController(obj, _cyclopsPrefab,
            exteriorSkyApplier, LightingControllerUtils.CreateSkyFactory(
                new LightingControllerUtils.SkyIntensityPreset(
                    new[] { 0.8f, 0.4f, 0.2f },
                    new[] {2f, 1f, 0.4f },
                    new[] { 0.5f, 0.2f, 0.1f }),
                new LightingControllerUtils.SkyIntensityPreset(
                    new[] { 0.73f, 0.4f, 0.4f },
                    new[] { 1.02f, 0.5f, 0.5f },
                    new[] { 0.44f, 0.3f, 0.3f })
                ),
        new[] { 1, 0.7f, 0.3f },
            interiorRenderers, exteriorRenderers.ToArray());
        lightControl.state = LightingController.LightingState.Operational;
        lightControl.fadeDuration = 3;

        // Create shield, etc.

        var shield = Object.Instantiate(_shieldPrefab, obj.transform);
        shield.SetActive(true);
        shield.transform.localPosition = new Vector3(0, -0.07f, 0f);
        shield.transform.localEulerAngles = Vector3.right * 90;
        shield.transform.localScale = new Vector3(0.744f, 0.744f, 0.677f);

        var shieldRenderer = shield.GetComponent<Renderer>();
        var shieldMaterial = shieldRenderer.material;
        shieldMaterial.SetFloat(ShaderPropertyID._Intensity, 1);
        shieldMaterial.SetVector("_WobbleParams", new Vector4(2, 0.7f, 8, 0));

        // Set up logic

        var interiorPivot = obj.transform.Find("Pivot/Alterra Dome15/DomeBaseInterior");

        var domeController = obj.AddComponent<InfectionDomeController>();
        domeController.domeRenderer = shieldRenderer;
        domeController.domeBaseColliders = new[]
        {
            obj.transform.Find("Pivot/DomeBaseCollisions/default")
                .GetComponent<Collider>(),
            obj.transform.Find("Pivot/DomeInteriorAdditionCollisions")
                .GetComponent<Collider>()
        };
        domeController.baseCenterTransform = interiorPivot;
        domeController.lightingController = lightControl;

        var constructionVfx = obj.AddComponent<DomeConstructionVfx>();
        constructionVfx.domeShieldRenderer = shieldRenderer;

        var fabricatorTask = CraftData.GetPrefabForTechTypeAsync(TechType.Fabricator);
        yield return fabricatorTask;
        constructionVfx.emissiveTex = fabricatorTask.GetResult().GetComponent<Fabricator>().ghost._EmissiveTex;

        var farPlaneAdjust = obj.AddComponent<AdjustFarPlane>();
        farPlaneAdjust.newFarClipPlane = 4000;
        farPlaneAdjust.transitionDuration = 10;
        farPlaneAdjust.maxDepthToApply = 25;

        obj.EnsureComponent<ConstructionObstacle>();

        var colliders = obj.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.gameObject.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
        }

        var decorations = obj.AddComponent<ConglomerateStructureLoader>();
        var entityParent = new GameObject("EntityParent").transform;
        entityParent.parent = obj.transform;
        entityParent.localPosition = Vector3.zero;
        decorations.entityParent = entityParent;
        decorations.pivot = interiorPivot;
        decorations.maxDistance = 400;
        decorations.structureBundle = AssetBundles.Act3;
        decorations.structureName = "TheDomeBase";

        var playerFix = obj.AddComponent<PlayerInsideDomeChecker>();
        playerFix.center = interiorPivot;
        playerFix.maxDistance = 400;

        // Set up teleporter

        obj.transform.Find("Pivot/DomeTeleportTrigger").gameObject.AddComponent<DomeBaseTeleportTrigger>()
            .targetPosition = new Vector3(1447.6f, -337.5f, 434.5f);

        // Zapping

        obj.transform.Find("Pivot/DomeBaseCollisions/ZapTrigger").gameObject.AddComponent<DomeZapTrigger>();

        prefab.Set(obj);
    }

    private static void OnCyclopsReferenceLoaded(GameObject obj)
    {
        _cyclopsPrefab = obj;
        _shieldPrefab = obj.transform.Find("FX/x_Cyclops_GlassShield").gameObject;
        _cyclopsLoaded = true;
    }


    private class DomeMaterialModifier : MaterialModifier
    {
        public override void EditMaterial(Material material, Renderer renderer, int materialIndex,
            MaterialUtils.MaterialType materialType)
        {
            if (renderer.gameObject.name == "PowerAugmenter")
            {
                material.SetColor("_GlowColor", new Color(1, 1.3f, 1));
                material.SetFloat(ShaderPropertyID._GlowStrength, 4);
                material.SetFloat(ShaderPropertyID._GlowStrengthNight, 3);
            }
        }
    }
}