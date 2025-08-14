

namespace Ramune.RamunesCustomizedStorage.Patches
{
    [HarmonyPatch(typeof(uGUI_Equipment))]
    public static class uGUI_EquipmentPatches
    {
        [HarmonyPatch(nameof(uGUI_Equipment.OnEquip)), HarmonyPostfix, HarmonyPriority(100)]
        public static void OnEquip() => Inventory.main?.container.Resize(RamunesCustomizedStorage.config.width_inventory + (int)PDAPatch.SizeAdditions.Values.SelectMany(list => list).Sum(v => v.x), RamunesCustomizedStorage.ShouldPatchCompatibility ? RamunesCustomizedStorage.config.height_inventory + (PDAPatch.BagTechTypes.TryGetValue(Inventory.main?.equipment.GetTechTypeInSlot("Bag") ?? TechType.None, out var heightToAdd) ? heightToAdd : 0) + (int)PDAPatch.SizeAdditions.Values.SelectMany(list => list).Sum(v => v.y) : RamunesCustomizedStorage.config.height_inventory + (int)PDAPatch.SizeAdditions.Values.SelectMany(list => list).Sum(v => v.y));


        [HarmonyPatch(nameof(uGUI_Equipment.OnUnequip)), HarmonyPostfix, HarmonyPriority(100)]
        public static void OnUnequip() => Inventory.main?.container.Resize(RamunesCustomizedStorage.config.width_inventory + (int)PDAPatch.SizeAdditions.Values.SelectMany(list => list).Sum(v => v.x), RamunesCustomizedStorage.ShouldPatchCompatibility ? RamunesCustomizedStorage.config.height_inventory + (PDAPatch.BagTechTypes.TryGetValue(Inventory.main?.equipment.GetTechTypeInSlot("Bag") ?? TechType.None, out var heightToAdd) ? heightToAdd : 0) + (int)PDAPatch.SizeAdditions.Values.SelectMany(list => list).Sum(v => v.y) : RamunesCustomizedStorage.config.height_inventory + (int)PDAPatch.SizeAdditions.Values.SelectMany(list => list).Sum(v => v.y));
    }
}