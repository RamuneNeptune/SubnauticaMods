

namespace Ramune.WhiteLightsRedux.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    public static class SeaMothPatch
    {
        [HarmonyPatch(nameof(SeaMoth.Start)), HarmonyPrefix]
        public static void Start(SeaMoth __instance)
        {
            if(!WhiteLightsRedux.config.AffectSeamoth || !__instance.toggleLights.lightsParent.TryGetComponentsInChildren<Light>(out var lights, true))
                return;

            lights.ForEach(x => x.color = Color.white);
        }
    }
}