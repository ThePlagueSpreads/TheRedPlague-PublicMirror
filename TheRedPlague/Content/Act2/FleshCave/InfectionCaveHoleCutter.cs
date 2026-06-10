using System;
using Nautilus.Assets;
using Nautilus.Utility;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.Environment;
using UnityEngine;

namespace TheRedPlague.Content.Act2.FleshCave;

[PrefabClass]
public static class InfectionCaveHoleCutter
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("InfectionCaveHole");

    [PrefabRegistration]
    private static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();
        
        new PropDestroyer(PrefabInfo.WithTechType("InfectionCavePropDestroyer"), new[]
        {
            TechType.Salt, TechType.DrillableSalt, TechType.DrillableTitanium, TechType.DrillableLead, TechType.Quartz,
            TechType.ScrapMetal, AmalgamatedBone.Info.TechType, TechType.DrillableSilver, TechType.DrillableQuartz
        }, Array.Empty<string>(), 22).Register();
    }

    private static GameObject GetGameObject()
    {
        var gameObject = new GameObject(Info.ClassID);
        gameObject.SetActive(false);
        PrefabUtils.AddBasicComponents(gameObject, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Far);
        var holeCutter = gameObject.AddComponent<CutHoleInTerrain>();
        holeCutter.holeRadius = 35;
        holeCutter.disableRenderers = true;
        holeCutter.disableColliders = true;
        holeCutter.disableGrass = true;
        return gameObject;
    }
}