using Nautilus.Handlers;
using Nautilus.Utility;
using Story;
using UnityEngine;

namespace TheRedPlague.Registration.TrpStory;

// NOTE: OnPlay + [story goal ID] is the goal that triggers when the player plays a radio message
public static partial class StoryUtils
{
    public const string PdaVoiceBus = "bus:/master/SFX_for_pause/all_no_pda_pause/all_voice_no_pda_pause/aI_voice";
    
    public static void RegisterStory()
    {
        Act1Story.RegisterAct1();
        Act2Story.RegisterAct2();
        GeneralStory.RegisterGeneralStoryGoals();
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
        return StoryGoalManager.main.IsGoalComplete(Act1Story.DomeConstructionFinishedEvent.key);
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

    public static string ChecklistUnlockGoalKey => "OnPlay" + Act2Story.Act2MissionInstructionsRadio.key;
    public static string ChecklistViewedFirstTimeGoalKey => "TrpChecklistViewed";

    #endregion

    #region Utilities

    public static void RegisterVoiceLog(string id, string clipName, Sprite icon)
    {
        var sound = AudioUtils.CreateSound(AssetBundles.Audio.LoadAsset<AudioClip>(clipName),
            AudioUtils.StandardSoundModes_2D);

        CustomSoundHandler.RegisterCustomSound(id, sound, PdaVoiceBus);

        PDAHandler.AddLogEntry(id, id, AudioUtils.GetFmodAsset(id), icon);
    }

    public static FMODAsset RegisterDatabankEntryVoiceLog(string id, string clipName)
    {
        var sound = AudioUtils.CreateSound(AssetBundles.Audio.LoadAsset<AudioClip>(clipName),
            AudioUtils.StandardSoundModes_2D);

        CustomSoundHandler.RegisterCustomSound(id, sound, PdaVoiceBus);

        return AudioUtils.GetFmodAsset(id);
    }

    #endregion
}