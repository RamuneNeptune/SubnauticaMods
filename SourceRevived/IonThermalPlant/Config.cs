

namespace Ramune.IonThermalPlant
{
    [Menu("IonThermalPlant")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider("Power generation multiplier (x)", Format = "{0:F1}x", DefaultValue = 2.2f, Min = 1f, Max = 10f, Step = 0.1f, Tooltip = "Power generation will be multiplied by this amount. (Default: 2.2x)")]
        public float powerMultiplier;

        [Slider("Maximum power capacity", Format = "{0:F0}", DefaultValue = 500f, Min = 1f, Max = 1000f, Step = 1f, Tooltip = "The maximum amount of power the thermal plant can store. (Default: 500)")]
        public float powerMaxCapacity;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}