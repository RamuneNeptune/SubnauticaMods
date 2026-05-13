

namespace Ramune.MouseCursorToggle.Patches
{
    [HarmonyPatch(typeof(uGUI_PinnedRecipes))]
    public static class uGUI_PinnedRecipesPatch
    {
        [HarmonyPatch(nameof(uGUI_PinnedRecipes.IsInteractable)), HarmonyPostfix]
        public static void IsInteractable(ref bool __result)
        {
            if(__result || !Monos.MouseCursorController.Active)
                return;

            __result = true;
        }
    }
}