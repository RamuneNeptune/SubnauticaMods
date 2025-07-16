

namespace Ramune.InventorySpaceWarnings
{
    [Menu("InventorySpaceWarnings")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Outcrops:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerOutcrops = false;

        [Toggle(" • Warning when full", Tooltip = " ")]
        public bool OutcropsDoWarning = true;

        [Toggle(" • Prevent breaking when full", Tooltip = " ")]
        public bool OutcropsDoPreventCollection = true;

        [Toggle(" • Show indicator icon when full", Tooltip = " ")]
        public bool OutcropsDoIndicator = true;

        [Toggle("<color=#ffc600>Plants:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerPlants = false;

        [Toggle(" • Warning when full", Tooltip = " ")]
        public bool PlantsDoWarning = true;

        [Toggle(" • Prevent collecting when full", Tooltip = " ")]
        public bool PlantsDoPreventCollection = true;

        [Toggle(" • Show indicator icon when full", Tooltip = " ")]
        public bool PlantsDoIndicator = true;

        [Toggle("<color=#ffc600>Harvesting:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerHarvesting = false;

        [Toggle(" • Warning when full", Tooltip = " ")]
        public bool HarvestingDoWarning = true;

        [Toggle(" • Prevent collecting when full", Tooltip = " ")]
        public bool HarvestingDoPreventCollection = true;

        [Toggle(" • Show indicator icon when full", Tooltip = " ")]
        public bool HarvestingDoIndicator = true;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;
    }
}