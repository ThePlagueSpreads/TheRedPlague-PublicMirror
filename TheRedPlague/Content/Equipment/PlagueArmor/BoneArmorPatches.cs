using HarmonyLib;

namespace TheRedPlague.Content.Equipment.PlagueArmor;

[HarmonyPatch(typeof(Player))]
public static class BoneArmorPatches
{
    [HarmonyPatch(nameof(Player.EquipmentChanged))]
    [HarmonyPostfix]
    public static void EquipmentChangedPostfix(Player __instance)
    {
        var equipment = Inventory.main.equipment;
        __instance.gameObject.EnsureComponent<PlagueArmorBehavior>()
            .SetArmorActive(equipment.GetTechTypeInSlot("Body") == BoneArmor.Info.TechType);
    }
}