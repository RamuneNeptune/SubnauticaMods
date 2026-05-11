

namespace Ramune.BigFriendlyFish
{
    [Menu("BigFriendlyFish")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider(" • Big Friendly Scale Multiplier", Format = "{0:F2}x", DefaultValue = 7f, Min = 1f, Max = 20f, Step = 1f, Tooltip = "Requires a restart to take effect")]
        public float bigFriendlyScale = 7f;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}