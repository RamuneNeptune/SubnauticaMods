

namespace Ramune.FasterBuilding.Patches
{
    [HarmonyPatch(typeof(Constructable))]
    public static class ConstructablePatch
    {
        [HarmonyPatch(nameof(Constructable.GetConstructInterval)), HarmonyPostfix]
        public static void GetConstructInterval(ref float __result) => __result /= FasterBuilding.config.MultiplierChoice == 0 ? FasterBuilding.config.Multiplier : FasterBuilding.config.MultiplierMega;
    }
}