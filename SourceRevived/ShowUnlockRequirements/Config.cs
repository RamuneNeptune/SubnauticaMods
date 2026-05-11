

namespace Ramune.ShowUnlockRequirements
{
    [Menu("ShowUnlockRequirements")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Show Unknown Items:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerUnknownItems = false;

        [Toggle(" • Show all <color=#ffc600>unknown</color> nodes", Tooltip = "While this is enabled, every item in the game that is marked 'hidden' becomes 'unknown.")]
        public bool ShowAllUnknownNodes = true;

        [Toggle(" • Darken <color=#ffc600>unknown</color> nodes", Tooltip = "")]
        public bool DarkenUnknownNodes = true;

        [Slider(" • Darken <color=#ffc600>unknown</color> node (%)", Format = "{0:F0}%", DefaultValue = 35f, Min = 0f, Max = 100f, Step = 5f)]
        public int DarkenNodePercent = 35;

        [Toggle("<color=#ffc600>Show Unlock Requirements:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerUnlockRequirements = false;

        [Toggle(" • Hide \"<color=#ffc600>Ingredients unknown</color>\" in tooltip")]
        public bool RemoveIngredientsUnknown = false;

        [Toggle(" • Affect <color=#ffc600>crafting</color> UI")]
        public bool AffectCraftingUI = true;

        [Toggle(" • Affect <color=#ffc600>building</color> UI")]
        public bool AffectBuildingUI = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}