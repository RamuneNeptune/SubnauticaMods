

namespace Ramune.TableCoralMultiplier
{
    [Menu("TableCoralMultiplier")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider("Table coral to spawn", Format = "{0:F0}", DefaultValue = 1f, Min = 1f, Max = 20f, Step = 1f, Tooltip = "Amount of table coral to spawn")]
        public float TableCoralToSpawn = 1f;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}