using Nautilus.Assets;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Utility.MaterialModifiers;
using TheRedPlague.Content.Creatures;
using TheRedPlague.Content.Creatures.Blisterback;
using TheRedPlague.Content.Creatures.Chaos;
using TheRedPlague.Content.Creatures.CrawlingFlesh;
using TheRedPlague.Content.Creatures.EyeyeCaptain;
using TheRedPlague.Content.Creatures.Grabber;
using TheRedPlague.Content.Creatures.HoverPet;
using TheRedPlague.Content.Creatures.Insectoid;
using TheRedPlague.Content.Creatures.LostReef;
using TheRedPlague.Content.Creatures.MimicGasopod;
using TheRedPlague.Content.Creatures.MrTeeth;
using TheRedPlague.Content.Creatures.Mutants;
using TheRedPlague.Content.Creatures.Neural;
using TheRedPlague.Content.Creatures.PhantomLeviathan;
using TheRedPlague.Content.Creatures.ScaryManny;
using TheRedPlague.Content.Creatures.Stabby;
using TheRedPlague.Content.Creatures.Sucker;
using TheRedPlague.Framework.Behaviour.Spawning;
using UnityEngine;

namespace TheRedPlague.Registration.Prefabs;

[PrefabClass]
public static class CreatureRegistration
{
    [PrefabRegistration]
    private static void RegisterCreatures()
    {
        RegisterNormalCreatures();
        RegisterLostReefCreatures();
    }

    private static void RegisterNormalCreatures()
    {
        var mutantDiver1 = new Mutant(GetInfoForMutants("MutantDiver1"), "MutatedDiver1", Mutant.Settings.Normal, true);
        mutantDiver1.Register();

        var mutantDiver2 = new Mutant(GetInfoForMutants("MutantDiver2"), "MutatedDiver2", Mutant.Settings.Normal, true);
        mutantDiver2.Register();

        var mutantDiver3 = new Mutant(GetInfoForMutants("MutantDiver3"), "MutatedDiver3",
            Mutant.Settings.HeavilyMutated | Mutant.Settings.Large, true);
        mutantDiver3.Register();

        var mutantDiver3Small =
            new Mutant(GetInfoForMutants("MutantDiver3Small"), "MutatedDiver3Small", Mutant.Settings.HeavilyMutated, false);
        mutantDiver3Small.Register();

        var mutantDiver4 = new Mutant(GetInfoForMutants("MutantDiver4"), "MutatedDiver4",
            Mutant.Settings.HeavilyMutated | Mutant.Settings.Large, true);
        mutantDiver4.Register();

        new SuckerPrefab(GetInfoForFleshCreation("Sucker"), true).Register();
        new SuckerPrefab(GetInfoForFleshCreation("SuckerGeneric"), false).Register();
        PDAHandler.AddCustomScannerEntry(SuckerPrefab.ArmoredSuckerTechType, 2, false, "ArmoredSucker");

        new SuckerController(GetInfoForFleshCreation("SuckerController")).Register();
        
        PDAHandler.AddEncyclopediaEntry("Sucker", CustomPdaPaths.PlagueCreationsPath, null,
            AssetBundles.Core.LoadAsset<Texture2D>("SuckerPDAEntry"),
            AssetBundles.Core.LoadAsset<Sprite>("SuckerPopup"));
        PDAHandler.AddEncyclopediaEntry("ArmoredSucker", CustomPdaPaths.PlagueCreationsPath, null,
            AssetBundles.Core.LoadAsset<Texture2D>("ArmoredSuckerPDAEntry"),
            AssetBundles.Core.LoadAsset<Sprite>("SuckerPopup"));

        var mrTeethSpawnPoint = new CustomPrefab(GetInfoForFleshCreation("MrTeethSpawnPoint"));
        mrTeethSpawnPoint.SetGameObject(() =>
        {
            var obj = new GameObject("MrTeethSpawnPoint");
            obj.SetActive(false);
            PrefabUtils.AddBasicComponents(obj, mrTeethSpawnPoint.Info.ClassID, mrTeethSpawnPoint.Info.TechType,
                LargeWorldEntity.CellLevel.Near);
            obj.AddComponent<MrTeethSpawnPoint>();
            return obj;
        });
        mrTeethSpawnPoint.Register();

        var mrTeethSpawner = new CustomPrefab(GetInfoForFleshCreation("MrTeethSpawner"));
        mrTeethSpawner.SetGameObject(() =>
        {
            var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("FleshMass"));
            obj.SetActive(false);
            PrefabUtils.AddBasicComponents(obj, mrTeethSpawner.Info.ClassID, mrTeethSpawner.Info.TechType,
                LargeWorldEntity.CellLevel.Near);
            MaterialUtils.ApplySNShaders(obj);
            obj.AddComponent<MrTeethSpawner>();
            return obj;
        });
        mrTeethSpawner.Register();

        var mrTeethReturnPoint = new CustomPrefab(GetInfoForFleshCreation("MrTeethReturnPoint"));
        mrTeethReturnPoint.SetGameObject(() =>
        {
            var obj = Object.Instantiate(AssetBundles.Core.LoadAsset<GameObject>("FleshMass"));
            obj.SetActive(false);
            PrefabUtils.AddBasicComponents(obj, mrTeethReturnPoint.Info.ClassID, mrTeethReturnPoint.Info.TechType,
                LargeWorldEntity.CellLevel.Near);
            MaterialUtils.ApplySNShaders(obj);
            obj.AddComponent<MrTeethReturnPoint>();
            return obj;
        });
        mrTeethReturnPoint.Register();

        var mrTeeth = new MrTeethPrefab(GetInfoForFleshCreation("MrTeeth"));
        mrTeeth.Register();

        new MimicPeeper(GetInfoForMimic("MimicPeeper")).Register();
        new MimicOculus(GetInfoForMimic("MimicOculus")).Register();
        new FleshStalker(GetInfoForMimic("FleshStalker")).Register();
        new PlagueBladderFish(GetInfoForMimic("PlagueBladderFish")).Register();
        new InvertedSpadeFish(GetInfoForMimic("InvertedSpadefish")).Register();
        new InfestedReefback(GetInfoForMimic("InfestedReefback")).Register();
        new MutantBoomerang(GetInfoForMimic("MutantBoomerang")).Register();
        new MimicGasopod(GetInfoForMimic("MimicGasopod")).Register();
        new TeethTeeth(GetInfoForMimic("TeethTeeth")).Register();
        new EyeyeCaptain(GetInfoForMimic("EyeyeCaptain")).Register();
        new PhantomLeviathan(GetInfoForMimic("PhantomLeviathan")).Register();
        
        new PlagueFloater(GetInfoForFleshCreation("PlagueFloater")).Register();

        // CHAOS LEVIATHAN
        var chaosLeviathanTechType = EnumHandler.AddEntry<TechType>("ChaosLeviathan");
        new ChaosLeviathanPrefab(
            new PrefabInfo("RoamingChaosLeviathan", "PrefabFile_RoamingChaosLeviathan", chaosLeviathanTechType)
                .WithFolderPath(TrpPrefabFolders.Creatures.FleshCreations),
            true).Register();
        // Register the normal command-spawnable one second so that it is used for commands
        new ChaosLeviathanPrefab(
            new PrefabInfo("ChaosLeviathan", "PrefabFile_ChaosLeviathan", chaosLeviathanTechType)
                .WithFolderPath(TrpPrefabFolders.Creatures.FleshCreations),
            false).Register();
        PDAHandler.AddEncyclopediaEntry("ChaosLeviathan", CustomPdaPaths.PlagueCreationsPath, null, null, null,
            AssetBundles.Creatures.LoadAsset<Sprite>("ChaosPopup"), PDAHandler.UnlockBasic);
        PDAHandler.AddCustomScannerEntry(chaosLeviathanTechType, 4, encyclopediaKey: "ChaosLeviathan");
        RoamingChaosLeviathanManager.RegisterSaveData();

        // Possessed vehicles
        new PossessedVehicle(TechType.Seamoth).Register();
        new PossessedVehicle(TechType.Exosuit).Register();

        ScaryMannySpawnPoint.Register();
        ScaryMannyTrigger.Register();

        // Stationary creatures
        StabbyPrefab.Register();
        GrabberPrefab.Register();

        // Insectoids
        new InsectoidPrefab(GetInfoForInsect("Insectoid"), 0.4f).Register();
        new InsectoidPrefab(GetInfoForInsect("SmallInsectoid"), 0.23f).Register();

        // The observer / plague cat
        var observerInfo = GetInfoForFleshCreation("Observer");
        new Observer(observerInfo).Register();
        var observerSpawner =
            new SpawnAfterStoryGoalPrefab(GetInfoForFleshCreation("ObserverSpawner"), observerInfo.ClassID,
                () => Act1Story.DomeConstructionEvent.key, LargeWorldEntity.CellLevel.Far);
        observerSpawner.Register();

        // Pets
        new Gilbert().Register();
        new ConsciousNeuralMatter().Register();
        new Hippopenomenon().Register();

        // Crawling flesh masses
        new CrawlingFleshPrefab(GetInfoForMiscPrefab("CrawlingFleshMass"),
            () => AssetBundles.Core.LoadAsset<GameObject>("FleshMass"), LargeWorldEntity.CellLevel.Medium,
            new CrawlingFleshPrefab.MovementSettings(
                0.5f, 4f, 40, 0.5f, 0f, 3f, 15f),
            new WavingEffectModifier(1)
                { Speed = new Vector4(0.1f, 0.2f), Scale = new Vector4(0.02f, 0.02f, 0.02f, 0.02f) }).Register();
    }

    private static void RegisterLostReefCreatures()
    {
        new CuteSlug(GetInfoForLostReef("CuteSlug")).Register();
    }

    private static PrefabInfo GetInfoForMimic(string classId) =>
        PrefabInfo.WithTechType(classId).WithFolderPath(TrpPrefabFolders.Creatures.Mimics);
    
    private static PrefabInfo GetInfoForFleshCreation(string classId) =>
        PrefabInfo.WithTechType(classId).WithFolderPath(TrpPrefabFolders.Creatures.FleshCreations);
    
    private static PrefabInfo GetInfoForMutants(string classId) =>
        PrefabInfo.WithTechType(classId).WithFolderPath(TrpPrefabFolders.Creatures.Mutants);

    private static PrefabInfo GetInfoForInsect(string classId) =>
        PrefabInfo.WithTechType(classId).WithFolderPath(TrpPrefabFolders.Creatures.Insects);
    
    private static PrefabInfo GetInfoForMiscPrefab(string classId) =>
        PrefabInfo.WithTechType(classId).WithFolderPath(TrpPrefabFolders.Creatures.Miscellaneous);
    
    private static PrefabInfo GetInfoForLostReef(string classId) =>
        PrefabInfo.WithTechType(classId).WithFolderPath(TrpPrefabFolders.LostReef.Creatures);
}