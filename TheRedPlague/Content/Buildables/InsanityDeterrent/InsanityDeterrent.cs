using System.Collections;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using TheRedPlague.Content.Creatures.Neural;
using TheRedPlague.Content.PlayerInfection;
using TheRedPlague.Framework.CommonPrefabs;
using UnityEngine;

namespace TheRedPlague.Content.Buildables.InsanityDeterrent;

[PrefabClass]
public static class InsanityDeterrent
{
    public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("InsanityDeterrent")
        .WithIcon(AssetBundles.Core.LoadAsset<Sprite>("InsanityDeterrentIcon"));

    [PrefabRegistration]
    private static void Register()
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
            AssetBundles.Core.LoadAsset<Sprite>("InsanityDeterrentPopup"));
        
        new DataboxPrefab("InsanityDeterrentDatabox", Info.TechType).Register();
    }

    private static IEnumerator CreatePrefab(IOut<GameObject> result)
    {
        var prefab = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("InsanityDeterrentPrefab"));
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