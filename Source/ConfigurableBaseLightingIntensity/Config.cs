

namespace Ramune.ConfigurableBaseLightingIntensity
{
    [Menu("ConfigurableBaseLightingIntensity")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Slider("• Base lighting intensity multiplier", Format = "{0:F2}x", DefaultValue = 1f, Min = 0.01f, Max = 1f, Step = 0.01f), OnChange(nameof(OnChange))]
        public float Multiplier = 1f;

        public void OnChange(SliderChangedEventArgs _)
        {
            var toRemove = new List<mset.Sky>();

            Patches.BaseRootPatch.Skies.ForEach(x =>
            {
                if(x.Key == null)
                    toRemove.Add(x.Key);
                else
                    x.Key.masterIntensity = x.Value * Multiplier;
            });

            toRemove.ForEach(x => Patches.BaseRootPatch.Skies.Remove(x));
        }

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}