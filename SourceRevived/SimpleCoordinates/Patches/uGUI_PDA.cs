

namespace Ramune.SimpleCoordinates.Patches
{
    [HarmonyPatch(typeof(uGUI_PDA))]
    public static class uGUI_PDAPatches
    {
        [HarmonyPatch(nameof(uGUI_PDA.OnOpenPDA)), HarmonyPostfix]
        public static void OnOpenPDA(PDATab tabId)
        {
            if(SimpleCoordinates.config.hideInPDA)
                Monos.CoordinateDisplay.HideAll(true);
        }

        [HarmonyPatch(nameof(uGUI_PDA.OnClosePDA)), HarmonyPostfix]
        public static void OnClosePDA() => Monos.CoordinateDisplay.stayHidden = false;
    }
}