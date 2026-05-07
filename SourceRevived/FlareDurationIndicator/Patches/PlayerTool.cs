

namespace Ramune.FlareDurationIndicator.Patches
{
    [HarmonyPatch(typeof(PlayerTool))]
    public static class PlayerToolPatch
    {
        [HarmonyPatch(nameof(PlayerTool.OnReloadDown)), HarmonyPrefix]
        public static bool AddPending(PlayerTool __instance) => __instance.pickupable.GetTechType() != TechType.Flare;
    }
}