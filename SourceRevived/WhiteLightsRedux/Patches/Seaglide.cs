

namespace Ramune.WhiteLightsRedux.Patches
{
    [HarmonyPatch(typeof(Seaglide))]
    public static class SeaglidePatch
    {
        [HarmonyPatch(nameof(Seaglide.Start)), HarmonyPostfix]
        public static void Start(Seaglide __instance)
        {
            if(!WhiteLightsRedux.config.AffectSeaglide || !__instance.toggleLights.lightsParent.TryGetComponentsInChildren<Light>(out var lights, true))
                return;

            lights.ForEach(x => x.color = Color.white);
        }
    }
}