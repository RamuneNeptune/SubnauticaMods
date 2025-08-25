

namespace Ramune.ConfigurableBaseLightingIntensity.Patches
{
    [HarmonyPatch(typeof(BaseRoot))]
    public static class BaseRootPatch
    {
        public static Dictionary<mset.Sky, float> Skies = new();


        [HarmonyPatch(nameof(BaseRoot.Start)), HarmonyPostfix]
        public static void Start(BaseRoot __instance)
        {
            if(!__instance.gameObject.TryGetComponentInChildren<mset.Sky>(out var sky))
                return;

            if(sky != null && !Skies.ContainsKey(sky))
                Skies.Add(sky, sky.MasterIntensity);
        }
    }
}