using System;
using System.Collections;
using HarmonyLib;
using Nautilus.Handlers.TitleScreen;
using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Patches.MainMenu;

[HarmonyPatch(typeof(uGUI_MainMenu))]
public class MainMenuPatches
{
    private const string ReloadSaveWarning =
        "[<color=#FF0000>The Red Plague</color>] Warning: Save corruption is possible for some mods if you load another save before quitting to desktop!";

    private const string ForceTitleScreenPlayerPrefKey = "TrpForceTitleScreen";
    
    private static int _timesLoaded;

    [HarmonyPostfix]
    [HarmonyPatch(nameof(uGUI_MainMenu.Start))]
    public static void StartPostfix()
    {
        UWE.CoroutineHost.StartCoroutine(SetTitleScreenToTrp());
        
        _timesLoaded++;
        if (_timesLoaded >= 2)
        {
            ErrorMessage.AddMessage(ReloadSaveWarning);
        }
    }

    private static IEnumerator SetTitleScreenToTrp()
    {
        if (PlayerPrefs.HasKey(ForceTitleScreenPlayerPrefKey))
        {
            yield break;
        }

        System.Reflection.FieldInfo choiceOptionField;
        System.Reflection.FieldInfo titleObjectDataField;
        System.Reflection.MethodInfo onActiveModChanged;
        try
        {
            var mainMenuPatcherType = AccessTools.TypeByName("Nautilus.Patchers.MainMenuPatcher");
            choiceOptionField = AccessTools.Field(mainMenuPatcherType, "_choiceOption");
            titleObjectDataField = AccessTools.Field(mainMenuPatcherType, "TitleObjectDatas");
            onActiveModChanged = AccessTools.Method(mainMenuPatcherType, "OnActiveModChanged");
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError("Exception thrown while trying to access Nautilus through reflection: " + e);
            yield break;
        }

        var giveUpTime = Time.realtimeSinceStartup + 100;
        
        yield return new WaitUntil(() => Time.realtimeSinceStartup > giveUpTime || choiceOptionField.GetValue(null) != null);
        
        var choiceOption = choiceOptionField.GetValue(null);
        if (choiceOption == null)
        {
            Plugin.Logger.LogError("Title screen changer timed out!");
            yield break;
        }

        var titleObjectData = titleObjectDataField.GetValue(null) as SelfCheckingDictionary<string, TitleScreenHandler.CustomTitleData>;

        var indexOfTrp = 0;
        bool trpFound = false;
        foreach (var data in titleObjectData)
        {
            if (data.Value.localizationKey == TrpTitleScreen.TitleScreenLocalizationName)
            {
                trpFound = true;
                break;
            }
            indexOfTrp++;
        }

        if (!trpFound)
        {
            Plugin.Logger.LogWarning("Failed to find TRP!");
            yield break;
        }

        var uguiChoice = choiceOption as uGUI_Choice;
        uguiChoice.value = indexOfTrp + 1;
        onActiveModChanged.Invoke(null, Array.Empty<object>());
        
        PlayerPrefs.SetInt(ForceTitleScreenPlayerPrefKey, 1);
    }
}