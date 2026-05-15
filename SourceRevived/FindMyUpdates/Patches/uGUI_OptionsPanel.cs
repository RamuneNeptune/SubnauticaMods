

namespace Ramune.FindMyUpdates.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatch
    {
        public const string AddToBlacklistLabel = "Add to blacklist";

        public const string RemoveFromBlacklistLabel = "Remove from blacklist";

        public const string BlacklistedDescription = "<b><color=#ffc834>{0}:</color></b>\n • <color=white>Update checks are disabled for this mod</color>\n • <size=70%><color=white>Current version:</color> <color=#ffc834>{1}</color></size>";

        public sealed class RegisteredMod
        {
            public string GUID { get; set; }

            public string ModName { get; set; }

            public string LatestURL { get; set; }

            public Version CurrentVersion { get; set; }

            public Version LatestVersion { get; set; }

            public string LatestVersionText { get; set; }

            public bool IsUpdated { get; set; }

            public bool HasUpdateCheckData { get; set; }
        }

        public static int UpdatesTabIndex;

        public static uGUI_OptionsPanel UpdatesTabPanel;

        public static Button latestButton;

        public static List<RegisteredMod> RegisteredMods = [];

        public static bool HasWarnedOnce = false;

        public static bool ShouldNotify = false;

        public static int LastOutdatedCount = 0;

        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTabs)), HarmonyPostfix]
        public static void AddTabs(uGUI_OptionsPanel __instance)
        {
            UpdatesTabPanel = __instance;
            UpdatesTabIndex = __instance.AddTab((FindMyUpdates.config.TranslateTabName ? "fmu.ui.tabname".LangKey() : GetEnglishName("fmu.ui.tabname", "Updates")) + (FindMyUpdates.config.OutdatedCountOnTab ? (LastOutdatedCount > 0 ? $" ({LastOutdatedCount})" : "") : ""));

            RenderRegisteredMods();
            ShowConfiguredWarnings();
        }


        [HarmonyPatch(nameof(uGUI_OptionsPanel.OnDisable)), HarmonyPostfix]
        public static void OnDisable()
        {
            UpdatesTabPanel = null;
            latestButton = null;
            uGUI_TabbedControlsPanelPatches.tmp = null;
        }


        public static string GetEnglishName(string jsonKey, string defaultReturnValue)
        {
            string englishLocalizationPath = Path.Combine(Paths.LocalizationFolder, "English.json");

            return File.Exists(englishLocalizationPath) ? JObject.Parse(File.ReadAllText(englishLocalizationPath))[jsonKey] is JToken token ? (string)token : defaultReturnValue : defaultReturnValue;
        }


        public static void RegisterMod(string guid, string modName, string latestUrl, Version currentVersion, Version latestVersion, string latestVersionText, bool isUpdated)
        {
            RegisteredMod existingMod = GetRegisteredMod(guid, modName, latestUrl);
            bool wasOutdated = existingMod is { HasUpdateCheckData: true, IsUpdated: false };

            if(existingMod == null)
            {
                RegisteredMods.Add(new RegisteredMod
                {
                    GUID = guid,
                    ModName = modName,
                    LatestURL = latestUrl,
                    CurrentVersion = currentVersion,
                    LatestVersion = latestVersion,
                    LatestVersionText = latestVersionText,
                    IsUpdated = isUpdated,
                    HasUpdateCheckData = true
                });
            }
            else
            {
                existingMod.GUID = guid;
                existingMod.ModName = modName;
                existingMod.LatestURL = latestUrl;
                existingMod.CurrentVersion = currentVersion;
                existingMod.LatestVersion = latestVersion;
                existingMod.LatestVersionText = latestVersionText;
                existingMod.IsUpdated = isUpdated;
                existingMod.HasUpdateCheckData = true;
            }

            RefreshNotificationState();

            if(UpdatesTabPanel != null)
            {
                RenderRegisteredMods();

                if(!isUpdated && !wasOutdated)
                    ShowConfiguredWarningForMod(modName, currentVersion, latestVersionText);
            }
        }


        public static void RegisterBlacklistedMod(string guid, string modName, Version currentVersion)
        {
            RegisteredMod existingMod = GetRegisteredMod(guid, modName, string.Empty);

            if(existingMod == null)
            {
                RegisteredMods.Add(new RegisteredMod
                {
                    GUID = guid,
                    ModName = modName,
                    LatestURL = string.Empty,
                    CurrentVersion = currentVersion,
                    LatestVersion = currentVersion,
                    LatestVersionText = currentVersion.ToString(),
                    IsUpdated = true,
                    HasUpdateCheckData = false
                });
            }
            else
            {
                existingMod.GUID = guid;
                existingMod.ModName = modName;
                existingMod.CurrentVersion = currentVersion;

                if(!existingMod.HasUpdateCheckData)
                {
                    existingMod.LatestVersion = currentVersion;
                    existingMod.LatestVersionText = currentVersion.ToString();
                }
            }

            RefreshNotificationState();

            if(UpdatesTabPanel != null)
                RenderRegisteredMods();
        }


        public static void RenderRegisteredMods()
        {
            if(UpdatesTabPanel == null)
                return;

            var container = UpdatesTabPanel.tabs[UpdatesTabIndex].container;

            for(int i = container.childCount - 1; i >= 0; i--)
                Object.Destroy(container.GetChild(i).gameObject);

            UpdatesTabPanel.AddHeading(UpdatesTabIndex, FindMyUpdates.config.TranslateHeadingName ? string.Format("fmu.ui.header".LangKey(), "fmu.ui.headername".LangKey()) : string.Format("fmu.ui.header".LangKey(), GetEnglishName("fmu.ui.headername", "Find My Updates")));
            UpdatesTabPanel.AddHeading(UpdatesTabIndex, "<align=center>\n<color=#1e86d6>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n</align>");

            RefreshNotificationState();

            foreach(var mod in RegisteredMods.OrderBy(GetSortPriority).ThenBy(x => x.ModName, StringComparer.OrdinalIgnoreCase))
                RenderRegisteredMod(mod);

            UpdateTabTitle();
        }


        public static void RenderRegisteredMod(RegisteredMod mod)
        {
            bool isBlacklisted = FindMyUpdates.IsGUIDBlacklisted(mod.GUID);
            var blacklisted = string.Format(BlacklistedDescription, mod.ModName, mod.CurrentVersion);
            var updated = string.Format("fmu.ui.mod.updated".LangKey(), mod.ModName, mod.CurrentVersion, mod.LatestURL.IsNullOrWhiteSpace() ? "N/A" : mod.LatestURL);
            var outdated = string.Format("fmu.ui.mod.outdated".LangKey(), mod.ModName, mod.LatestVersionText, mod.CurrentVersion, mod.LatestURL.IsNullOrWhiteSpace() ? "N/A" : mod.LatestURL);

            UpdatesTabPanel.AddHeading(UpdatesTabIndex, isBlacklisted ? blacklisted : mod.IsUpdated ? updated : outdated);

            if(isBlacklisted)
            {
                UpdatesTabPanel.AddButton(UpdatesTabIndex, RemoveFromBlacklistLabel, () =>
                {
                    if(!FindMyUpdates.RemoveGUIDFromBlacklist(mod.GUID))
                        return;

                    if(!mod.HasUpdateCheckData)
                    {
                        RegisteredMods.Remove(mod);
                        RefreshNotificationState();
                        RenderRegisteredMods();
                        CoroutineHost.StartCoroutine(Function.CheckGUIDUpdate(mod.GUID));
                        return;
                    }

                    RefreshNotificationState();
                    RenderRegisteredMods();
                });
            }
            else
            {
                var hasFirstClick = false;

                UpdatesTabPanel.AddButton(UpdatesTabIndex, mod.IsUpdated ? "fmu.ui.button.updated".LangKey() : "fmu.ui.button.outdated".LangKey(), () =>
                {
                    if(!Uri.TryCreate(mod.LatestURL, UriKind.Absolute, out var uri))
                    {
                        Screen.Error(string.Format("fmu.warning.invalidurl".LangKey(), mod.LatestURL.IsNullOrWhiteSpace() ? "N/A" : mod.LatestURL));
                        return;
                    }

                    if(!hasFirstClick && FindMyUpdates.config.OpenURLBehaviour == 0)
                    {
                        Screen.Message(string.Format("fmu.warning.urlcheck".LangKey(), mod.LatestURL));

                        hasFirstClick = true;
                        return;
                    }

                    Process.Start(mod.LatestURL);

                    if(FindMyUpdates.config.LogURLsToScreen)
                        Screen.Message(string.Format("fmu.warning.urlopened".LangKey(), mod.LatestURL));
                });

                if(!mod.IsUpdated && !mod.GUID.IsNullOrWhiteSpace())
                    UpdatesTabPanel.AddButton(UpdatesTabIndex, AddToBlacklistLabel, () =>
                    {
                        if(!FindMyUpdates.AddGUIDToBlacklist(mod.GUID))
                            return;

                        RefreshNotificationState();
                        RenderRegisteredMods();
                    });
            }

            UpdatesTabPanel.AddHeading(UpdatesTabIndex, "<align=center>\n\n<color=#1e86d6>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n\n</align>");
        }


        public static void ShowConfiguredWarnings()
        {
            var outdatedMods = RegisteredMods.Where(x => x.HasUpdateCheckData && !x.IsUpdated && !FindMyUpdates.IsGUIDBlacklisted(x.GUID)).OrderBy(x => x.ModName, StringComparer.OrdinalIgnoreCase).ToList();

            if(outdatedMods.Count < 1)
                return;

            switch(FindMyUpdates.config.DisplayOptionsMenuWarnings)
            {
                case 0:
                    if(FindMyUpdates.config.ConfigHint)
                        Screen.Message("fmu.warning.hint".LangKey());

                    outdatedMods.ForEach(mod => Screen.Message(string.Format("fmu.warning.outdated".LangKey(), mod.ModName, mod.CurrentVersion, mod.LatestVersionText)));
                    break;

                case 1 when !HasWarnedOnce:
                    if(FindMyUpdates.config.ConfigHint)
                        Screen.Message("fmu.warning.hint".LangKey());

                    outdatedMods.ForEach(mod => Screen.Message(string.Format("fmu.warning.outdated".LangKey(), mod.ModName, mod.CurrentVersion, mod.LatestVersionText)));

                    HasWarnedOnce = true;
                    break;
            }
        }


        public static void ShowConfiguredWarningForMod(string modName, Version currentVersion, string latestVersionText)
        {
            switch(FindMyUpdates.config.DisplayOptionsMenuWarnings)
            {
                case 0:
                    if(FindMyUpdates.config.ConfigHint)
                        Screen.Message("fmu.warning.hint".LangKey());

                    Screen.Message(string.Format("fmu.warning.outdated".LangKey(), modName, currentVersion, latestVersionText));
                    break;

                case 1 when !HasWarnedOnce:
                    if(FindMyUpdates.config.ConfigHint)
                        Screen.Message("fmu.warning.hint".LangKey());

                    Screen.Message(string.Format("fmu.warning.outdated".LangKey(), modName, currentVersion, latestVersionText));

                    HasWarnedOnce = true;
                    break;
            }
        }


        public static RegisteredMod GetRegisteredMod(string guid, string modName, string latestUrl)
        {
            if(!guid.IsNullOrWhiteSpace())
                return RegisteredMods.FirstOrDefault(x => x.GUID == guid);

            return RegisteredMods.FirstOrDefault(x => x.ModName == modName && x.LatestURL == latestUrl);
        }


        public static void RefreshNotificationState()
        {
            LastOutdatedCount = RegisteredMods.Count(x => x.HasUpdateCheckData && !x.IsUpdated && !FindMyUpdates.IsGUIDBlacklisted(x.GUID));
            ShouldNotify = LastOutdatedCount > 0;
        }


        public static int GetSortPriority(RegisteredMod mod)
        {
            bool isBlacklisted = FindMyUpdates.IsGUIDBlacklisted(mod.GUID);

            if(!isBlacklisted && mod.HasUpdateCheckData && !mod.IsUpdated)
                return 0;

            if(isBlacklisted)
                return 1;

            return 2;
        }


        public static void UpdateTabTitle()
        {
            if(UpdatesTabPanel == null)
                return;

            string title = (FindMyUpdates.config.TranslateTabName ? "fmu.ui.tabname".LangKey() : GetEnglishName("fmu.ui.tabname", "Updates")) + (FindMyUpdates.config.OutdatedCountOnTab ? (LastOutdatedCount > 0 ? $" ({LastOutdatedCount})" : "") : "");

            if(uGUI_TabbedControlsPanelPatches.tmp != null)
                uGUI_TabbedControlsPanelPatches.tmp.text = title;
        }
    }
}