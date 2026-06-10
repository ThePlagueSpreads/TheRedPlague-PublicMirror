using TheRedPlague.Content.Equipment.PlagueKnife;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.Sucker;

public class AuroraSuckerBreachBlocker : HandTarget, IHandTarget
{
    public Material armoredMaterial;
    public Renderer mainBodyRenderer;
    public TechTag techTag;

    private bool _inDriveCore;
    
    private void OnEnable()
    {
        RadiationLeakSuckerObscuring.RegisterSucker(transform);
    }

    private void OnDisable()
    {
        RadiationLeakSuckerObscuring.UnregisterSucker(transform);
    }

    private void Start()
    {
        _inDriveCore = RadiationLeakSuckerObscuring.IsSuckerInDriveCoreRoom(transform.position);
        if (_inDriveCore)
        {
            mainBodyRenderer.sharedMaterial = armoredMaterial;
            if (techTag) techTag.type = SuckerPrefab.ArmoredSuckerTechType;
            else Plugin.Logger.LogWarning("Sucker failed to find tech tag");
        }
    }

    public void OnHandHover(GUIHand hand)
    {
        if (!_inDriveCore)
            return;
        
        if (KnownTech.Contains(PlagueKnife.Info.TechType) || Inventory.main.container.Contains(PlagueKnife.Info.TechType))
        {
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, "AuroraSuckerHintKnown", true);
            return;
        }
        
        HandReticle.main.SetText(HandReticle.TextType.HandSubscript, "AuroraSuckerHint", true);
    }

    public void OnHandClick(GUIHand hand)
    {
        
    }
}