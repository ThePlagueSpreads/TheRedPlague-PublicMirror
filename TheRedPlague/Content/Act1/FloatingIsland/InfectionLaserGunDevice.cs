using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace TheRedPlague.Content.Act1.FloatingIsland;

[PrefabClass]
public static class InfectionLaserGunDevice
{
    [PrefabRegistration]
    private static void Register()
    {
        var infectionLaserDevicePrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionLaserDevice")
            .WithFolderPath(TrpPrefabFolders.Precursor));
        var infectionLaserDeviceTemplate =
            new CloneTemplate(infectionLaserDevicePrefab.Info, "22fb9ee9-690d-426c-844f-a80e527b5fe6");
        infectionLaserDeviceTemplate.ModifyPrefab += go =>
        {
            go.GetComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            Object.DestroyImmediate(go.GetComponent<PrecursorGunStoryEvents>());
            Object.DestroyImmediate(go.GetComponent<PrecursorGunAim>());
            Object.DestroyImmediate(go.GetComponent<AnimateOnStoryGoal>());
            var modelParent = go.transform.Find("precursor_base/Instances");
            modelParent.Find("precursor_base_22").gameObject.SetActive(false);
            modelParent.Find("precursor_base_23").gameObject.SetActive(false);
            modelParent.Find("precursor_base_24").gameObject.SetActive(false);
            modelParent.Find("precursor_base_25").gameObject.SetActive(false);
        };
        infectionLaserDevicePrefab.SetGameObject(infectionLaserDeviceTemplate);
        infectionLaserDevicePrefab.SetSpawns(new SpawnLocation(new Vector3(-80.000f, 304, -47.790f),
            new Vector3(0, 0, 353), Vector3.one * 0.3f));
        infectionLaserDevicePrefab.Register();
    }
}