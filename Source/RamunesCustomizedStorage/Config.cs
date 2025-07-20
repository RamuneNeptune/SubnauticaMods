

namespace Ramune.RamunesCustomizedStorage
{
    [Menu("RamunesCustomizedStorage")]
    public class Config : ConfigFile
    {
        public const string tooltip = "Changes are applied when you re-open the storage container. Vanilla: ";

        public const float heightMinValue = 1f;

        public const float heightMaxValue = 30f;

        public const float widthMinValue = 1f;

        public const float widthMaxValue = 10f;

        public const float step = 1f;


        [Toggle("<color=#ffc600>Inventory storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 1)]
        public bool divider_inventory;

        [Slider(" • Inventory width (x)", Format = "{0:F1}", DefaultValue = 6f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "6", Order = 2)]
        public int width_inventory = 6;

        [Slider(" • Inventory height (y)", Format = "{0:F1}", DefaultValue = 8f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "8", Order = 3)]
        public int height_inventory = 8;


        [Toggle("<color=#ffc600>Lifepod storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 4)]
        public bool divider_lifepod;

        [Slider(" • Lifepod storage width (x)", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "4", Order = 5)]
        public int width_lifepod = 4;

        [Slider(" • Lifepod storage height (y)", Format = "{0:F1}", DefaultValue = 8f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "8", Order = 6)]
        public int height_lifepod = 8;


        [Toggle("<color=#ffc600>Standing locker storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 7)]
        public bool divider_locker;

        [Slider(" • Standing locker storage width (x)", Format = "{0:F1}", DefaultValue = 6f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "6", Order = 8)]
        public int width_locker = 6;

        [Slider(" • Standing locker storage height (y)", Format = "{0:F1}", DefaultValue = 8f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "8", Order = 9)]
        public int height_locker = 8;


        [Toggle("<color=#ffc600>Wall locker storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 10)]
        public bool divider_wallLocker;

        [Slider(" • Wall locker storage width (x)", Format = "{0:F1}", DefaultValue = 5f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "5", Order = 11)]
        public int width_wallLocker = 5;

        [Slider(" • Wall locker storage height (y)", Format = "{0:F1}", DefaultValue = 6f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "6", Order = 12)]
        public int height_wallLocker = 6;


        [Toggle("<color=#ffc600>Prawn Suit storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 13)]
        public bool divider_prawnSuit;

        [Slider(" • Prawn Suit storage width (x)", Format = "{0:F1}", DefaultValue = 6f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "6", Order = 14)]
        public int width_prawnSuit = 4;

        [Slider(" • Prawn Suit storage height (y)", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "4", Order = 15)]
        public int height_prawnSuit = 6;

        [Slider(" • Prawn Suit storage height per module", Format = "{0:F1}", DefaultValue = 1f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "1", Order = 16)]
        public int height_prawnSuitModule = 1;


        [Toggle("<color=#ffc600>Seamoth storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 17)]
        public bool divider_seamoth;

        [Slider(" • Seamoth storage width per module (x)", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "4", Order = 18)]
        public int width_seamoth = 4;

        [Slider(" • Seamoth storage height per module (y)", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "4", Order = 19)]
        public int height_seamoth = 4;


        [Toggle("<color=#ffc600>Cyclops storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 22)]
        public bool divider_cyclops;

        [Slider(" • Cyclops storage width (x)", Format = "{0:F1}", DefaultValue = 3f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "3", Order = 23)]
        public int width_cyclops = 3;

        [Slider(" • Cyclops storage height (y)", Format = "{0:F1}", DefaultValue = 6f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "6", Order = 24)]
        public int height_cyclops = 6;



        [Toggle("<color=#ffc600>Bio reactor storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 25)]
        public bool divider_bioReactor;

        [Slider(" • Bio reactor storage width (x)", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "4", Order = 26)]
        public int width_bioReactor = 4;

        [Slider(" • Bio reactor storage height (y)", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "4", Order = 27)]
        public int height_bioReactor = 4;



        [Toggle("<color=#ffc600>Waterproof locker storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 28)]
        public bool divider_waterproofLocker;

        [Slider(" • Waterproof locker storage width (x)", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "4", Order = 29)]
        public int width_waterproofLocker = 4;

        [Slider(" • Waterproof locker storage height (y)", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "4", Order = 30)]
        public int height_waterproofLocker = 4;



        [Toggle("<color=#ffc600>Carry-all storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 31)]
        public bool divider_carryAll;

        [Slider(" • Carry-all storage width (x)", Format = "{0:F1}", DefaultValue = 3f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "3", Order = 32)]
        public int width_carryAll = 3;

        [Slider(" • Carry-all storage height (y)", Format = "{0:F1}", DefaultValue = 3f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "3", Order = 33)]
        public int height_carryAll = 3;


        [Toggle("<color=#ffc600>Water filtration storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 34)]
        public bool divider_filtration;

        [Slider(" • Water filtration storage width (x)", Format = "{0:F1}", DefaultValue = 2f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "2", Order = 35)]
        public int width_filtration = 2;

        [Slider(" • Water filtration storage height (y)", Format = "{0:F1}", DefaultValue = 2f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "2", Order = 36)]
        public int height_filtration = 2;

        [Slider(" • Water filtration max salt", Format = "{0:F1}", DefaultValue = 2f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "2", Order = 37)]
        public int salt_filtration = 2;

        [Slider(" • Water filtration max water", Format = "{0:F1}", DefaultValue = 2f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "2", Order = 38)]
        public int water_filtration = 2;


        [Toggle("<color=#ffc600>Trash can storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 39)]
        public bool divider_trashcan;

        [Slider(" • Trash can storage width (x)", Format = "{0:F1}", DefaultValue = 4f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "2", Order = 40)]
        public int width_trashcan = 4;

        [Slider(" • Trash can storage height (y)", Format = "{0:F1}", DefaultValue = 5f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "2", Order = 41)]
        public int height_trashcan = 5;


        [Toggle("<color=#ffc600>Nuclear waste storage size:</color> <alpha=#00>----------------------------------------------------------------------------</alpha>", Order = 42)]
        public bool divider_nuclear;

        [Slider(" • Nuclear waste storage width (x)", Format = "{0:F1}", DefaultValue = 3f, Min = widthMinValue, Max = widthMaxValue, Step = step, Tooltip = tooltip + "2", Order = 43)]
        public int width_nuclear = 3;

        [Slider(" • Nuclear waste storage height (y)", Format = "{0:F1}", DefaultValue = 4f, Min = heightMinValue, Max = heightMaxValue, Step = step, Tooltip = tooltip + "2", Order = 44)]
        public int height_nuclear = 4;


        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}