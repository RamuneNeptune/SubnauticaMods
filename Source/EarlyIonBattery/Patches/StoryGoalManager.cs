

namespace Ramune.EarlyIonBattery.Patches
{
    [HarmonyPatch(typeof(StoryGoalManager))]
    public static class StoryGoalManagerPatch
    {
        public static readonly string[] Goals = {
            "Precursor_Gun_DataDownload1",
            "FindPrecursorLostRiverFacility",
            "Precursor_Cache_DataDownloadLostRiver"
        };


        [HarmonyPatch(nameof(StoryGoalManager.OnGoalComplete)), HarmonyPostfix]
        public static void OnGoalComplete(StoryGoalManager __instance)
        {
            if(!KnownTech.Contains(TechType.PrecursorIonBattery) && __instance.completedGoals.Contains(Goals[EarlyIonBattery.config.UnlockBattery]))
                KnownTech.Add(TechType.PrecursorIonBattery, true);

            if(!KnownTech.Contains(TechType.PrecursorIonPowerCell) && __instance.completedGoals.Contains(Goals[EarlyIonBattery.config.UnlockPowerCell]))
                KnownTech.Add(TechType.PrecursorIonPowerCell, true);
        }
    }
}