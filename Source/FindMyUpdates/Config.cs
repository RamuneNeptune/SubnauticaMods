

namespace Ramune.FindMyUpdates
{
    [Menu("Find My Updates")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Logging:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerLogging = false;

        [Toggle(" • Enable main menu notice", Tooltip = "Displays a message on-screen when you reach the main menu if any mods are found to be oudated")]
        public bool MainMenuNotice = true;

        [Slider(" • Main menu notice duration", Format = "{0:F0}s", DefaultValue = 8f, Min = 1f, Max = 30f, Step = 1f, Tooltip = "The duration in seconds to display the outdated mods main menu notice")]
        public float MainMenuNoticeDuration = 8f;

        [Toggle(" • Enable on-screen warnings", Tooltip = "Displays text on-screen when you open the options menu if any mods are found to be oudated")]
        public bool OnScreenWarning = true;

        [Toggle(" • On-screen warnings every time?", Tooltip = "Determines whether on-screen warnings should show every time you open the Options menu or only once")]
        public bool OnScreenWarningEveryTime = true;

        [Toggle(" • Warn before opening URLs", Tooltip = "Determines whether buttons like \"Update\" and \"Up-to date\" open URLs immediately, or require an additional confirmational click")]
        public bool WarnOnButtonClicks = true;

        [Toggle(" • Log clicked URLs on screen", Tooltip = "Displays the opened URL on-screen when you click buttons like \"Update\" and \"Up-to date\"")]
        public bool LogURLsToScreen = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}