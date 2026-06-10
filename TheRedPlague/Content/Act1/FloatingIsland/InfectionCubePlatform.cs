using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using TheRedPlague.Framework.Migration;
using UnityEngine;

namespace TheRedPlague.Content.Act1.FloatingIsland;

[PrefabClass]
public static class InfectionCubePlatform
{
    [PrefabRegistration]
    private static void Register()
    {
        var infectionCubePlatform = new CustomPrefab(PrefabInfo.WithTechType("InfectionCubePlatform")
            .WithFolderPath(TrpPrefabFolders.Precursor));
        var infectionCubeTemplate =
            new CloneTemplate(infectionCubePlatform.Info, "6b0104e8-979e-46e5-bc17-57c4ac2e6e39");
        infectionCubeTemplate.ModifyPrefab += go =>
        {
            Object.DestroyImmediate(go.GetComponent<DisableEmissiveOnStoryGoal>());
            go.transform.Find("CullVolumeManager").gameObject.SetActive(false);
            go.AddComponent<DestroyIfIdMatches>().ids = new[] { "1071b882-3848-4984-b9d4-b5e0a9a81dfb" };
        };
        infectionCubePlatform.SetGameObject(infectionCubeTemplate);
        infectionCubePlatform.Register();
    }
}