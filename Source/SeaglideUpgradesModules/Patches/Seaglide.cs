

namespace Ramune.SeaglideUpgradesModules.Patches
{
    [HarmonyPatch(typeof(Seaglide))]
    public static class SeaglidePatch
    {
        public static Seaglide ActiveSeaglide;


        [HarmonyPatch(nameof(Seaglide.OnDraw)), HarmonyPrefix]
        public static void OnDraw(Seaglide __instance) => ActiveSeaglide = __instance;
    }
}