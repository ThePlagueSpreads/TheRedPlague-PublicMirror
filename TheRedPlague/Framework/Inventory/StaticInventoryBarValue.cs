using UnityEngine;

namespace TheRedPlague.Framework.Inventory;

public class StaticInventoryBarValue : MonoBehaviour, ICustomInventoryBarValue
{
    public float value;
    
    public float GetBarPercentage()
    {
        return value;
    }
}