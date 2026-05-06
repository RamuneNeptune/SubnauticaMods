

namespace Ramune.RamunesFasterBuilding.Patches
{
    [HarmonyPatch(typeof(Constructable))]
    public static class ConstructablePatch
    {
        [HarmonyPatch(nameof(Constructable.GetConstructInterval)), HarmonyPostfix]
        public static void GetConstructInterval(ref float __result) => __result /= RamunesFasterBuilding.config.MultiplierChoice == 0 ? RamunesFasterBuilding.config.Multiplier : RamunesFasterBuilding.config.MultiplierMega;
    }
}