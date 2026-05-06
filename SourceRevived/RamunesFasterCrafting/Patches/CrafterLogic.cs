

namespace Ramune.RamunesFasterCrafting.Patches
{
    [HarmonyPatch(typeof(CrafterLogic))]
    public static class CrafterLogicPatch
    {
        [HarmonyPatch(nameof(CrafterLogic.Craft)), HarmonyPostfix]
        public static void Craft(CrafterLogic __instance, float craftTime) => __instance.timeCraftingEnd = (__instance.timeCraftingBegin + craftTime / (RamunesFasterCrafting.config.MultiplierChoice == 0 ? RamunesFasterCrafting.config.Multiplier : RamunesFasterCrafting.config.MultiplierMega)) + 0.1f;
    }
}