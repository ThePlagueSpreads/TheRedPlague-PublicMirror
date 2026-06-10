using Nautilus.Utility;
using Story;
using TheRedPlague.Content.Act3.DomeBase;
using UnityEngine;

namespace TheRedPlague.Content.Act1.Dome;

// TODO: Fix PDA scale inside dome base
public static class DomeBaseUtils
{
    public const float DomeBaseScale = 4.5f;
    
    private static bool _registered;

    private static bool _isPreviousStateSaved;
    private static bool _insideDomeBase;

    private static Vector3 _originalScale;
    private static float _originalGroundAcceleration;
    private static float _originalAirAcceleration;
    private static float _originalForwardMoveSpeed;
    private static float _originalSideMoveSpeed;
    private static float _originalBackwardMoveSpeed;
    private static float _originalGravity;
    private static float _originalJumpBaseHeight;
    private static float _originalJumpHeight;
    private static float _originalStepOffset;
    private static bool _originalInvincibleState;
    private static float _originalMaxUIInteractionDistance;

    public static bool GetIsPlayerInsideDomeBase() => _insideDomeBase;
    
    public static void EnterDomeBase()
    {
        if (_insideDomeBase)
            return;

        if (!_registered)
        {
            SaveUtils.RegisterOnQuitEvent(OnGameQuit);
            _registered = true;
        }
        
        var player = Player.main;
        
        if (!_isPreviousStateSaved)
        {
            if (player)
            {
                _originalScale = player.transform.localScale;
                _originalGroundAcceleration = player.groundMotor.groundAcceleration;
                _originalAirAcceleration = player.groundMotor.airAcceleration;
                _originalForwardMoveSpeed = player.groundMotor.forwardMaxSpeed;
                _originalBackwardMoveSpeed = player.groundMotor.movement.maxBackwardsSpeed;
                _originalSideMoveSpeed = player.groundMotor.movement.maxSidewaysSpeed;
                _originalGravity = player.groundMotor.gravity;
                _originalJumpBaseHeight = player.groundMotor.jumping.baseHeight;
                _originalJumpHeight = player.groundMotor.jumpHeight;
                _originalStepOffset = player.groundMotor.controller.stepOffset;
                _originalInvincibleState = player.liveMixin.invincible;
                _originalMaxUIInteractionDistance = FPSInputModule.current.maxInteractionDistance;
            }
            
            _isPreviousStateSaved = true;
        }

        if (player)
        {
            var playerScale = Vector3.one * DomeBaseScale;
            player.gameObject.AddComponent<ForcePlayerScaleWhileInDome>().scale = playerScale;
            player.transform.localScale = playerScale;
            player.groundMotor.groundAcceleration = _originalGroundAcceleration * DomeBaseScale;
            player.groundMotor.airAcceleration = _originalAirAcceleration * DomeBaseScale;
            player.groundMotor.forwardMaxSpeed = _originalForwardMoveSpeed * DomeBaseScale;
            player.groundMotor.movement.maxBackwardsSpeed = _originalBackwardMoveSpeed * DomeBaseScale;
            player.groundMotor.movement.maxSidewaysSpeed = _originalSideMoveSpeed * DomeBaseScale;
            player.groundMotor.gravity = _originalGravity * DomeBaseScale;
            player.groundMotor.jumping.baseHeight = _originalJumpBaseHeight * DomeBaseScale;
            player.groundMotor.jumpHeight = _originalJumpHeight * DomeBaseScale;
            player.groundMotor.controller.stepOffset = _originalStepOffset * DomeBaseScale;
            player.liveMixin.invincible = true;
            FPSInputModule.current.maxInteractionDistance = _originalMaxUIInteractionDistance * DomeBaseScale;
            Player.main.pda.SetIgnorePDAInput(true);
        }
        
        _insideDomeBase = true;
    }

    public static void ExitDomeBase()
    {
        if (!_insideDomeBase)
            return;
        
        if (_isPreviousStateSaved)
        {
            var player = Player.main;
            
            if (player)
            {
                Object.Destroy(player.gameObject.GetComponent<ForcePlayerScaleWhileInDome>());
                player.transform.localScale = _originalScale;
                player.groundMotor.groundAcceleration = _originalGroundAcceleration;
                player.groundMotor.airAcceleration = _originalAirAcceleration;
                player.groundMotor.forwardMaxSpeed = _originalForwardMoveSpeed;
                player.groundMotor.movement.maxBackwardsSpeed = _originalBackwardMoveSpeed;
                player.groundMotor.movement.maxSidewaysSpeed = _originalSideMoveSpeed;
                player.groundMotor.gravity = _originalGravity;
                player.groundMotor.jumping.baseHeight = _originalJumpBaseHeight;
                player.groundMotor.jumpHeight = _originalJumpHeight;
                player.groundMotor.controller.stepOffset = _originalStepOffset;
                player.liveMixin.invincible = _originalInvincibleState;
                FPSInputModule.current.maxInteractionDistance = _originalMaxUIInteractionDistance;
                Player.main.pda.SetIgnorePDAInput(false);
            }
        }
        
        _insideDomeBase = false;
    }
    
    private static void OnGameQuit()
    {
        _isPreviousStateSaved = false;
        _insideDomeBase = false;
    }
}