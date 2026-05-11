

namespace Ramune.SkipLifepodIntro.Patches
{
    [HarmonyPatch(typeof(uGUI_SceneIntro))]
    public static class SceneIntroPatch
    {
        [HarmonyPatch(nameof(uGUI_SceneIntro.Play)), HarmonyPostfix]
        public static void Play(uGUI_SceneIntro __instance)
        {
            if(!__instance.showing)
                return;

            __instance.Stop(true);
        }
    }
}