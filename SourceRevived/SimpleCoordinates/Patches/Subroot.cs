

namespace Ramune.SimpleCoordinates.Patches
{
    [HarmonyPatch(typeof(SubRoot))]
    public static class SubRootPatch
    {
        [HarmonyPatch(nameof(SubRoot.OnPlayerEntered)), HarmonyPostfix]
        public static void OnPlayerEntered()
        {
            if(SimpleCoordinates.config.hideInCyclops)
                Monos.CoordinateDisplay.HideAll(true);
        }

        [HarmonyPatch(nameof(SubRoot.OnPlayerExited)), HarmonyPostfix]
        public static void OnPlayerExited() => Monos.CoordinateDisplay.stayHidden = false;
    }
}