

namespace Ramune.RamunesCustomizedStorage.Patches
{
    [HarmonyPatch(typeof(PDA))]
    public static class PDAPatch
    {
        public static Dictionary<TechType, int> BagTechTypes = IBagTechTypes();

        public static Dictionary<TechType, int> IBagTechTypes()
        {
            return new Dictionary<TechType, int>
            {
                [GetTechTypeOrNone("BagEquipment1")] = 2,
                [GetTechTypeOrNone("BagEquipment2")] = 4,
                [GetTechTypeOrNone("BagEquipment3")] = 6,
                [GetTechTypeOrNone("SuperBag")] = 50
            };
        }


        [HarmonyPatch(nameof(PDA.Open)), HarmonyPrefix]
        public static void Open() => Inventory.main?.container.Resize(RamunesCustomizedStorage.config.width_inventory, RamunesCustomizedStorage.ShouldPatchCompatibility ? RamunesCustomizedStorage.config.height_inventory + (BagTechTypes.TryGetValue(Inventory.main?.equipment.GetTechTypeInSlot("Bag") ?? TechType.None, out var heightToAdd) ? heightToAdd : 0) : RamunesCustomizedStorage.config.height_inventory);


        private static TechType GetTechTypeOrNone(string id) => EnumHandler.TryGetValue<TechType>(id, out var techType) ? techType : TechType.None;
    }
}