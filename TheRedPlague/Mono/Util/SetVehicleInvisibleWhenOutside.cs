using System;
using UnityEngine;

namespace TheRedPlague.Mono.Util;

public class SetVehicleInvisibleWhenOutside : MonoBehaviour, IScheduledUpdateBehaviour
{
    public Vehicle vehicle;
    public EcoTarget target;
    public EcoTargetType newTargetType = EcoTargetType.None;

    private bool _hasCachedTargetType;
    private EcoTargetType _originalTargetType;
    
    public int scheduledUpdateIndex { get; set; }

    private void Start()
    {
        if (target == null)
        {
            Plugin.Logger.LogWarning("This object has no EcoTarget assigned! Skipping fix.");
            return;
        }

        if (!_hasCachedTargetType)
        {
            _originalTargetType = target.GetTargetType();
            _hasCachedTargetType = true;
        }
        
        UpdateSchedulerUtils.Register(this);
    }

    private void OnDestroy()
    {
        UpdateSchedulerUtils.Deregister(this);
    }
    
    public void ScheduledUpdate()
    {
        if (!isActiveAndEnabled) return;
        
        var playerInside = Player.main.GetVehicle() == vehicle;
        target.SetTargetType(playerInside ? _originalTargetType : newTargetType);
    }
    
    public string GetProfileTag()
    {
        return "TRP:VehicleInvisibleWhenOutside";
    }
}