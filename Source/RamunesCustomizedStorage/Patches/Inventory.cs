

namespace Ramune.RamunesCustomizedStorage.Patches
{
    [HarmonyPatch(typeof(Inventory))]
    public static class InventoryPatch
    {
        [HarmonyPatch(nameof(Inventory.SetUsedStorage)), HarmonyPrefix]
        public static void SetUsedStorage(IItemsContainer container)
        {
            //Screen.Debug((container?.GetType().Name ?? "null") + $" (label: {container.label})");

            var config = RamunesCustomizedStorage.config;

            if(container == null)
            {
                //Logfile.Warning("Inventory.SetUsedStorage called with null container");
                return;
            }

            if(container is not ItemsContainer itemsContainer)
            { 
                //Logfile.Warning($"Inventory.SetUsedStorage called with non-ItemsContainer: {container.GetType().Name}");
                return;
            }

            if(!ItemsContainerPatch.TryGetStorageTypeForContainerFromCache(itemsContainer, out var storageType))
            {
                //Logfile.Warning($"Could not identify StorageType for: \"{itemsContainer.tr?.name}\" (\"{itemsContainer._label}\")");
                return;
            }

            switch(storageType)
            {
                case ItemsContainerPatch.StorageType.Seamoth:
                    //if(!itemsContainer.tr.root.gameObject.TryGetComponentInChildren<SeaMoth>(out var seamoth))
                        //return;

                    itemsContainer.Resize(config.width_seamoth, config.height_seamoth);
                    //Screen.Info("Resized Seamoth storage to: " + config.width_seamoth + "x" + (seamothModuleCount > 0 ? config.height_seamoth + config.height_seamothModule * seamothModuleCount : config.height_seamoth));
                    break;

                case ItemsContainerPatch.StorageType.Exosuit:
                    if(!itemsContainer.tr.gameObject.TryGetComponentInParent<Exosuit>(out var exosuit))
                        return;

                    int exosuitModuleCount = exosuit.modules.GetCount(TechType.VehicleStorageModule);
                    itemsContainer.Resize(config.width_prawnSuit, exosuitModuleCount > 0 ? config.height_prawnSuit + config.height_prawnSuitModule * exosuitModuleCount : config.height_prawnSuit);
                    //Screen.Info("Resized Prawn Suit storage to: " + config.width_prawnSuit + "x" + (exosuitModuleCount > 0 ? config.height_prawnSuit + config.height_prawnSuitModule * exosuitModuleCount : config.height_prawnSuit));
                    break;

                case ItemsContainerPatch.StorageType.CyclopsLocker:
                    itemsContainer.Resize(config.width_locker, config.height_locker);
                    //Screen.Info("Resized Cyclops Locker storage to: " + config.width_locker + "x" + config.height_locker);
                    break;

                case ItemsContainerPatch.StorageType.WallLocker:
                    itemsContainer.Resize(config.width_wallLocker, config.height_wallLocker);
                    //Screen.Info("Resized Wall Locker storage to: " + config.width_wallLocker + "x" + config.height_wallLocker);
                    break;

                case ItemsContainerPatch.StorageType.StandingLocker:
                    itemsContainer.Resize(config.width_locker, config.height_locker);
                    //Screen.Info("Resized Standing Locker storage to: " + config.width_locker + "x" + config.height_locker);
                    break;

                case ItemsContainerPatch.StorageType.LifepodLocker:
                    itemsContainer.Resize(config.width_lifepod, config.height_lifepod);
                    //Screen.Info("Resized Lifepod Locker storage to: " + config.width_lifepod + "x" + config.height_lifepod);
                    break;

                case ItemsContainerPatch.StorageType.WaterproofLocker:
                    itemsContainer.Resize(config.width_waterproofLocker, config.height_waterproofLocker);
                    //Screen.Info("Resized Waterproof Locker storage to: " + config.width_waterproofLocker + "x" + config.height_waterproofLocker);
                    break;

                case ItemsContainerPatch.StorageType.CarryAll:
                    itemsContainer.Resize(config.width_carryAll, config.height_carryAll);
                    //Screen.Info("Resized Carry All storage to: " + config.width_carryAll + "x" + config.height_carryAll);
                    break;

                case ItemsContainerPatch.StorageType.BioReactor:
                    itemsContainer.Resize(config.width_bioReactor, config.height_bioReactor);
                    //Screen.Info("Resized Bio Reactor storage to: " + config.width_bioReactor + "x" + config.height_bioReactor);
                    break;

                case ItemsContainerPatch.StorageType.WaterFiltration:
                    if(!itemsContainer.tr.gameObject.TryGetComponentInParent<FiltrationMachine>(out var filtration))
                        return;
                    filtration.maxSalt = config.salt_filtration;
                    filtration.maxWater = config.water_filtration;
                    itemsContainer.Resize(config.width_filtration, config.height_filtration);
                    //Screen.Info("Resized Water Filtration storage to: " + config.width_filtration + "x" + config.height_filtration);
                    break;

                case ItemsContainerPatch.StorageType.NuclearWaste:
                    itemsContainer.Resize(config.width_nuclear, config.height_nuclear);
                    //Screen.Info("Resized Nuclear Waste storage to: " + config.width_locker + "x" + config.height_locker);
                    break;

                case ItemsContainerPatch.StorageType.TrashCan:
                    itemsContainer.Resize(config.width_trashcan, config.height_trashcan);
                    //Screen.Info("Resized Trash Can storage to: " + config.width_locker + "x" + config.height_locker);
                    break;
            }
        }
    }
}