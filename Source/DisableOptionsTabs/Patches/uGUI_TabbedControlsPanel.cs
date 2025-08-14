

namespace Ramune.DisableOptionsTabs.Patches
{
    [HarmonyPatch(typeof(uGUI_TabbedControlsPanel))]
    public static class uGUI_TabbedControlsPanelPatch
    {
        private static readonly Dictionary<uGUI_TabbedControlsPanel, int> eoifjausd = new();


        [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.SetVisibleTab)), HarmonyPrefix]
        public static bool SetVisibleTab(uGUI_OptionsPanel __instance, int tabIndex)
        {
            if(__instance == null)
            {
                Logfile.Debug("__instance is null");
                return false;
            }

            if(eoifjausd.TryGetValue(__instance, out int last) && last == tabIndex)
            {
                Logfile.Debug($"skipping duplicate call to SetVisibleTab({tabIndex})");
                return false;
            }

            eoifjausd[__instance] = tabIndex;

            Logfile.Debug($"tabIndex: {tabIndex}, count: {__instance.tabs?.Count}");

            if(__instance.tabs == null)
            {
                Logfile.Debug("tabs is null");
                return false;
            }

            if(__instance.tabs.Count < 0)
            {
                Logfile.Debug("tabs.Count is less than zero");
                return false;
            }

            if(tabIndex < 0)
            {
                Logfile.Debug($"tabIndex is less than zero {tabIndex}");
                return false;
            }

            if(__instance.currentTab < 0)
            {
                Logfile.Debug($"currentTab is less than zero {__instance.currentTab}");
                return false;
            }

            if(__instance.currentTab >= __instance.tabs.Count)
            {
                Logfile.Debug($"currentTab {__instance.currentTab} is more than or equal to {__instance.tabs.Count}");

                if(__instance.tabs.Count > 0)
                {
                    __instance.currentTab = 0;
                    Logfile.Debug($"^^ is true, BUT.. if you're seeing this, it has been resolved, so we will now proceed as intended");
                }
                else return false;
            }

            if(tabIndex > __instance.tabs.Count)
            {
                Logfile.Debug($"tabIndex {tabIndex} is more than {__instance.tabs.Count}");
                return false;
            }

            if(tabIndex == 0 && __instance.tabs.Count == 0)
            {
                Logfile.Debug($"tabIndex & tabs.Count is zero");
                return false;
            }

            var tab = __instance.tabs[tabIndex];

            if (tab.pane == null)
            {
                Logfile.Debug($"tab.pane is null");
                return false;
            }

            if(tab.firstSelectable == null || tab.firstSelectable.transform == null)
            {
                Logfile.Debug($"tab.firstSelectable is null");
                return false;
            }

            Logfile.Debug($"setting visible tab to: {tabIndex} (count: {__instance.tabs.Count})");

            return true;
        }


        [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.HighlightCurrentTab)), HarmonyPrefix]
        public static bool HighlightCurrentTab(uGUI_OptionsPanel __instance)
        {
            uGUI_LegendBar.ClearButtons();

            uGUI_LegendBar.ChangeButton(0, uGUI.FormatButton(GameInput.Button.UICancel, false, " / ", true), Language.main.GetFormat("Back"));

            uGUI_LegendBar.ChangeButton(1, uGUI.FormatButton(GameInput.Button.UISubmit, false, " / ", true), Language.main.GetFormat("ItemSelectorSelect"));

            __instance.StartCoroutine(Globgo(__instance));

            Logfile.Debug($"Globgo coroutine called");

            return false;
        }


        public static IEnumerator Globgo(uGUI_TabbedControlsPanel __instance)
        {
            yield return new WaitForEndOfFrame();

            Logfile.Debug($"(Globgo start) tabIndex: {__instance.currentTab}, count: {__instance.tabs?.Count}");

            if(__instance == null)
            {
                Logfile.Debug("(Globgo) __instance is null");
                yield break;
            }

            if(__instance.tabs == null)
            {
                Logfile.Debug("(Globgo) tabs is null");
                yield break;
            }

            if(__instance.tabs.Count < 0)
            {
                Logfile.Debug("(Globgo) tabs.Count is less than zero");
                yield break;
            }

            if(__instance.currentTab < 0)
            {
                Logfile.Debug($"(Globgo) currentTab is less than zero {__instance.currentTab}");
                yield break;
            }

            if(__instance.currentTab >= __instance.tabs.Count)
            {
                Logfile.Debug($"(Globgo) currentTab {__instance.currentTab} is more than or equal to {__instance.tabs.Count}");
                yield break;
            }

            uGUI_TabbedControlsPanel.Tab tab = __instance.tabs[__instance.currentTab];

            if(tab.pane == null)
            {
                Logfile.Debug($"(Globgo) currentTab.pane is null");
                yield break;
            }

            if(tab.tabButton == null)
            {
                Logfile.Debug($"(Globgo) currentTab.tabButton is null");
                yield break;
            }

            tab.prevSelectable = GamepadInputModule.current.GetCurrentGrid().GetSelectedItem() as Selectable;

            Logfile.Debug($"(Globgo end) tabIndex: {__instance.currentTab}, count: {__instance.tabs.Count}");

            __instance.tabs[__instance.currentTab] = tab;

            GamepadInputModule.current.SelectItem(__instance.tabs[__instance.currentTab].tabButton);

            __instance.tabOpen = false;

            yield break;
        }
    }
}