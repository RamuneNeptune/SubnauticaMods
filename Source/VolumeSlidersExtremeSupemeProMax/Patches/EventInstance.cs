

namespace Ramune.VolumeSlidersExtremeSupremeProMax.Patches
{
    [HarmonyPatch(typeof(EventInstance))]
    public static class EventInstancePatch
    {
        [HarmonyPatch(nameof(EventInstance.setVolume)), HarmonyPrefix]
        public static void GetEventImpl(EventInstance __instance, ref float volume)
        {
            if(__instance.getDescription(out var desc) == RESULT.OK && desc.getPath(out var path) == RESULT.OK && uGUI_ControlsPatch.Map.TryGetValue(path, out var getter))
            {
                var intendedVolume = getter.Invoke();

                Logfile.Warning($"FMOD :: Current volume is '{volume}', expecting '{intendedVolume}'");

                if(volume != intendedVolume)
                {
                    Logfile.Fatal($"FMOD :: Forcing current volume to intended value '{intendedVolume}'");
                    volume = intendedVolume;
                }
            }
        }
    }
}