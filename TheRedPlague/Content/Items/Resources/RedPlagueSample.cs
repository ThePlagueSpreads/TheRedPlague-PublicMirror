using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.Gadgets;
using UnityEngine;

namespace TheRedPlague.Content.Items.Resources;

[PrefabClass]
public static class RedPlagueSample
{
    private const string SuperEnormouslyLongNameForTheNameForOctoʼsThingThatOctoNamedBecauseHeExportedTheImageMultipleTimesAndNeededDifferentNamesForEachOneSoHeWouldntGetConfusedPlagueSampleSpriteImageAssetNameDotComSlashLogin = "PlagueSampleIconTransparent2RealLastRenderForRealThisTime_Final_2_Last_Finished";
    
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("RedPlagueSample")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>(SuperEnormouslyLongNameForTheNameForOctoʼsThingThatOctoNamedBecauseHeExportedTheImageMultipleTimesAndNeededDifferentNamesForEachOneSoHeWouldntGetConfusedPlagueSampleSpriteImageAssetNameDotComSlashLogin))
        .WithSizeInInventory(new Vector2int(1, 2));

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.SetBackgroundType(CustomBackgroundTypes.PlagueItem);
        prefab.Register();
        
        BaseBioReactor.charge[Info.TechType] = 300;
    }

    private static GameObject GetGameObject()
    {
        var obj = Object.Instantiate(
            AssetBundles.Core.LoadAsset<GameObject>("RedPlagueSamplePrefab"));
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(obj, 6f);
        obj.AddComponent<Pickupable>();
        var rb = obj.AddComponent<Rigidbody>();
        rb.mass = 2;
        rb.useGravity = false;
        rb.isKinematic = true;
        var wf = obj.AddComponent<WorldForces>();
        wf.useRigidbody = rb;
        return obj;
    }
}