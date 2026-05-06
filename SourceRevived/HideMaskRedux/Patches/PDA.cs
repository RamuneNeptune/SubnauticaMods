

namespace Ramune.HideMaskRedux.Patches
{
    [HarmonyPatch(typeof(PDA))]
    public static class PDAPatches
    {
        public static Player.ScubaMaskState lastState;


        [HarmonyPatch(typeof(PDA), nameof(PDA.Open)), HarmonyPrefix]
        public static void Open()
        {
            if(!HideMaskRedux.config.HideMaskPDA)
                return;

            lastState = Player.main.currentScubaMaskState;
            
            if(lastState == Player.ScubaMaskState.On)
                Player.main.SetScubaMaskState(Player.ScubaMaskState.Off);
        }


        [HarmonyPatch(typeof(PDA), nameof(PDA.Close)), HarmonyPrefix]
        public static void Close()
        {
            if(!HideMaskRedux.config.HideMaskPDA)
                return;

            if(lastState == Player.ScubaMaskState.On && Player.main.motorMode == Player.MotorMode.Dive || Player.main.motorMode == Player.MotorMode.Seaglide)
                Player.main.SetScubaMaskState(Player.ScubaMaskState.On);
        }
    }
}