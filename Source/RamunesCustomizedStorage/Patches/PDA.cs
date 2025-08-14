

namespace Ramune.RamunesCustomizedStorage.Patches
{
    [HarmonyPatch(typeof(PDA))]
    public static class PDAPatch
    {
        public static Dictionary<TechType, int> BagTechTypes = new()
        {

            [GetTechTypeOrNone("BagEquipment1")] = 2,
            [GetTechTypeOrNone("BagEquipment2")] = 4,
            [GetTechTypeOrNone("BagEquipment3")] = 6,
            [GetTechTypeOrNone("SuperBag")] = 50
        };


        public static Dictionary<string, List<Vector2>> SizeAdditions = new();


        [HarmonyPatch(nameof(PDA.Open)), HarmonyPrefix]
        public static void Open() => Inventory.main?.container.Resize(RamunesCustomizedStorage.config.width_inventory + (int)SizeAdditions.Values.SelectMany(list => list).Sum(v => v.x), RamunesCustomizedStorage.ShouldPatchCompatibility ? RamunesCustomizedStorage.config.height_inventory + (BagTechTypes.TryGetValue(Inventory.main?.equipment.GetTechTypeInSlot("Bag") ?? TechType.None, out var heightToAdd) ? heightToAdd : 0) + (int)SizeAdditions.Values.SelectMany(list => list).Sum(v => v.y) : RamunesCustomizedStorage.config.height_inventory + (int)SizeAdditions.Values.SelectMany(list => list).Sum(v => v.y));
        

        private static TechType GetTechTypeOrNone(string id) => EnumHandler.TryGetValue<TechType>(id, out var techType) ? techType : TechType.None;
    }
}