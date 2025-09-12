using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using UnityEngine;

namespace TheRedPlague.MaterialModifiers;

public class ShrineBaseModifier : MaterialModifier
{
    private static readonly int Shininess = Shader.PropertyToID("_Shininess");
    private static readonly int SpecInt = Shader.PropertyToID("_SpecInt");
    private static readonly int LightmapStrength = Shader.PropertyToID("_LightmapStrength");

    public override void EditMaterial(Material material, Renderer renderer, int materialIndex, MaterialUtils.MaterialType materialType)
    {
        material.SetFloat(LightmapStrength, 1);
        var isSteel = material.name.ToLower().Contains("steel");
        if (isSteel)
            material.SetFloat(Shininess, 6);
        if (renderer.gameObject.name == "Exterior-Front")
        {
            material.SetFloat(ShaderPropertyID._GlowStrength, 0.2f);
            material.SetFloat(ShaderPropertyID._GlowStrengthNight, 0.2f);
        }
    }
}