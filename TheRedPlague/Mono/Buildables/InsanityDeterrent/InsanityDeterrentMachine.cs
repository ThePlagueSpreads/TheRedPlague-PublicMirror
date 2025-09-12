using TheRedPlague.Mono.Insanity;
using UnityEngine;

namespace TheRedPlague.Mono.Buildables.InsanityDeterrent;

public class InsanityDeterrentMachine : Constructable, IHandTarget
{
    private const float PowerPerSecond = 0.35f;

    public InsanityOverrideZone overrideZone;
    public FMOD_CustomLoopingEmitter soundEmitter;

    private PowerRelay _powerRelay;
    private bool _working;

    public override void Start()
    {
        base.Start();
        UpdateActive();
        _powerRelay = GetComponentInParent<PowerRelay>();
        InvokeRepeating(nameof(ConsumePower), Random.value, 1);
    }

    public override void OnConstructedChanged(bool constructed)
    {
        base.OnConstructedChanged(constructed);
        UpdateActive();
        if (_powerRelay == null)
        {
            _powerRelay = GetComponentInParent<PowerRelay>();
        }
    }

    private void UpdateActive()
    {
        SetWorking(constructed && (!GameModeUtils.RequiresPower() ||
                                   (_powerRelay != null && _powerRelay.GetPower() >= PowerPerSecond)));

    }

    private void ConsumePower()
    {
        if (!isActiveAndEnabled)
            return;
        if (!constructed)
            return;
        SetWorking(!GameModeUtils.RequiresPower() ||
                   (_powerRelay != null && _powerRelay.ConsumeEnergy(PowerPerSecond, out _)));
    }

    private void SetWorking(bool enable)
    {
        overrideZone.enabled = enable;
        _working = enable;
        if (enable)
            soundEmitter.Play();
        else soundEmitter.Stop();
    }

    public void OnHandHover(GUIHand hand)
    {
        if (!constructed) return;
        if (_working)
        {
            HandReticle.main.SetText(HandReticle.TextType.Hand,
                Language.main.GetFormat("InsanityDeterrentWorking", PowerPerSecond), false);
        }
        else
        {
            HandReticle.main.SetText(HandReticle.TextType.Hand, "InsanityDeterrentNoPower", true);
        }

        HandReticle.main.SetIcon(HandReticle.IconType.Info);
    }

    public void OnHandClick(GUIHand hand)
    {
    }
}