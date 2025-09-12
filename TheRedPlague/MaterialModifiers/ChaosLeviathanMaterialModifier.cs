using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using UnityEngine;

namespace TheRedPlague.MaterialModifiers;

public class ChaosLeviathanMaterialModifier : MaterialModifier
{
    public override void EditMaterial(Material material, Renderer renderer, int materialIndex, MaterialUtils.MaterialType materialType)
    {
        if (material.name.Contains("Body"))
        {
            material.color = new Color(0.7f, 0.7f, 0.7f);
            material.EnableKeyword("MARMO_EMISSION");
            material.SetColor("_GlowColor", Color.black);
            material.SetFloat("_EmissionLM", 0.02f);
            material.SetFloat("_EmissionLMNight", 0.04f);
        }
    }
}