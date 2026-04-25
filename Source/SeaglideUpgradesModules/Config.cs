

using static iTween;

namespace Ramune.SeaglideUpgradesModules
{
    [Menu("SeaglideUpgrades:Modules")]
    public class Config : ConfigFile
    {
        const string _div = "<alpha=#00>---------------------------------------------------------------------------------------------------</alpha>";
        const string _mk1 = "<color=#37B8FD>MK1</color>";
        const string _mk2 = "<color=#C6FF53>MK2</color>";
        const string _mk3 = "<color=#FE6A4D>MK3</color>";



        //[Toggle($"<color=#ffc600>Configuration:</color> {_div}")]
        //public bool DividerCfg = false;



        [Toggle($"<color=#ffc600>Speed Upgrade:</color> {_div}")]
        public bool DividerSpeed = true;

        [Slider($" • Speed Multiplier Per Upgrade", Format = "{0:F2}x", DefaultValue = 1.5f, Min = 1f, Max = 5f, Step = 0.25f, Tooltip = $"")]
        public float SpeedMultiplier = 1.5f;

        [Choice($" • Max Upgrades", Options = ["None", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "Unlimited"], Tooltip = $"")]
        public int MaxSpeedUpgrades = 5;


        [Toggle($"<color=#ffc600>Efficiency Upgrade:</color> {_div}")]
        public bool DividerEfficiency = true;

        [Slider($" • Efficiency % Per Upgrade", Format = "{0:F2}%", DefaultValue = 1.5f, Min = 1f, Max = 5f, Step = 0.25f, Tooltip = $"")]
        public float EfficiencyMultiplier = 1.5f;

        [Choice($" • Max Upgrades", Options = ["None", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "Unlimited"], Tooltip = $"")]
        public int MaxEfficiencyUpgrades = 5;


        [Toggle($"<color=#ffc600>Boost Upgrade:</color> {_div}")]
        public bool DividerBoost = true;

        [Slider($" • Boost Speed Multiplier", Format = "{0:F2}x", DefaultValue = 1.5f, Min = 1f, Max = 5f, Step = 0.25f, Tooltip = $"")]
        public float BoostMultiplier = 1.5f;

        [Choice($" • Max Upgrades", Options = ["None", "1"], Tooltip = $"")]
        public int MaxBoostUpgrades = 1;


        [Toggle($"<color=#ffc600>Battery Swap Upgrade:</color> {_div}", Tooltip = $"")]
        public bool DividerBatterySwap = true;

        [Choice($" • Battery Swap Priority", Options = ["<color=#ffc600>1.</color> Charge, <color=#ffc600>2.</color> Capacity", "<color=#ffc600>1.</color> Capacity, <color=#ffc600>2.</color> Charge"], Tooltip = $"")]
        public int BatterySwapPriority = 0;

        [Choice($" • Battery Swap Charge Priority", Options = ["Use <color=#ffc600>highest</color> charge first", "Use <color=#ffc600>lowest</color> charge first"], Tooltip = $"")]
        public int BatterySwapChargePriority = 0;

        [Choice($" • Battery Swap Capacity Priority", Options = ["Use <color=#ffc600>highest</color> capacity first", "Use <color=#ffc600>lowest</color> capacity first"], Tooltip = $"")]
        public int BatterySwapCapacityPriority = 0;

        [Choice($" • Max Upgrades", Options = ["None", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "Unlimited"], Tooltip = $"")]
        public int MaxBatterySwapUpgrades = 1;



        [Toggle($"<color=#ffc600>Seaglide {_mk1}:</color> {_div}")]
        public bool DividerMk1 = true;

        [Toggle($" • Disable original {_mk1} speed boost (+15%)", Tooltip = $"")]
        public bool DisableSpeedMk1 = false;

        [Slider($" • {_mk1} Module storage width", Format = "{0:F0}", DefaultValue = 2, Min = 1, Max = 8, Step = 1, Tooltip = $"")]
        public int ModuleStorageWidthMk1 = 2;

        [Slider($" • {_mk1} Module storage height", Format = "{0:F0}", DefaultValue = 1, Min = 1, Max = 8, Step = 1, Tooltip = $"")]
        public int ModuleStorageHeightMk1 = 1;



        [Toggle($"<color=#ffc600>Seaglide {_mk2}:</color> {_div}")]
        public bool DividerMk2 = true;

        [Toggle($" • Disable original {_mk2} speed boost (+25%)", Tooltip = $"")]
        public bool DisableSpeedMk2 = false;

        [Slider($" • {_mk2} Module storage width", Format = "{0:F0}", DefaultValue = 2, Min = 1, Max = 8, Step = 1, Tooltip = $"")]
        public int ModuleStorageWidthMk2 = 2;

        [Slider($" • {_mk2} Module storage height", Format = "{0:F0}", DefaultValue = 2, Min = 1, Max = 8, Step = 1, Tooltip = $"")]
        public int ModuleStorageHeightMk2 = 2;



        [Toggle($"<color=#ffc600>Seaglide {_mk3}:</color> {_div}")]
        public bool DividerMk3 = true;

        [Toggle($" • Disable original {_mk3} speed boost (+40%)", Tooltip = $"")]
        public bool DisableSpeedMk3 = false;

        [Slider($" • {_mk3} Module storage width", Format = "{0:F0}", DefaultValue = 3, Min = 1, Max = 8, Step = 1, Tooltip = $"")]
        public int ModuleStorageWidthMk3 = 3;

        [Slider($" • {_mk3} Module storage height", Format = "{0:F0}", DefaultValue = 2, Min = 1, Max = 8, Step = 1, Tooltip = $"")]
        public int ModuleStorageHeightMk3 = 2;



        [Toggle($"<color=#ffc600>Miscellaneous:</color> {_div}")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}