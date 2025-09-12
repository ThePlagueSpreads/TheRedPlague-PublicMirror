using TheRedPlague.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheRedPlague.Mono.UI;

public class ChecklistUIEntry : MonoBehaviour
{
    public Image checkImage;
    public TextMeshProUGUI text;

    private ChecklistUI _checklistUI;
    private PdaChecklistAPI.ChecklistEntry _entry;

    private bool _almostCompleted;

    public void SetUp(ChecklistUI checklistUI, PdaChecklistAPI.ChecklistEntry entry)
    {
        _checklistUI = checklistUI;
        _entry = entry;
        Refresh();
    }

    public void Refresh()
    {
        var titleText = _entry.FormatHandler != null
            ? _entry.FormatHandler.Invoke(_entry)
            : Language.main.Get(_entry.GetNameLanguageKey);

        var isComplete = PdaChecklistAPI.IsEntryCompleted(_entry);
        checkImage.sprite = isComplete ? _checklistUI.checkedSprite : _checklistUI.uncheckedSprite;

        if (!isComplete && !_almostCompleted)
        {
            _almostCompleted = PdaChecklistAPI.IsEntryAlmostCompleted(_entry);
        }

        var descriptionText = GetDescriptionText();

        text.text = $"<b><color=#25e6af>{titleText}</color></b>\n{descriptionText}";
    }

    private string GetDescriptionText()
    {
        // Main text
        var descText = Language.main.Get(_entry.GetDescLanguageKey);

        // Additional info on 'almost completed' entries
        if (_almostCompleted && !string.IsNullOrEmpty(_entry.AlmostCompletedMessage.RequiredStoryGoal))
            descText += $"\n\n<color=#FF1199>{Language.main.Get(_entry.GetAlmostCompletedDescLanguageKey)}</color>";

        return descText;
    }
}