using UnityEngine;

namespace TheRedPlague.Registration.Assets;

public static class EssentialAssets
{
    public static Texture2D ZombieInfectionTexture { get; private set; }
    public static Sprite MiscDecoIcon { get; private set; }
    
    public static Sprite VoiceLogIconAlterra { get; private set; } = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-Alterra");

    public static Sprite VoiceLogIconPda { get; private set; } = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-PDA");

    public static Sprite VoiceLogIconRadio { get; private set; } // reserve this for non-alterra comms
    public static Sprite VoiceLogIconDome { get; private set; }
    public static Sprite VoiceLogIconDerman { get; private set; }
    public static Sprite VoiceLogIconUnknown { get; private set; }
    public static Sprite VoiceLogIconB3NT { get; private set; }
    
    internal static void RegisterEssentials()
    {
        ZombieInfectionTexture = AssetBundles.Core.LoadAsset<Texture2D>("zombie_infection_bloody");
        MiscDecoIcon = AssetBundles.Core.LoadAsset<Sprite>("GenericDecoIcon");
        LoadStoryAssets();
    }
    
    private static void LoadStoryAssets()
    {
        VoiceLogIconAlterra = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-Alterra");
        VoiceLogIconPda = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-PDA");
        VoiceLogIconRadio = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-Radio");
        VoiceLogIconDome = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-Dome");
        VoiceLogIconDerman = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-Derman");
        VoiceLogIconUnknown = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-Unknown");
        VoiceLogIconB3NT = AssetBundles.Core.LoadAsset<Sprite>("LogIcon-B3NT");
    }
}