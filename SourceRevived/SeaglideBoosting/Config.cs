

namespace Ramune.SeaglideBoosting
{
    [Menu("SeaglideBoosting")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider("Boost Speed Multiplier (x)", Format = "{0:F1}x", DefaultValue = 1.5f, Min = 0.1f, Max = 10f, Step = 0.1f, Tooltip = "Changes are applied automatically")]
        public float boostMultiplier = 1.5f;

        [Slider("Boost Energy Usage Multiplier (x)", Format = "{0:F1}x", DefaultValue = 3.5f, Min = 0.1f, Max = 10f, Step = 0.1f, Tooltip = "Changes are applied automatically")]
        public float energyMultiplier = 3.5f;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}