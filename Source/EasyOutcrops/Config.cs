

namespace Ramune.EasyOutcrops
{
    [Menu("EasyOutcrops")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool dividerMisc = false;

        [Toggle(" • Check for updates", Tooltip = "On load, compares your current version to the latest version on GitHub. If you are using an oudated version, a link to obtain the latest version is logged to your LogOutput.log file")]
        public bool CheckForUpdates = true;
    }
}