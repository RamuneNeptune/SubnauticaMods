﻿

namespace Ramune.NoUnderwaterCaustics.Patches
{
    [HarmonyPatch(typeof(WaterscapeVolume))]
    public static class WaterscapeVolumePatch
    {
        [HarmonyPatch(nameof(WaterscapeVolume.Awake)), HarmonyPostfix]
        public static void Awake(WaterscapeVolume __instance)
        {
            __instance.causticsAmount = 0f;
        }
    }
}