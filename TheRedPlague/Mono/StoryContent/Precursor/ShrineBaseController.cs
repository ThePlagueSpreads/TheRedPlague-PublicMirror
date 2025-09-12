using TheRedPlague.Mono.VFX.Flickering;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent.Precursor;

public class ShrineBaseController : MonoBehaviour
{
    public Renderer[] renderers;

    private Material[] _materials;
    
    public static ShrineBaseController Main { get; private set; }

    private void Start()
    {
        Main = this;
        PrepareMaterialsForFlickering();
    }

    private void PrepareMaterialsForFlickering()
    {
        var materialCount = 0;
        foreach (var renderer in renderers)
        {
            if (renderer.gameObject.name == "DATA_LightmapsMesh")
                continue;
            
            foreach (var material in renderer.sharedMaterials)
            {
                if (material != null)
                {
                    materialCount++;
                }
            }
        }
        
        _materials = new Material[materialCount];
        int i = 0;
        foreach (var renderer in renderers)
        {
            if (renderer.gameObject.name == "DATA_LightmapsMesh")
                continue;
            
            foreach (var material in renderer.materials)
            {
                if (material != null)
                {
                    _materials[i++] = material;
                }
            }
        }
    }

    public void FlickerLights(float duration)
    {
        if (!MiscSettings.flashes)
            return;
        
        if (_materials == null || _materials.Length == 0)
        {
            Plugin.Logger.LogWarning("Shrine base materials not initialized!");
            return;
        }

        var flickerController = gameObject.AddComponent<LightFlickerEvent>();
        flickerController.minDelay = 0.09f;
        flickerController.maxDelay = 0.22f;
        var lightFlicker = new MaterialEmissionFlicker(_materials, true);
        flickerController.SetUp(new[] { lightFlicker }, duration);
    }

    private void OnDestroy()
    {
        if (_materials == null) return;
        foreach (var material in _materials)
        {
            Destroy(material);
        }
    }
}