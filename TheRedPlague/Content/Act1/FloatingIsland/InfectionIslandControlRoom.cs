using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using TheRedPlague.Framework.Behaviour.Fixes;
using UnityEngine;

namespace TheRedPlague.Content.Act1.FloatingIsland;

[PrefabClass]
public static class InfectionIslandControlRoom
{
    [PrefabRegistration]
    private static void Register()
    {
        var infectionControlRoomPrefab = new CustomPrefab(PrefabInfo.WithTechType("InfectionControlRoom")
            .WithFolderPath(TrpPrefabFolders.Precursor));
        var infectionControlRoomTemplate =
            new CloneTemplate(infectionControlRoomPrefab.Info, "963fa3a3-9192-4912-8c8d-d0d98f22ed13");
        infectionControlRoomTemplate.ModifyPrefab += go =>
        {
            go.transform.GetChild(0).gameObject.SetActive(false);
            go.transform.GetChild(3).gameObject.SetActive(false);
            var colliders = go.transform.Find("Control_Room_Collision/Cube").GetComponents<Collider>();
            var colliderIndicesToDisable = new int[] { 11, 12, 19, 49 };
            foreach (var index in colliderIndicesToDisable)
            {
                colliders[index].enabled = false;
            }

            var modelParent = go.transform.Find("Precursor_Gun_ControlRoom");
            var modelIndicesToDisable = new int[] { 66, 67, 69, 70, 78, 100, 110, 111, 112, 113 };
            foreach (var index in modelIndicesToDisable)
            {
                modelParent.GetChild(index).gameObject.SetActive(false);
            }

            go.AddComponent<StupidAssInfectionControlRoomFix>();
        };
        infectionControlRoomPrefab.SetGameObject(infectionControlRoomTemplate);
        infectionControlRoomPrefab.Register();
    }
}