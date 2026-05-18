

namespace Ramune.ForceSuperUltrawideResolution.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatch
    {
        [HarmonyPatch(nameof(uGUI_OptionsPanel.GetResolutionOptions)), HarmonyPostfix]
        public static void GetResolutionOptions(ref string[] __result, ref List<Resolution> resolutions)
        {
            if(resolutions.Any(r => r.width == 5120 && r.height == 1440))
                return;

            resolutions.Add(new Resolution
            {
                width = 5120,
                height = 1440
            });

            __result = [.. resolutions.Select(x => $"{x.width} x {x.height}")];
        }
    }


    [HarmonyPatch(typeof(DisplayManager))]
    public static class DisplayManagerPatch
    {
        [HarmonyPatch(nameof(DisplayManager.SetResolution)), HarmonyPrefix]
        public static void SetResolution(ref int width, ref int height, ref bool fullscreen)
        {
            fullscreen = true;
            width = 5120;
            height = 1440;
        }
    }


    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanel_GetCurrentResolutionIndex_Patch
    {
        [HarmonyPatch(nameof(uGUI_OptionsPanel.GetCurrentResolutionIndex)), HarmonyPrefix]
        public static bool GetCurrentResolutionIndex(List<Resolution> resolutions, ref int __result)
        {
            for(int i = 0; i < resolutions.Count; i++)
            {
                var res = resolutions[i];

                if(res.width == 5120 && res.height == 1440)
                {
                    __result = i;
                    return false;
                }
            }

            return true;
        }
    }
}