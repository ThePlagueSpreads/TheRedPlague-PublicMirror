using System;

namespace TheRedPlague.Framework.API.Upgrades;

public static class VehicleUpgradeUtils
{
    public static void SetOnUpgradeChanged(TechType upgrade, Action<Vehicle, int> action)
    {
        VehicleUpgradePatcher.OnChanged.Add(upgrade, action);
    }
}