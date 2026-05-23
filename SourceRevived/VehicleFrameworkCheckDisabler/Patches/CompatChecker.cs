

namespace Ramune.VehicleFrameworkCheckDisabler.Patches
{
    [HarmonyPatch(typeof(VehicleFramework.CompatChecker))]
    public static class PlayerPatch
    {
        [HarmonyPatch(nameof(VehicleFramework.CompatChecker.CheckAll)), HarmonyPrefix]
        public static bool CheckAll() => false;
    }
}