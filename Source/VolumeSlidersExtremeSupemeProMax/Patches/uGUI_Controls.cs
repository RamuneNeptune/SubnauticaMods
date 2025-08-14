

namespace Ramune.VolumeSlidersExtremeSupremeProMax.Patches
{
    [HarmonyPatch(typeof(uGUI_Controls))]
    public static class uGUI_ControlsPatch
    {
        public static Dictionary<string, float> Cache = new();


        [HarmonyPatch(nameof(uGUI_Controls.AddSlider)), HarmonyPostfix]
        public static void AddSlider(GameObject __result, string label)
        {
            if(!label.StartsWith("event"))
                return;

            if(!__result.gameObject.TryGetComponentInChildren<uGUI_SnappingSlider>(out var slider))
                return;

            if(Cache.TryGetValue(label, out var stored))
                slider.value = stored;
            else
                Cache[label] = slider.value;

            if(!Map.ContainsKey(label))
                Map[label] = () => Cache.TryGetValue(label, out var val) ? val : slider.value;

            slider.onValueChanged.AddListener(val =>
            {
                Cache[label] = val;
                OnSliderChanged(label, val);
            });
        }


        public static void OnSliderChanged(string label, float value)
        {
            Logfile.Warning($"'{label}' changed to '{value}'");
            Screen.Warning($"'{label}' changed to '{value}'");
        }


        public static Dictionary<string, Func<float>> Map = new();
    }
}