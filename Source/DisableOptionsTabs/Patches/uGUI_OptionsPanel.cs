

namespace Ramune.DisableOptionsTabs.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatch
    {
        /*
        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddGeneralTab)), HarmonyPrefix]
        public static bool AddGeneralTab(uGUI_OptionsPanel __instance) => !DisableOptionsTabs.config.DisableGeneral;
        */

        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddGraphicsTab)), HarmonyPrefix]
        public static bool AddGraphicsTab(uGUI_OptionsPanel __instance) => !DisableOptionsTabs.config.DisableGraphics;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddKeyboardTab)), HarmonyPrefix]
        public static bool AddKeyboardTab(uGUI_OptionsPanel __instance) => !DisableOptionsTabs.config.DisableKeyboard;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddControllerTab)), HarmonyPrefix]
        public static bool AddControllerTab(uGUI_OptionsPanel __instance) => !DisableOptionsTabs.config.DisableController;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddAccessibilityTab)), HarmonyPrefix]
        public static bool AddAccessibilityTab(uGUI_OptionsPanel __instance) => !DisableOptionsTabs.config.DisableAccessibility;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddKeyRedemptionTab)), HarmonyPrefix]
        public static bool AddKeyRedemptionTab(uGUI_OptionsPanel __instance) => !DisableOptionsTabs.config.DisableRedeemKey;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTroubleshootingTab)), HarmonyPrefix]
        public static bool AddTroubleshootingTab(uGUI_OptionsPanel __instance) => !DisableOptionsTabs.config.DisableTroubleshooting;
    }
}