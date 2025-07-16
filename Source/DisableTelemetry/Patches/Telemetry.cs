

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(Telemetry))]
    public static class TelemetryPatch
    {
        [HarmonyPatch(nameof(Telemetry.Start)), HarmonyPrefix]
        public static bool Start() => false;


        [HarmonyPatch(nameof(Telemetry.Awake)), HarmonyPrefix]
        public static bool Awake() => false;


        [HarmonyPatch(nameof(Telemetry.ScheduledUpdate)), HarmonyPrefix]
        public static bool ScheduledUpdate() => false;
    }
}