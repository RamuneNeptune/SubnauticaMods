

namespace Ramune.VolumeSlidersExtremeSupremeProMax.Patches
{
    [HarmonyPatch(typeof(FMODUWE))]
    public static class FMODUWEPatch
    {
        [HarmonyPatch(nameof(FMODUWE.GetEventImpl)), HarmonyPostfix]
        public static void GetEventImpl(string eventPath, ref EventInstance __result) => ModifyVolume(eventPath, __result);


        public static void ModifyVolume(string eventPath, EventInstance eventInstance)
        {
            ErrorMessage.AddError($"<size=65%><color=#a5a7a7>{eventPath}</color></size>");

            if(!uGUI_ControlsPatch.Map.TryGetValue(eventPath, out var volumeGetter))
                return;

            if(eventInstance.getVolume(out var currentVolume) != RESULT.OK)
                return;

            var newVolume = volumeGetter.Invoke();

            if(currentVolume == newVolume)
                return;

            eventInstance.setVolume(newVolume);

            Screen.Warning($"{eventPath} :: from '{currentVolume}' to '{newVolume}'");
        }
    }
}