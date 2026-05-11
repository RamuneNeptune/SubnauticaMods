

namespace Ramune.ShowUnlockRequirements
{
    [Menu("ShowUnlockRequirements")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerConfiguration = false;

        [Toggle(" • Hide \"<color=#ffc600>Ingredients unknown</color>\" text")]
        public bool RemoveIngredientsUnknown = false;

        [Toggle(" • Force <color=#ffc600>hidden</color> nodes to be <color=#ffc600>unknown</color>", Tooltip = "While this is enabled, every item in the game that is marked 'hidden' becomes 'unknown'.\n\nThis allows almost all items which you don't have unlocked to now appear in crafting and building menus.")]
        public bool ShowAllUnknownNodes = true;

        [Slider(" • Darken <color=#ffc600>unknown</color> nodes (%)", Format = "{0:F0}%", DefaultValue = 40f, Min = 0f, Max = 100f, Step = 5f, Tooltip = "Applies a tint to unknown item nodes, so you can differentiate them from unlocked item nodes.\n\n100% is fully tinted, 0% is no tint.")]
        public int DarkenNodePercent = 40;

        [Toggle("<color=#ffc600>Affects:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerAffects = false;

        [Toggle(" • Affect <color=#ffc600>crafting</color> menus", Tooltip = "This affects: fabricators, workbenches, etc.")]
        public bool AffectCraftingUI = true;

        [Toggle(" • Affect <color=#ffc600>building</color> menus", Tooltip = "This affects: habitat builder")]
        public bool AffectBuildingUI = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}