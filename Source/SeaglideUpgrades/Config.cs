

namespace Ramune.SeaglideUpgrades
{
    [Menu("SeaglideUpgrades")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        const string _div = "<alpha=#00>---------------------------------------------------------------------------------------------------</alpha>";
        const string _mk1 = "<color=#37B8FD>MK1</color>";
        const string _mk2 = "<color=#C6FF53>MK2</color>";
        const string _mk3 = "<color=#FE6A4D>MK3</color>";
        const string _tooltip = "Changes are applied automatically for everything except speed multipliers, you must re-equip your Seaglide to apply those.";
        const string _red = "Light Red (<color=#FFDD44>R</color>)";
        const string _green = "Light Green (<color=#FFDD44>G</color>)";
        const string _blue = "Light Blue (<color=#FFDD44>B</color>)";
        const string _range = "Light Range";
        const string _intensity = "Light Intensity";
        const string _conesize = "Light Cone Size";
        const string _multiplierFormat = "{0:F1}x";
        const string _colorFormat = "{0:F1}";
        const float _multiplierMax = 5f;
        const float _colorMax = 1f;
        const float _defaultR = 1f;
        const float _defaultG = 1f;
        const float _defaultB = 1f;
        const float _default = 1f;
        const float _step = 0.1f;
        const float _min = 0f;


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Toggle("<b>General settings:</b> <alpha=#00>---------------------------------------------------------------------------------------------------</alpha>")]
        public bool generalDivider = false;

        [Toggle($"   • Use glossy/metallic textures (better clarity)")]
        public bool glossyBool = true;

        [Slider($"   • {_mk1} Speed Multiplier", Format = _multiplierFormat, DefaultValue = _default, Min = 0.1f, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeSpeedMK1))]
        public float speedmk1 = 1f;

        [Slider($"   • {_mk2} Speed Multiplier", Format = _multiplierFormat, DefaultValue = _default, Min = 0.1f, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeSpeedMK2))]
        public float speedmk2 = 1f;

        [Slider($"   • {_mk3} Speed Multiplier", Format = _multiplierFormat, DefaultValue = _default, Min = 0.1f, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeSpeedMK3))]
        public float speedmk3 = 1f;


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Toggle(_div)]
        public bool _divider = false;

        [Toggle($"<b>Seaglide {_mk1} light settings:</b> <alpha=#00>-----------------------------------------------------------------------------------</alpha>")]
        public bool boolmk1 = true;

        [Slider($"   • {_mk1} {_red}", Format = _colorFormat, DefaultValue = 0.0f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK1))]
        public float redmk1 = 0.0f;

        [Slider($"   • {_mk1} {_green}", Format = _colorFormat, DefaultValue = 0.6f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK1))]
        public float greenmk1 = 0.6f;

        [Slider($"   • {_mk1} {_blue}", Format = _colorFormat, DefaultValue = 0.8f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK1))]
        public float bluemk1 = 0.8f;

        [Slider($"   • {_mk1} {_range}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK1))]
        public float rangemk1 = 1f;

        [Slider($"   • {_mk1} {_intensity}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK1))]
        public float intensitymk1 = 1f;

        [Slider($"   • {_mk1} {_conesize}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK1))]
        public float conesizemk1 = 1f;


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Toggle(_div)]
        public bool __divider = false;

        [Toggle($"<b>Seaglide {_mk2} light settings:</b> <alpha=#00>-----------------------------------------------------------------------------------</alpha>\"")]
        public bool boolmk2 = true;

        [Slider($"   • {_mk2} {_red}", Format = _colorFormat, DefaultValue = 0.5f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK2))]
        public float redmk2 = 0.5f;

        [Slider($"   • {_mk2} {_green}", Format = _colorFormat, DefaultValue = 0.8f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK2))]
        public float greenmk2 = 0.8f;

        [Slider($"   • {_mk2} {_blue}", Format = _colorFormat, DefaultValue = 0.3f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK2))]
        public float bluemk2 = 0.3f;

        [Slider($"   • {_mk2} {_range}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK2))]
        public float rangemk2 = 1f;

        [Slider($"   • {_mk2} {_intensity}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK2))]
        public float intensitymk2 = 1f;

        [Slider($"   • {_mk2} {_conesize}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK2))]
        public float conesizemk2 = 1f;


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [Toggle(_div)]
        public bool ___divider = false;

        [Toggle($"<b>Seaglide {_mk3} light settings:</b> <alpha=#00>-----------------------------------------------------------------------------------</alpha>\"")]
        public bool boolmk3 = true;

        [Slider($"   • {_mk3} {_red}", Format = _colorFormat, DefaultValue = 0.8f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK3))]
        public float redmk3 = 0.8f;

        [Slider($"   • {_mk3} {_green}", Format = _colorFormat, DefaultValue = 0.4f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK3))]
        public float greenmk3 = 0.4f;

        [Slider($"   • {_mk3} {_blue}", Format = _colorFormat, DefaultValue = 0.3f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK3))]
        public float bluemk3 = 0.3f;

        [Slider($"   • {_mk3} {_range}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK3))]
        public float rangemk3 = 1f;

        [Slider($"   • {_mk3} {_intensity}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK3))]
        public float intensitymk3 = 1f;

        [Slider($"   • {_mk3} {_conesize}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChangeMK3))]
        public float conesizemk3 = 1f;


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public static void OnChangeSpeedMK1() => SeaglideUpgrades.SetSeaglideSpeed(42f, 42f, SeaglideUpgrades.config.speedmk1);


        public static void OnChangeSpeedMK2() => SeaglideUpgrades.SetSeaglideSpeed(50f, 50f, SeaglideUpgrades.config.speedmk2);

        public static void OnChangeSpeedMK3() => SeaglideUpgrades.SetSeaglideSpeed(58f, 58f, SeaglideUpgrades.config.speedmk3);


        public static void OnChangeMK1() { return; }

        public static void OnChangeMK2() { return; }

        public static void OnChangeMK3() { return; }



        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}