

namespace Ramune.ShowUnknownItems.Patches
{
    [HarmonyPatch(typeof(uGUI_CraftingMenu))]
    public static class uGUI_CraftingMenuPatch
    {
        public static readonly Color LockedTint = new(0.75f, 0.75f, 0.75f, 1f);

        [HarmonyPatch(nameof(uGUI_CraftingMenu.UpdateNotification)), HarmonyPostfix]
        public static void UpdateNotification(uGUI_CraftingMenu.Node node)
        {
            if(!ShowUnknownItems.config.TintNodes || node == null || node.action != TreeAction.Craft || node.icon == null)
                return;

            var color = CrafterLogic.IsCraftRecipeUnlocked(node.techType) ? Color.white : LockedTint;

            if(node.icon.foreground != null)
                node.icon.foreground.color = color;

            if(node.icon.background != null)
                node.icon.background.color = color;
        }
    }
}