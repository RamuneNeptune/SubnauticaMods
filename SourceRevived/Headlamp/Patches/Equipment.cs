

namespace Ramune.Headlamp.Patches
{
    [HarmonyPatch(typeof(Equipment))]
    public static class EquipmentPatches
    {
        public static readonly List<string> AllowedSlots = ["", ""];


        [HarmonyPatch(nameof(Equipment.NotifyEquip)), HarmonyPostfix]
        public static void NotifyEquip(InventoryItem item)
        {
            if(item == null || item.item == null || item.item.GetTechType() != Prefabs.Equipment.Headlamp.Prefab.Info.TechType)
                return;

            Monos.Headlamp.main.gameObject.SetActive(true);
        }


        [HarmonyPatch(nameof(Equipment.NotifyUnequip)), HarmonyPostfix]
        public static void NotifyUnequip(InventoryItem item)
        {
            if(item == null || item.item == null || item.item.GetTechType() != Prefabs.Equipment.Headlamp.Prefab.Info.TechType)
                return;

            Monos.Headlamp.main.gameObject.SetActive(false);
        }


        [HarmonyPatch(nameof(Equipment.AllowedToAdd)), HarmonyPrefix]
        public static void AllowedToAdd(string slot, Pickupable pickupable)
        {
            Logfile.Warning(slot);
            Screen.Warning(slot);

            if(!pickupable || pickupable.GetTechType() != Prefabs.Equipment.Headlamp.Prefab.Info.TechType || !AllowedSlots.Contains(slot))
                return;
        }
    }
}