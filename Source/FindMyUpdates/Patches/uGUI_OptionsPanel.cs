

namespace Ramune.FindMyUpdates.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatch
    {
        public static string UpdatesTabName = "Updates";

        public static int UpdatesTabIndex;

        public static uGUI_OptionsPanel UpdatesTabPanel;

        public static Button latestButton;

        public static Queue<Action> PendingRegistrations = new();

        public static bool HasWarnedOnce = false;


        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTabs)), HarmonyPostfix]
        public static void AddTabs(uGUI_OptionsPanel __instance)
        {
            UpdatesTabPanel = __instance;
            UpdatesTabIndex = __instance.AddTab(UpdatesTabName);

            __instance.AddHeading(UpdatesTabIndex, "<align=center>\n<size=115%><color=#ffc834><b>Find My Updates</b></color></size>\nAny mods that support version checking will be listed here\n\n</align>");
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
                var updated = $"<b><color=#ffc834>{modName}:</color></b>\n • You are using the latest version: <color=#ffc834>{currentVersion}</color>";
                var outdated = $"<b><color=#ffc834>{modName}:</color></b>\n • An update is available: <color=#ffc834>{latestVersion}</color>! (current: <color=#ffc834>{currentVersion}</color>)\n • <size=70%>URL: <color=#ffc834>{(latestUrl.IsNullOrWhiteSpace() ? "N/A" : latestUrl)}</size></color>";

                UpdatesTabPanel.AddHeading(UpdatesTabIndex, isUpdated ? updated : outdated);

                bool hasFirstClick = false;

                if(!isUpdated)
                {
                    if(FindMyUpdates.config.OnScreenWarning)
                    {
                        if(FindMyUpdates.config.OnScreenWarningEveryTime)
                        {
                            Screen.Message($"You can disable these messages in the mod config");
                            Screen.Message($"<color=#ffc834>{modName}</color> is outdated! You have: <color=#ffc834>{currentVersion}</color> (latest is: <color=#ffc834>{latestVersion}</color>)");
                        }
                        else if(!HasWarnedOnce)
                        {
                            Screen.Message($"You can disable these messages in the mod config");
                            Screen.Message($"<color=#ffc834>{modName}</color> is outdated! You have: <color=#ffc834>{currentVersion}</color> (latest is: <color=#ffc834>{latestVersion}</color>)");
                            HasWarnedOnce = true;
                        }
                    }

                    UpdatesTabPanel.AddButton(UpdatesTabIndex, "Update", () =>
                    {
                        if(!Uri.TryCreate(latestUrl, UriKind.Absolute, out var uri))
                        {
                            Screen.Error($"Invalid URL: {(latestUrl.IsNullOrWhiteSpace() ? "N/A" : latestUrl)}");
                            return;
                        }

                        if(!hasFirstClick)
                        {
                            Screen.Message($"<b>Are you sure you want to open this link?</b>\n<color=#ffc802><size=75%>:: {latestUrl}</size></color>");

                            hasFirstClick = true;

                            return;
                        }

                        Process.Start(latestUrl);

                        Screen.Message($"<b>Opened URL</b>\n<color=#ffc802><size=75%>:: {latestUrl}</size></color>");
                    });
                }
                else
                {
                    UpdatesTabPanel.AddButton(UpdatesTabIndex, "Up to date");    
                }

                UpdatesTabPanel.AddHeading(UpdatesTabIndex, "<align=center>\n\n<color=#1e86d6>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n\n</align>");
            }
        }
    }
}