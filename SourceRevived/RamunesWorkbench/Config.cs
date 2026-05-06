

namespace Ramune.RamunesWorkbench
{
    [Menu("RamunesWorkbench")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Choice("Tab node style")]
        public NodeStyle tabStyle = NodeStyle.Fancy;

        [Toggle("Toggle light/glow", Tooltip = "Requires restart! Toggles the purple light/glow around the workbench when you open it.")]
        public bool light = true;

        [Toggle("Toggle fade animation", Tooltip = "Toggles the smooth rise and fall animation of the workbench texture.")]
        public bool animation = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;


        public enum NodeStyle
        {
            Vanilla,
            Fancy
        };
    }
}