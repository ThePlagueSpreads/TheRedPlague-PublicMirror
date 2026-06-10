using Nautilus.Assets;
using TheRedPlague.Utilities;
using UnityEngine;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class Ham
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("RedPlagueHam")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("Ham"));
    
    [PrefabRegistration]
    private static void Register()
    {
        var hamPrefab = new CustomPrefab(Info);
        hamPrefab.SetGameObject(() => TrpPrefabUtils.CreateLootCubePrefab(Info));
        hamPrefab.Register();
    }
}