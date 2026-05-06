

namespace Ramune.EarlyIonBattery.Patches
{
    [HarmonyPatch(typeof(StoryGoalManager))]
    public static class StoryGoalManagerPatch
    {
        public const string opt1 = "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal";

        public const string opt2 = "<color=#ffcf3c><b>2/3 </b></color> Disease Research Facility";

        public const string opt3 = "<color=#ffcf3c><b>3/3 </b></color> Lost River Cache Terminal";

        public static string battery;

        public static string powercell;


        [HarmonyPatch(nameof(StoryGoalManager.OnGoalComplete)), HarmonyPostfix]
        public static void OnGoalComplete(StoryGoalManager __instance)
        {
            if(KnownTech.Contains(TechType.PrecursorIonBattery) || KnownTech.Contains(TechType.PrecursorIonPowerCell))
                return;

            switch(EarlyIonBattery.config.unlockBatt)
            {
                case opt1:
                    battery = "Precursor_Gun_DataDownload1";
                    break;
                case opt2:
                    battery = "FindPrecursorLostRiverFacility";
                    break;
                case opt3:
                    battery = "Precursor_Cache_DataDownloadLostRiver";
                    break;
            }

            switch(EarlyIonBattery.config.unlockCell)
            {
                case opt1:
                    powercell = "Precursor_Gun_DataDownload1";
                    break;
                case opt2:
                    powercell = "FindPrecursorLostRiverFacility";
                    break;
                case opt3:
                    powercell = "Precursor_Cache_DataDownloadLostRiver";
                    break;
            }

            if(__instance.completedGoals.Contains(battery))
            {
                KnownTech.Add(TechType.PrecursorIonBattery, true);
                Logfile.Info("Unlocked: Ion battery");
                Screen.Message("<color=#09f88a>Unlocked:</color> Ion battery");
            }

            if(__instance.completedGoals.Contains(powercell))
            {
                KnownTech.Add(TechType.PrecursorIonPowerCell, true);
                Logfile.Info("Unlocked: Ion power cell");
                Screen.Message("<color=#09f88a>Unlocked:</color> Ion power cell");
            }
        }
    }
}