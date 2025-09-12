using Nautilus.Assets;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Decorations;

public class FloatingCorpsePrefab
{
    public PrefabInfo Info { get; }
    private readonly string _prefabName;

    public FloatingCorpsePrefab(string classId, string prefabName)
    {
        Info = PrefabInfo.WithTechType(classId);
        _prefabName = prefabName;
    }

    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
    }

    private GameObject GetGameObject()
    {
        var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>(_prefabName));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj, 5, 1, 1, new FloatingCorpseMaterialModifier());
        PrefabUtils.AddWorldForces(obj, 10, 0);
        obj.AddComponent<EcoTarget>().type = EcoTargetType.Shark;
        ModifyPrefab(obj);
        return obj;
    }

    protected virtual void ModifyPrefab(GameObject prefab)
    {
        
    }

    private class FloatingCorpseMaterialModifier : MaterialModifier
    {
        public override void EditMaterial(Material material, Renderer renderer, int materialIndex, MaterialUtils.MaterialType materialType)
        {
            if (material.name.StartsWith("player_head"))
            {
                material.color = new Color(0.08f, 0, 0);
                material.SetColor(ShaderPropertyID._SpecColor, new Color(1.5f, 0.238f, 0.238f));
                material.SetFloat("_Shininess", 7);
                material.SetFloat("_Fresnel", 7);
            }
        }
    }
}