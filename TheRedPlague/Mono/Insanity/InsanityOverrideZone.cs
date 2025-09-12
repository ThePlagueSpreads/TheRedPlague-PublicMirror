using System.Collections.Generic;
using UnityEngine;

namespace TheRedPlague.Mono.Insanity;

public class InsanityOverrideZone : MonoBehaviour
{
    public float overrideValue;
    public float radius;
    public bool onlyIndoors;
    
    private static readonly List<InsanityOverrideZone> Zones = new();

    private void OnEnable()
    {
        Zones.Add(this);
    }

    private void OnDisable()
    {
        Zones.Remove(this);
    }

    public static bool TryGetZone(Vector3 position, bool indoors, out float value)
    {
        foreach (var zone in Zones)
        {
            if (zone.onlyIndoors && !indoors)
            {
                continue;
            }

            if (Vector3.SqrMagnitude(position - zone.transform.position) < zone.radius * zone.radius)
            {
                value = zone.overrideValue;
                return true;
            }
        }

        value = -1f;
        return false;
    }
}