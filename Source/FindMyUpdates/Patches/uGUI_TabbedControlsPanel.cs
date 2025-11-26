

using static Unity.Burst.Intrinsics.X86.Avx;

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

        
        public static TextMeshProUGUI tmp;

        // Should do this better in future, I don't like copying entire methods just to grab one thing from the middle of it 

        [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddTab)), HarmonyPrefix]
        public static bool AddTabPrefix(uGUI_TabbedControlsPanel __instance, string label, ref int __result)
        {
            if(!label.StartsWith("fmu.ui.tabname".LangKey()))
                return true;

            uGUI_TabbedControlsPanel.Tab tab = new uGUI_TabbedControlsPanel.Tab();
            tab.pane = Object.Instantiate(__instance.panePrefab, __instance.panesContainer, false);
            tab.tab = Object.Instantiate(__instance.tabPrefab, __instance.tabsContainer, false);
            TextMeshProUGUI componentInChildren = tab.tab.GetComponentInChildren<TextMeshProUGUI>();
            if(componentInChildren != null)
            {
                tmp = componentInChildren;
                componentInChildren.text = Language.main.Get(label);
                tab.tab.GetComponentInChildren<TranslationLiveUpdate>().translationKey = label;
            }
            int tabIndex = __instance.tabs.Count;
            ToggleButton componentInChildren2 = tab.tab.GetComponentInChildren<ToggleButton>();
            UnityAction<bool> call = delegate (bool value)
            {
                if(value)
                {
                    __instance.SetVisibleTab(tabIndex);
                }
            };
            componentInChildren2.onValueChanged.AddListener(call);
            UnityAction call2 = delegate ()
            {
                __instance.SelectTab(tabIndex);
            };
            componentInChildren2.onButtonPressed.AddListener(call2);
            bool flag = tabIndex == 0;
            tab.pane.SetActive(flag);
            componentInChildren2.isOn = flag;
            componentInChildren2.group = __instance.tabsContainer.GetComponentInChildren<ToggleGroup>();
            GameObject gameObject = Utils.FindChild(tab.pane, "Content");
            if(gameObject == null)
            {
                gameObject = tab.pane;
            }
            Selectable selectable = (__instance.tabs.Count > 0) ? __instance.tabs[__instance.tabs.Count - 1].tabButton : null;
            Navigation navigation = componentInChildren2.navigation;
            navigation.mode = Navigation.Mode.Explicit;
            navigation.selectOnUp = selectable;
            __instance.navigationDirty = true;
            componentInChildren2.navigation = navigation;
            if(selectable != null)
            {
                navigation = selectable.navigation;
                navigation.selectOnDown = componentInChildren2;
                selectable.navigation = navigation;
            }
            tab.tabButton = componentInChildren2;
            tab.container = gameObject.GetComponent<RectTransform>();
            __instance.tabs.Add(tab);
            __result = tabIndex;
            return false;
        }   
    }
}