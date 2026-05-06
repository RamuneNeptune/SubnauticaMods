

namespace Ramune.KeepInventoryRedux
{
    [Menu("KeepInventoryRedux")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 0)]
        public bool DividerCfg = false;

        [Toggle(" • Keep everything", Order = 1)]
        public bool KeepEverything = true;

        [Toggle(" • Keep hotbar items", Order = 2)]
        public bool KeepHotbar = false;

        [Toggle(" • Keep equipped items", Order = 3)]
        public bool KeepEquipped = false;

        [Toggle("<color=#ffc600>Whitelist:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 4)]
        public bool DividerWhitelist = false;

        [Toggle(" • Use whitelist", Order = 5)]
        public bool UseWhitelist = false;

        [Choice(" • Whitelist behaviour", ["Also keep <color=#ffc600>hotbar/equipped</color> items", "Also keep <color=#ffc600>hotbar</color> items", "Also keep <color=#ffc600>equipped</color> items", "Only keep <color=#ffc600>whitelisted</color> items"], Order = 6)]
        public int WhitelistBehaviour = 0;

        [Button("Open whitelist", Order = 7)]
        public void OpenWhitelist(ButtonClickedEventArgs _) => Process.Start(Path.Combine(Paths.ConfigurationFolder, "Whitelist.json"));

        [Toggle("<color=#ffc600>Blacklist:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 8)]
        public bool DividerBlacklist = false;

        [Toggle(" • Use blacklist", Order = 9)]
        public bool UseBlacklist = false;

        [Button("Open blacklist", Order = 10)]
        public void OpenBlacklist(ButtonClickedEventArgs _) => Process.Start(Path.Combine(Paths.ConfigurationFolder, "Blacklist.json"));

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 11)]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it", Order = 12)]
        public bool EnableThisMod = true;
    }
}