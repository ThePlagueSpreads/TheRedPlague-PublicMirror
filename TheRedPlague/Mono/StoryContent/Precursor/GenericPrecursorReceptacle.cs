using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent.Precursor;

public abstract class GenericPrecursorReceptacle : HandTarget, IHandTarget
{
    private PlayerCinematicController _cinematicController;

    private Animator _animator;

    protected virtual FMODAsset UseSound { get; } = AudioUtils.GetFmodAsset("event:/player/cube terminal_use");

    protected virtual FMODAsset OpenSound { get; } = AudioUtils.GetFmodAsset("event:/player/cube terminal_open");

    protected virtual FMODAsset CloseSound { get; } = AudioUtils.GetFmodAsset("event:/player/cube terminal_close");

    private GameObject _insertedItem;
    private int _restoreQuickSlot = -1;
    
    private static readonly int OpenAnimationParameter = Animator.StringToHash("Open");

    public override void Awake()
    {
        base.Awake();
        
        _animator = GetComponent<Animator>();
        _cinematicController = GetComponent<PlayerCinematicController>();
        _cinematicController.informGameObject = gameObject;
    }

    public void OnHandHover(GUIHand hand)
    {
        if (!IsAcceptingItems()) return;
        
        HandReticle.main.SetText(HandReticle.TextType.Hand, GetHandUseText(), true,
            GameInput.Button.LeftHand);

        HandReticle.main.SetText(HandReticle.TextType.HandSubscript, string.Empty, false);
        HandReticle.main.SetIcon(HandReticle.IconType.Hand);
    }

    public void OnHandClick(GUIHand hand)
    {
        if (!IsAcceptingItems()) return;

        var techType = TechTypeToRemove;
        var removedItem = Inventory.main.container.RemoveItem(techType);

        if (removedItem == null) return;

        _restoreQuickSlot = Inventory.main.quickSlots.activeSlot;
        Inventory.main.ReturnHeld(true);
        _insertedItem = removedItem.gameObject;
        Destroy(_insertedItem.GetComponent<PlagueHeartBehavior>());
        _insertedItem.transform.SetParent(Inventory.main.toolSocket);
        OnCinematicStarted(_insertedItem.transform, techType);
        _insertedItem.SetActive(true);
        var component = _insertedItem.GetComponent<Rigidbody>();
        if (component != null)
        {
            UWE.Utils.SetIsKinematicAndUpdateInterpolation(component, true);
        }

        _cinematicController.StartCinematicMode(Player.main);
        if (UseSound)
            Utils.PlayFMODAsset(UseSound, transform);
    }

    public void OpenDeck()
    {
        if (!IsAcceptingItems())
        {
            return;
        }

        _animator.SetBool(OpenAnimationParameter, true);
        if (OpenSound)
            Utils.PlayFMODAsset(OpenSound, base.transform);
    }

    public void CloseDeck()
    {
        if (!_animator.GetBool(OpenAnimationParameter)) return;
        _animator.SetBool(OpenAnimationParameter, false);
        if (CloseSound)
            Utils.PlayFMODAsset(CloseSound, base.transform);
    }

    public void OnPlayerCinematicModeEnd(PlayerCinematicController controller)
    {
        if (ShouldDestroyInsertedItem())
        {
            Destroy(_insertedItem);
        }

        CloseDeck();
        OnCinematicEnded();

        if (_restoreQuickSlot != -1)
        {
            Inventory.main.quickSlots.Select(_restoreQuickSlot);
        }
    }

    protected abstract TechType TechTypeToRemove { get; }
    protected abstract bool IsAcceptingItems();
    protected abstract void OnCinematicStarted(Transform itemTransform, TechType itemTechType);
    protected abstract void OnCinematicEnded();
    protected abstract string GetHandUseText();
    protected virtual bool ShouldDestroyInsertedItem() => true;
}