using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nautilus.Commands;
using Nautilus.Handlers;
using Nautilus.Utility;
using Newtonsoft.Json;
using Story;
using TheRedPlague.Content.Act1.Dome;
using TheRedPlague.Content.Act2.Ending;
using TheRedPlague.Content.Act2.FleshCave;
using TheRedPlague.Content.Act2.PlagueHeart;
using TheRedPlague.Content.Act2.ThrusterEvent;
using TheRedPlague.Content.Buildables.PlagueAltar;
using TheRedPlague.Content.Creatures.Chaos;
using TheRedPlague.Content.Creatures.Grabber;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Content.PlayerInfection;
using TheRedPlague.Content.PlayerInfection.Symptoms;
using TheRedPlague.Content.Scares.GhostCyclops;
using TheRedPlague.Content.Scares.NpcSurvivors;
using TheRedPlague.Content.UI;
using TheRedPlague.Content.Unused;
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
        ConsoleCommandsHandler.AddGotoTeleportPosition("domebase", new Vector3(2.8f, 2105f, 42.2f));

        ConsoleCommandsHandler.AddBiomeTeleportPosition("fleshcave", new Vector3(-1384, -408, -117));
        ConsoleCommandsHandler.AddBiomeTeleportPosition("fleshchamber", new Vector3(-1509, -863, 393));
        ConsoleCommandsHandler.AddBiomeTeleportPosition("fleshcavecache", new Vector3(-1389, -860, 634));
    }
    
    [ConsoleCommand("trpcommands")]
    public static void TRPCommands()
    {
        var allCommands = typeof(Commands)
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Select(method => new
            {
                Method = method,
                Attribute = method.GetCustomAttributes(false)
                    .FirstOrDefault(attr => attr is ConsoleCommandAttribute)
            })
            .Where(x => x.Attribute != null)
            .Select(x =>
            {
                var propertyInfo = typeof(ConsoleCommandAttribute).GetProperty("Command");
                return propertyInfo != null
                    ? propertyInfo.GetValue(x.Attribute)?.ToString()
                    : null;
            })
            .Where(name => !string.IsNullOrEmpty(name))
            .OrderBy(name => name)
            .Select(c => $"<color=#{GetCommandColor(c)}>{c}</color>");

        ErrorMessage.AddMessage("<color=#FF0000>All Red Plague commands</color>:");

        var output = string.Join(", ", allCommands);
        ErrorMessage.AddMessage(output);
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
        Content.Scares.PrawnSuitCinematic.PlayCinematic();
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
    public static void SpawnDome(bool instant)
    {
        var goalManager = StoryGoalManager.main;
        if (instant && goalManager != null)
        {
            goalManager.completedGoals.Add(Act1Story.DomeConstructionEvent.key);
        }
        UWE.CoroutineHost.StartCoroutine(DomeConstructionTrigger.SpawnDome());
    }

    [ConsoleCommand("shatterdome")]
    public static void ShatterDome()
    {
        DomeExplosion.ExplodeDome();
    }
    
    [ConsoleCommand("deletedome")]
    public static void DeleteDome()
    {
        var dome = InfectionDomeController.main;
        if (dome != null)
        {
            Object.Destroy(dome.gameObject);
        }

        var goalManager = StoryGoalManager.main;
        if (goalManager != null)
        {
            goalManager.completedGoals.Remove(Act1Story.DomeConstructionEvent.key);
        }
    }

    [ConsoleCommand("nuclearexplosion")]
    public static void NuclearExplosion()
    {
        Content.Unused.NuclearExplosion.PlayCinematic();
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
        var scenePaths = AssetBundles.Scenes.GetAllScenePaths();
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
        Act2Story.Act2MissionsFollowUp4.Trigger();
    }

    [ConsoleCommand("plagueresources")]
    public static void GivePlagueResources()
    {
        CraftData.AddToInventory(DormantNeuralMatter.Info.TechType);
        CraftData.AddToInventory(AmalgamatedBone.Info.TechType);
        CraftData.AddToInventory(WarperHeart.Info.TechType);
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
        Act2Story.ObeyBennetEvent.Trigger();
    }

    [ConsoleCommand("disobeycinematic")]
    public static void DisobeyCinematic()
    {
        Act2Story.DisobeyBennetEvent.Trigger();
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
            AssetBundles.Creatures.LoadAsset<GameObject>("ChaosLeviathanEndCinematicPrefab")));
    }
    
    [ConsoleCommand("startact2")]
    public static void StartAct2()
    {
        SkipActUtilities.StartAct2();
    }

    [ConsoleCommand("startact3")]
    public static void StartAct3()
    {
        SkipActUtilities.StartAct3();
    }
    
    [ConsoleCommand("plaguealtarscare")]
    public static void PlagueAltarScare()
    {
        if (!AltarIntrusionEvent.TriggerTest(15))
        {
            ErrorMessage.AddMessage("No plague altar found nearby");
        }
    }
    
    private static string GetCommandColor(string commandName)
    {
        var hash = commandName.GetHashCode();
        float hue = Mathf.Abs(hash % 1000) / 1000f;
        var color = Color.HSVToRGB(hue, 0.8f, 1f);
        return ColorUtility.ToHtmlStringRGB(color);
    }
}