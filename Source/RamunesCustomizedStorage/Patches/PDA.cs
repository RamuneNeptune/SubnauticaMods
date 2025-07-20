

namespace Ramune.RamunesCustomizedStorage.Patches
{
    [HarmonyPatch(typeof(PDA))]
    public static class PDAPatches
    {
        [HarmonyPatch(nameof(PDA.Open)), HarmonyPrefix]
        public static void Open() => Inventory.main.container.Resize(RamunesCustomizedStorage.config.width_inventory, RamunesCustomizedStorage.config.height_inventory);
    }
}