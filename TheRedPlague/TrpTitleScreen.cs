using BepInEx;
using mset;
using Nautilus.Handlers.TitleScreen;
using Nautilus.Utility;
using TheRedPlague.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheRedPlague;

public static class TrpTitleScreen
{
    public const string TitleScreenLocalizationName = "RedPlagueTitleScreenName";
    
    private static GameObject _overlayLogoInstance;

    public static void RegisterTitleScreenCompatibility(BaseUnityPlugin plugin)
    {
        TitleScreenHandler.RegisterTitleScreenObject("RedPlagueTitleScreen",
            new TitleScreenHandler.CustomTitleData(TitleScreenLocalizationName,
                new WorldObjectTitleAddon(SpawnOverlayLogo),
                new WorldObjectTitleAddon(SpawnRedPlagueLogo),
                new WorldObjectTitleAddon(SpawnMainMenuButtons),
                new MusicTitleAddon(AudioUtils.GetFmodAsset("ProjectTRP")),
                new SkyChangeTitleAddon(3f, new SkyChangeTitleAddon.Settings(6, 0.2f)))
        );
    }

    private static GameObject SpawnOverlayLogo()
    {
        var logo = Object.Instantiate(Plugin.RedPlagueMainMenu.LoadAsset<GameObject>("RedPlagueOverlayLogoPrefab"));
        var sa = logo.AddComponent<SkyApplier>();
        sa.renderers = logo.GetComponentsInChildren<Renderer>(true);
        sa.anchorSky = Skies.Custom;
        sa.SetCustomSky(Object.FindObjectOfType<Sky>());
        logo.transform.position = new Vector3(-25.5f, 1, 40);
        logo.transform.eulerAngles = Vector3.up * 180;
        MaterialUtils.ApplySNShaders(logo);
        UpdateOverlayLogoProgress(logo.transform);
        _overlayLogoInstance = logo;
        return logo;
    }

    private static GameObject SpawnRedPlagueLogo()
    {
        var logo = Object.Instantiate(Plugin.RedPlagueMainMenu.LoadAsset<GameObject>("RedPlagueLogoPrefab"));
        var sa = logo.AddComponent<SkyApplier>();
        sa.renderers = logo.GetComponentsInChildren<Renderer>();
        sa.anchorSky = Skies.Custom;
        sa.SetCustomSky(Object.FindObjectOfType<Sky>());
        logo.transform.position = new Vector3(5, 6f, 35f);
        logo.transform.eulerAngles = Vector3.up * 180;
        logo.transform.localScale = Vector3.one * 0.6f;
        MaterialUtils.ApplySNShaders(logo);
        return logo;
    }

    private static void UpdateOverlayLogoProgress(Transform parent)
    {
        int activeProgress = GlobalRedPlagueProgressTracker.GetCurrentProgressValue();
        int highestChildIndex = parent.childCount - 1;
        int activeChildIndex = Mathf.Min(activeProgress - 1, highestChildIndex);
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.SetActive(activeChildIndex == i);
        }
    }

    public static void RefreshMainMenu()
    {
        if (_overlayLogoInstance != null)
        {
            UpdateOverlayLogoProgress(_overlayLogoInstance.transform);
        }
    }
    
    private static GameObject SpawnMainMenuButtons()
    {
        if (Plugin.Options.DisableMainMenuButtons)
        {
            return new GameObject("TrpTitleScreenImpostor");
        }
        
        var mainMenu = uGUI_MainMenu.main;
        if (mainMenu == null)
        {
            Plugin.Logger.LogWarning("uGUI_MainMenu.main was not found! Skipping adding main menu buttons.");
            return new GameObject("TrpTitleScreenImpostor");
        }

        var parent = new GameObject("TheRedPlagueMainMenuButtons");
        var rectTransform = parent.AddComponent<RectTransform>();
        rectTransform.SetParent(mainMenu.transform.Find("Panel"));
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchorMin = new Vector2(0.04f, 0.92f);
        rectTransform.anchorMax = new Vector2(0.04f, 0.92f);
        rectTransform.offsetMax = new Vector2(20, 0);
        rectTransform.sizeDelta = new Vector2(0, 160);
        var layoutGroup = parent.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childAlignment = TextAnchor.LowerLeft;
        layoutGroup.childControlHeight = false;
        layoutGroup.childControlWidth = false;
        layoutGroup.childScaleHeight = false;
        layoutGroup.childScaleWidth = false;
        
        // ADD BUTTONS
        SpawnButton("RedPlagueDiscordText", rectTransform, () => OpenURL("https://www.discord.com/invite/the-red-plague"));
        SpawnButton("RedPlaguePatreonText", rectTransform, () => OpenURL("https://www.patreon.com/TheRedPlagueDevelopmentTeam"));
        SpawnButton("RedPlagueYouTubeText", rectTransform, () => OpenURL("https://www.youtube.com/@ThePlagueSpreads"));

        var text = new GameObject("TrpSocialsText");
        var textTransform = text.AddComponent<RectTransform>();
        text.AddComponent<LayoutElement>().ignoreLayout = true;
        textTransform.SetParent(rectTransform);
        textTransform.localRotation = Quaternion.identity;
        textTransform.localScale = Vector3.one;
        textTransform.pivot = new Vector2(0.5f, 0);
        textTransform.sizeDelta = new Vector2(250, 50);
        textTransform.localPosition = new Vector3(112, -10);
        var textComponent = text.AddComponent<TextMeshProUGUI>();
        textComponent.font = FontUtils.Aller_W_Bd;
        textComponent.text = Language.main.Get("RedPlagueSocialsText");
        textComponent.color = new Color(0.82f, 0, 0);
        textComponent.outlineColor = Color.black;
        textComponent.outlineWidth = 0.3f;
        textComponent.fontSize = 24.5f;
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.raycastTarget = false;

        // Force refresh layout
        parent.SetActive(false);
        parent.SetActive(true);

        return parent;

        void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }

    private static void SpawnButton(string text, Transform parent, System.Action onClick)
    {
        var obj = new GameObject("Button - " + text);
        var rectTransform = obj.AddComponent<RectTransform>();
        rectTransform.SetParent(parent, false);
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;
        rectTransform.pivot = new Vector2(0, 0.5f);
        rectTransform.sizeDelta = new Vector2(190, 42);
        
        var image = obj.AddComponent<Image>();
        image.sprite = Plugin.RedPlagueMainMenu.LoadAsset<Sprite>("MainMenuButtonNormal");
        image.type = Image.Type.Sliced;
        image.pixelsPerUnitMultiplier = 4;
        
        var selectedImage = Plugin.RedPlagueMainMenu.LoadAsset<Sprite>("MainMenuButtonSelected");
        
        var button = obj.AddComponent<Button>();
        var buttonEvent = new Button.ButtonClickedEvent();
        buttonEvent.AddListener(onClick.Invoke);
        button.transition = Selectable.Transition.SpriteSwap;
        button.onClick = buttonEvent;
        button.targetGraphic = image;
        var state = button.spriteState;
        state.pressedSprite = selectedImage;
        state.highlightedSprite = selectedImage;
        button.spriteState = state;

        var textObj = new GameObject("Text");
        var textTransform = textObj.AddComponent<RectTransform>();
        textTransform.SetParent(rectTransform, false);
        textTransform.localPosition = Vector3.zero;
        textTransform.localRotation = Quaternion.identity;
        textTransform.localScale = Vector3.one;
        textTransform.anchorMin = Vector2.zero;
        textTransform.anchorMax = Vector2.one;
        textTransform.offsetMin = Vector2.zero;
        textTransform.offsetMax = Vector2.zero;
        var textMesh = textObj.AddComponent<TextMeshProUGUI>();
        textMesh.font = FontUtils.Aller_Rg;
        textMesh.fontSize = 18;
        textMesh.text = Language.main.Get(text);
        textMesh.raycastTarget = false;
        textMesh.alignment = TextAlignmentOptions.Center;
    }
}