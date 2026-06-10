using Nautilus.Assets;
using TheRedPlague.Framework.CommonPrefabs;
using TheRedPlague.Utilities;

namespace TheRedPlague.Content.Items.Consumable;

[PrefabClass]
public static class BioBattery
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("BioBattery");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(() => TrpPrefabUtils.CreateLootCubePrefab(Info));
        prefab.Register();
        
        new DataboxPrefab("BioBatteryDatabox", Info.TechType).Register();
    }
}