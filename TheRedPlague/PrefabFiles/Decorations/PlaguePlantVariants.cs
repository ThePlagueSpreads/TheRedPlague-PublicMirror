using System;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Decorations;

public static class PlaguePlantVariants
{
    private static readonly int SpecColor = Shader.PropertyToID("_SpecColor");
    private static readonly int GlowColor = Shader.PropertyToID("_GlowColor");
    private static readonly int SpecInt = Shader.PropertyToID("_SpecInt");
    private static readonly int Fresnel = Shader.PropertyToID("_Fresnel");

    public static void Register()
    {
        RegisterVariant("Patrick1", "57a31bf5-5b86-4bf6-9a14-9291c6e8a79c", ModifyPrefab_Patrick);
        RegisterVariant("Patrick2", "57a31bf5-5b86-4bf6-9a14-9291c6e8a79c", ModifyPrefab_Patrick);
        RegisterVariant("Patrick3", "57a31bf5-5b86-4bf6-9a14-9291c6e8a79c", ModifyPrefab_Patrick);
        RegisterVariant("PlaguedLilly", "f97bf790-a5bd-4e7f-a5e8-9fca1b37f81c", ModifyPrefab_Lilly);
        RegisterVariant("BloodAmoebaPurple", "375a4ade-a7d9-401d-9ecf-08e1dce38d6b", ModifyPrefab_AmoebaPurple);
        RegisterVariant("BloodAmoebaGreen", "375a4ade-a7d9-401d-9ecf-08e1dce38d6b", ModifyPrefab_AmoebaGreen);
        RegisterVariant("PlagueClaw1", "04d69bba-6c65-414d-bdaf-cc9b53fb9f3b", ModifyPrefab_Claw);
        RegisterVariant("PlagueClaw2", "1fd81ec0-16be-4667-a818-0ebfcc74170b", ModifyPrefab_Claw);
        RegisterVariant("PlagueClaw3", "b628d104-dcad-4fac-8a12-d0c4ef473d93", ModifyPrefab_Claw);
        RegisterVariant("BloodyStalactite1", "92b48933-f89e-4d9d-a432-323785d7cdd2", ModifyPrefab_Stalactites);
        RegisterVariant("BloodyStalactite2", "90f032d5-193a-4a00-b892-3d80be04dca3", ModifyPrefab_Stalactites);
        RegisterVariant("BloodyStalactite3", "c081e286-bb51-4cc6-b48c-ea03d817ccf9", ModifyPrefab_Stalactites);
        RegisterVariant("VeinRoots1", "9dafed34-133e-43e4-9234-f012ec3872e2", ModifyPrefab_Roots);
        RegisterVariant("VeinRoots1a", "04a2d0ec-8036-4945-812b-5dc51d17c5f6", ModifyPrefab_Roots);
        RegisterVariant("VeinRoots2", "41a08c65-ad37-4095-bd48-a8025fe4d016", ModifyPrefab_Roots);
        RegisterVariant("VeinRoots3", "690e2455-05db-4c69-a48a-288b0a49082a", ModifyPrefab_Roots);
    }

    private static void RegisterVariant(string classId, string cloneClassId, Action<GameObject> modifyPrefab)
    {
        var prefab = new CustomPrefab(PrefabInfo.WithTechType(classId));
        prefab.SetGameObject(new CloneTemplate(prefab.Info, cloneClassId)
        {
            ModifyPrefab = modifyPrefab
        });
        prefab.Register();
    }

    private static void ModifyPrefab_Patrick(GameObject prefab)
    {
        foreach (var renderer in prefab.GetComponentsInChildren<Renderer>())
        {
            foreach (var material in renderer.materials)
            {
                if (material.name.Contains("alpha"))
                {
                    material.color = Color.clear;
                    continue;
                }

                material.color = new Color(1, 0f, 0f);
                material.SetColor(GlowColor, new Color(3, 0, 0));
            }
        }
    }

    private static void ModifyPrefab_Lilly(GameObject prefab)
    {
        var material = prefab.GetComponentInChildren<Renderer>().material;
        material.color = new Color(1, 0f, 0f);
        material.SetColor(SpecColor, new Color(1, 0, 0.9f));
        material.SetColor(GlowColor, new Color(6, 0.62f, 0.333f));
        prefab.GetComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
    }

    private static void ModifyPrefab_Claw(GameObject prefab)
    {
        var material = prefab.GetComponentInChildren<Renderer>().material;
        material.color = new Color(1, 0.3f, 0.3f);
        material.DisableKeyword("MARMO_EMISSION");
    }

    private static void ModifyPrefab_AmoebaPurple(GameObject prefab)
    {
        prefab.transform.Find("lost_river_plant_04/lost_river_plant_04").GetComponent<Renderer>().material.color =
            new Color(1, 0, 0);
        prefab.transform.Find("lost_river_plant_04/lost_river_plant_04_membrane").GetComponent<Renderer>().material
            .SetColor(SpecColor, new Color(1, 0, 0));
    }

    private static void ModifyPrefab_AmoebaGreen(GameObject prefab)
    {
        prefab.transform.Find("lost_river_plant_04/lost_river_plant_04").GetComponent<Renderer>().material.color =
            new Color(1, 0.38f, 0.285f);
        prefab.transform.Find("lost_river_plant_04/lost_river_plant_04_membrane").GetComponent<Renderer>().material
            .SetColor(SpecColor, new Color(0.7f, 0.7f, 0.3f));
    }

    private static void ModifyPrefab_Stalactites(GameObject prefab)
    {
        var renderers = prefab.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var material = renderer.material;
            material.color = new Color(1, 0.3f, 0.3f);
            material.SetColor(ShaderPropertyID._SpecColor, new Color(1, 0.3f, 0.3f));
        }
    }
    
    private static void ModifyPrefab_Roots(GameObject prefab)
    {
        var renderers = prefab.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var material = renderer.material;
            material.color = new Color(0.4f, 0.15f, 0.2f);
            material.SetColor(ShaderPropertyID._SpecColor, new Color(1f, 0.15f, 0.2f));
            material.SetFloat(SpecInt, 7);
            material.SetFloat(Fresnel, 0.7f);
        }
    }
}