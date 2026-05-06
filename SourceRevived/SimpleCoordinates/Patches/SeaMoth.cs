

namespace Ramune.SimpleCoordinates.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    public static class SeaMothPatch
    {
        [HarmonyPatch(nameof(SeaMoth.OnPilotModeBegin)), HarmonyPostfix]
        public static void OnPilotModeBegin()
        {
            if(SimpleCoordinates.config.hideInSeamoth)
                Monos.CoordinateDisplay.HideAll(true);
        }

        [HarmonyPatch(nameof(SeaMoth.OnPilotModeEnd)), HarmonyPostfix]
        public static void OnPilotModeEnd() => Monos.CoordinateDisplay.stayHidden = false;
    }
}