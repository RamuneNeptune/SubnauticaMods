﻿

namespace Ramune.DisableTelemetry.Patches
{
    [HarmonyPatch(typeof(UnityWebRequest))]
    public static class UnityWebRequestPatch
    {
        [HarmonyPatch(nameof(UnityWebRequest.SendWebRequest)), HarmonyPrefix]
        public static bool SendWebRequest(UnityWebRequest __instance)
        {
            var url = __instance?.url;

            if(string.IsNullOrEmpty(url))
                return true;

            if(url.ToLowerInvariant().Contains("unknownworlds.com"))
                return false;

            return true;
        }
    }
}