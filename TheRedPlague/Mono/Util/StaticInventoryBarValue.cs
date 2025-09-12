using TheRedPlague.Interfaces;
using UnityEngine;

namespace TheRedPlague.Mono.Util;

public class StaticInventoryBarValue : MonoBehaviour, ICustomInventoryBarValue
{
    public float value;
    
    public float GetBarPercentage()
    {
        return value;
    }
}