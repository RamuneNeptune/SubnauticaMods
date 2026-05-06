

namespace Ramune.DropOnDestroyRedux.Patches
{
    [HarmonyPatch(typeof(Vehicle))]
    public static class VehiclePatch
    {
        [HarmonyPatch(nameof(Vehicle.OnKill)), HarmonyPrefix]
        public static void OnKill(Vehicle __instance)
        {
            var position = __instance.gameObject.transform.position;
            var upgrades = __instance.modules?.equipment;

            if(DropOnDestroyRedux.config.DropUpgrades && upgrades != null)
                foreach(var upgrade in upgrades.ToList())
                    if(upgrade.Value != null && upgrade.Value.item != null)
                        upgrade.Value.item.DropRandom(position);

            if(DropOnDestroyRedux.config.DropPowerSources)
                foreach(var source in __instance.energyInterface.sources)
                    if(source != null && source.batterySlot != null && source.batterySlot.storedItem != null)
                        source.batterySlot.storedItem.item.DropRandom(position);

            if(DropOnDestroyRedux.config.DropStorageItems)
            {
                var itemContainers = new List<IItemsContainer>();

                __instance.GetAllStorages(itemContainers);

                foreach(var container in itemContainers.ToList())
                {
                    if(container == null)
                        continue;

                    var items = new List<InventoryItem>();

                    foreach(var item in container)
                        items.Add(item);

                    foreach(var item in items)
                    {
                        if(item == null || item.item == null)
                            continue;

                        item.item.DropRandom(position);
                    }
                }
            }

            if(DropOnDestroyRedux.config.DropTorpedoes)
            {
                var torpedoContainers = new List<ItemsContainer>();

                for(int i = 0; i < __instance.slotIDs.Length; i++)
                {
                    torpedoContainers.Add(__instance.GetStorageInSlot(i, TechType.SeamothTorpedoModule));
                    torpedoContainers.Add(__instance.GetStorageInSlot(i, TechType.ExosuitTorpedoArmModule));
                }

                foreach(var container in torpedoContainers.ToList())
                {
                    if(container == null)
                        continue;

                    var items = new List<InventoryItem>();

                    foreach(var item in container)
                        items.Add(item);

                    foreach(var item in items)
                    {
                        if(item == null || item.item == null)
                            continue;

                        item.item.DropRandom(position);
                    }
                }
            }

            if(DropOnDestroyRedux.config.DropCraftingMaterials)
            {
                var vehicleTechType = CraftData.GetTechType(__instance.gameObject);

                if(vehicleTechType == TechType.None)
                    return;

                var recipeData = CraftDataHandler.GetRecipeData(vehicleTechType);

                if(recipeData == null)
                    return;

                var craftingMaterialsToDrop = new Dictionary<TechType, int>();

                foreach(var ingredient in recipeData.Ingredients)
                    craftingMaterialsToDrop[ingredient.techType] = Extensions.RoundCraftingMaterialAmount(ingredient.amount * (DropOnDestroyRedux.config.CraftingMaterialsPercentage / 100f));

                CoroutineHost.StartCoroutine(Extensions.DropCraftingMaterials(craftingMaterialsToDrop, position));
            }
        }
    }
}