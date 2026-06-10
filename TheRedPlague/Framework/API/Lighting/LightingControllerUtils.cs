using mset;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheRedPlague.Framework.API.Lighting;

public static class LightingControllerUtils
{
    public delegate LightingController.MultiStatesSky[] SkyFactory(Sky interior, Sky glass);

    // Allows an interior to have a Lighting Controller without being a SubRoot
    public static LightingController AddCustomLightingController(GameObject root, GameObject cyclopsPrefab,
        SkyApplier exteriorSkyApplier, SkyFactory skySettings, float[] emissiveLevels, Renderer[] interiorRenderers,
        Renderer[] exteriorRenderers)
    {
        // Load skies
        var interiorSky = Object
            .Instantiate(cyclopsPrefab.transform.Find("SkyBaseInterior").gameObject, root.transform, false)
            .GetComponent<Sky>();
        var glassSky = Object
            .Instantiate(cyclopsPrefab.transform.Find("SkyBaseGlass").gameObject, root.transform, false)
            .GetComponent<Sky>();

        // Fix skies
        exteriorSkyApplier.renderers = exteriorRenderers;

        // Add lighting controller
        var lightControl = root.AddComponent<LightingController>();
        lightControl.skies = skySettings.Invoke(interiorSky, glassSky);
        lightControl.emissiveController = new LightingController.MultiStatesEmissive
        {
            intensities = emissiveLevels,
        };
        lightControl.fadeDuration = 0.3f;

        var interiorSkyApplier = root.AddComponent<SkyApplier>();
        interiorSkyApplier.renderers = interiorRenderers;
        interiorSkyApplier.anchorSky = Skies.BaseInterior;
        interiorSkyApplier.lightControl = lightControl;
        interiorSkyApplier.emissiveFromPower = true;

        // Necessary for the structure to be recognized as an environment for Sky Appliers 
        var lights = root.AddComponent<LightControllerSkyLink>();
        lights.interiorSky = interiorSky;
        lights.glassSky = glassSky;

        return lightControl;
    }

    public static float[] DefaultEmissiveLevels { get; } = { 1, 0.7f, 0f };

    public static SkyFactory GetDefaultSkyFactory()
    {
        return (skyInterior, skyGlass) =>
        {
            return new[]
            {
                new LightingController.MultiStatesSky
                {
                    sky = skyInterior,
                    masterIntensities = new[] { 1.4f, 1f, 0.5f },
                    diffIntensities = new[] { 7, 1.5f, 1.2f },
                    specIntensities = new[] { 2, 0.6f, 0.5f }
                },

                new LightingController.MultiStatesSky
                {
                    sky = skyGlass,
                    masterIntensities = new[] { 0.73f, 0.4f, 0.4f },
                    diffIntensities = new[] { 1.02f, 0.5f, 0.5f },
                    specIntensities = new[] { 0.44f, 0.3f, 0.3f }
                }
            };
        };
    }
    
    public static SkyFactory CreateSkyFactory(SkyIntensityPreset interiorSkyPreset, SkyIntensityPreset glassSkyPreset)
    {
        return (skyInterior, skyGlass) =>
        {
            return new[]
            {
                Convert(skyInterior, interiorSkyPreset),
                Convert(skyGlass, glassSkyPreset)
            };
        };
    }

    private static LightingController.MultiStatesSky Convert(Sky sky, SkyIntensityPreset preset)
    {
        return new LightingController.MultiStatesSky
        {
            sky = sky,
            masterIntensities = preset.Master,
            diffIntensities = preset.Diff,
            specIntensities = preset.Spec,
            startMasterIntensity = preset.Master[0],
            startDiffuseIntensity = preset.Diff[0],
            startSpecIntensity = preset.Spec[0]
        };
    }

    public record SkyIntensityPreset(
        float[] Master,
        float[] Diff,
        float[] Spec);
}