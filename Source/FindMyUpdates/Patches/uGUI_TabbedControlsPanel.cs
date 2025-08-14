

namespace Ramune.FindMyUpdates.Patches
{
    [HarmonyPatch(typeof(uGUI_TabbedControlsPanel))]
    public static class uGUI_TabbedControlsPanelPatches
    {
        [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddButton)), HarmonyPrefix]
        public static bool AddButtonPrefix(uGUI_TabbedControlsPanel __instance, int tabIndex, string label, UnityAction callback = null)
        {
            if(tabIndex != uGUI_OptionsPanelPatch.UpdatesTabIndex)
                return true;

            var buttonGo = __instance.controls.AddButton(__instance.GetParent(tabIndex), label, callback);

            var buttonComponent = buttonGo.GetComponentInChildren<Button>();

            uGUI_OptionsPanelPatch.latestButton = buttonComponent;

            /*
            if(callback != null && buttonComponent != null)
                buttonComponent.onClick.AddListener(callback);
            */

            return false;
        }


        [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddButton)), HarmonyPostfix]
        public static void AddButtonPostfix(uGUI_TabbedControlsPanel __instance, int tabIndex, string label, UnityAction callback = null)
        {
            if(tabIndex != uGUI_OptionsPanelPatch.UpdatesTabIndex)
                return;

            if(uGUI_OptionsPanelPatch.latestButton == null)
                return;

            Color color = Color.white;

            if(label == "fmu.ui.button.updated".LangKey())
                color = new Color(0.6f, 0.6f, 0.6f);

            else if(label == "fmu.ui.button.outdated".LangKey())
                color = new Color(0.1176f, 0.8431f, 0.3764f);

            uGUI_OptionsPanelPatch.latestButton.image.color = color;
            uGUI_OptionsPanelPatch.latestButton.gameObject.GetComponent<RectTransform>().sizeDelta = new(-200f, 60f);
        }
    }
}