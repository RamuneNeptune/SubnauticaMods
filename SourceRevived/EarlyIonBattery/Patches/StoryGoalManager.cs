

namespace Ramune.EarlyIonBattery.Patches
{
    [HarmonyPatch(typeof(StoryGoalManager))]
    public static class StoryGoalManagerPatch
    {
        public static readonly Dictionary<int, string> UnlocksWithMap = new() 
        {
            { 0, "Precursor_Gun_DataDownload1" },
            { 1, "FindPrecursorLostRiverFacility" },
            { 2, "Precursor_Cache_DataDownloadLostRiver" }
        };


        [HarmonyPatch(nameof(StoryGoalManager.OnGoalComplete)), HarmonyPostfix]
        public static void OnGoalComplete(StoryGoalManager __instance)
        {
            if(KnownTech.Contains(TechType.PrecursorIonBattery) || KnownTech.Contains(TechType.PrecursorIonPowerCell))
                return;

            var battery = UnlocksWithMap[EarlyIonBattery.config.IonBatteryUnlocksWith];
            var powerCell = UnlocksWithMap[EarlyIonBattery.config.IonPowerCellUnlocksWith];

            if(__instance.completedGoals.Contains(battery))
            {
                KnownTech.Add(TechType.PrecursorIonBattery, true);
                Logfile.Info("Unlocked: Ion battery");
                Screen.Message("<color=#09f88a>Unlocked:</color> Ion battery");
            }

            if(__instance.completedGoals.Contains(powerCell))
            {
                KnownTech.Add(TechType.PrecursorIonPowerCell, true);
                Logfile.Info("Unlocked: Ion power cell");
                Screen.Message("<color=#09f88a>Unlocked:</color> Ion power cell");
            }
        }
    }
}