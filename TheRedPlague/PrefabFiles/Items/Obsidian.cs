using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Items;

public static class Obsidian
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Obsidian")
        .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("ObsidianIcon"));

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.SetSpawns(new LootDistributionData.BiomeData[]
        {
            new()
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Ceiling,
                probability = 0.3f,
                count = 1
            },
            new()
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Lava,
                probability = 0.5f,
                count = 1
            },
            new()
            {
                biome = BiomeType.ActiveLavaZone_Chamber_Floor,
                probability = 0.15f,
                count = 1
            },
            new()
            {
                biome = BiomeType.InactiveLavaZone_Corridor_Wall,
                probability = 0.15f,
                count = 1
            },
            new()
            {
                biome = BiomeType.InactiveLavaZone_Corridor_Floor,
                probability = 0.2f,
                count = 1
            }
        });
        prefab.Register();
    }

    private static GameObject CreatePrefab()
    {
        var obj = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("ObsidianPrefab"));
        obj.SetActive(false);
        
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        PrefabUtils.AddWorldForces(obj, 20, 1, 1, true);
        MaterialUtils.ApplySNShaders(obj, 7);
        
        obj.AddComponent<Pickupable>();
        
        return obj;
    }
}