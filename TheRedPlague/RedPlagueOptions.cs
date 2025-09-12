using Nautilus.Json;
using Nautilus.Options;
using Nautilus.Options.Attributes;
using TheRedPlague.Managers;
using TheRedPlague.Patches.MainMenu;
using TheRedPlague.Patches.UI;

namespace TheRedPlague;

[Menu("The Red Plague")]
public class RedPlagueOptions : ConfigFile
{
    [Toggle("Disable jumpscares")]
    public bool DisableJumpScares = false;

    [Toggle("Disable insanity screen effect")]
    public bool DisableInsanityScreenEffect = false;

    [Toggle("Disable power fluctuations")]
    public bool DisablePowerFluctuations = false;

    [Toggle("Enable insanity in creative mode")]
    public bool EnableInsanityInCreative = false;

    [Toggle("Disable main menu socials buttons")]
    public bool DisableMainMenuButtons = false;

    [Slider("Sanity drain multiplier", 0, 3, DefaultValue = 1, Format = "{0:0.00}x", Step = 0.02f,
        Tooltip = "Affects the rate of how fast your sanity bar drains. The default and recommended value is 1.00x.\n" +
                  "0.00x: no drain.\n3.00x: three times as fast as the default rate.")]
    public float InsanityDrainMultiplier = 1f;
    
    [Button("Reset logo infection progress")]
    public void ResetLogoInfectionProgress(ButtonClickedEventArgs e)
    {
        GlobalRedPlagueProgressTracker.ForgetCurrentProgress();
        TrpTitleScreen.RefreshMainMenu();
    }
}