

namespace Ramune.MouseCursorToggle.Patches
{
    [HarmonyPatch(typeof(FPSInputModule))]
    public static class FPSInputModulePatch
    {
        [HarmonyPatch(nameof(FPSInputModule.EscapeMenu)), HarmonyPrefix]
        public static bool EscapeMenu() => !Monos.MouseCursorController.TryHandleEscapeMenu();
    }
}