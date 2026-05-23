

namespace Ramune.NoFeedback.Patches
{
    [HarmonyPatch(typeof(uGUI_FeedbackCollector))]
    public static class uGUI_FeedbackCollectorPatch
    {
        [HarmonyPatch(nameof(uGUI_FeedbackCollector.IsEnabled)), HarmonyPrefix]
        public static bool IsEnabled(ref bool __result)
        {
            __result = false;
            return false;
        }
    }
}