using System;
using System.Collections.Generic;
using Nautilus.Commands;
using Nautilus.Handlers;
using Nautilus.Utility;
using Newtonsoft.Json;
using Story;
using TheRedPlague.Mono.CinematicEvents;
using TheRedPlague.Mono.CreatureBehaviour.Chaos;
using TheRedPlague.Mono.CreatureBehaviour.Grabber;
using TheRedPlague.Mono.InfectionLogic;
using TheRedPlague.Mono.Insanity;
using TheRedPlague.Mono.Insanity.Symptoms;
using TheRedPlague.Mono.StoryContent;
using TheRedPlague.Mono.StoryContent.PlagueHeart;
using TheRedPlague.Mono.Systems;
using TheRedPlague.PrefabFiles.Items;
using TheRedPlague.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace TheRedPlague;

public static class Commands
{
    public static void RegisterTeleportCommands()
    {
        ConsoleCommandsHandler.AddGotoTeleportPosition("elevatorplatform", new Vector3(-53, 3, -49));
        ConsoleCommandsHandler.AddGotoTeleportPosition("forcefieldisland", new Vector3(-78.1f, 315.5f, -68.7f));
        ConsoleCommandsHandler.AddGotoTeleportPosition("dunesisland", new Vector3(-1327, -193, 283));
        ConsoleCommandsHandler.AddGotoTeleportPosition("cassyhome", new Vector3(-120, -190, -990));
        ConsoleCommandsHandler.AddGotoTeleportPosition("dantebase", new Vector3(1317, -441, 1371));
        ConsoleCommandsHandler.AddGotoTeleportPosition("lochinvarbase", new Vector3(-355, -70, -460));
        ConsoleCommandsHandler.AddGotoTeleportPosition("chainbase", new Vector3(-873, -327, -1147));
        ConsoleCommandsHandler.AddGotoTeleportPosition("cyclopswreck", new Vector3(-192, -798, 339));
        ConsoleCommandsHandler.AddGotoTeleportPosition("mazebase", new Vector3(-1245, -234, 737));
        ConsoleCommandsHandler.AddGotoTeleportPosition("voidisland", new Vector3(-974, 0, 1795));
        ConsoleCommandsHandler.AddGotoTeleportPosition("fleshcavehatch", new Vector3(-1370, -340, -105));
        ConsoleCommandsHandler.AddGotoTeleportPosition("shrinebase", new Vector3(-1516, -885, 822));
        ConsoleCommandsHandler.AddGotoTeleportPosition("meteor", new Vector3(-1115, -306, 1070));
        ConsoleCommandsHandler.AddGotoTeleportPosition("fleshcavecache", new Vector3(-1389, -860, 634));

        ConsoleCommandsHandler.AddBiomeTeleportPosition("fleshcave", new Vector3(-1384, -408, -117));
        ConsoleCommandsHandler.AddBiomeTeleportPosition("fleshchamber", new Vector3(-1509, -863, 393));
    }

    [ConsoleCommand("jumpscare")]
    public static void JumpScare()
    {
        JumpScares.main.JumpScareNow();
    }

    [ConsoleCommand("spawndiver")]
    public static void SpawnDiver(string diverName)
    {
        var survivorManager = NpcSurvivorManager.main;
        if (survivorManager == null)
        {
            ErrorMessage.AddMessage("No NpcSurvivorManager found");
        }

        var survivors = survivorManager.gameObject.GetComponents<NpcSurvivor>();
        foreach (var survivor in survivors)
        {
            if (string.Equals(survivor.survivorName, diverName, StringComparison.OrdinalIgnoreCase))
            {
                survivor.ForceSpawnWithCommand();
                return;
            }
        }
    }

    [ConsoleCommand("prawnsuitcinematic")]
    public static void PrawnSuitCinematic()
    {
        Mono.CinematicEvents.PrawnSuitCinematic.PlayCinematic();
    }

    [ConsoleCommand("dropadminpod")]
    public static void SpawnAdminPod()
    {
        AdminDropPodFall.SpawnAdministratorDropPod();
    }

    [ConsoleCommand("thrusterevent")]
    public static void ThrusterEvent()
    {
        AuroraThrusterEvent.PlayCinematic();
    }

    [ConsoleCommand("spawndome")]
    public static void SpawnDome()
    {
        UWE.CoroutineHost.StartCoroutine(DomeConstructionTrigger.SpawnDome());
    }

    [ConsoleCommand("shatterdome")]
    public static void ShatterDome()
    {
        DomeExplosion.ExplodeDome();
    }

    [ConsoleCommand("nuclearexplosion")]
    public static void NuclearExplosion()
    {
        Mono.CinematicEvents.NuclearExplosion.PlayCinematic();
    }

    [ConsoleCommand("createcodeforbase")]
    public static void CreateCodeForBase()
    {
        GenerateBasePrefabCode.ConvertBaseToPrefabConstructionCode(Player.main.GetCurrentSub().GetComponent<Base>());
    }

    [ConsoleCommand("redplaguecredits")]
    public static void StartRedPlagueCredits()
    {
        OnScreenCredits.Play();
    }

    [ConsoleCommand("infectiondamage")]
    public static void TakeInfectionDamage(float damage)
    {
        PlagueDamageStat.main.TakeInfectionDamage(damage, true);
    }

    [ConsoleCommand("plaguedamage")]
    public static void TakeInfectionDamageAlias(float damage)
    {
        TakeInfectionDamage(damage);
    }

    [ConsoleCommand("chargesuit")]
    public static void ChargeSuit(float charge)
    {
        PlagueDamageStat.main.HealInfectionDamage(charge);
    }

    [ConsoleCommand("loadredplaguescene")]
    public static void LoadRedPlagueScene(string scene = null)
    {
        var scenePaths = Plugin.ScenesAssetBundle.GetAllScenePaths();
        var loadedSceneSuccessfully = false;

        foreach (var scenePath in scenePaths)
        {
            if (scenePath != scene) continue;
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            loadedSceneSuccessfully = true;
            break;
        }

        // Exit early if the check failed
        if (!loadedSceneSuccessfully)
        {
            ErrorMessage.AddMessage($"No scene found by name '{scene}'! Valid scene names:");
            foreach (var scenePath in scenePaths)
            {
                ErrorMessage.AddMessage(scenePath);
            }

            return;
        }

        var objects = SceneManager.GetSceneByPath(scene).GetRootGameObjects();

        foreach (var obj in objects)
        {
            MaterialUtils.ApplySNShaders(obj);
        }
    }

    [ConsoleCommand("openplaguecave")]
    public static void OpenPlagueCave()
    {
        PlagueCaveEntrance.OpenAll();
    }

    [ConsoleCommand("closeplaguecave")]
    public static void ClosePlagueCave()
    {
        PlagueCaveEntrance.CloseAll();
    }

    [ConsoleCommand("unlockchecklist")]
    public static void UnlockChecklist()
    {
        new StoryGoal(StoryUtils.ChecklistUnlockGoalKey, Story.GoalType.Story, 0).Trigger();
    }

    [ConsoleCommand("spawngrassyplateaubasesignal")]
    public static void SpawnGrassyPlateauBaseSignal()
    {
        var goal = new StoryGoal("Command_GrassyPlateauBaseSignal", Story.GoalType.Story, 0);
        StoryGoalHandler.RegisterOnGoalUnlockData(goal.key, signals: new[]
        {
            new UnlockSignalData
            {
                targetPosition = new Vector3(-365, -76, -464),
                targetDescription = "RedPlagueSurvivorBase2Signal"
            }
        });
        goal.Trigger();
    }

    [ConsoleCommand("completechecklist")]
    public static void CompleteChecklist()
    {
        StoryUtils.Act2MissionsFollowUp4.Trigger();
    }

    [ConsoleCommand("plagueresources")]
    public static void GivePlagueResources()
    {
        CraftData.AddToInventory(DormantNeuralMatter.Info.TechType);
        CraftData.AddToInventory(ModPrefabs.AmalgamatedBone.TechType);
        CraftData.AddToInventory(ModPrefabs.WarperHeart.TechType);
        CraftData.AddToInventory(PlagueIngot.Info.TechType);
        CraftData.AddToInventory(MysteriousRemains.Info.TechType);
        CraftData.AddToInventory(PlagueCatalyst.Info.TechType);
    }

    [ConsoleCommand("partytime")]
    public static void PartyTime()
    {
        Utils.PlayFMODAsset(AudioUtils.GetFmodAsset("RedPlaguePartyTime"), Player.main.transform.position);
    }

    [ConsoleCommand("previewgrabbers")]
    public static void PreviewGrabbers()
    {
        GrabberCreature.TogglePreviewMode();
        ErrorMessage.AddMessage(
            "Changing grabber display for structure placement purposes. Use the 'previewgrabbers' command again to toggle this mode.");
    }

    [ConsoleCommand("obeycinematic")]
    public static void ObeyCinematic()
    {
        StoryUtils.ObeyBennetEvent.Trigger();
    }
    
    [ConsoleCommand("disobeycinematic")]
    public static void DisobeyCinematic()
    {
        StoryUtils.DisobeyBennetEvent.Trigger();
    }
    
    [ConsoleCommand("previewghostcyclops")]
    public static void PreviewGhostCyclops(float startX, float startY, float startZ, float endX, float endY, float endZ)
    {
        GhostCyclopsCinematic.StartCinematic(new GhostCyclopsCinematic.PathData(false,
            new Vector3(startX, startY, startZ),
            new Vector3(endX, endY, endZ)));
    }
    
    [ConsoleCommand("savechaostrailmanagers")]
    public static void SaveChaosTrailManagers()
    {
        var findChaos = Object.FindObjectOfType<ChaosLeviathanRoar>();
        if (findChaos == null)
        {
            ErrorMessage.AddMessage("Failed to find ChaosLeviathan.");
            return;
        }

        var data = new Dictionary<string, TrailManagerData>();

        var trailManagers = findChaos.GetComponentsInChildren<TrailManager>();
        foreach (var trailManager in trailManagers)
        {
            var id = trailManager.rootSegment.transform.name;
            var saved = TrailManagerUtils.GetData(trailManager);
            data.Add(id, saved);
        }
        
        var json = JsonConvert.SerializeObject(new MultipleTrailManagersData
        {
            trailManagers = data
        });
        var path = System.IO.Path.Combine(BepInEx.Paths.PluginPath, "TEMP", "ChaosTrailManagers.json");
        System.IO.File.WriteAllText(path, json);
    }
    
    [ConsoleCommand("hatchchaostest")]
    public static void HatchChaosTest()
    {
        UWE.CoroutineHost.StartCoroutine(ChaosLeviathanHatch.PlaySequence(PlagueHeartBehaviour.Main.transform,
            Plugin.CreaturesBundle.LoadAsset<GameObject>("ChaosLeviathanEndCinematicPrefab")));
    }
}