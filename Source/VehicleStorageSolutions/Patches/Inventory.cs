

namespace Ramune.VehicleStorageSolutions.Patches
{
    [HarmonyPatch(typeof(Inventory))]
    public static class InventoryPatch
    {
        [HarmonyPatch(nameof(Inventory.SetUsedStorage)), HarmonyPrefix, HarmonyPriority(2)]
        public static void SetUsedStorage(IItemsContainer container)
        {
            if(container == null)
                return;

            if(container is not ItemsContainer itemsContainer)
                return;

            if(!ItemsContainerPatch.TryGetStorageTypeForContainerFromCache(itemsContainer, out var storageType))
                return;

            var config = VehicleStorageSolutions.config;

            if(storageType == ItemsContainerPatch.StorageType.Seamoth && itemsContainer.tr.parent.parent.parent.gameObject.TryGetComponent<VehicleStorageSolutions.VSSStorageModule>(out var seamothVss))
            {
                if(!seamothVss.SeamothGetters.TryGetValue(SeamothStorageInputPatches.CurrentSlotID, out var seamothGetter))
                    return;

                itemsContainer.Resize(config.width_seamoth, config.height_seamoth + seamothGetter.Invoke());
                return;
            }

            if(storageType == ItemsContainerPatch.StorageType.Exosuit && itemsContainer.tr.parent.TryGetComponent<VehicleStorageSolutions.VSSStorageModule>(out var exosuitVss))
            {
                itemsContainer.Resize(config.width_prawnSuit, config.height_prawnSuit + exosuitVss.Getters.Sum(x => x()));
                return;
            }
        }
    }
}