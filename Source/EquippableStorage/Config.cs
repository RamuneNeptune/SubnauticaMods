

namespace Ramune.EquippableStorage
{
    [Menu("EquippableStorage")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Backpack slots:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 1)]
        public bool DividerBackpacksSlots;

        [Slider(" • Backpack slots to generate", Format = "{0:F1}", DefaultValue = 1f, Min = 1f, Max = 4f, Step = 1f, Tooltip = "Default: 1", Order = 2)]
        public int backpackSlotsToGenerate = 1;



        [Toggle("<color=#ffc600>Backpacks:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 1)]
        public bool DividerBackpacks;

        [Slider(" • Backpacks to generate", Format = "{0:F1}", DefaultValue = 5f, Min = 5f, Max = 30f, Step = 1f, Tooltip = "Default: 5", Order = 2)]
        public int backpacksToGenerate = 5;

        [Slider(" • Small backpack extra height", Format = "{0:F1}", DefaultValue = 2f, Min = 1f, Max = 30f, Step = 1f, Tooltip = "Default: 2", Order = 3)]
        public int backpack1_height = 2;

        [Slider(" • Regular backpack extra height", Format = "{0:F1}", DefaultValue = 5f, Min = 1f, Max = 30f, Step = 1f, Tooltip = "Default: 5", Order = 4)]
        public int backpack2_height = 5;

        [Slider(" • Large backpack extra height", Format = "{0:F1}", DefaultValue = 8f, Min = 1f, Max = 30f, Step = 1f, Tooltip = "Default: 8", Order = 5)]
        public int backpack3_height = 8;

        [Slider(" • Huge backpack extra height", Format = "{0:F1}", DefaultValue = 12f, Min = 1f, Max = 30f, Step = 1f, Tooltip = "Default: 12", Order = 6)]
        public int backpack4_height = 12;

        [Slider(" • Creative backpack extra height", Format = "{0:F1}", DefaultValue = 100f, Min = 1f, Max = 100f, Step = 1f, Tooltip = "Default: 100", Order = 7)]
        public int backpack5_height = 100;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}