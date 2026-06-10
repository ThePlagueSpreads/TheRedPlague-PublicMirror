using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using TheRedPlague.Framework.UI;
using UnityEngine;

namespace TheRedPlague.Content.Unused;

[PrefabClass]
public static class AdminDropPodBeacon
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("AdminDropPodBeacon");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        var template = new CloneTemplate(Info, TechType.Beacon)
        {
            ModifyPrefab = ModifyPrefab
        };
        prefab.SetGameObject(template);
        prefab.Register();
    }

    private static void ModifyPrefab(GameObject prefab)
    {
        var ping = prefab.GetComponent<PingInstance>();
        ping.SetType(AdministratorDropPod.PingType);
        Object.DestroyImmediate(prefab.GetComponent<Beacon>());
        prefab.AddComponent<SetBeaconColor>().colorIndex = 2;
    }
}