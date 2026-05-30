

namespace Ramune.ScannerBlipIcons
{
    [Menu("ScannerBlipIcons")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 0)]
        public bool DividerCfg = false;

        [Slider(" • Blip icon size multiplier (x)", Format = "{0:F1}x", DefaultValue = 1f, Min = 0.1f, Max = 5f, Step = 0.1f, Tooltip = "Changes are applied automatically", Order = 1)]
        public float BlipIconSizeMultiplier = 1f;


        [Toggle("<color=#ffc600>Blacklist:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 2)]
        public bool DividerBlacklist = false;

        [Button("Open blacklist", Order = 3)]
        public void OpenBlacklist(ButtonClickedEventArgs _) => Process.Start(Path.Combine(Paths.ConfigurationFolder, "BlacklistedTechTypes.json"));


        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 4)]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it", Order = 5)]
        public bool EnableThisMod = true;
    }
}