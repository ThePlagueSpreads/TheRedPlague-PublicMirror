using System;
using TheRedPlague.Patches.Features;

namespace TheRedPlague.Utilities;

public static class VehicleUpgradeUtils
{
    public static void SetOnUpgradeChanged(TechType upgrade, Action<Vehicle, int> action)
    {
        VehicleUpgradePatcher.OnChanged.Add(upgrade, action);
    }
}