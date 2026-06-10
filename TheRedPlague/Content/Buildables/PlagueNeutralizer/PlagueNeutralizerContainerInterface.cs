using Nautilus.Extensions;
using Nautilus.Utility;
using TheRedPlague.Compatibility;
using TheRedPlague.Content.Items.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheRedPlague.Content.Buildables.PlagueNeutralizer;

public class PlagueNeutralizerContainerInterface : StorageContainer
{
    public PlagueNeutralizerMachine machine;

    private const string PlagueNeutralizerUIParentName = "PlagueNeutralizerUI";
    
    private static readonly FMODAsset InteractSound = AudioUtils.GetFmodAsset("PlagueNeutralizerInteract");

    public override void Open(Transform useTransform)
    {
        base.Open(useTransform);
        if (open)
        {
            EnableUI();
        }
    }

    public override void OnClose()
    {
        base.OnClose();
        DisableUI();
    }

    public override void Awake()
    {
        base.Awake();
        container.isAllowedToAdd += IsAllowedToAddItem;
        container.isAllowedToRemove += IsAllowedToRemoveItem;
        container.onRemoveItem += OnRemoveItem;
    }

    private bool IsAllowedToAddItem(Pickupable pickupable, bool verbose)
    {
        var canAdd = BaseBioReactor.charge.TryGetValue(pickupable.GetTechType(), out var charge) && charge > 0 ||
                     pickupable.gameObject.GetComponent<Eatable>() != null;
        if (!canAdd && verbose)
            ErrorMessage.AddMessage(Language.main.Get("PlagueNeutralizerOrganicItemsWarning"));
        return canAdd;
    }

    private void OnRemoveItem(InventoryItem item)
    {
        if (item != null && item.item != null && item.item.GetTechType() == PlagueIngot.Info.TechType)
        {
            Act2Story.NeutralizedPlagueIngotItemGoal.Trigger();
        }
    }

    private bool IsAllowedToRemoveItem(Pickupable pickupable, bool verbose)
    {
        return pickupable.GetTechType() == PlagueIngot.Info.TechType;
    }

    private void OnDisable()
    {
        DisableUI();
    }

    private void OnInsertCatalystButtonPressed()
    {
        if (machine.HasSpaceForCatalysts())
        {
            if (!GameModeUtils.RequiresIngredients() || Inventory.main.DestroyItem(PlagueCatalyst.Info.TechType))
                machine.AddPlagueCatalyst();
            else
                ErrorMessage.AddMessage(Language.main.Get("TrpMachineNotEnoughItems"));
        }
        else
        {
            ErrorMessage.AddMessage(Language.main.Get("PlagueNeutralizerFullWarning"));
        }
    }
    
    private void OnRemoveCatalystButtonPressed()
    {
        if (!Inventory.main.HasRoomFor(PlagueCatalyst.Info.TechType))
        {
            ErrorMessage.AddMessage(Language.main.Get("InventoryFull"));
            return;
        }
        
        if (!machine.CanRemovePlagueCatalyst())
        {
            ErrorMessage.AddMessage(Language.main.Get("PlagueNeutralizerEmptyWarning"));
            return;
        }

        machine.RemovePlagueCatalyst();
    }

    private void EnableUI()
    {
        if (!TryGetStorageContainerInventoryTab(out var tabRoot)) return;

        // create interface parent
        var uiParent = new GameObject(PlagueNeutralizerUIParentName).EnsureComponent<RectTransform>();
        uiParent.SetParent(tabRoot);
        uiParent.localPosition = Vector3.zero;
        uiParent.localEulerAngles = Vector3.zero;
        uiParent.localScale = Vector3.one;
        uiParent.anchorMin = new Vector2(0.5f, 0);
        uiParent.anchorMax = new Vector2(0.5f, 1f);

        var insertTextTransform = AddUIObject("InsertText", uiParent, new Vector2(0, ModCompatibilityManager.HasAdvancedInventory() ? 50 : 250), new Vector2(300, 20));
        var insertText = insertTextTransform.gameObject.AddComponent<TextMeshProUGUI>();
        insertText.raycastTarget = false;
        insertText.text = Language.main.Get("PlagueNeutralizerUIInsertText");
        insertText.enableWordWrapping = false;
        insertText.overflowMode = TextOverflowModes.Overflow;
        insertText.alignment = TextAlignmentOptions.Center;
        insertText.fontSize = 23;

        var insertButtonTransform =
            AddUIObject("InsertCatalystButton", uiParent, new Vector2(-60, ModCompatibilityManager.HasAdvancedInventory() ? -480 : -280), new Vector2(100, 100));
        var buttonImage = insertButtonTransform.gameObject.AddComponent<Image>();
        buttonImage.sprite = AssetBundles.Core.LoadAsset<Sprite>("NeutralizerInsertPlagueCatalystButton");
        var button = insertButtonTransform.gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnInsertCatalystButtonPressed);
        var buttonTooltip = insertButtonTransform.gameObject.AddComponent<PlagueNeutralizerCatalystButtonTooltip>();
        buttonTooltip.machine = machine;
        buttonTooltip.insert = true;
        
        var removeButtonTransform =
            AddUIObject("RemoveCatalystButton", uiParent, new Vector2(60, ModCompatibilityManager.HasAdvancedInventory() ? -480 : -280), new Vector2(100, 100));
        var removeButtonImage = removeButtonTransform.gameObject.AddComponent<Image>();
        removeButtonImage.sprite = AssetBundles.Core.LoadAsset<Sprite>("NeutralizerRemovePlagueCatalystButton");
        var removeButton = removeButtonTransform.gameObject.AddComponent<Button>();
        removeButton.onClick.AddListener(OnRemoveCatalystButtonPressed);
        var removeButtonTooltip = removeButtonTransform.gameObject.AddComponent<PlagueNeutralizerCatalystButtonTooltip>();
        removeButtonTooltip.machine = machine;
        removeButtonTooltip.insert = false;

        if (ModCompatibilityManager.HasAdvancedInventory())
        {
            var mask = tabRoot.GetComponentInParent<RectMask2D>();
            if (mask)
            {
                mask.enabled = false;
            }
        }
        
        Utils.PlayFMODAsset(InteractSound, transform.position);
    }

    private void DisableUI()
    {
        if (!TryGetStorageContainerInventoryTab(out var tabRoot)) return;
        var ui = tabRoot.Find(PlagueNeutralizerUIParentName);
        if (ui != null)
        {
            Destroy(ui.gameObject);
        }

        if (ModCompatibilityManager.HasAdvancedInventory())
        {
            var mask = tabRoot.GetComponentInParent<RectMask2D>();
            if (mask)
            {
                mask.enabled = true;
            }
        }
    }

    private RectTransform AddUIObject(string objectName, RectTransform parent, Vector2 position, Vector2 sizeDelta)
    {
        var obj = new GameObject(objectName);
        var rectTransform = obj.EnsureComponent<RectTransform>();
        rectTransform.SetParent(parent);
        rectTransform.localEulerAngles = Vector3.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.sizeDelta = sizeDelta;
        rectTransform.localScale = Vector3.one;
        rectTransform.localPosition = position;
        return rectTransform;
    }

    private bool TryGetStorageContainerInventoryTab(out Transform tabRoot)
    {
        var pdaScreen = uGUI_PDA.main;
        if (pdaScreen == null)
        {
            tabRoot = null;
            return false;
        }

        tabRoot = pdaScreen.transform.SearchChild("StorageContainer");
        return tabRoot != null;
    }
}