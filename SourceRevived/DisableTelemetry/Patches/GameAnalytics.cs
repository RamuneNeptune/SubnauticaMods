

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(GameAnalytics))]
    public static class GameAnalyticsPatch
    {
        [HarmonyPatch(nameof(GameAnalytics.Send), [typeof(GameAnalytics.EventInfo), typeof(bool), typeof(string)]), HarmonyPrefix]
        public static bool Send() => false;
    }
}