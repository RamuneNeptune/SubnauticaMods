

namespace Ramune.EarlyIonBattery
{
    [Menu("EarlyIonBattery")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 1)]
        public bool DividerCfg = false;

        [Choice(" • Ion battery unlock:", new[] { "<color=#ffcf3c><b>1/3:</b></color> QEP Data Terminal", "<color=#ffcf3c><b>2/3:</b></color> Disease Research Facility", "<color=#ffcf3c><b>3/3:</b></color> Lost River Cache Terminal" }, Order = 2)]
        public int UnlockBattery;

        [Choice(" • Ion power cell unlock:", new[] { "<color=#ffcf3c><b>1/3:</b></color> QEP Data Terminal", "<color=#ffcf3c><b>2/3:</b></color> Disease Research Facility", "<color=#ffcf3c><b>3/3:</b></color> Lost River Cache Terminal" }, Order = 3)]
        public int UnlockPowerCell;

        [Toggle("<color=#ffc600>Force learn/un-learn:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 4)]
        public bool DividerForceLearn = false;

        [Toggle(" • Ion battery: <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 4)]
        public bool TitleIonBattery = false;

        [Button("Learn", Order = 5)]
        public void LearnBattery(ButtonClickedEventArgs _) => LearnOrUnlearn(true, TechType.PrecursorIonBattery);

        [Button("Un-learn", Order = 6)]
        public void UnlearnBattery(ButtonClickedEventArgs _) => LearnOrUnlearn(false, TechType.PrecursorIonBattery);

        [Toggle(" • Ion power cell: <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 7)]
        public bool TitleIonPowerCell = false;

        [Button("Learn", Order = 8)]
        public void LearnPowerCell(ButtonClickedEventArgs _) => LearnOrUnlearn(true, TechType.PrecursorIonPowerCell);

        [Button("Un-learn", Order = 9)]
        public void UnlearnPowerCell(ButtonClickedEventArgs _) => LearnOrUnlearn(false, TechType.PrecursorIonPowerCell);

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>", Order = 10)]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it", Order = 11)]
        public bool EnableThisMod = true;


        public static void LearnOrUnlearn(bool learn, TechType techType)
        {
            if(!KnownTech.initialized)
            {
                Screen.Warning("A save file must be loaded");
                Logfile.Warning("\"KnownTech\" is not initialized yet, a save file must be loaded");
                return;
            }

            Screen.Info(learn ? $"Added \"{techType.ID()}\" to KnownTech ({KnownTech.Add(techType)})" : $"Removed \"{techType.ID()}\" from KnownTech ({KnownTech.Remove(techType)})");
            Logfile.Info(learn ? $"Added \"{techType.ID()}\" to KnownTech ({KnownTech.Add(techType)})" : $"Removed \"{techType.ID()}\" from KnownTech ({KnownTech.Remove(techType)})");
        }
    }
}