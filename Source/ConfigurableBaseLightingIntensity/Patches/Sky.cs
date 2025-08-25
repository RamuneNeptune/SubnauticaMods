

namespace Ramune.ConfigurableBaseLightingIntensity.Patches
{
    [HarmonyPatch(typeof(mset.Sky))]
    public static class SkyPatch
    {
        [HarmonyPatch(nameof(mset.Sky.MasterIntensity), MethodType.Setter), HarmonyPrefix]
        public static void Setter(mset.Sky __instance, ref float value)
        {
            if(!BaseRootPatch.Skies.TryGetValue(__instance, out var defaultIntensity))
                return;

            value = defaultIntensity * ConfigurableBaseLightingIntensity.config.Multiplier;
        }
    }
}