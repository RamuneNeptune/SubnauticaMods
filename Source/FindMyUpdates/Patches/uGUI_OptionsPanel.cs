

namespace Ramune.FindMyUpdates.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatch
    {
        public static int UpdatesTabIndex;

        public static uGUI_OptionsPanel UpdatesTabPanel;

        public static Button latestButton;

        public static Queue<Action> PendingRegistrations = new();

        public static bool HasWarnedOnce = false;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTabs)), HarmonyPostfix]
        public static void AddTabs(uGUI_OptionsPanel __instance)
        {
            UpdatesTabPanel = __instance;
            UpdatesTabIndex = __instance.AddTab("fmu.ui.tabname".LangKey());

          //__instance.AddHeading(UpdatesTabIndex, "<align=center>\n<size=115%><color=#ffc834><b>Find My Updates</b></color></size>\nAny mods that support version checking will be listed here\n\n</align>");
            __instance.AddHeading(UpdatesTabIndex, "fmu.ui.header".LangKey());
            __instance.AddHeading(UpdatesTabIndex, "<align=center>\n<color=#1e86d6>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n</align>"); //━━━━━━━━━━━━━━━━━━━━━━━━━

            PendingRegistrations.Where(x => x != null).ForEach(r => r.Invoke());
        }


        public static void RegisterMod(string modName, string latestUrl, Version currentVersion, Version latestVersion, bool isUpdated)
        {
            if(UpdatesTabPanel != null)
            {
                DoRegistration();
            }
            else PendingRegistrations.Enqueue(DoRegistration);


            void DoRegistration()
            {
              //var updated = $"<b><color=#ffc834>{modName}:</color></b>\n • You are using the latest version: <color=#ffc834>{currentVersion}</color>\n • <size=70%>URL: <color=#ffc834>{(latestUrl.IsNullOrWhiteSpace() ? "N/A" : latestUrl)}</size></color>";
                var updated = string.Format("fmu.ui.mod.updated".LangKey(), modName, currentVersion, latestUrl.IsNullOrWhiteSpace() ? "N/A" : latestUrl);

              //var outdated = $"<b><color=#ffc834>{modName}:</color></b>\n • An update is available: <color=#ffc834>{latestVersion}</color>! (current: <color=#ffc834>{currentVersion}</color>)\n • <size=70%>URL: <color=#ffc834>{(latestUrl.IsNullOrWhiteSpace() ? "N/A" : latestUrl)}</size></color>";
                var outdated = string.Format("fmu.ui.mod.outdated".LangKey(), modName, latestVersion, currentVersion, latestUrl.IsNullOrWhiteSpace() ? "N/A" : latestUrl);

                UpdatesTabPanel.AddHeading(UpdatesTabIndex, isUpdated ? updated : outdated);

                bool hasFirstClick = false;

                if(!isUpdated)
                {
                    if(FindMyUpdates.config.OnScreenWarning)
                    {
                        if(FindMyUpdates.config.OnScreenWarningEveryTime)
                        {
                            Screen.Message("fmu.warning.hint".LangKey());
                            Screen.Message(string.Format("fmu.warning.outdated".LangKey(), modName, currentVersion, latestVersion));
                        }
                        else if(!HasWarnedOnce)
                        {
                            Screen.Message("fmu.warning.hint".LangKey());
                            Screen.Message(string.Format("fmu.warning.outdated".LangKey(), modName, currentVersion, latestVersion));
                            HasWarnedOnce = true;
                        }
                    }
                }

                UpdatesTabPanel.AddButton(UpdatesTabIndex, isUpdated ? "fmu.ui.button.updated".LangKey() : "fmu.ui.button.outdated".LangKey(), () =>
                {
                    if(!Uri.TryCreate(latestUrl, UriKind.Absolute, out var uri))
                    {
                        Screen.Error(string.Format("fmu.warning.invalidurl".LangKey(), latestUrl.IsNullOrWhiteSpace() ? "N/A" : latestUrl));
                        return;
                    }
                    
                    if(!hasFirstClick && FindMyUpdates.config.WarnOnButtonClicks)
                    {
                        Screen.Message(string.Format("fmu.warning.urlcheck".LangKey(), latestUrl));

                        hasFirstClick = true;

                        return;
                    }
                    
                    Process.Start(latestUrl);

                    if(FindMyUpdates.config.LogURLsToScreen)
                        Screen.Message(string.Format("fmu.warning.urlopened".LangKey(), latestUrl));
                });

                UpdatesTabPanel.AddHeading(UpdatesTabIndex, "<align=center>\n\n<color=#1e86d6>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n\n</align>");
            }
        }
    }
}