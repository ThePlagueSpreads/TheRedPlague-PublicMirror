using System.Collections;
using Nautilus.Assets;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Plants;

[PrefabClass]
public static class MeatShroom
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("MeatShroom")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("MeatShroomIcon")).WithFolderPath(TrpPrefabFolders.Environment.Flesh);

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.Register();
        CraftDataHandler.SetBackgroundType(Info.TechType, CustomBackgroundTypes.PlagueItem);
    }

    private static IEnumerator CreatePrefab(IOut<GameObject> prefab)
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("MeatShroom"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Far);
        MaterialUtils.ApplySNShaders(obj, 6);
        var edible = obj.AddComponent<Eatable>();
        edible.decomposes = false;
        edible.foodValue = 3;
        edible.waterValue = 4;
        obj.AddComponent<Pickupable>();
        PrefabUtils.AddWorldForces(obj, 2, 1, 0.1f, true);
        PrefabUtils.AddResourceTracker(obj, Info.TechType);
        prefab.Set(obj);
        yield break;
    }
}