

namespace Ramune.SimpleCoordinates.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    public static class ExosuitPatch
    {
        [HarmonyPatch(nameof(Exosuit.OnPilotModeBegin)), HarmonyPostfix]
        public static void OnPilotModeBegin()
        {
            if(SimpleCoordinates.config.hideInPrawnSuit)
                Monos.CoordinateDisplay.HideAll(true);
        }

        [HarmonyPatch(nameof(Exosuit.OnPilotModeEnd)), HarmonyPostfix]
        public static void OnPilotModeEnd() => Monos.CoordinateDisplay.stayHidden = false;
    }
}