

namespace Ramune.WhiteLightsRedux
{
    [Menu("WhiteLightsRedux")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Affect Seaglide", Tooltip = "Requires a restart to take effect if this option is configured in a save file")]
        public bool AffectSeaglide = true;

        [Toggle(" • Affect Seamoth", Tooltip = "Requires a restart to take effect if this option is configured in a save file")]
        public bool AffectSeamoth = true;

        [Toggle(" • Affect Prawn Suit", Tooltip = "Requires a restart to take effect if this option is configured in a save file")]
        public bool AffectExosuit = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}