

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(SentrySdk))]
    public static class SentrySdkPatch
    {
        [HarmonyPatch(nameof(SentrySdk.Start)), HarmonyPrefix]
        public static bool Start() => false;
    }
}