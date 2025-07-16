

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(GameAnalytics))]
    public static class GameAnalyticsPatches
    {
        [HarmonyPatch(nameof(GameAnalytics.Send), new[] { typeof(GameAnalytics.EventInfo), typeof(bool), typeof(string) }), HarmonyPrefix]
        public static bool Send() => false;
    }
}