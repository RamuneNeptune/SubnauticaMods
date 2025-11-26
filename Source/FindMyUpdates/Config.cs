

namespace Ramune.FindMyUpdates
{
    [Menu("Find My Updates")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Notices:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerLogging = false;

        [Toggle(" • Display main menu notice", Tooltip = "Displays a message on the left side of your screen when you reach the main menu if any mods are found to be oudated")]
        public bool MainMenuNotice = true;

        [Slider(" • Main menu notice duration", Format = "{0:F0}s", DefaultValue = 15f, Min = 1f, Max = 60f, Step = 1f, Tooltip = "The duration in seconds to display the outdated mods main menu notice")]
        public float MainMenuNoticeDuration = 15f;

        [Choice(" • Display options menu warnings", Options = new[] { "<color=#ffcf3c><b>(1/3)</b></color> Always", "<color=#ffcf3c><b>(2/3)</b></color> Only once", "<color=#ffcf3c><b>(3/3)</b></color> Never", }, Tooltip = "Determines whether on-screen warnings should show every time you open the options menu, only once, or never")]
        public int DisplayOptionsMenuWarnings = 0;

        [Toggle(" • Show hint in options warnings", Tooltip = "Includes a hint in on-screen warnings about being able to disable them in the mod configuration")]
        public bool ConfigHint = true;

        [Toggle(" • Display outdated count on tab", Tooltip = "Displays the amount of outdated mods in the \"Updates\" tab text")]
        public bool OutdatedCountOnTab = true;

        [Toggle("<color=#ffc600>Safety:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerSafety = false;

        [Choice(" • URL opening behaviour", Options = new[] { "<color=#ffcf3c><b>(1/2)</b></color> Require confirmation", "<color=#ffcf3c><b>(2/2)</b></color> Open links immediately" }, Tooltip = "Determines whether links should require an additional confirmation click to open, or if they should open immediately in your browser")]
        public int OpenURLBehaviour = 0;

        [Toggle("<color=#ffc600>Hardcoded Checks:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerNautilus = false;

        [Toggle(" • Check for Nautilus updates", Tooltip = "Automatically checks if Nautilus needs to be updated by comparing your installed version to the latest (pre-)release on GitHub")]
        public bool CheckNautilus = true;

        [Toggle("<color=#ffc600>Localization:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerLocalization = false;

        [Toggle(" • Localize \"Find My Updates\"", Tooltip = "Translates the \"Find My Updates\" heading in the \"Updates\" tab into your selected language")]
        public bool TranslateHeadingName = true;

        [Toggle(" • Localize \"Updates\"", Tooltip = "Translates the \"Updates\" tab name into your selected language")]
        public bool TranslateTabName = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Log for up-to date mods", Tooltip = "Logs information about up-to date (current version is same as latest version) mods after processing")]
        public bool LogForUpToDateMods = true;

        [Toggle(" • Log for outdated mods", Tooltip = "Logs information about outdated (current version is lower than latest version) mods after processing")]
        public bool LogForOutdatedMods = true;

        [Toggle(" • Log for overdated mods", Tooltip = "Logs information about overdated (current version is higher than latest version) mods after processing")]
        public bool LogForOverdatedMods = true;

        [Toggle(" • Log web requests to logfile", Tooltip = "Logs all web requests made by the mod to your logfile")]
        public bool LogWebRequests = false;

        [Choice(" • Log web requests level", Options = new[] { "<color=#ffcf3c><b>(1/5)</b></color> Debug", "<color=#ffcf3c><b>(2/5)</b></color> Info", "<color=#ffcf3c><b>(3/5)</b></color> Warning", "<color=#ffcf3c><b>(4/5)</b></color> Error", "<color=#ffcf3c><b>(5/5)</b></color> Fatal" }, Tooltip = "The level (info, warning, etc.) to log web requests to your logfile with")]
        public int LoggingLevel = 0;

        [Toggle(" • Log clicked URLs on screen", Tooltip = "Displays the opened URL on-screen when you click buttons like \"Update\" and \"Up-to date\"")]
        public bool LogURLsToScreen = true;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}