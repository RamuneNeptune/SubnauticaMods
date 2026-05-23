

namespace Ramune.ArchonCheckDisabler.Patches
{
    [HarmonyPatch(typeof(AVS.CompatChecker))]
    public static class PlayerPatch
    {
        [HarmonyPatch(nameof(AVS.CompatChecker.CheckAll)), HarmonyPrefix]
        public static bool CheckAll() => false;
    }
}