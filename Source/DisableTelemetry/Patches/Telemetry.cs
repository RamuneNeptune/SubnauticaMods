

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(Telemetry))]
    public static class TelemetryPatch
    {
        [HarmonyPatch(nameof(Telemetry.Start)), HarmonyPrefix]
        public static bool Start() => false;
    }
}