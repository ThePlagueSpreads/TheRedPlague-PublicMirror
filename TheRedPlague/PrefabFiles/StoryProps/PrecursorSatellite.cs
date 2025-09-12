using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Mono.VFX;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.StoryProps;

public static class PrecursorSatellite
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PrecursorSatellite");

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PrecursorSatellitePrefab"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(obj, 4, 1f, 1, new SatelliteMaterialModifier(), new IgnoreParticleSystemsModifier());
        var orbit = obj.AddComponent<PrecursorSatelliteOrbit>();
        orbit.ringPivot = obj.transform.Find("PrecursorSatellite/PrecursorSatellite-Rings");
        return obj;
    }

    private class SatelliteMaterialModifier : MaterialModifier
    {
        public override void EditMaterial(Material material, Renderer renderer, int materialIndex, MaterialUtils.MaterialType materialType)
        {
            if (materialType == MaterialUtils.MaterialType.Cutout)
            {
                material.SetFloat(ShaderPropertyID._Cutoff, 0.63f);
            }
        }
    }
}