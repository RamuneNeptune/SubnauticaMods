

namespace Ramune.SimpleCoordinates.Patches
{
    [HarmonyPatch(typeof(Player))]
    public static class PlayerPatch
    {
        [HarmonyPatch(nameof(Player.Start)), HarmonyPostfix]
        public static void Start(Player __instance)
        {

        }
    }
}