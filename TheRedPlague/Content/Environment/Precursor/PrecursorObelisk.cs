using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Framework.Behaviour.Animation;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Precursor;

[PrefabClass]
public class PrecursorObelisk
{
    private static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PrecursorObeliskA")
        .WithFolderPath(TrpPrefabFolders.Precursor);
    
    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetPrefab);
        prefab.Register();
    }

    private static GameObject GetPrefab()
    {
        var prefab = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("PrecursorObeliskA"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);
        prefab.AddComponent<ConstructionObstacle>();
        MaterialUtils.ApplySNShaders(prefab, 7f, 3f);
        return prefab;
    }
}