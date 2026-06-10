using Nautilus.Handlers;
using Story;
using TheRedPlague.Content.Items.Story;
using TheRedPlague.Content.Scares;
using UnityEngine;

namespace TheRedPlague.Registration.TrpStory;

// General story goals
public static class GeneralStory
{
    public static CompoundGoal CallOfAsteroidGoal { get; private set; }
    public static CompoundGoal CallOfAsteroidEntryGoal { get; private set; }
    public static StoryGoal BananaphobiaGoal { get; private set; }
    public static BiomeGoal EnterPlagueDunesGoal { get; private set; }
    public static StoryGoal SendShuttleFirstTime { get; private set; }
    public static StoryGoal ShuttleInvalidItem1 { get; private set; }
    public static StoryGoal ShuttleInvalidItem2 { get; private set; }
    public static StoryGoal ShuttleInvalidItem3 { get; private set; }
    public static StoryGoal GrabGun { get; private set; }
    public static StoryGoal InsanityWarning { get; private set; }
    public static StoryGoal Enzyme42Warning { get; private set; }
    public static StoryGoal LaunchRocketWhileDomeActive { get; private set; }
    public static StoryGoal PickUpInfectedEnzyme { get; private set; }
    public static LocationGoal PrawnSuitGunScare { get; private set; }

    internal static void RegisterGeneralStoryGoals()
    {
        RegisterShuttleGoals();
        RegisterInsanityGoals();
        RegisterBaseGameRelatedGoals();
        RegisterBonusContent();
        RegisterOfficialRedPlagueReports();
        RegisterPersonnelReports();
    }

    private static void RegisterOfficialRedPlagueReports()
    {
        // report 1
        PDAHandler.AddEncyclopediaEntry("RedPlagueReport1", CustomPdaPaths.RedPlagueOfficialReportsPath, null, null);
        StoryGoalHandler.RegisterCompoundGoal("RedPlagueReport1", Story.GoalType.Encyclopedia,
            47, Act1Story.SendPlagueSampleViaShuttleEvent.key);

        // report 2
        PDAHandler.AddEncyclopediaEntry("RedPlagueReport2", CustomPdaPaths.RedPlagueOfficialReportsPath, null, null);
        StoryGoalHandler.RegisterCompoundGoal("RedPlagueReport2", Story.GoalType.Encyclopedia,
            20, Act1Story.DomeSpeakEvent.key);
    }


    private static void RegisterBaseGameRelatedGoals()
    {
        EnterPlagueDunesGoal =
            StoryGoalHandler.RegisterBiomeGoal("EnterPlagueDunes", Story.GoalType.PDA, "dunes", 1, 3);
        StoryUtils.RegisterVoiceLog("EnterPlagueDunes", "EnterPlagueDunes", EssentialAssets.VoiceLogIconPda);

        // OTHER
        GrabGun = new StoryGoal("TentaclesDisableGun", Story.GoalType.Story, 0);

        // BASE GAME EVENTS
        Enzyme42Warning = new StoryGoal("PdaEnzyme42Warning", Story.GoalType.PDA, 0);
        StoryUtils.RegisterVoiceLog(Enzyme42Warning.key, "PdaEnzymeWarning", EssentialAssets.VoiceLogIconPda);
        LaunchRocketWhileDomeActive = new StoryGoal("PdaLaunchRocketWhileDomeActive", Story.GoalType.PDA, 0);
        StoryUtils.RegisterVoiceLog(LaunchRocketWhileDomeActive.key, "PdaRocketDomeWarning",
            EssentialAssets.VoiceLogIconPda);

        // CUT CONTENT
        PickUpInfectedEnzyme = StoryGoalHandler.RegisterItemGoal("Pickup_Infected_Enzyme", Story.GoalType.Story,
            InfectedEnzyme.EnzymeParticleInfo.TechType);
        StoryGoalHandler.RegisterOnGoalUnlockData("Pickup_Infected_Enzyme",
            new[]
            {
                new UnlockBlueprintData
                {
                    techType = EnzymeContainer.Info.TechType,
                    unlockType = UnlockBlueprintData.UnlockType.Available
                }
            });
        
        PrawnSuitGunScare = StoryGoalHandler.RegisterLocationGoal("PrawnSuitGunScare", Story.GoalType.Story,
            new Vector3(327, 15, 1172), 80, 1, 1);
        StoryGoalHandler.RegisterCustomEvent(PrawnSuitGunScare.key, PrawnSuitCinematic.PlayCinematic);
    }

    private static void RegisterShuttleGoals()
    {
        // SHUTTLE
        SendShuttleFirstTime = new StoryGoal("PDASendShuttleFirstTime", Story.GoalType.PDA, 2);
        StoryUtils.RegisterVoiceLog(SendShuttleFirstTime.key, "PDASendShuttleFirstTime",
            EssentialAssets.VoiceLogIconPda);
        ShuttleInvalidItem1 = new StoryGoal("ShuttleInvalidItem1", Story.GoalType.PDA, 0);
        StoryUtils.RegisterVoiceLog(ShuttleInvalidItem1.key, "PDAShuttleInvalidItem1", EssentialAssets.VoiceLogIconPda);
        ShuttleInvalidItem2 = new StoryGoal("ShuttleInvalidItem2", Story.GoalType.PDA, 0);
        StoryUtils.RegisterVoiceLog(ShuttleInvalidItem2.key, "PDAShuttleInvalidItem2", EssentialAssets.VoiceLogIconPda);
        ShuttleInvalidItem3 = new StoryGoal("ShuttleInvalidItem3", Story.GoalType.PDA, 0);
        StoryUtils.RegisterVoiceLog(ShuttleInvalidItem3.key, "PDAShuttleInvalidItem3", EssentialAssets.VoiceLogIconPda);
    }

    private static void RegisterInsanityGoals()
    {
        InsanityWarning = new StoryGoal("PDAInsanityWarning", Story.GoalType.PDA, 3);
        StoryUtils.RegisterVoiceLog(InsanityWarning.key, "PDAInsanityWarning", EssentialAssets.VoiceLogIconPda);
    }

    private static void RegisterBonusContent()
    {
        // "Medicinal breakthrough" entry
        CallOfAsteroidGoal = StoryGoalHandler.RegisterCompoundGoal("CallOfAsteroidRadio", Story.GoalType.Radio,
            60 * 60 * 6, Act1Story.SendPlagueSampleViaShuttleEvent.key);
        StoryUtils.RegisterVoiceLog(CallOfAsteroidGoal.key, "CallOfAsteroidRadioMessage",
            EssentialAssets.VoiceLogIconAlterra);
        CallOfAsteroidEntryGoal = StoryGoalHandler.RegisterCompoundGoal("CallOfAsteroid",
            Story.GoalType.Encyclopedia,
            60, "OnPlay" + CallOfAsteroidGoal.key);
        PDAHandler.AddEncyclopediaEntry(CallOfAsteroidEntryGoal.key, CustomPdaPaths.RedPlagueOfficialReportsPath, null, null);

        // Bananaphobia easter egg
        BananaphobiaGoal = new StoryGoal("BananaphobiaMessage", Story.GoalType.PDA, 15);
        StoryUtils.RegisterVoiceLog(BananaphobiaGoal.key, "Bananaphobia", EssentialAssets.VoiceLogIconAlterra);
    }

    private static void RegisterPersonnelReports()
    {
        // Cassy
        PDAHandler.AddEncyclopediaEntry("CassyPersonnelReport", CustomPdaPaths.PersonnelReportsPath, null, null,
            AssetBundles.Core.LoadAsset<Texture2D>("PersonnelProfile_Cassy"));
    }
}