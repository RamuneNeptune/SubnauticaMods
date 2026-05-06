

namespace Ramune.DisableOptionsTabs.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatches
    {
        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddGraphicsTab)), HarmonyPrefix]
        public static bool AddGraphicsTab() => !DisableOptionsTabs.config.DisableGraphics;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddAccessibilityTab)), HarmonyPrefix]
        public static bool AddAccessibilityTab() => !DisableOptionsTabs.config.DisableAccessibility;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddKeyRedemptionTab)), HarmonyPrefix]
        public static bool AddKeyRedemptionTab() => !DisableOptionsTabs.config.DisableRedeemKey;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTroubleshootingTab)), HarmonyPrefix]
        public static bool AddTroubleshootingTab() => !DisableOptionsTabs.config.DisableTroubleshooting;
    }
}