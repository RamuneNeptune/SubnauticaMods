

using Newtonsoft.Json.Schema;

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

        public static bool ShouldNotify = false;

        public static int LastOutdatedCount = 0;

        public static bool IsFirstRun = false;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTabs)), HarmonyPostfix]
        public static void AddTabs(uGUI_OptionsPanel __instance)
        {
            UpdatesTabPanel = __instance;
            UpdatesTabIndex = __instance.AddTab((FindMyUpdates.config.TranslateTabName ? "fmu.ui.tabname".LangKey() : GetEnglishName("fmu.ui.tabname", "Updates")) + (FindMyUpdates.config.OutdatedCountOnTab ? (LastOutdatedCount > 0 ? $" ({LastOutdatedCount})" : "") : ""));

          //__instance.AddHeading(UpdatesTabIndex, "<align=center>\n<size=115%><color=#ffc834><b>Find My Updates</b></color></size>\nAny mods that support version checking will be listed here\n\n</align>");
            __instance.AddHeading(UpdatesTabIndex, FindMyUpdates.config.TranslateHeadingName ? string.Format("fmu.ui.header".LangKey(), "fmu.ui.headername".LangKey()) : string.Format("fmu.ui.header".LangKey(), GetEnglishName("fmu.ui.headername", "Find My Updates")));
            __instance.AddHeading(UpdatesTabIndex, "<align=center>\n<color=#1e86d6>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n</align>"); //━━━━━━━━━━━━━━━━━━━━━━━━━

            LastOutdatedCount = 0;

            PendingRegistrations.Where(x => x != null).ForEach(r => r.Invoke());

            if(uGUI_TabbedControlsPanelPatches.tmp != null)
                uGUI_TabbedControlsPanelPatches.tmp.text = (FindMyUpdates.config.TranslateTabName ? "fmu.ui.tabname".LangKey() : GetEnglishName("fmu.ui.tabname", "Updates")) + (FindMyUpdates.config.OutdatedCountOnTab ? (LastOutdatedCount > 0 ? $" ({LastOutdatedCount})" : "") : "");
        }


        public static string GetEnglishName(string jsonKey, string defaultReturnValue)
        {
            string englishLocalizationPath = Path.Combine(Paths.LocalizationFolder, "English.json");

            return File.Exists(englishLocalizationPath) ? JObject.Parse(File.ReadAllText(englishLocalizationPath))[jsonKey] is JToken token ? (string)token : defaultReturnValue : defaultReturnValue;
        }


        public static void RegisterMod(string modName, string latestUrl, Version currentVersion, Version latestVersion, bool isUpdated)
        {
            if(!ShouldNotify && !isUpdated)
                ShouldNotify = true;

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
                    LastOutdatedCount++;

                    /*
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
                    */

                    var displayOption = FindMyUpdates.config.DisplayOptionsMenuWarnings;

                    if(displayOption == 0 || displayOption == 1)
                    {
                        if(displayOption == 0)
                        {
                            if(FindMyUpdates.config.ConfigHint)
                                Screen.Message("fmu.warning.hint".LangKey());

                            Screen.Message(string.Format("fmu.warning.outdated".LangKey(), modName, currentVersion, latestVersion));
                        }
                        else if(!HasWarnedOnce)
                        {
                            if(FindMyUpdates.config.ConfigHint)
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
                    
                    if(!hasFirstClick && FindMyUpdates.config.OpenURLBehaviour == 0)
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