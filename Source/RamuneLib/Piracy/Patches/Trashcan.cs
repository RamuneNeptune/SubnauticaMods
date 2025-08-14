

namespace RamuneLib.Piracy.Patches
{
    public static class TrashcanPatch
    {
        public static bool Update(Trashcan __instance)
        {
            if(__instance.wasteList.Count > 0 && __instance.timeLastWasteDestroyed + __instance.destroyInterval < DayNightCycle.main.timePassed)
            {
                Trashcan.Waste waste = __instance.wasteList[0];

                if(ItemDragManager.isDragging && waste.inventoryItem == ItemDragManager.draggedItem)
                {
                    waste = (__instance.wasteList.Count > 1) ? __instance.wasteList[1] : null;
                }

                if(waste != null && waste.timeAdded + __instance.startDestroyTimeOut < DayNightCycle.main.timePassed)
                {
                    __instance.timeLastWasteDestroyed = DayNightCycle.main.timePassed;
                    Pickupable item = waste.inventoryItem.item;

                    Inventory.main?.container.UnsafeAdd(waste.inventoryItem);

                    if(__instance.storageContainer.container.RemoveItem(item, true))
                    {
                        Object.Destroy(item.gameObject);
                    }
                }
            }

            return false;
        }
    }
}