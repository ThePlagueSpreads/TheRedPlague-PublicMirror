using System.Collections;
using System.Collections.Generic;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Extensions;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Mono.Buildables.PdaExploder;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Buildable;

public static class PdaExploder
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("PdaExploder")
        .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("PdaExploderIcon"));
    
    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetRecipe(new RecipeData(new Ingredient(TechType.TitaniumIngot, 1),
            new Ingredient(TechType.Sulphur, 3), new Ingredient(TechType.CopperWire, 1)));
        prefab.SetPdaGroupCategoryAfter(TechGroup.ExteriorModules, TechCategory.ExteriorModule, TechType.FarmingTray);

        prefab.SetGameObject(GetPrefab);

        KnownTechHandler.SetAnalysisTechEntry(new KnownTech.AnalysisTech
        {
            techType = Info.TechType,
            unlockSound = KnownTechHandler.DefaultUnlockData.BasicUnlockSound,
            unlockPopup = Plugin.AssetBundle.LoadAsset<Sprite>("PdaExploderPopup"),
            unlockTechTypes = new List<TechType>(),
            unlockMessage = KnownTechHandler.DefaultUnlockData.BlueprintUnlockMessage
        });
        prefab.Register();
    }

    private static IEnumerator GetPrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("PdaExploderPrefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Near);
        MaterialUtils.ApplySNShaders(prefab, 7);
        var model = prefab.transform.Find("ExplosiveCase").gameObject;
        var constructable = PrefabUtils.AddConstructable(prefab, Info.TechType,
            ConstructableFlags.Outside | ConstructableFlags.Ground
                                       | ConstructableFlags.AllowedOnConstructable | ConstructableFlags.Rotatable,
            model);
        constructable.placeMinDistance = 3;
        constructable.placeMaxDistance = 10;
        constructable.placeDefaultDistance = 4;

        var bounds = prefab.AddComponent<ConstructableBounds>();
        bounds.bounds = new OrientedBounds(new Vector3(0, 0.977f, 0), Quaternion.identity, new Vector3(1.33f, 0.65f, 1f));

        var cinematicController = prefab.AddComponent<PlayerCinematicController>();
        var animationParent = prefab.transform.Find("PdaExplosiveCinematic");
        cinematicController.animator = animationParent.GetComponent<Animator>();
        cinematicController.playInVr = true;
        cinematicController.animParam = "animation";
        cinematicController.animParamReceivers = System.Array.Empty<GameObject>();
        cinematicController.enforceCinematicModeEnd = true;
        cinematicController.animatedTransform = animationParent.SearchChild("CameraAnimatedTransform");
        
        var exploder = prefab.AddComponent<PdaExploderBehaviour>();
        exploder.cinematicObject = animationParent.gameObject;
        exploder.triggerType = CinematicModeTriggerBase.TriggerType.HandTarget;
        exploder.cinematicController = cinematicController;
        exploder.crateModel = prefab.transform.Find("ExplosiveCase").gameObject;
        exploder.handText = "ExplodePdaPrompt";
        exploder.colliders = prefab.GetComponentsInChildren<Collider>();
        exploder.pdaHolder = prefab.transform.Find("ExplosiveCase/PdaHolder");
        
        result.Set(prefab);
        yield return null;
    }
}