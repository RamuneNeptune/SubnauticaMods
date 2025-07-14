

namespace Ramune.StalkersDropTeethWhenTheyDie
{
    [Menu("StalkersDropTeethWhenTheyDie")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider(" • Amount of teeth to drop", Format = "{0:F0}", DefaultValue = 1f, Min = 1f, Max = 25f, Step = 1f, Tooltip = "Changes are applied automatically")]
        public float TeethToDrop = 1f;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}