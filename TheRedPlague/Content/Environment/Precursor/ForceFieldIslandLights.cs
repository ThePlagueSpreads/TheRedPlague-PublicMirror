using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using UnityEngine;

namespace TheRedPlague.Content.Environment.Precursor;

[PrefabClass]
public static class ForceFieldIslandLights
{
    [PrefabRegistration]
    private static void Register()
    {
        var forceFieldIslandLightPrefab = new CustomPrefab(PrefabInfo.WithTechType("ForceFieldIslandLight")
            .WithFolderPath(TrpPrefabFolders.Precursor));
        var forceFieldIslandLightTemplate =
            new CloneTemplate(forceFieldIslandLightPrefab.Info, "081ef6c1-aa78-46fd-a20f-a6b63ca5c5f3");
        forceFieldIslandLightTemplate.ModifyPrefab += go =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponent<SkyApplier>().customSkyPrefab = null;
        };
        forceFieldIslandLightPrefab.SetGameObject(forceFieldIslandLightTemplate);
        forceFieldIslandLightPrefab.Register();

        var forceFieldIslandLight2Prefab = new CustomPrefab(PrefabInfo.WithTechType("ForceFieldIslandLight2")
            .WithFolderPath(TrpPrefabFolders.Precursor));
        var forceFieldIslandLight2Template =
            new CloneTemplate(forceFieldIslandLight2Prefab.Info, "081ef6c1-aa78-46fd-a20f-a6b63ca5c5f3");
        forceFieldIslandLight2Template.ModifyPrefab += go =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponentInChildren<Light>().enabled = false;
            go.GetComponent<SkyApplier>().customSkyPrefab = null;
        };
        forceFieldIslandLight2Prefab.SetGameObject(forceFieldIslandLight2Template);
        forceFieldIslandLight2Prefab.Register();
    }
}