using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Wreck;

[PrefabClass]
public static class RustyPack
{
    private static readonly int DetailDiffuseTex = Shader.PropertyToID("_DetailDiffuseTex");
    private static readonly int DetailDiffuseSt = Shader.PropertyToID("_DetailDiffuseTex_ST");
    private static readonly int DetailSpecTex = Shader.PropertyToID("_DetailSpecTex");
    private static readonly int DetailSpecSt = Shader.PropertyToID("_DetailSpecTex_ST");
    private static readonly int DetailNormalTex = Shader.PropertyToID("_DetailBumpTex");
    private static readonly int DetailIntensities = Shader.PropertyToID("_DetailIntensities");

    [PrefabRegistration]
    private static void Register()
    {
        RegisterPrefab("RustyWreckLarge", "740f385f-ae35-4e06-a88f-023db82cbf6b",
            ["explorable_wreckage/Explorable_wreckage_exterior"],
            [0, 1, 2, 9]);

        RegisterPrefab("RustyWreckHull1", "aad81104-9f02-47ec-8095-e99ede823b90",
            ["explorable_wreckage_modular_hull_01"],
            [0]);

        RegisterPrefab("RustyWreckHull2", "76825855-c939-48ae-812d-79b6d0529dd9",
            ["explorable_wreckage_modular_hull_02"],
            [0, 2, 3]);

        RegisterPrefab("RustyMetalStrip", "275a6441-ed4f-4a1c-bd98-7a9728f8d625",
            null,
            null);

        RegisterPrefab("RustyMetalPlate", "72437ebc-7d61-49b8-bac4-cb7f3af3af8e",
            ["Starship_exploded_debris_22"],
            [0, 1, 8]);

        RegisterPrefab("RustyCurvedMetalPlate", "5cd34124-935f-4628-b694-a266bc2f5517",
            null,
            [0, 2]);
    }

    private static void RegisterPrefab(
        string classId,
        string originalClassId,
        string[] targetPaths,
        int[] allowedSlots)
    {
        var info = new PrefabInfo(classId, classId + "Prefab", TechType.None)
            .WithFolderPath(TrpPrefabFolders.Environment.Rusty);

        var prefab = new CustomPrefab(info);
        var template = new CloneTemplate(info, originalClassId);

        // horrendous code right here, my bad
        template.ModifyPrefab = obj =>
        {
            var diffuse = AssetBundles.Common.LoadAsset<Texture2D>("RustyDetailAlbedo");
            var normal = AssetBundles.Common.LoadAsset<Texture2D>("DetailNormal_Noise");
            
            IEnumerable<Renderer> renderers;

            // get children
            if (targetPaths is { Length: > 0 })
            {
                var list = new List<Renderer>();

                foreach (var path in targetPaths)
                {
                    var t = obj.transform.Find(path);
                    if (t == null) continue;

                    list.Add(t.GetComponent<Renderer>());
                }

                renderers = list;
            }
            else
            {
                renderers = obj.GetComponentsInChildren<Renderer>(true);
            }

            // filter slots
            foreach (var r in renderers)
            {
                if (r == null || r.materials == null)
                    continue;

                var mats = r.materials;

                for (int i = 0; i < mats.Length; i++)
                {
                    if (allowedSlots is { Length: > 0 })
                    {
                        bool allowed = false;

                        for (int j = 0; j < allowedSlots.Length; j++)
                        {
                            if (allowedSlots[j] == i)
                            {
                                allowed = true;
                                break;
                            }
                        }

                        if (!allowed)
                            continue;
                    }

                    var mat = mats[i];
                    if (mat == null) continue;
                    
                    mat.EnableKeyword("UWE_DETAILMAP");
                    mat.SetTexture(DetailDiffuseTex, diffuse);
                    mat.SetTexture(DetailSpecTex, diffuse);
                    mat.SetTexture(DetailNormalTex, normal);
                    mat.SetVector(DetailDiffuseSt, new Vector4(0.5f, 0.5f));
                    mat.SetVector(DetailSpecSt, new Vector4(0.5f, 0.5f));
                    var originalColor = mat.color;
                    mat.color = new Color(originalColor.r * 0.57f, originalColor.g * 0.57f, originalColor.b * 0.57f, originalColor.a);
                    // in order: diffuse, normal, specular
                    mat.SetVector(DetailIntensities, new Vector4(0.982f, 0.542f, 0.802f, 0));
                    mats[i] = mat;
                }

                r.materials = mats;
            }
        };

        prefab.SetGameObject(template);
        prefab.Register();
    }
}