

namespace Ramune.SeamothDepthModulesRedux
{
    [Menu("SeamothDepthModulesRedux")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider(" • Seamoth Depth Module MK4 Depth", 100, 5000, DefaultValue = 1100, Step = 100)]
        public int SeamothDepthModuleMK4Depth = 1100;

        [Slider(" • Seamoth Depth Module MK5 Depth", 100, 5000, DefaultValue = 1500, Step = 100)]
        public int SeamothDepthModuleMK5Depth = 1500;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}