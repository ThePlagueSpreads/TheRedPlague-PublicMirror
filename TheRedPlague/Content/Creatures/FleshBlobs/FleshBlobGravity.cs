using System.Collections.Generic;
using UnityEngine;

namespace TheRedPlague.Content.Creatures.FleshBlobs;

public class FleshBlobGravity : MonoBehaviour
{
    public FleshBlobGrowth growth;

    public float MaxDistance
    {
        get => field * growth.Size;
    } = 200f;

    public float GravitationalConstant
    {
        get => field * growth.Size;
    } = 200000;

    private static List<FleshBlobGravity> _targets = new List<FleshBlobGravity>();

    private void Start()
    {
        growth = GetComponent<FleshBlobGrowth>();
    }

    private void OnEnable()
    {
        _targets.Add(this);
    }
    
    private void OnDisable()
    {
        _targets.Remove(this);
    }

    public float GetGravitationalForceMagnitude(Vector3 atPosition)
    {
        if (Vector3.SqrMagnitude(transform.position - atPosition) > MaxDistance * MaxDistance) return 0;
        return Mathf.Clamp(GravitationalConstant / Vector3.SqrMagnitude(transform.position - atPosition), 0, 50 * growth.Size);
    }

    public static FleshBlobGravity GetClosest(Vector3 pos)
    {
        var closestDist = 200f * 200f;
        FleshBlobGravity closestBlob = null;

        foreach (var target in _targets)
        {
            var dist = Vector3.SqrMagnitude(pos - target.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestBlob = target;
            }
        }

        return closestBlob;
    }
    
    public static FleshBlobGravity GetStrongest(Vector3 pos)
    {
        var highestStrength = 0f;
        var actualMaxSqrDistance = 200 * 200;
        FleshBlobGravity strongestBlob = null;

        foreach (var target in _targets)
        {
            var strength = target.GetGravitationalForceMagnitude(pos);
            if (strength > highestStrength && Vector3.SqrMagnitude(pos - target.transform.position) < actualMaxSqrDistance)
            {
                highestStrength = strength;
                strongestBlob = target;
            }
        }

        return strongestBlob;
    }
}