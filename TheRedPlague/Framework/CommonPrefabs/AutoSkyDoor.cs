using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using UnityEngine;

namespace TheRedPlague.Framework.CommonPrefabs;

[PrefabClass]
public static class AutoSkyDoor
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("WreckDoor_AutoSky");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(new CloneTemplate(Info, "b86d345e-0517-4f6e-bea4-2c5b40f623b4")
        {
            ModifyPrefab = obj =>
            {
                obj.GetComponent<SkyApplier>().anchorSky = Skies.Auto;
            }
        });
        WorldEntityDatabaseHandler.AddCustomInfo(Info.ClassID, Info.TechType, Vector3.one, false, LargeWorldEntity.CellLevel.Medium);
        prefab.Register();
    }
}