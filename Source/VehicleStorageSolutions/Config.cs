


namespace Ramune.VehicleStorageSolutions
{
    [Menu("VehicleStorageSolutions")]
    public class Config : ConfigFile
    {
        public const string tooltip = "Changes are applied when you re-open the storage container. Vanilla: ";

        public const string tooltip2 = "Changes are applied when you re-open the storage container. Default: ";

        public const float heightMinValue = 1f;

        public const float heightMaxValue = 30f;

        public const float widthMinValue = 1f;

        public const float widthMaxValue = 10f;

        public const float step = 1f;


        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 1)]
        public bool DividerCfg = false;

        [Slider(" • Storage modules to generate", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = "The amount of VSSStorageModuleMk's to generate. Make sure you add a new recipe file, sprite, and expand the localization (e.g. English.json) (based on the existing naming conventions) to account for however many extra modules you choose to add. Default: 4", Order = 2)]
        public int storageModulesToGenerate = 4;


        [Toggle("<color=#ffc600>Prawn Suit storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 10)]
        public bool divider_prawnSuit;

        [Slider(" • Prawn Suit storage width (x)", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "4", Order = 11)]
        public int width_prawnSuit = 4;

        [Slider(" • Prawn Suit storage height (y)", Format = "{0:F1}", DefaultValue = 6f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "6", Order = 12)]
        public int height_prawnSuit = 6;

        [Slider(" • Prawn Suit storage Mk2 extra height", Format = "{0:F1}", DefaultValue = 2f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "2", Order = 13)]
        public int height_prawnSuitMk2Module = 2;

        [Slider(" • Prawn Suit storage Mk3 extra height", Format = "{0:F1}", DefaultValue = 3f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "3", Order = 14)]
        public int height_prawnSuitMk3Module = 3;

        [Slider(" • Prawn Suit storage Mk4 extra height", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "4", Order = 15)]
        public int height_prawnSuitMk4Module = 4;

        [Slider(" • Prawn Suit storage Mk5 extra height", Format = "{0:F1}", DefaultValue = 5f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "5", Order = 16)]
        public int height_prawnSuitMk5Module = 5;


        [Toggle("<color=#ffc600>Seamoth storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 17)]
        public bool divider_seamoth;

        [Slider(" • Seamoth storage width (x)", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "4", Order = 18)]
        public int width_seamoth = 4;

        [Slider(" • Seamoth storage height (y)", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "4", Order = 19)]
        public int height_seamoth = 4;

        [Slider(" • Seamoth storage Mk2 extra height", Format = "{0:F1}", DefaultValue = 1f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "1", Order = 20)]
        public int height_seamothMk2Module = 1;

        [Slider(" • Seamoth storage Mk3 extra height", Format = "{0:F1}", DefaultValue = 2f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "2", Order = 21)]
        public int height_seamothMk3Module = 2;

        [Slider(" • Seamoth storage Mk4 extra height", Format = "{0:F1}", DefaultValue = 3f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "3", Order = 22)]
        public int height_seamothMk4Module = 3;

        [Slider(" • Seamoth storage Mk5 extra height", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip2 + "4", Order = 23)]
        public int height_seamothMk5Module = 4;


        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}