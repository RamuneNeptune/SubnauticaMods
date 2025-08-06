

namespace Ramune.VehicleStorageSolutions.Patches
{
    [HarmonyPatch(typeof(Vehicle))]
    public static class VehiclePatch
    {
        [HarmonyPatch(nameof(Vehicle.GetStorageInSlot)), HarmonyPrefix]
        public static bool GetStorageInSlot(Vehicle __instance, int slotID, TechType techType, ref ItemsContainer __result)
        {
            var slotItem = __instance.GetSlotItem(slotID);

            if(slotItem == null)
                return false;

            var item = slotItem.item;

            var itemTechType = item.GetTechType();

            if(!VehicleStorageSolutions.StorageDictionary.Keys.Contains(itemTechType))
                return true;

            SeamothStorageContainer component = item.GetComponent<SeamothStorageContainer>();

            if(component == null)
                return false;

            __result = component.container;

            return false;
        }
    }
}