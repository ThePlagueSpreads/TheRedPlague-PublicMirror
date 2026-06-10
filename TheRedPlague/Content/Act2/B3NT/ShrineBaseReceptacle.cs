using System;
using Story;
using TheRedPlague.Content.Items.Resources;
using TheRedPlague.Framework.Behaviour.Precursor;
using TheRedPlague.Framework.UI;
using UnityEngine;

namespace TheRedPlague.Content.Act2.B3NT;

public class ShrineBaseReceptacle : GenericPrecursorReceptacle, IStoryGoalListener
{
    public CanvasFader fader;
    public Collider collider;

    protected override TechType TechTypeToRemove => PlagueCatalyst.Info.TechType;
    protected override FMODAsset OpenSound => null;
    protected override FMODAsset CloseSound => null;

    private void Start()
    {
        StoryGoalManager.main.AddListener(this);
        var acceptingItems = IsAcceptingItems();
        collider.enabled = acceptingItems;
        fader.SetAlpha(acceptingItems ? 1 : 0, true);
    }

    protected override bool IsAcceptingItems()
    {
        return StoryGoalManager.main.IsGoalComplete(Act2Story.BennetAcceptingCatalyst.key) &&
               !StoryGoalManager.main.IsGoalComplete(Act2Story.BennetReceivedCatalyst.key);
    }

    protected override void OnCinematicStarted(Transform itemTransform, TechType itemTechType)
    {
        Act2Story.BennetReceivedCatalyst.Trigger();

        itemTransform.localPosition = new Vector3(0, 0.02f, -0.05f);
        itemTransform.localEulerAngles = new Vector3(34, 0, 0);
        itemTransform.localScale = Vector3.one * 0.45f;
        itemTransform.GetChild(0).localPosition = new Vector3(0, 0.099f, 0);
        itemTransform.GetChild(0).localEulerAngles = Vector3.zero;

        BennetCatalystAnimationController.CreateInstance(itemTransform,
            BennetController.Main != null ? BennetController.Main.eyeTransform : null);
    }

    protected override void OnCinematicEnded()
    {
    }

    protected override string GetHandUseText()
    {
        return Language.main.Get("ShrineBaseReceptacleInteract");
    }

    public void NotifyGoalComplete(string key)
    {
        if (string.Equals(key, Act2Story.BennetAcceptingCatalyst.key, StringComparison.OrdinalIgnoreCase))
        {
            collider.enabled = true;
            fader.SetAlpha(1, false);
        }
        else if (string.Equals(key, Act2Story.BennetReceivedCatalyst.key, StringComparison.OrdinalIgnoreCase))
        {
            collider.enabled = false;
            fader.SetAlpha(0, false);
        }
    }

    private void OnDestroy()
    {
        StoryGoalManager.main.RemoveListener(this);
    }

    protected override bool ShouldDestroyInsertedItem()
    {
        return false;
    }
}