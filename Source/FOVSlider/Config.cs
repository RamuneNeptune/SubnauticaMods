

namespace Ramune.FOVSlider
{
    [Menu("FOVSlider")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider("Field of view", Format = "{0:F0}", DefaultValue = 80f, Min = 80f, Max = 200f, Step = 1f, Tooltip = "Amount of table coral to spawn"), OnChange(nameof(OnChangeFOV))]
        public float FOV = 80f;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;

        public static void OnChangeFOV() => MiscSettings.fieldOfView = FOVSlider.config.FOV;
    }
}