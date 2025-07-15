﻿

namespace Ramune.NoEpilepsyWarning.Patches
{
    [HarmonyPatch(typeof(StartScreen))]
    public static class StartScreenPatch
    { 
        [HarmonyPatch(nameof(StartScreen.TryToShowDisclaimer)), HarmonyPrefix]
        public static bool TryToShowDisclaimer() => false;
    }
}