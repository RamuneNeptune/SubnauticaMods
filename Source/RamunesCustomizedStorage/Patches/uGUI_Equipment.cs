

namespace Ramune.RamunesCustomizedStorage.Patches
{
    [HarmonyPatch(typeof(uGUI_Equipment))]
    public static class uGUI_EquipmentPatches
    {
        [HarmonyPatch(nameof(uGUI_Equipment.OnEquip)), HarmonyPostfix, HarmonyPriority(100)]
        public static void OnEquip() => Inventory.main?.container.Resize(RamunesCustomizedStorage.config.width_inventory, RamunesCustomizedStorage.ShouldPatchCompatibility ? RamunesCustomizedStorage.config.height_inventory + (PDAPatch.BagTechTypes.TryGetValue(Inventory.main?.equipment.GetTechTypeInSlot("Bag") ?? TechType.None, out var heightToAdd) ? heightToAdd : 0) : RamunesCustomizedStorage.config.height_inventory);


        [HarmonyPatch(nameof(uGUI_Equipment.OnUnequip)), HarmonyPostfix, HarmonyPriority(100)]
        public static void OnUnequip() => Inventory.main?.container.Resize(RamunesCustomizedStorage.config.width_inventory, RamunesCustomizedStorage.ShouldPatchCompatibility ? RamunesCustomizedStorage.config.height_inventory + (PDAPatch.BagTechTypes.TryGetValue(Inventory.main?.equipment.GetTechTypeInSlot("Bag") ?? TechType.None, out var heightToAdd) ? heightToAdd : 0) : RamunesCustomizedStorage.config.height_inventory);
    }
}