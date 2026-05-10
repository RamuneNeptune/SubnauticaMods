

namespace Ramune.BulkVending
{
    [Menu("BulkVending")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Snacks:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerSnacks = false;

        [Slider(" • Snacks per vending machine use", Format = "{0:F0}", DefaultValue = 1f, Min = 1f, Max = 20f, Step = 1f)]
        public int SnacksPerUse = 1;

        [Toggle(" • Add processing time per snack")]
        public bool SnackAddsTime = true;

        [Toggle(" • Require power cost per snack")]
        public bool SnackRequiresPower = false;

        [Slider(" • Seconds of processing per snack", Format = "{0:F0}s", DefaultValue = 1f, Min = 1f, Max = 60f, Step = 1f)]
        public int SecondsPerSnack = 1;

        [Slider(" • Power cost per snack", Format = "{0:F1}", DefaultValue = 1f, Min = 1f, Max = 50f, Step = 1f)]
        public float SnackPowerCostPerItem = 1f;

        [Toggle("<color=#ffc600>Coffee:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCoffee = false;

        [Slider(" • Coffees per dispenser use", Format = "{0:F0}", DefaultValue = 1f, Min = 1f, Max = 20f, Step = 1f)]
        public int CoffeesPerUse = 1;

        [Toggle(" • Add processing time per coffee")]
        public bool CoffeeAddsTime = true;

        [Toggle(" • Require power cost per coffee")]
        public bool CoffeeRequiresPower = false;

        [Slider(" • Seconds of processing per coffee", Format = "{0:F0}s", DefaultValue = 19f, Min = 1f, Max = 60f, Step = 1f)]
        public int SecondsPerCoffee = 19;

        [Slider(" • Power cost per coffee", Format = "{0:F1}", DefaultValue = 1f, Min = 1f, Max = 50f, Step = 1f)]
        public float CoffeePowerCostPerItem = 1f;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}