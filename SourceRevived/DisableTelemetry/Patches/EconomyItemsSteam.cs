

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(EconomyItemsSteam))]
    public static class EconomyItemsSteamPatch
    {
        [HarmonyPatch(nameof(EconomyItemsSteam.InitializeAsync)), HarmonyPrefix]
        public static bool InitializeAsync() => DisableTelemetry.config.AllowSpecialItemCheck;
    }
}