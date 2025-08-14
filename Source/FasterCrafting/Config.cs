

namespace Ramune.FasterCrafting
{
    [Menu("FasterCrafting")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider("• Crafting speed multiplier", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f)]
        public float Multiplier = 1f;

        [Slider("• Crafting speed multiplier (mega)", Format = "{0:F1}x", DefaultValue = 5f, Min = 5f, Max = 100f, Step = 0.1f)]
        public float MultiplierMega = 5f;

        [Choice("• Multiplier to use", Options = new[] { "Normal multiplier", "Mega multiplier" })]
        public int MultiplierChoice = 0;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}