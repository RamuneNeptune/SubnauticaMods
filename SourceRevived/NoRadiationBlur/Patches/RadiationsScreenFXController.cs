

namespace Ramune.NoRadiationBlur.Patches
{
    [HarmonyPatch(typeof(RadiationsScreenFXController))]
    public static class RadiationsScreenFXControllerPatch
    {
        [HarmonyPatch(nameof(RadiationsScreenFXController.Start)), HarmonyPostfix]
        public static void Start(RadiationsScreenFXController __instance)
        {
            __instance.minRadiation = 0f;
            __instance.radiationMultiplier = 0f;
        }
    }
}