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

            url = url.ToLowerInvariant();

            if(DisableTelemetry.config.AllowSpecialItemCheck && url.StartsWith("https://economy.unknownworlds.com/api/getcontext"))
                return true;

            if(url.Contains("unknownworlds.com"))
                return false;

            return true;
        }
    }
}