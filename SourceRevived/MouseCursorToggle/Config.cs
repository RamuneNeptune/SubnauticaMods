

namespace Ramune.MouseCursorToggle
{
    [Menu("MouseCursorToggle")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Pause game while cursor is shown", Tooltip = "Pauses the game while cursor mode is active.")]
        public bool PauseGame = false;

        [Toggle("<color=#ffc600>Exit hint:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerExitHint = false;

        [Toggle(" • Show exit hint")]
        public bool ShowExitHint = true;

        [ColorPicker(" • Exit hint color", Advanced = true)]
        public Color ExitHintColor = new(1f, 1f, 1f);

        [Choice(" • Exit hint style")]
        public FontStyles ExitHintStyle = FontStyles.Normal;

        [Slider(" • Exit hint size", Format = "{0:F0}", DefaultValue = 24f, Min = 1f, Max = 100f, Step = 1f)]
        public int ExitHintSize = 24;

        [Slider(" • Exit hint offset (X)", Format = "{0:F0}", DefaultValue = 0f, Min = -1000f, Max = 1000f, Step = 5f)]
        public int ExitHintX = 0;

        [Slider(" • Exit hint offset (Y)", Format = "{0:F0}", DefaultValue = -420f, Min = -1000f, Max = 1000f, Step = 5f)]
        public int ExitHintY = -420;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}