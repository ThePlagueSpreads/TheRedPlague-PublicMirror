using System.Collections;
using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Mono.Buildables.SatelliteCommunicator;
using TheRedPlague.PrefabFiles.Items;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Buildable;

public static class SatelliteCommunicationDevice
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SatelliteCommunicationDevice")
        .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("SatelliteCommunicatorIcon"));

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetRecipe(new RecipeData(new Ingredient(TechType.TitaniumIngot, 1),
            new Ingredient(TechType.Beacon, 1), new Ingredient(TechType.AdvancedWiringKit, 1),
            new Ingredient(PlagueCatalyst.Info.TechType, 2),
            new Ingredient(ModPrefabs.GoldTabletInfo.TechType, 1)));
        prefab.SetPdaGroupCategoryAfter(TechGroup.ExteriorModules, TechCategory.ExteriorModule, TechType.FarmingTray);

        prefab.SetGameObject(GetPrefab);

        KnownTechHandler.SetAnalysisTechEntry(new KnownTech.AnalysisTech
        {
            techType = Info.TechType,
            unlockSound = KnownTechHandler.DefaultUnlockData.BasicUnlockSound,
            unlockPopup = Plugin.AssetBundle.LoadAsset<Sprite>("SatelliteCommunicatorPopup"),
            unlockTechTypes = new List<TechType>(),
            unlockMessage = KnownTechHandler.DefaultUnlockData.BlueprintUnlockMessage
        });
        prefab.Register();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("SatelliteCommunicatorPrefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(prefab, 7, 1, 1, new IgnoreParticleSystemsModifier());
        var model = prefab.transform.Find("Pivot/SatelliteCommunicator").gameObject;
        var constructable = PrefabUtils.AddConstructable(prefab, Info.TechType,
            ConstructableFlags.Outside | ConstructableFlags.Ground | ConstructableFlags.AllowedOnConstructable,
            model);
        constructable.forceUpright = true;
        constructable.placeMinDistance = 3;
        constructable.placeMaxDistance = 12;
        constructable.placeDefaultDistance = 5;

        var bounds = prefab.AddComponent<ConstructableBounds>();
        bounds.bounds = new OrientedBounds(new Vector3(0, 6.44f, 0), Quaternion.identity, new Vector3(5, 6.2f, 5));

        var behaviour = prefab.AddComponent<SatelliteCommunicatorBehaviour>();
        behaviour.animator = model.GetComponent<Animator>();
        behaviour.mainRenderer = model.transform.Find("SatelliteCommunicator").GetComponent<Renderer>();
        behaviour.beaconEnableParticleSystem =
            prefab.transform.Find("BeamActivateParticleSystem").GetComponent<ParticleSystem>();

        var failVoiceNotification = prefab.AddComponent<VoiceNotification>();
        failVoiceNotification.text = "SatelliteCommunicatorFail";
        failVoiceNotification.sound = AudioUtils.GetFmodAsset("SatelliteCommunicatorFail");
        behaviour.failVoiceNotification = failVoiceNotification;

        var beamParent = prefab.transform.Find("Beam");
        var beacon = beamParent.gameObject.AddComponent<SatelliteCommunicatorBeacon>();
        beacon.renderers = beamParent.GetComponentsInChildren<Renderer>(true);
        behaviour.beacon = beacon;

        result.Set(prefab);
        yield return null;
    }
}