

namespace Ramune.DisableOptionsTabs.Patches
{
    [HarmonyPatch(typeof(GameInput))]
    public static class GameInputPatch
    {
        [HarmonyPatch(nameof(GameInput.PopulateSettings)), HarmonyPrefix]
        public static bool PopulateSettings() => !DisableOptionsTabs.config.DisableInput;
    }
}