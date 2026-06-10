using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Items.Resources;

[PrefabClass]
public static class DormantNeuralMatter
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("DormantNeuralMatter")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("DormantPlagueMatterIcon"))
        .WithSizeInInventory(new Vector2int(2, 2));

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        prefab.Register();
        
        BaseBioReactor.charge[Info.TechType] = 1400;
    }

    private static GameObject GetPrefab()
    {
        var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("DormantNeuralMatter"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj, 8, 1.2f);
        PrefabUtils.AddWorldForces(obj, 14, 0, 2f).useRigidbody.angularDrag = 1;
        obj.AddComponent<Pickupable>();
        PrefabUtils.AddResourceTracker(obj, Info.TechType);
        return obj;
    }
}