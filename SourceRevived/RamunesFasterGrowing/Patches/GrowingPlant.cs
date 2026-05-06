

namespace Ramune.RamunesFasterGrowing.Patches
{
    [HarmonyPatch(typeof(GrowingPlant))]
    public static class GrowingPlantPatch
    {
        [HarmonyPatch(nameof(GrowingPlant.GetGrowthDuration)), HarmonyPostfix]
        public static void GetGrowthDuration(ref float __result) => __result /= RamunesFasterGrowing.config.MultiplierChoice == 0 ? RamunesFasterGrowing.config.Multiplier : RamunesFasterGrowing.config.MultiplierMega;
    }
}