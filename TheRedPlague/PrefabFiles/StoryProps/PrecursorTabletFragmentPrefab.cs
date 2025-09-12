using System;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.StoryProps;

public class PrecursorTabletFragmentPrefab
{
    private static readonly int SpecInt = Shader.PropertyToID("_SpecInt");
    
    public PrefabInfo Info { get; }
    private Func<Texture2D> ShatteredTexture { get; }
    private TechType BlueprintUnlock { get; }
    private Color LightColor { get; }

    public PrecursorTabletFragmentPrefab(PrefabInfo info, Func<Texture2D> shatteredTexture, TechType blueprintUnlock, Color lightColor)
    {
        Info = info;
        ShatteredTexture = shatteredTexture;
        BlueprintUnlock = blueprintUnlock;
        LightColor = lightColor;
    }
    
    public void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info,
            "83b61f89-1456-4ff5-815a-ecdc9b6cc9e4")
        {
            ModifyPrefab = ModifyPrefab
        });
        prefab.Register();
        PDAHandler.AddCustomScannerEntry(Info.TechType, BlueprintUnlock,
            true, 1, 2, false);
    }
    
    private void ModifyPrefab(GameObject prefab)
    {
        prefab.GetComponent<BoxCollider>().size = new Vector3(2f, 0.1f, 2f);

        var tex = ShatteredTexture.Invoke();

        var fragment1 = prefab.transform.GetChild(0).GetChild(0);
        ApplyTexture(fragment1, tex);

        var fragment2 = prefab.transform.GetChild(0).GetChild(1);
        ApplyTexture(fragment2, tex);

        var lightObj = new GameObject("Light");
        lightObj.transform.parent = prefab.transform;
        lightObj.transform.localPosition = Vector3.up * 0.5f;
        // ryuzaki???
        var l = lightObj.AddComponent<Light>();
        l.color = LightColor;
        l.range = 3.5f;
        l.intensity = 2f;
    }

    private static void ApplyTexture(Transform fragment, Texture2D texture)
    {
        var mr = fragment.gameObject.GetComponent<MeshRenderer>();
        var materials = mr.materials;
        materials[1].SetTexture(ShaderPropertyID._MainTex, texture);
        materials[1].SetTexture(ShaderPropertyID._Illum, texture);
        materials[1].SetFloat(SpecInt, 0.1f);
        mr.materials = materials;
    }
}