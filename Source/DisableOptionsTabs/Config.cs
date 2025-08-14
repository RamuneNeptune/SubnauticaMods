

namespace Ramune.DisableOptionsTabs
{
    [Menu("DisableOptionsTabs")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Disable <b>Graphics</b>", Tooltip = "Re-open options menu to apply")]
        public bool DisableGraphics = false;

        [Toggle(" • Disable <b>Keyboard</b>", Tooltip = "Re-open options menu to apply")]
        public bool DisableKeyboard = false;

        [Toggle(" • Disable <b>Controller</b>", Tooltip = "Re-open options menu to apply")]
        public bool DisableController = false;
        
        [Toggle(" • Disable <b>Accessibility</b>", Tooltip = "Re-open options menu to apply")]
        public bool DisableAccessibility = false;

        [Toggle(" • Disable <b>Redeem a Key</b>", Tooltip = "Re-open options menu to apply")]
        public bool DisableRedeemKey = false;

        [Toggle(" • Disable <b>Troubleshooting</b>", Tooltip = "Re-open options menu to apply")]
        public bool DisableTroubleshooting = false;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}