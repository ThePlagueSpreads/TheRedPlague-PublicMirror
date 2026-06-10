using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Items.Resources;

[PrefabClass]
public static class WarperHeart
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("WarperHeart");
    
    [PrefabRegistration]
    private static void Register()
    {
        Info.WithIcon(AssetBundles.Core.LoadAsset<Sprite>("WarperHeartIcon"));
        var warperHeart = new CustomPrefab(Info);
        warperHeart.SetGameObject(GetWarperInnardsPrefab);
        warperHeart.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        warperHeart.Register();
    }
    
    private static IEnumerator GetWarperInnardsPrefab(IOut<GameObject> prefab)
    {
        var go = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("WarperInnards_Prefab"));
        go.SetActive(false);
        PrefabUtils.AddBasicComponents(go, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Near);
        go.AddComponent<Pickupable>();
        var rb = go.AddComponent<Rigidbody>();
        rb.mass = 10;
        rb.useGravity = false;
        rb.isKinematic = true;
        var wf = go.AddComponent<WorldForces>();
        wf.useRigidbody = rb;
        var warperTask = CraftData.GetPrefabForTechTypeAsync(TechType.Warper);
        yield return warperTask;
        var heartMaterial = new Material(warperTask.GetResult().transform.Find("warper_anim/warper_geos/Warper_geo")
            .gameObject.GetComponent<Renderer>().materials[1]);
        heartMaterial.color = Color.red;
        heartMaterial.SetColor(ShaderPropertyID._SpecColor, Color.red * 4);
        heartMaterial.SetColor(ShaderPropertyID._GlowColor, Color.red * 4);
        heartMaterial.SetFloat("_Shininess", 8);
        go.GetComponentInChildren<Renderer>().material = heartMaterial;
        PrefabUtils.AddResourceTracker(go, Info.TechType);
        prefab.Set(go);
    }
}