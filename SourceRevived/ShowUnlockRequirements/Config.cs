

namespace Ramune.ShowUnlockRequirements
{
    [Menu("ShowUnlockRequirements")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Affect <color=#ffc600>crafting</color> UI")]
        public bool AffectCraftingUI = true;

        [Toggle(" • Affect <color=#ffc600>building</color> UI")]
        public bool AffectBuildingUI = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;

        [Toggle(" • Hide \"<color=#ffc600>Ingredients unknown</color>\" text")]
        public bool RemoveIngredientsUnknown = true;
    }
}