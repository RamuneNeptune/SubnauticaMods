

namespace Ramune.Headlamp
{
    [Menu("Headlamp")]
    public class Config : ConfigFile
    {
        const string _tooltip = "Changes are applied immediately.";
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
        const float _default = 1f;
        const float _step = 0.1f;
        const float _min = 0f;


        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider($" • Headlamp {_red}", Format = _colorFormat, DefaultValue = 0.0f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChange))]
        public float Red = 0.0f;

        [Slider($" • Headlamp {_green}", Format = _colorFormat, DefaultValue = 0.6f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChange))]
        public float Green = 0.6f;

        [Slider($" • Headlamp {_blue}", Format = _colorFormat, DefaultValue = 0.8f, Min = _min, Max = _colorMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChange))]
        public float Blue = 0.8f;

        [Slider($" • Headlamp {_range}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChange))]
        public float Range = 1f;

        [Slider($" • Headlamp {_intensity}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChange))]
        public float Intensity = 1f;

        [Slider($" • Headlamp {_conesize}", Format = _multiplierFormat, DefaultValue = _default, Min = _min, Max = _multiplierMax, Step = _step, Tooltip = _tooltip), OnChange(nameof(OnChange))]
        public float Conesize = 1f;

        [Toggle($" • Headlamp Rainbow Mode"), OnChange(nameof(OnChangeRainbow))]
        public bool Rainbow = false;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;


        public void OnChange(SliderChangedEventArgs _) => Monos.Headlamp.main?.Refresh();


        public void OnChangeRainbow(ToggleChangedEventArgs _)
        {
            Monos.Headlamp.main.doRainbow = false;
            Monos.Headlamp.main.Refresh();
        }
    }
}