using Nautilus.Handlers;
using TheRedPlague.Content.Environment.Corpses;
using TheRedPlague.Content.Equipment.PlagueArmor;
using UnityEngine;

namespace TheRedPlague.Content.Fragments;

[PrefabClass]
public class PlagueArmorFragment : FloatingCorpsePrefab
{
    public PlagueArmorFragment() : base("PlagueArmorFragment", "PlagueArmorFragment")
    {
        Info.WithFolderPath(TrpPrefabFolders.FragmentsAndDataboxes);
        PDAHandler.AddCustomScannerEntry(Info.TechType, BoneArmor.Info.TechType,
            true, 3, 2.5f, false);
    }

    [PrefabRegistration]
    private static void RegisterPrefab()
    {
        new PlagueArmorFragment().Register();
    }

    protected override void ModifyPrefab(GameObject prefab)
    {
        var material = BoneArmor.GetMaterial();

        var renderers = prefab.transform.Find("BoneArmor_Prefab").GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material = material;
        }

        var light = prefab.AddComponent<Light>();
        light.intensity = 2;
        light.range = 3;
        light.color = Color.red;
        if (MiscSettings.flashes)
        {
            var flicker = prefab.AddComponent<LightFlicker>();
            flicker.maxLightIntensity = 3;
            flicker.minFlickerSpeed = 0.05f;
            flicker.maxFlickerSpeed = 0.2f;
        }
    }
}