

namespace Ramune.PrawnSuitLightSwitch
{
    [Menu("PrawnSuitLightSwitch")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle("Enable toggle on/off UI text")]
        public bool ui = true;

        [Toggle("Enable toggle on/off sounds")]
        public bool sounds = true;

        [Toggle("Enable toggle on/off subtitles")]
        public bool debug = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}