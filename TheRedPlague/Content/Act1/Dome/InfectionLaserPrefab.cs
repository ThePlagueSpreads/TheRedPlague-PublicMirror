using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Act1.Dome;

[PrefabClass]
public static class InfectionLaserPrefab
{
    private static PrefabInfo Info { get; } = PrefabInfo.WithTechType("InfectionLaser");

    [PrefabRegistration]
    private static void Register()
    {
        var infectionLaserPrefab = new CustomPrefab(Info);
        infectionLaserPrefab.SetGameObject(GetInfectionLaserPrefab);
        infectionLaserPrefab.SetSpawns(new SpawnLocation(Vector3.zero));
        infectionLaserPrefab.Register();
    }
    
    private static IEnumerator GetInfectionLaserPrefab(IOut<GameObject> prefab)
    {
        var solarPanelRequest = CraftData.GetPrefabForTechTypeAsync(TechType.SolarPanel);
        yield return solarPanelRequest;
        var solarPanelPrefab = solarPanelRequest.GetResult();
        var obj = Object.Instantiate(solarPanelPrefab.GetComponent<PowerFX>().vfxPrefab);
        obj.SetActive(false);
        PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType,
            LargeWorldEntity.CellLevel.Global);
        var line = obj.GetComponent<LineRenderer>();
        var newMaterial = new Material(line.material);
        newMaterial.color = new Color(1.5f, 0.142f, 0.285f);
        line.material = newMaterial;
        line.widthMultiplier = 30;
        line.endWidth = 20;
        line.SetPositions(new[] { new Vector3(-78.393f, 341.175f, -57.684f), new Vector3(0, 2055, 0) });
        obj.AddComponent<LaserMaterialManager>();
        prefab.Set(obj);
    }
}