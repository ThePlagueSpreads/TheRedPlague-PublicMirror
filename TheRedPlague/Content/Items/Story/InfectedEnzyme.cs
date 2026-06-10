using System.Collections;
using Nautilus.Assets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Items.Story;

[PrefabClass]
public static class InfectedEnzyme
{
    public static PrefabInfo EnzymeParticleInfo { get; private set; }

    [PrefabRegistration]
    private static void Register()
    {
        EnzymeParticleInfo = PrefabInfo.WithTechType("InfectedEnzyme")
            .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("InfectedEnzyme42"));
        var enzymeParticle = new CustomPrefab(EnzymeParticleInfo);
        enzymeParticle.SetGameObject(GetEnzymeParticlePrefab);
        enzymeParticle.Register();
    }
    
    private static IEnumerator GetEnzymeParticlePrefab(IOut<GameObject> prefab)
    {
        var request = UWE.PrefabDatabase.GetPrefabAsync("505e7eff-46b3-4ad2-84e1-0fadb7be306c");
        yield return request;
        request.TryGetPrefab(out var reference);
        var go = Object.Instantiate(reference);
        PrefabUtils.AddBasicComponents(go, EnzymeParticleInfo.ClassID, EnzymeParticleInfo.TechType,
            LargeWorldEntity.CellLevel.VeryFar);
        Object.DestroyImmediate(go.GetComponent<EnzymeBall>());
        Object.DestroyImmediate(go.transform.Find("collider").GetComponent<GenericHandTarget>());
        var renderer = go.transform.Find("Leviathan_enzymeBall_anim/enzymeBall_geo").GetComponent<Renderer>();
        var material = renderer.material;
        material.color = Color.red;
        material.SetColor(ShaderPropertyID._SpecColor, new Color(0.877f, 1f, 0.838f));
        go.AddComponent<Pickupable>();
        prefab.Set(go);
    }
}