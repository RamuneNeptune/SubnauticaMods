

namespace Ramune.EarlyIonBattery
{
    [Menu("EarlyIonBattery")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Choice("Ion battery unlocks with:", new[] { "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal", "<color=#ffcf3c><b>2/3 </b></color> Disease Research Facility", "<color=#ffcf3c><b>3/3 </b></color> Lost River Cache Terminal" })]
        public string unlockBatt = "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal"; //= "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal";

        [Choice("Ion power cell unlocks with:", new[] { "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal", "<color=#ffcf3c><b>2/3 </b></color> Disease Research Facility", "<color=#ffcf3c><b>3/3 </b></color> Lost River Cache Terminal" })]
        public string unlockCell = "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal"; //= "<color=#ffcf3c><b>1/3 </b></color> QEP Data Terminal";

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;

        [Button("Force <color=#ff4200>un-learn</color> Ion battery & power cell", Tooltip = "Click to un-learn the Ion battery & power cell blueprints")]
        public void Unlearn(ButtonClickedEventArgs _)
        {
            if(!Player.main)
                return;

            KnownTech.Remove(TechType.PrecursorIonBattery);
            KnownTech.Remove(TechType.PrecursorIonPowerCell);
            ErrorMessage.AddError("<color=#ff4200>Removed </color>'Ion Battery'<color=#ff3417> & </color>'Ion power cell'<color=#ff3417> from KnownTech</color>");
        }

        [Button("Force <color=#9ae86e>learn</color> Ion battery & power cell", Tooltip = "Click to learn the Ion battery & power cell blueprints")]
        public void Learn(ButtonClickedEventArgs _)
        {
            if(!Player.main)
                return;

            KnownTech.Add(TechType.PrecursorIonBattery);
            KnownTech.Add(TechType.PrecursorIonPowerCell);
            ErrorMessage.AddError("<color=#9ae86e>Added </color>'Ion battery'<color=#9ae86e> & </color>'Ion power cell'<color=#9ae86e> to KnownTech</color>");
        }
    }
}