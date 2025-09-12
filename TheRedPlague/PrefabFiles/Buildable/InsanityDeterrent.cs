using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Mono.Buildables.InsanityDeterrent;
using TheRedPlague.Mono.Insanity;
using TheRedPlague.PrefabFiles.Creatures;
using UnityEngine;

namespace TheRedPlague.PrefabFiles.Buildable;

public static class InsanityDeterrent
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("InsanityDeterrent")
        .WithIcon(Plugin.AssetBundle.LoadAsset<Sprite>("InsanityDeterrentIcon"));

    public static void Register()
    {
        var prefab = new CustomPrefab(Info);
        prefab.SetGameObject(CreatePrefab);
        prefab.SetPdaGroupCategoryBefore(TechGroup.InteriorModules, TechCategory.InteriorModule,
            TechType.BatteryCharger);
        prefab.SetRecipe(new RecipeData(
            new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.Gold, 1),
            new Ingredient(TechType.UraniniteCrystal, 2),
            new Ingredient(ConsciousNeuralMatter.Info.TechType, 1)));
        prefab.Register();
        
        KnownTechHandler.SetAnalysisTechEntry(Info.TechType, System.Array.Empty<TechType>(),
            KnownTechHandler.DefaultUnlockData.BlueprintUnlockSound,
            Plugin.AssetBundle.LoadAsset<Sprite>("InsanityDeterrentPopup"));
    }

    private static IEnumerator CreatePrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(Plugin.AssetBundle.LoadAsset<GameObject>("InsanityDeterrentPrefab"));
        prefab.SetActive(false);
        PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
        MaterialUtils.ApplySNShaders(prefab, 7f, 1f, 1.3f);

        var machine = PrefabUtils.AddConstructable<InsanityDeterrentMachine>(prefab, Info.TechType,
            ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Base |
            ConstructableFlags.Rotatable | ConstructableFlags.Inside,
            prefab.transform.Find("InsanityDeterrenceMachine").gameObject);
        
        var deterrenceZone = prefab.AddComponent<InsanityOverrideZone>();
        deterrenceZone.radius = 10;
        deterrenceZone.onlyIndoors = true;
        machine.overrideZone = deterrenceZone;

        var sound = prefab.transform.Find("SoundEmitter").gameObject.AddComponent<FMOD_CustomLoopingEmitter>();
        sound.SetAsset(AudioUtils.GetFmodAsset("InsanityDeterrentWorking"));
        sound.playOnAwake = false;
        sound.followParent = true;
        sound.restartOnPlay = false;
        machine.soundEmitter = sound;
        
        result.Set(prefab);

        yield break;
    }
}