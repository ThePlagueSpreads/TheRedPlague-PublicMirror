using Nautilus.Handlers;
using Nautilus.Utility;
using Story;
using TheRedPlague.Data;
using TheRedPlague.Mono.CinematicEvents;
using TheRedPlague.PrefabFiles.Items;
using UnityEngine;

namespace TheRedPlague;

// NOTE: OnPlay + [story goal ID] is the goal that triggers when the player plays a radio message
public static partial class StoryUtils
{
    // FUTURE ACTS
    public static StoryGoal AuroraThrusterEvent { get; private set; }
    public static StoryGoal HiveMindReleasedGoal { get; private set; }
    public static StoryGoal PickUpInfectedEnzyme { get; private set; }

    #region GENERAL

    public static StoryGoal IslandElevatorActivatedGoal { get; private set; }
    public static StoryGoal UseElevatorGoal { get; private set; }
    public static BiomeGoal EnterPlagueDunesGoal { get; private set; }
    public static StoryGoal SendShuttleFirstTime { get; private set; }
    public static StoryGoal ShuttleInvalidItem1 { get; private set; }
    public static StoryGoal ShuttleInvalidItem2 { get; private set; }
    public static StoryGoal ShuttleInvalidItem3 { get; private set; }
    public static StoryGoal GrabGun { get; private set; }
    public static StoryGoal InsanityWarning { get; private set; }
    public static StoryGoal Enzyme42Warning { get; private set; }
    public static StoryGoal LaunchRocketWhileDomeActive { get; private set; }

    #endregion

    #region ResearchTeams

    public static ItemGoal ResearchTeamARadioGoal { get; private set; }

    #endregion

    #region Bonus

    public static CompoundGoal CallOfAsteroidGoal { get; private set; }
    public static CompoundGoal CallOfAsteroidEntryGoal { get; private set; }
    public static StoryGoal BananaphobiaGoal { get; private set; }

    #endregion

    public const string PdaVoiceBus = "bus:/master/SFX_for_pause/all_no_pda_pause/all_voice_no_pda_pause/aI_voice";

    private static readonly Vector3 DomeConstructionTriggerLocation = new Vector3(-60, 315, -55);

    #region ASSETS

    private static Sprite VoiceLogIconAlterra { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-Alterra");

    // private static Sprite VoiceLogIconPerson { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-Person");
    private static Sprite VoiceLogIconPda { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-PDA");

    // reserve this for non-alterra comms
    private static Sprite VoiceLogIconRadio { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-Radio");

    // yes the literal dome can talk
    private static Sprite VoiceLogIconDome { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-Dome");
    private static Sprite VoiceLogIconDerman { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-Derman");
    private static Sprite VoiceLogIconUnknown { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-Unknown");
    private static Sprite VoiceLogIconB3NT { get; } = Plugin.AssetBundle.LoadAsset<Sprite>("LogIcon-B3NT");

    #endregion

    public static void RegisterStory()
    {
        RegisterGeneralStoryGoals();
        RegisterAct1();
        RegisterAct2();
        RegisterResearchTeamStoryLines();
        RegisterOfficialRedPlagueReports();
        RegisterPersonnelReports();
        RegisterBonusContent();

        AuroraThrusterEvent = new StoryGoal("AuroraThrusterEvent", Story.GoalType.Story, 0);
        PickUpInfectedEnzyme = StoryGoalHandler.RegisterItemGoal("Pickup_Infected_Enzyme", Story.GoalType.Story,
            ModPrefabs.EnzymeParticleInfo.TechType);
        StoryGoalHandler.RegisterOnGoalUnlockData("Pickup_Infected_Enzyme",
            new[]
            {
                new UnlockBlueprintData
                {
                    techType = ModPrefabs.EnzymeContainer.TechType,
                    unlockType = UnlockBlueprintData.UnlockType.Available
                }
            });
    }

    private static void RegisterGeneralStoryGoals()
    {
        IslandElevatorActivatedGoal = new StoryGoal("IslandElevatorActivated", Story.GoalType.Story, 0);

        UseElevatorGoal = new StoryGoal("UseIslandElevator", Story.GoalType.Story, 0);
        EnterPlagueDunesGoal =
            StoryGoalHandler.RegisterBiomeGoal("EnterPlagueDunes", Story.GoalType.PDA, "dunes", 1, 3);
        RegisterVoiceLog("EnterPlagueDunes", "EnterPlagueDunes", VoiceLogIconPda);

        // SHUTTLE
        SendShuttleFirstTime = new StoryGoal("PDASendShuttleFirstTime", Story.GoalType.PDA, 2);
        RegisterVoiceLog(SendShuttleFirstTime.key, "PDASendShuttleFirstTime", VoiceLogIconPda);
        ShuttleInvalidItem1 = new StoryGoal("ShuttleInvalidItem1", Story.GoalType.PDA, 0);
        RegisterVoiceLog(ShuttleInvalidItem1.key, "PDAShuttleInvalidItem1", VoiceLogIconPda);
        ShuttleInvalidItem2 = new StoryGoal("ShuttleInvalidItem2", Story.GoalType.PDA, 0);
        RegisterVoiceLog(ShuttleInvalidItem2.key, "PDAShuttleInvalidItem2", VoiceLogIconPda);
        ShuttleInvalidItem3 = new StoryGoal("ShuttleInvalidItem3", Story.GoalType.PDA, 0);
        RegisterVoiceLog(ShuttleInvalidItem3.key, "PDAShuttleInvalidItem3", VoiceLogIconPda);

        // OTHER
        GrabGun = new StoryGoal("TentaclesDisableGun", Story.GoalType.Story, 0);
        InsanityWarning = new StoryGoal("PDAInsanityWarning", Story.GoalType.PDA, 3);
        RegisterVoiceLog(InsanityWarning.key, "PDAInsanityWarning", VoiceLogIconPda);
        
        // BASE GAME EVENTS
        Enzyme42Warning = new StoryGoal("PdaEnzyme42Warning", Story.GoalType.PDA, 0);
        RegisterVoiceLog(Enzyme42Warning.key, "PdaEnzymeWarning", VoiceLogIconPda);
        LaunchRocketWhileDomeActive = new StoryGoal("PdaLaunchRocketWhileDomeActive", Story.GoalType.PDA, 0);
        RegisterVoiceLog(LaunchRocketWhileDomeActive.key, "PdaRocketDomeWarning", VoiceLogIconPda);
    }

    private static void RegisterResearchTeamStoryLines()
    {
        ResearchTeamARadioGoal = StoryGoalHandler.RegisterItemGoal("ResearchTeamARadioLog", Story.GoalType.Radio,
            RedPlagueSample.Info.TechType, 180);
        RegisterVoiceLog(ResearchTeamARadioGoal.key, "ResearchTeamARadioLog", VoiceLogIconRadio);
        StoryGoalHandler.RegisterOnGoalUnlockData("OnPlayResearchTeamARadioLog", signals: new UnlockSignalData[]
        {
            new()
            {
                targetPosition = new Vector3(-111, -190, -985),
                targetDescription = "ResearchTeamASignal"
            }
        });

        PDAHandler.AddEncyclopediaEntry("ResearchTeamADeathLog", CustomPdaPaths.RedPlagueVictimsPath, null, null, null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("ResearchTeamADeathLog", "ResearchTeamADeathLog"));
        PDAHandler.AddEncyclopediaEntry("ResearchTeamATabletLog", CustomPdaPaths.RedPlagueVictimsPath, null, null, null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("ResearchTeamATabletLog", "ResearchTeamATabletLog"));

        PDAHandler.AddEncyclopediaEntry("RedPlagueSurvivorBase1Log", CustomPdaPaths.RedPlagueVictimsPath, null, null,
            null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("RedPlagueSurvivorBase1Log", "DanteBaseLog")
        );
        StoryGoalHandler.RegisterOnGoalUnlockData("RedPlagueSurvivorBase1Log", signals: new[]
        {
            new UnlockSignalData
            {
                targetPosition = new Vector3(-867, -330, -1124),
                targetDescription = "RedPlagueSurvivorBase3Signal"
            }
        });
        
        PDAHandler.AddEncyclopediaEntry("RedPlagueSurvivorBase1Log2", CustomPdaPaths.RedPlagueVictimsPath, null, null,
            null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("RedPlagueSurvivorBase1Log2", "DanteBaseLog2")
        );

        PDAHandler.AddEncyclopediaEntry("RedPlagueSurvivorBase2Log", CustomPdaPaths.RedPlagueVictimsPath, null, null,
            null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("RedPlagueSurvivorBase2Log", "LochinvarBaseLog"));
        StoryGoalHandler.RegisterOnGoalUnlockData("RedPlagueSurvivorBase2Log", signals: new[]
        {
            new UnlockSignalData
            {
                targetPosition = new Vector3(1302, -443, 1378),
                targetDescription = "RedPlagueSurvivorBase1Signal"
            }
        });

        PDAHandler.AddEncyclopediaEntry("RedPlagueSurvivorBase2Log2", CustomPdaPaths.RedPlagueVictimsPath, null, null,
            null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("RedPlagueSurvivorBase2Log2", "LochinvarBaseLog2"));

        PDAHandler.AddEncyclopediaEntry("RedPlagueSurvivorBase3Log", CustomPdaPaths.RedPlagueVictimsPath, null, null,
            null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("RedPlagueSurvivorBase3Log", "ChainBaseLog")
        );
        
        PDAHandler.AddEncyclopediaEntry("RedPlagueSurvivorBase3Log2", CustomPdaPaths.RedPlagueVictimsPath, null, null,
            null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("RedPlagueSurvivorBase3Log2", "ChainBaseLog2")
        );
        
        PDAHandler.AddEncyclopediaEntry("DermanConnectionLog", CustomPdaPaths.RedPlagueResearchPath, null, null,
            null,
            null, PDAHandler.UnlockBasic,
            RegisterDatabankEntryVoiceLog("DermanConnectionLog", "Derman_Connection")
        );
    }

    private static void RegisterOfficialRedPlagueReports()
    {
        // report 1
        PDAHandler.AddEncyclopediaEntry("RedPlagueReport1", CustomPdaPaths.RedPlagueResearchPath, null, null);
        StoryGoalHandler.RegisterCompoundGoal("RedPlagueReport1", Story.GoalType.Encyclopedia,
            47, SendPlagueSampleViaShuttleEvent.key);

        // report 2
        PDAHandler.AddEncyclopediaEntry("RedPlagueReport2", CustomPdaPaths.RedPlagueResearchPath, null, null);
        StoryGoalHandler.RegisterCompoundGoal("RedPlagueReport2", Story.GoalType.Encyclopedia,
            20, DomeSpeakEvent.key);
    }

    private static void RegisterPersonnelReports()
    {
        // Cassy
        PDAHandler.AddEncyclopediaEntry("CassyPersonnelReport", CustomPdaPaths.PersonnelReportsPath, null, null,
            Plugin.AssetBundle.LoadAsset<Texture2D>("PersonnelProfile_Cassy"));
    }

    private static void RegisterBonusContent()
    {
        // "Medicinal breakthrough" entry
        CallOfAsteroidGoal = StoryGoalHandler.RegisterCompoundGoal("CallOfAsteroidRadio", Story.GoalType.Radio,
            60 * 60 * 6, SendPlagueSampleViaShuttleEvent.key);
        RegisterVoiceLog(CallOfAsteroidGoal.key, "CallOfAsteroidRadioMessage", VoiceLogIconAlterra);
        CallOfAsteroidEntryGoal = StoryGoalHandler.RegisterCompoundGoal("CallOfAsteroid", Story.GoalType.Encyclopedia,
            60, "OnPlay" + CallOfAsteroidGoal.key);
        PDAHandler.AddEncyclopediaEntry(CallOfAsteroidEntryGoal.key, "DownloadedData/RedPlagueResearch", null, null);

        // Bananaphobia easter egg
        BananaphobiaGoal = new StoryGoal("BananaphobiaMessage", Story.GoalType.PDA, 15);
        RegisterVoiceLog(BananaphobiaGoal.key, "Bananaphobia", VoiceLogIconAlterra);
    }

    public static void RegisterLanguageLines()
    {
        LanguageHandler.SetLanguageLine("InfectionLaserTerminal", "Infected terminal");
        LanguageHandler.SetLanguageLine("InfectionLaserReceptacle", "Multi-purpose receptacle");
        LanguageHandler.SetLanguageLine("InsertPlagueHeart", "Insert plague heart");
        LanguageHandler.SetLanguageLine("InsertEnzymeContainer", "Insert concentrated enzyme");
        LanguageHandler.SetLanguageLine("DisableDomePrompt", "Disable dome");
        LanguageHandler.SetLanguageLine("Ency_Infection", Language.main.Get("Ency_Infection_REPLACE"));
        LanguageHandler.SetLanguageLine("EncyDesc_Infection", Language.main.Get("EncyDesc_Infection_REPLACE"));
    }

    #region Public utility methods

    public static bool IsAct1Complete()
    {
        return StoryGoalManager.main.IsGoalComplete(DomeConstructionFinishedEvent.key);
    }

    public static bool IsHivemindReleased()
    {
        return StoryGoalManager.main.IsGoalComplete(HiveMindReleasedGoal.key);
    }

    public static string GetStoryGoalKeyForShuttleDelivery(string itemName)
    {
        return "ShuttleDelivery" + itemName;
    }

    // Used for various checks to prevent PDA spam in creative mode worlds
    public static bool InCreativeMode()
    {
        GameModeUtils.GetGameMode(out var mode, out _);
        return mode.HasFlag(GameModeOption.Creative);
    }

    public static string ChecklistUnlockGoalKey => "OnPlay" + Act2MissionInstructionsRadio.key;
    public static string ChecklistViewedFirstTimeGoalKey => "TrpChecklistViewed";

    #endregion

    #region Private utilities

    private static void RegisterVoiceLog(string id, string clipName, Sprite icon)
    {
        var sound = AudioUtils.CreateSound(Plugin.AudioBundle.LoadAsset<AudioClip>(clipName),
            AudioUtils.StandardSoundModes_2D);

        CustomSoundHandler.RegisterCustomSound(id, sound, PdaVoiceBus);

        PDAHandler.AddLogEntry(id, id, AudioUtils.GetFmodAsset(id), icon);
    }

    private static FMODAsset RegisterDatabankEntryVoiceLog(string id, string clipName)
    {
        var sound = AudioUtils.CreateSound(Plugin.AudioBundle.LoadAsset<AudioClip>(clipName),
            AudioUtils.StandardSoundModes_2D);

        CustomSoundHandler.RegisterCustomSound(id, sound, PdaVoiceBus);

        return AudioUtils.GetFmodAsset(id);
    }

    #endregion
}