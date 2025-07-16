

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(SentrySdk))]
    public static class SentrySdkPatch
    {
        [HarmonyPatch(nameof(SentrySdk.Start)), HarmonyPrefix]
        public static bool Start() => false;


        [HarmonyPatch(nameof(SentrySdk.DoCaptureEvent)), HarmonyPrefix]
        public static bool DoCaptureEvent() => false;
        

        [HarmonyPatch(nameof(SentrySdk.ScheduleException)), HarmonyPrefix]
        public static bool ScheduleException() => false;
    }
}