

namespace Ramune.FasterCrafting.Patches
{
    [HarmonyPatch(typeof(CrafterLogic))]
    public static class CrafterLogicPatch
    {
        [HarmonyPatch(nameof(CrafterLogic.Craft)), HarmonyPostfix]
        public static void Craft(CrafterLogic __instance, ref float craftTime) => __instance.timeCraftingEnd = __instance.timeCraftingBegin + craftTime / (FasterCrafting.config.MultiplierChoice == 0 ? FasterCrafting.config.Multiplier : FasterCrafting.config.MultiplierMega);
    }
}