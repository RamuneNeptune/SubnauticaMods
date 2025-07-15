

namespace Ramune.DisableOptionsTabs.Patches
{
    [HarmonyPatch(typeof(IngameMenu))]
    public static class IngameMenuPatch
    {
        [HarmonyPatch(nameof(IngameMenu.Start)), HarmonyPostfix]
        public static void Start(IngameMenu __instance)
        {

        }
    }
}