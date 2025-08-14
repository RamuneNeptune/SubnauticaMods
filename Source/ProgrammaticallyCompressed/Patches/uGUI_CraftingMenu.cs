

namespace Ramune.ProgrammaticallyCompressed.Patches
{
    [HarmonyPatch(typeof(uGUI_CraftingMenu))]
    public static class uGUI_CraftingMenuPatch
    {
        public static CraftTree.Type CurrentCraftTreeType;

        public static Sprite Tab1 = ImageUtils.GetSprite("Tab1");

        public static Sprite TabHover1 = ImageUtils.GetSprite("Tab2");

        public static Sprite Tab2 = ImageUtils.GetSprite("Tab3");

        public static Sprite TabHover2 = ImageUtils.GetSprite("Tab4");

        public static Sprite Tab3 = ImageUtils.GetSprite("Tab5");

        public static Sprite TabHover3 = ImageUtils.GetSprite("Tab6");

        public static uGUI_CraftingMenu.Node CurrentNode;


        [HarmonyPatch(nameof(uGUI_CraftingMenu.Open)), HarmonyPrefix]
        public static void Open(CraftTree.Type treeType) => CurrentCraftTreeType = treeType;


        [HarmonyPatch(nameof(uGUI_CraftingMenu.Close)), HarmonyPrefix]
        public static void Close() => CurrentCraftTreeType = CraftTree.Type.None;


        [HarmonyPatch(nameof(uGUI_CraftingMenu.CreateIcon)), HarmonyPostfix]
        public static void CreateIcon(uGUI_CraftingMenu __instance, uGUI_CraftingMenu.Node node) => Modify(__instance, node);


        [HarmonyPatch("uGUI_IIconManager.OnPointerEnter"), HarmonyPostfix]
        public static void OnPointerEnter(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon) => Modify(__instance, __instance.GetNode(icon), true);


        [HarmonyPatch("uGUI_IIconManager.OnPointerExit"), HarmonyPostfix]
        public static void OnPointerExit(uGUI_CraftingMenu __instance, uGUI_ItemIcon icon) => Modify(__instance, __instance.GetNode(icon));


        [HarmonyPatch(typeof(uGUI_InputGroup), nameof(uGUI_InputGroup.Update)), HarmonyPostfix]
        public static void Update(uGUI_CraftingMenu __instance)
        {
            if(__instance is not uGUI_CraftingMenu craftingMenu)
                return;

            if(CurrentNode == null)
                return;

            Modify(craftingMenu, CurrentNode, true, Input.GetKey(ProgrammaticallyCompressed.config.DecompressKey));
        }


        public static void Modify(uGUI_CraftingMenu __instance, uGUI_CraftingMenu.Node node, bool hover = false, bool keyDown = false)
        {
            if(CurrentCraftTreeType != ProgrammaticallyCompressed.craftTreeType)
                return;

            if(node is null)
                return;

            if(node.icon is null)
                return;

            if(node.action == TreeAction.Craft)
            {
                node.techType = keyDown ? (ProgrammaticallyCompressed.TechTypeMap.TryGetValue(node.techType, out var decompressed) ? decompressed : node.techType) : (ProgrammaticallyCompressed.TechTypeMapReversed.TryGetValue(node.techType, out var compressed) ? compressed : node.techType);
                
                CurrentNode = hover ? node : null;

                node.icon.SetBackgroundSprite(keyDown ? (hover ? TabHover2 : Tab2) : (hover ? TabHover3 : Tab3));

                return;
            }

            if(node.action != TreeAction.Expand)
                return;
            
            node.icon.SetBackgroundSprite(hover ? TabHover1 : Tab1);
        }
    }
}