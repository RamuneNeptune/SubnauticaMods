

namespace Ramune.FasterGrowing.Patches
{
    [HarmonyPatch(typeof(GrowingPlant))]
    public static class GrowingPlantPatch
    {
        [HarmonyPatch(nameof(GrowingPlant.GetGrowthDuration)), HarmonyPostfix]
        public static void GetGrowthDuration(ref float __result) => __result /= FasterGrowing.config.MultiplierChoice == 0 ? FasterGrowing.config.Multiplier : FasterGrowing.config.MultiplierMega;
    }
}