using System;
using Nautilus.Handlers;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using Nautilus.Utility;
using Story;
using TheRedPlague.Content.Equipment.PlagueArmor;
using TheRedPlague.Content.UI.Sanity;
using UnityEngine;
using UnityEngine.XR;

namespace TheRedPlague.Content.PlayerInfection;

public class PlagueDamageStat : MonoBehaviour
{
    public static PlagueDamageStat main;

    private const float DrainRate = 1f / 28f;
    private const float EarlyGameRegenerationRate = 1f / 15f;
    private const float SunlightRegenerationRate = 1f / 60f;
    
    private const float DeathDamageInterval = 5;
    private const float DeathDamageValue = 5;
    
    private const float MaxDepthForSunlight = 10;
    private const float MinSunlightScalarForRecharge = 0.65f;

    // The warning is VR-exclusive
    public float warnInterval = 60 * 8;
    public float warnThreshold = 75; // greater than 75 means 25% charge or less

    private float _timeDoDeathDamageAgain;

    private static FMODAsset LowChargeWarningSound { get; } = AudioUtils.GetFmodAsset("BiochemicalSuitRechargeRequired");
    private static string LowChargeWarningText => "BiochemicalSuitRechargeRequired";
    private float _timeCanWarnAgain;

    // Normal range of 0 to 100
    public float InfectionPercent
    {
        get => _saveData.plagueDamage;
        private set => _saveData.plagueDamage = value;
    }

    public Action<float> OnChargeEvent;

    private static readonly SaveData _saveData = SaveDataHandler.RegisterSaveDataCache<SaveData>();
    
    private void ChangeInfectionPercent(float change)
    {
        InfectionPercent = Mathf.Clamp(InfectionPercent + change, 0, 100);
    }
    
    public void TakeInfectionDamage(float change, bool ignorePlagueArmor = false)
    {
        var multiplier = 1f;
        if (!ignorePlagueArmor && PlagueArmorBehavior.IsPlagueArmorEquipped())
        {
            multiplier = 0.5f;
        }
        
        ChangeInfectionPercent(change * multiplier);
    }
    
    public void HealInfectionDamage(float change)
    {
        ChangeInfectionPercent(-change);
    }

    public void Charge(float percent)
    {
        ChangeInfectionPercent(-percent);
        OnChargeEvent?.Invoke(percent);
    }

    private void Awake()
    {
        main = this;
        _saveData.Load();
        Plugin.Logger.LogDebug("Loaded PlagueDamageStat SaveData");
    }

    private void Start()
    {
        PlagueDamageUI.Create();
        
        if (SuitChargeDisplayPatcher.SceneHud)
            SuitChargeDisplayPatcher.SceneHud.UpdateElements();
        
        Player.main.playerRespawnEvent.AddHandler(gameObject, OnRespawn);
    }

    private void OnRespawn(Player p)
    {
        InfectionPercent = 5;
    }

    private void Update()
    {
        if (InfectionPercent >= 99f)
        {
            if (Time.time > _timeDoDeathDamageAgain)
            {
                Player.main.liveMixin.TakeDamage(DeathDamageValue);
                _timeDoDeathDamageAgain = Time.time + DeathDamageInterval;
            }
        }
        
        if (ShouldWarn())
        {
            PlayWarningVoiceLine();
            _timeCanWarnAgain = Time.time + warnInterval;
        }

        var passiveDrainingAllowed = PassiveDrainingEnabled();
        if (!passiveDrainingAllowed)
        {
            // Give unconditional passive regen in the early game
            ChangeInfectionPercent(-EarlyGameRegenerationRate * Time.deltaTime);
            return;
        }
        
        // If passive draining IS enabled, but the player is in sunlight, regen instead

        if (IsInSunlight())
        {
            ChangeInfectionPercent(-SunlightRegenerationRate * Time.deltaTime);
            return;
        }

        // Code beyond this point is only valid for a DECREASE in sanity:
        
        if (GameModeUtils.IsInvisible())
            return;

        if (Player.main.IsFrozenStats())
            return;

        var drainMultiplier = 1f;
        
        // Decrease drain when inside a safe place
        if (Player.main.GetCurrentSub() != null)
            drainMultiplier = 0.5f;

        if (Player.main.GetVehicle() != null)
            drainMultiplier = 0.5f;
        
        ChangeInfectionPercent(DrainRate * drainMultiplier * Time.deltaTime * Plugin.Options.InsanityDrainMultiplier);
    }

    private bool PassiveDrainingEnabled()
    {
        if (StoryGoalManager.main == null)
            return false;
        
        if (StoryGoalManager.main.IsGoalComplete(Act1Story.UseBiochemicalProtectionSuitEvent.key))
        {
            return true;
        }

        if (StoryUtils.IsAct1Complete())
        {
            return true;
        }

        return false;
    }

    private bool IsInSunlight()
    {
        var depthOfPlayer = Ocean.GetDepthOf(Player.main.gameObject);
        if (depthOfPlayer > MaxDepthForSunlight) return false;
        return DayNightCycle.main.GetLocalLightScalar() > MinSunlightScalarForRecharge;
    }

    private bool ShouldWarn()
    {
        // Make sure the PDA only warns about this while the game is in VR
        if (!XRSettings.enabled)
            return false;
        if (Time.time < _timeCanWarnAgain)
            return false;
        return InfectionPercent > warnThreshold;
    }

    private void PlayWarningVoiceLine()
    {
        if (WaitScreen.IsWaiting) return;
        FMODUWE.PlayOneShot(LowChargeWarningSound, transform.position);
        Subtitles.Add(LowChargeWarningText);
    }

    [FileName("PlagueDamage")]
    public class SaveData : SaveDataCache
    {
        public float plagueDamage;
    }
}