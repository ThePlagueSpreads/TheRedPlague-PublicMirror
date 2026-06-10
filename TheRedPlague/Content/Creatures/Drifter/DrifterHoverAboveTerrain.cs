using TheRedPlague.Compatibility;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.Drifter;

public class DrifterHoverAboveTerrain : CreatureAction
{
    public float maxDistanceAboveTerrain = 80;
    public float desiredDistanceAboveTerrain = 40;
    public float minDistanceAboveTerrain = 30;
    public float maxDepthToConsider = 800;
    public float chanceToBeABottom = 0.5f;
    public float raycastCheckDistance = 130f;
    public float forwardModifier = 5f;

    private const float UpdateRate = 2.3f;
    
    private float _targetHeight;
    private bool _active;
    
    private void Start()
    {
        if (Random.value <= chanceToBeABottom || transform.position.y < -maxDepthToConsider)
        {
            InvokeRepeating(nameof(CheckForTerrain), Random.value, UpdateRate);
        }
    }

    public override float Evaluate(Creature creature, float time)
    {
        if (!_active) return 0f;
        var yPos = transform.position.y;
        if (yPos > _targetHeight + maxDistanceAboveTerrain || yPos < _targetHeight + minDistanceAboveTerrain)
            return evaluatePriority;
        return 0f;
    }

    public override void Perform(Creature creature, float time, float deltaTime)
    {
        if (!_active) return;
        swimBehaviour.SwimTo(GetSwimPosition(), DrifterPrefab.BaseVelocity);
    }

    private Vector3 GetSwimPosition()
    {
        var pos = new Vector3(
            transform.position.x,
            _targetHeight + desiredDistanceAboveTerrain,
            transform.position.z);
        var forward = transform.forward * forwardModifier;
        return pos + forward;
    }

    private void CheckForTerrain()
    {
        if (!isActiveAndEnabled)
        {
            _active = false;
            return;
        }
        
        if (!TryGetTerrainHeightAtPosition(transform.position, out var val))
        {
            _active = false;
            return;
        }

        if (val < -maxDepthToConsider || transform.position.y < val - 10)
        {
            _active = false;
            return;
        }

        _targetHeight = val;
        _active = true;
    }
    
    
    private bool TryGetTerrainHeightAtPosition(Vector3 position, out float height)
    {
        if (ModCompatibilityManager.WorldHeightLibInstalled)
        {
            // SOFT DEPENDENCY - THIS CODE WILL THROW EXCEPTIONS ON ITS OWN
            if (WorldHeightLib.HeightMap.Instance.TryGetValueAtPosition(new Vector2(position.x, position.z),
                    out height))
            {
                return true;
            }
        }
        
        var streamer = LargeWorldStreamer.main;
        if (streamer == null)
        {
            height = 0;
            return false;
        }
        
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, raycastCheckDistance, 1 << LayerID.TerrainCollider))
        {
            height = hit.point.y;
            return true;
        }

        height = 0f;
        return false;
    }
}