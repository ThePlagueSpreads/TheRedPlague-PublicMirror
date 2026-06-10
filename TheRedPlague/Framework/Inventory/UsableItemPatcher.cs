using HarmonyLib;
using UnityEngine;

namespace TheRedPlague.Framework.Inventory;

[HarmonyPatch]
public class UsableItemPatcher
{
    [HarmonyPatch(typeof(Survival), nameof(Survival.Use))]
    [HarmonyPostfix]
    public static void SurvivalUsePostfix(GameObject useObj, ref bool __result)
    {
        if (useObj == null) return;
        if (useObj.TryGetComponent<PlayerTool>(out var tool) && tool.isDrawn) return;
        if (useObj.TryGetComponent<UsableItem>(out var item))
        {
            try
            {
                item.ExecuteOnUseAction();
            }
            catch (System.Exception e)
            {
                Plugin.Logger.LogError($"Failed to execute on use action for item {item}. Exception: " + e);
            }
            __result = true;
        }
    }
    
    [HarmonyPatch(typeof(global::Inventory), nameof(global::Inventory.GetAllItemActions))]
    [HarmonyPostfix]
    public static void InventoryGetAllItemActionsPostfix(global::Inventory __instance, InventoryItem item, ref ItemAction __result)
    {
        if (__result.HasFlag(ItemAction.Use)) return;
        if (item == null) return;
        if (item.container != __instance.container ||
            __instance.GetOppositeContainer(item) != __instance.equipment) return;
        if (item.item.gameObject.GetComponent<UsableItem>() != null)
        {
            __result |= ItemAction.Use;
        }
    }
}