

namespace Ramune.RamunesWorkbench.Patches
{
    [HarmonyPatch(typeof(uGUI_CraftingMenu))]
    public static class uGUI_CraftingMenuPatches
    {
        public static CraftTree.Type CurrentCraftTreeType;
        public static Sprite[] Vanilla = [ImageUtils.GetSprite("Vanilla.TabNode"), ImageUtils.GetSprite("Vanilla.TabNodeHover")];
        public static Sprite[] Fancy = [ImageUtils.GetSprite("Fancy.TabNode"), ImageUtils.GetSprite("Fancy.TabNodeHover")];


        public static Sprite GetBackgroundSprite(int index)
        {
            return RamunesWorkbench.config.tabStyle == Config.NodeStyle.Vanilla ? Vanilla[index] : Fancy[index];
        }


        [HarmonyPatch(nameof(uGUI_CraftingMenu.Open)), HarmonyPrefix]
        public static void Open(CraftTree.Type treeType)
        {
            CurrentCraftTreeType = treeType;
        }


        [HarmonyPatch(nameof(uGUI_CraftingMenu.Close)), HarmonyPrefix]
        public static void Close()
        {
            CurrentCraftTreeType = CraftTree.Type.None;
        }


        [HarmonyPatch(nameof(uGUI_CraftingMenu.CreateIcon)), HarmonyPostfix]
        public static void CreateIcon(uGUI_CraftingMenu.Node node, RectTransform canvas, float size, float x, float y)
        {
            if(CurrentCraftTreeType != Buildables.RamunesWorkbench.craftTreeType || node == null ||  node.icon == null || node.action != TreeAction.Expand)
                return;

            node.icon.SetBackgroundSprite(GetBackgroundSprite(0));
        }


        [HarmonyPatch("uGUI_IIconManager.OnPointerEnter"), HarmonyPostfix]
        public static void OnPointerEnter(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon)
        {
            if(CurrentCraftTreeType != Buildables.RamunesWorkbench.craftTreeType || __instance.GetNode(icon) is not uGUI_CraftingMenu.Node node || node == null || node.action != TreeAction.Expand)
                return;

            node.icon.SetBackgroundSprite(GetBackgroundSprite(1));
        }


        [HarmonyPatch("uGUI_IIconManager.OnPointerExit"), HarmonyPostfix]
        public static void OnPointerExit(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon)
        {
            if(CurrentCraftTreeType != Buildables.RamunesWorkbench.craftTreeType || __instance.GetNode(icon) is not uGUI_CraftingMenu.Node node || node == null || node.action != TreeAction.Expand)
                return;

            node.icon.SetBackgroundSprite(GetBackgroundSprite(0));
        }
    }
}