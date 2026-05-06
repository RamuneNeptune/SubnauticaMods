

namespace Ramune.DropOnDestroyRedux.Patches
{
    [HarmonyPatch(typeof(SubRoot))]
    public static class SubRootPatch
    {
        [HarmonyPatch(nameof(SubRoot.OnKill)), HarmonyPrefix]
        public static void OnKill(SubRoot __instance)
        {
            var position = __instance.gameObject.transform.position;
            var hasConsoles = __instance.gameObject.TryGetComponentsInChildren<UpgradeConsole>(out var consoles);

            if(DropOnDestroyRedux.config.DropUpgrades && hasConsoles)
                foreach(var console in consoles)
                    if(console != null && console.modules != null && console.modules.equipment != null)
                        foreach(var upgrade in console.modules.equipment.ToList())
                            if(upgrade.Value != null && upgrade.Value.item != null)
                                upgrade.Value.item.DropRandom(position);

            if(DropOnDestroyRedux.config.DropPowerSources)
                foreach(var source in __instance.GetComponentsInChildren<BatterySource>(true))
                    if(source != null && source.batterySlot != null && source.batterySlot.storedItem != null)
                        source.batterySlot.storedItem.item.DropRandom(position);

            if(DropOnDestroyRedux.config.DropStorageItems)
            {
                foreach(var storageContainer in __instance.GetComponentsInChildren<StorageContainer>(true))
                {
                    var container = storageContainer?.container;

                    if(container == null)
                        continue;

                    foreach(var item in container.ToList())
                    {
                        if(item == null || item.item == null)
                            continue;

                        item.item.DropRandom(position);
                    }
                }
            }

            if(DropOnDestroyRedux.config.DropDecoys)
            {
                foreach(var decoyTube in __instance.GetComponentsInChildren<CyclopsDecoyLoadingTube>(true))
                {
                    if(decoyTube?.decoySlots == null)
                        continue;

                    foreach(var decoy in decoyTube.decoySlots.equipment.ToList())
                    {
                        if(decoy.Value == null || decoy.Value.item == null)
                            continue;

                        decoy.Value.item.DropRandom(position);
                    }
                }
            }

            if(DropOnDestroyRedux.config.DropCraftingMaterials)
            {
                var subRootTechType = CraftData.GetTechType(__instance.gameObject);

                if(subRootTechType == TechType.None)
                    return;

                var recipeData = CraftDataHandler.GetRecipeData(subRootTechType);

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