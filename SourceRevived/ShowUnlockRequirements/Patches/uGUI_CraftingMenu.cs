

namespace Ramune.ShowUnlockRequirements.Patches
{
    [HarmonyPatch(typeof(uGUI_CraftingMenu))]
    public static class uGUI_CraftingMenuPatch
    {
        [HarmonyPatch(nameof(uGUI_CraftingMenu.UpdateNotification)), HarmonyPostfix]
        public static void UpdateNotification(uGUI_CraftingMenu.Node node)
        {
            if(node == null || node.action != TreeAction.Craft || node.icon == null)
                return;

            var tint = 1f - ShowUnlockRequirements.config.DarkenNodePercent / 100f;

            var color = CrafterLogic.IsCraftRecipeUnlocked(node.techType) ? Color.white : new Color(tint, tint, tint);

            if(node.icon.foreground != null)
                node.icon.foreground.color = color;

            if(node.icon.background != null)
                node.icon.background.color = color;
        }
    }
}