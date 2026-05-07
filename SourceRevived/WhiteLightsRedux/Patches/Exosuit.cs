

namespace Ramune.WhiteLightsRedux.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    public static class ExosuitPatch
    {
        [HarmonyPatch(nameof(Exosuit.Start)), HarmonyPrefix]
        public static void Start(Exosuit __instance)
        {
            if(!WhiteLightsRedux.config.AffectExosuit || !__instance.gameObject.TryGetComponentsInChildren<Light>(out var lights, true))
                return;

            lights.ForEach(x => x.color = Color.white);
        }
    }
}