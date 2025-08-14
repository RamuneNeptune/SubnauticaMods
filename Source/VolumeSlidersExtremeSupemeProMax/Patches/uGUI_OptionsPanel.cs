

namespace Ramune.VolumeSlidersExtremeSupremeProMax.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class uGUI_OptionsPanelPatch
    {
        [HarmonyPatch(nameof(uGUI_OptionsPanel.AddTabs)), HarmonyPostfix]
        public static void AddTabs(uGUI_OptionsPanel __instance)
        {
            int tabIndex = __instance.AddTab("Audio");

            var lines = File.ReadAllLines(Path.Combine(Paths.ConfigurationFolder, "SN1-FMODEvents.txt"));


            Dictionary<string, List<string>> categories = new();

            foreach(string line in lines)
            {
                if(string.IsNullOrWhiteSpace(line)) continue;

                string category = GetCategory(line);

                if(!categories.ContainsKey(category))
                    categories[category] = new List<string>();

                categories[category].Add(line);
            }


            foreach(var pair in categories)
            {
                var key = pair.Key;
                var value = pair.Value;

                __instance.AddHeading(tabIndex, key);

                foreach(var v in value)
                {
                    __instance.AddSliderOption(tabIndex, v, 1f, 0f, 5f, uGUI_ControlsPatch.Cache.ContainsKey(v) ? uGUI_ControlsPatch.Cache[v] : 1f, 0.1f, null, SliderLabelMode.Float, "0.0");
                }
            }
        }


        public static string GetCategory(string path)
        {
            string trimmed = path.Contains(":") ? path.Substring(path.IndexOf(':') + 1) : path;

            string[] parts = trimmed.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            if(parts.Length < 2)
                return "Other";

            string category = parts[parts.Length - 2];

            if(string.IsNullOrEmpty(category)) 
                return category;

            return char.ToUpper(category[0]) + category.Substring(1);
        }
    }
}