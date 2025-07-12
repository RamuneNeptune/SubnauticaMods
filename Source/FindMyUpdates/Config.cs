

namespace Ramune.FindMyUpdates
{
    [Menu("Find My Updates")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Logging:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerLogging = false;

        [Toggle(" • Enable on-screen warnings", Tooltip = "Displays text on-screen when you open the options menu if any mods are found to be oudated")]
        public bool OnScreenWarning = true;

        [Toggle(" • On-screen warnings every time?", Tooltip = "Determines whether on-screen warnings should show every time you open options, or only once for the first open")]
        public bool OnScreenWarningEveryTime = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}