

namespace Ramune.HideMaskInPDA.Patches
{
    [HarmonyPatch(typeof(PDA))]
    public static class PDAPatches
    {
        [HarmonyPatch(typeof(PDA), nameof(PDA.Open)), HarmonyPostfix]
        public static void Open() => Player.main.SetScubaMaskState(Player.ScubaMaskState.Off);


        [HarmonyPatch(typeof(PDA), nameof(PDA.Close)), HarmonyPostfix]
        public static void Close() => Player.main.SetScubaMaskState(Player.ScubaMaskState.On);
    }
}