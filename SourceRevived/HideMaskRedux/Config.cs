

namespace Ramune.HideMaskRedux
{
    [Menu("HideMaskRedux")]
    public class Config : ConfigFile
    {
        [Toggle("<color=#ffc600>Configuration:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerCfg = false;

        [Toggle(" • Hide mask always"), OnChange(nameof(OnChange))]
        public bool HideMaskAlways = true;

        [Toggle(" • Hide mask while underwater"), OnChange(nameof(OnChange))]
        public bool HideMaskUnderwater = false;

        [Toggle(" • Hide mask while in PDA"), OnChange(nameof(OnChange))]
        public bool HideMaskPDA = false;

        [Toggle("<color=#ffc600>Miscellaneous:</color> <alpha=#00>------------------------------------------------------------------------------------------------------------</alpha>")]
        public bool DividerMisc = false;

        [Toggle(" • Enable this mod", Tooltip = "Requires a restart to take effect, but allows you to disable the mod without uninstalling it")]
        public bool EnableThisMod = true;


        public void OnChange(ToggleChangedEventArgs _)
        {
            if(!Player.main)
                return;

            var shouldHide = HideMaskRedux.config.HideMaskAlways || (HideMaskRedux.config.HideMaskUnderwater && (Player.main.motorMode == Player.MotorMode.Dive || Player.main.motorMode == Player.MotorMode.Seaglide)) || (HideMaskRedux.config.HideMaskPDA && Player.main.pda && Player.main.pda.isOpen);

            Player.main.SetScubaMaskState(shouldHide ? Player.ScubaMaskState.Off : Player.ScubaMaskState.On);
        }
    }
}