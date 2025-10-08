

namespace Ramune.SeaglideUpgrades.Patches
{
    [HarmonyPatch(typeof(PlayerTool))]
    public static class PlayerToolPatch
    {
        [HarmonyPatch(nameof(PlayerTool.OnDraw)), HarmonyPostfix]
        public static void OnDraw(PlayerTool __instance)
        {

        }
    }
}