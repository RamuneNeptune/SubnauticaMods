

namespace Ramune.HideMaskRedux.Patches
{
    [HarmonyPatch(typeof(Player))]
    public static class PlayerPatch
    {
        [HarmonyPatch(nameof(Player.Update)), HarmonyPrefix]
        public static void Update(Player __instance)
        {
            if(Cursor.visible)
                return;

            if(GameInput.GetButtonDown(HideMaskRedux.SwitchMask))
            {
                __instance.SetScubaMaskState(Player.main.currentScubaMaskState == Player.ScubaMaskState.On ? Player.ScubaMaskState.Off : Player.ScubaMaskState.On);
            }
            else if(GameInput.GetButtonDown(HideMaskRedux.SetMaskOn))
            {
                __instance.SetScubaMaskState(Player.ScubaMaskState.On);
            }
            else if(GameInput.GetButtonDown(HideMaskRedux.SetMaskOff))
            {
                __instance.SetScubaMaskState(Player.ScubaMaskState.Off);
            }
        }


        [HarmonyPatch(nameof(Player.SetScubaMaskState)), HarmonyPostfix]
        public static void Start(Player __instance)
        {
            if(HideMaskRedux.config.HideMaskAlways)
            {
                __instance.scubaMaskModel.SetActive(false);
                __instance.currentScubaMaskState = Player.ScubaMaskState.Off;
                return;
            }

            if(HideMaskRedux.config.HideMaskUnderwater)
            {
                if(Player.main.motorMode != Player.MotorMode.Dive && Player.main.motorMode != Player.MotorMode.Seaglide)
                    return;

                __instance.scubaMaskModel.SetActive(false);
                __instance.currentScubaMaskState = Player.ScubaMaskState.Off;
            }
        }
    }
}