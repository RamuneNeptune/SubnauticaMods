

namespace Ramune.EquippableStorage.Patches
{
    [HarmonyPatch(typeof(Inventory))]
    public static class InventoryPatch
    {
        [HarmonyPatch(nameof(Inventory.UnlockDefaultEquipmentSlots)), HarmonyPostfix]
        public static void UnlockDefaultEquipmentSlots(Inventory __instance) => __instance.equipment.AddSlots(Enumerable.Range(1, EquippableStorage.config.backpackSlotsToGenerate).Select(x => $"EquippableStorage{x}").ToList());
    }
}