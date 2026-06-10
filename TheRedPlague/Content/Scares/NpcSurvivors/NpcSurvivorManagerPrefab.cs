using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Scares.NpcSurvivors;

[PrefabClass]
public static class NpcSurvivorManagerPrefab
{
    private static PrefabInfo Info { get; } = PrefabInfo.WithTechType("NpcSurvivorManager");

    [PrefabRegistration]
    private static void Register()
    {
        var npcSurvivorManager = new CustomPrefab(Info);
        npcSurvivorManager.SetGameObject(GetNpcSurvivorManagerPrefab);
        npcSurvivorManager.SetSpawns(new SpawnLocation(Vector3.zero));
        npcSurvivorManager.Register();
    }
    
    private static IEnumerator GetNpcSurvivorManagerPrefab(IOut<GameObject> prefab)
    {
        var go = new GameObject(Info.ClassID);
        go.SetActive(false);
        PrefabUtils.AddBasicComponents(go, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Global);
        go.AddComponent<NpcSurvivorManager>();
        var johnKyle = go.AddComponent<NpcSurvivor>();
        johnKyle.survivorName = "JohnKyle";
        var sylvie = go.AddComponent<NpcSurvivor>();
        sylvie.survivorName = "Sylvie";
        sylvie.model = NpcSurvivor.ModelType.PrawnSuit;
        var simon = go.AddComponent<NpcSurvivor>();
        simon.survivorName = "Simon";
        prefab.Set(go);
        yield break;
    }

}