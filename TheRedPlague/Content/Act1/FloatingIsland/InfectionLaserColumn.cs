using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using TheRedPlague.Content.Infection;
using UnityEngine;

namespace TheRedPlague.Content.Act1.FloatingIsland;

[PrefabClass]
public static class InfectionLaserColumn
{
    [PrefabRegistration]
    private static void Register()
    {
        var infectionLaserColumnPrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionLaserColumn")
            .WithFolderPath(TrpPrefabFolders.Precursor));
        var infectionLaserColumnTemplate =
            new CloneTemplate(infectionLaserColumnPrefab.Info, "3d625dbb-d15a-4351-bca0-0a0526f01e6e");
        infectionLaserColumnTemplate.ModifyPrefab += go =>
        {
            go.GetComponents<DisableEmissiveOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<LightIntensityOnStoryGoal>().ForEach(c => c.enabled = false);
            go.GetComponents<PrecursorGunCentralColumn>().ForEach(c => c.enabled = false);
            go.transform
                .Find(
                    "precursor_column_maze_08_06_08_hlpr/precursor_column_maze_08_06_08_ctrl/precursor_column_maze_08_06_08/light")
                .GetComponent<Light>().color = Color.red;
            go.transform
                .Find(
                    "precursor_column_maze_08_06_08_hlpr/precursor_column_maze_08_06_08_ctrl/precursor_column_maze_08_06_08/precursor_column_maze_08_06_08_glass_01")
                .GetComponent<Renderer>().material.color = Color.red;
            var renderer = go.transform
                .Find(
                    "precursor_column_maze_08_06_08_hlpr/precursor_column_maze_08_06_08_ctrl/precursor_column_maze_08_06_08/precursor_column_maze_08_06_08_glass_02_hlpr/precursor_column_maze_08_06_08_glass_02_ctrl/precursor_column_maze_08_06_08_glass_02")
                .GetComponent<Renderer>();
            var materials = renderer.materials;
            materials[1].color = Color.red;
            renderer.materials = materials;
            go.AddComponent<InfectAnything>();
        };
        infectionLaserColumnPrefab.SetGameObject(infectionLaserColumnTemplate);
        infectionLaserColumnPrefab.SetSpawns(new SpawnLocation(new Vector3(-59.640f, 301.000f, -24.840f),
            Vector3.up * 343, Vector3.one * 0.7f));
        infectionLaserColumnPrefab.Register();
    }
}