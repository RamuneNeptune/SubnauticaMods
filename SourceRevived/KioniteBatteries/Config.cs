

namespace Ramune.KioniteBatteries
{
    [Menu("KioniteBatteries")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider("Kionite battery capacity", Format = "{0:0}", DefaultValue = 200f, Min = 10f, Max = 1000f, Step = 10f, Tooltip = "Configuring outside of slider limits can be done manually in BepInEx\\config\\KioniteBatteries\\config.json. Changes are applied on game restart")]
        public int batteryCapacity = 200;

        [Slider("Kionite power cell capacity", Format = "{0:0}", DefaultValue = 400f, Min = 10f, Max = 1000f, Step = 10f, Tooltip = "Configuring outside of slider limits can be done manually in BepInEx\\config\\KioniteBatteries\\config.json. Changes are applied on game restart")]
        public int powerCellCapacity = 400;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;

        [Button("Open battery recipe file", Order = 2)]
        public void OpenBatteryRecipe(ButtonClickedEventArgs _)
        {
            Process.Start(Path.Combine(Paths.RecipeFolder, "KioniteBattery.json"));
        }

        [Button("Open power cell recipe file", Order = 3)]
        public void OpenPowerCellRecipe(ButtonClickedEventArgs _)
        {
            Process.Start(Path.Combine(Paths.RecipeFolder, "KionitePowerCell.json"));
        }
    }
}