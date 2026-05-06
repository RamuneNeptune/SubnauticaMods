

namespace Ramune.SimpleCoordinates
{
    [Menu("SimpleCoordinates")]
    public class Config : ConfigFile
    {
        public enum CoordPos
        {
            TopLeft,
            TopRight,
            TopCenter,
            LeftMiddle,
            RightMiddle,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

        [Toggle("<color=#ffc600>Display:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 0)]
        public bool DividerDisplay = false;

        [Toggle("Hide coordinates while in PDA", Order = 1)]
        public bool hideInPDA = false;

        [Toggle("Hide coordinates while in Cyclops", Order = 2)]
        public bool hideInCyclops = false;

        [Toggle("Hide coordinates while in Seamoth", Order = 3)]
        public bool hideInSeamoth = false;

        [Toggle("Hide coordinates while in Prawn Suit", Order = 4)]
        public bool hideInPrawnSuit = false;


        [Toggle("<color=#ffc600>Player coords:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 5)]
        public bool DividerSettings = false;

        [Toggle("Display player coords", Order = 6)]
        public bool display = true;

        [ColorPicker("Player coords color", Advanced = true, Order = 8), OnChange(nameof(SendChanges))]
        public Color color = Monos.CoordinateDisplay.defaultColor;

        [Choice("Player coords style", Tooltip = "Changes are applied automatically", Order = 9), OnChange(nameof(SendChanges))]
        public FontStyles fontStyle = FontStyles.Normal;

        [Slider("Player coords size", Format = "{0:F0}", DefaultValue = 23f, Min = 1f, Max = 100f, Step = 1f, Tooltip = "Changes are applied automatically", Order = 10), OnChange(nameof(SendChanges))]
        public float textSize = 23f;

        [Slider("Player coords offset (X)", Format = "{0:F0}", DefaultValue = -830f, Min = -1000f, Max = 1000f, Step = 5f, Tooltip = "Changes are applied automatically", Order = 11), OnChange(nameof(SendChanges))]
        public float textX = -830f;

        [Slider("Player coords offset (Y)", Format = "{0:F0}", DefaultValue = 510f, Min = -1000f, Max = 1000f, Step = 5f, Tooltip = "Changes are applied automatically", Order = 12), OnChange(nameof(SendChanges))]
        public float textY = 510f;


        [Toggle("<color=#ffc600>Target coords:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 13)]
        public bool DividerTarget = false;

        [Toggle("Display target coords", Order = 14)]
        public bool targetDisplay = false;

        [ColorPicker("Target coords color", Advanced = true, Order = 16), OnChange(nameof(SendChanges))]
        public Color targetColor = Monos.CoordinateDisplay.defaultColor;

        [Choice("Target coords style", Tooltip = "Changes are applied automatically", Order = 17), OnChange(nameof(SendChanges))]
        public FontStyles targetFontStyle = FontStyles.Normal;

        [Slider("Target coords size", Format = "{0:F0}", DefaultValue = 21f, Min = 1f, Max = 100f, Step = 1f, Tooltip = "Changes are applied automatically", Order = 19), OnChange(nameof(SendChanges))]
        public float targetTextSize = 21f;

        [Slider("Target coords offset (X)", Format = "{0:F0}", DefaultValue = -830f, Min = -1000f, Max = 1000f, Step = 5f, Tooltip = "Changes are applied automatically", Order = 20), OnChange(nameof(SendChanges))]
        public float targetTextX = -830f;

        [Slider("Target coords offset (Y)", Format = "{0:F0}", DefaultValue = 480f, Min = -1000f, Max = 1000f, Step = 5f, Tooltip = "Changes are applied automatically", Order = 21), OnChange(nameof(SendChanges))]
        public float targetTextY = 480f;


        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 22)]
        public bool DividerMisc = false;


        [Button("Open TargetCoordinates.json", Order = 23)]
        public void Open(ButtonClickedEventArgs _)
        {
            Process.Start(Path.Combine(Paths.ConfigurationFolder, "TargetCoordinates.json"));
        }

        [Button("Refresh target coordinates", Tooltip = "Use this after editing TargetCoordinates.json", Order = 24)]
        public void Refresh(ButtonClickedEventArgs _)
        {
            Monos.CoordinateDisplay.RefreshJson();
        }

        public void SendChanges(object sender, SliderChangedEventArgs args)
        {
            Monos.CoordinateDisplay.AdjustForConfig();
        }

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it", Order = 25)]
        public bool EnableThisMod = true;
    }
}