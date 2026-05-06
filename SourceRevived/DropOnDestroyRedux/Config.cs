

namespace Ramune.DropOnDestroyRedux
{
    [Menu("DropOnDestroyRedux")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Drop decoys")]
        public bool DropDecoys = true;

        [Toggle(" • Drop upgrades")]
        public bool DropUpgrades = true;

        [Toggle(" • Drop torpedoes")]
        public bool DropTorpedoes = true;

        [Toggle(" • Drop power sources")]
        public bool DropPowerSources = true;

        [Toggle(" • Drop storage items")]
        public bool DropStorageItems = true;

        [Toggle("<color=#ffc600>Materials:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCrafting = false;

        [Toggle(" • Drop crafting materials")]
        public bool DropCraftingMaterials = false;

        [Slider("• Crafting materials to drop (%)", Format = "{0:F0}%", DefaultValue = 50f, Min = 1f, Max = 100f, Step = 5f)]
        public float CraftingMaterialsPercentage = 50f;

        [Choice("• Crafting material amount rounding", ["Always <color=#ffc600>round to nearest</color>", "Always <color=#ffc600>round down</color>", "Always <color=#ffc600>round up</color>"])]
        public int CraftingMaterialAmountRounding = 0;

        [Toggle(" • Guarantee one of each crafting material")]
        public bool GuaranteeOneOfEachCraftingMaterial = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}