

namespace Ramune.EquippableStorage.Patches
{
    [HarmonyPatch(typeof(uGUI_Equipment))]
    public static class uGUI_EquipmentPatches
    {
        public static int Slots = 0;

        [HarmonyPatch(nameof(uGUI_Equipment.Awake)), HarmonyPrefix]
        public static void Awake(uGUI_Equipment __instance)
        {
            CreateEquippableStorageSlot(__instance.transform.Find("Gloves"), localPosition:new(130, -230, 0), sprite:ImageUtils.GetSprite("EquippableStorageSlot2"), spriteColor:new(0.46f, 0.667f, 0.8f, 0.354f));
            
            CreateEquippableStorageSlot(__instance.transform.Find("Gloves"), localPosition:new(130, -330, 0), sprite:ImageUtils.GetSprite("EquippableStorageSlot2"), spriteColor:new(1f, 0f, 0f, 0.354f));

            CreateEquippableStorageSlot(__instance.transform.Find("Gloves"), localPosition:new(30, -330, 0), sprite:ImageUtils.GetSprite("EquippableStorageSlot2"), spriteColor:new(0f, 1f, 0f, 0.354f));

            /*
            var gloves = __instance.transform.Find("Gloves");

            var equippableStorage = Object.Instantiate(gloves, gloves.parent);

            equippableStorage.name = "EquippableStorage";

            var slot = equippableStorage.GetComponentInChildren<uGUI_EquipmentSlot>();

            slot.slot = "EquippableStorage";
            slot.transform.localPosition = new Vector3(130, -228, 0);
            var image = slot.transform.Find("Hint").GetComponent<Image>();
            image.sprite = ImageUtils.GetUnitySprite("EquippableStorageSlot2");
            image.color = new(0.46f, 0.667f, 0.8f, 0.354f);
            */
        }


        public static void CreateEquippableStorageSlot(Transform gloves, Vector3 localPosition, Sprite sprite, Color spriteColor)
        {
            Slots++;

            var eqn = "EquippableStorage" + Slots;

            var equippableStorage = Object.Instantiate(gloves, gloves.parent);
            equippableStorage.name = eqn;

            var slot = equippableStorage.GetComponentInChildren<uGUI_EquipmentSlot>();
            slot.slot = eqn;
            slot.transform.localPosition = localPosition;

            var image = slot.transform.Find("Hint").GetComponent<Image>();
            image.sprite = sprite;
            image.color = spriteColor;
        }


        [HarmonyPatch(nameof(uGUI_Equipment.OnEquip)), HarmonyPrefix]
        public static void OnEquip(uGUI_Equipment __instance, string slot, InventoryItem item)
        {
            var itemId = item.techType.ToString();

            if(!EquippableStorage.IDs.ContainsKey(itemId))
                return;
            
            if(EquippableStorage.ShouldPatchCompatibility && EquippableStorage.IDs.TryGetValue(itemId, out var index))
            {
                ModMessageSystem.SendGlobal("RamunesCustomizedStorage", "EquippableStorage", new Vector2(0, int.Parse($"eq.backpack.{index}.heightToAdd".LangKey())), false);
                return;
            }
        }


        [HarmonyPatch(nameof(uGUI_Equipment.OnUnequip)), HarmonyPrefix]
        public static void OnUnequip(uGUI_Equipment __instance, string slot, InventoryItem item)
        {
            var itemId = item.techType.ToString();

            if(!EquippableStorage.IDs.ContainsKey(itemId))
                return;

            if(EquippableStorage.ShouldPatchCompatibility && EquippableStorage.IDs.TryGetValue(itemId, out var index))
            {
                ModMessageSystem.SendGlobal("RamunesCustomizedStorage", "EquippableStorage", new Vector2(0, BackpackHeight(index)), true);
                return;
            }
        }


        public static int BackpackHeight(int i)
        {
            return i switch
            {
                1 => EquippableStorage.config.backpack1_height,
                2 => EquippableStorage.config.backpack2_height,
                3 => EquippableStorage.config.backpack3_height,
                4 => EquippableStorage.config.backpack4_height,
                5 => EquippableStorage.config.backpack5_height,
                _ => int.Parse($"eq.backpack.{i}.heightToAdd".LangKey()),
            };
        }
    }
}