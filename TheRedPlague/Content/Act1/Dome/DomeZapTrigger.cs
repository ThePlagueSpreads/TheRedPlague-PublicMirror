using Nautilus.Utility;
using UnityEngine;

namespace TheRedPlague.Content.Act1.Dome;

public class DomeZapTrigger : MonoBehaviour
{
    public float electricalDamage = 25;
    public float normalDamage = 100;
    public float cooldown = 2f;

    private static readonly FMODAsset Sound = AudioUtils.GetFmodAsset("DomeBaseZap");

    private float _nextZapTime;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < _nextZapTime) return;
        var player = Player.main;
        if (other.gameObject == player.gameObject)
        {
            if (player.justSpawned) return;
            if (!DomeBaseUtils.GetIsPlayerInsideDomeBase()) return;
            if (player.isWaitingForTeleportation) return;
            if (GotoConsoleCommand.main && GotoConsoleCommand.main.movingPlayer) return;
            if (player.IsSwimming()) return;
            if (player.cinematicModeActive) return;
            if (player.groundMotor.GetVelocity().y >= 0) return;
            
            DomeBaseUtils.ExitDomeBase();
            var lm = player.liveMixin;
            lm.TakeDamage(electricalDamage, type: DamageType.Electrical);
            lm.TakeDamage(normalDamage);
            FMODUWE.PlayOneShot(Sound, transform.position);
            _nextZapTime = Time.time + cooldown;
        }
    }
}