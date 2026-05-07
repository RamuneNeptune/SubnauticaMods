

namespace Ramune.KeepInventoryRedux.Patches
{
    [HarmonyPatch(typeof(Inventory))]
    public static class InventoryPatch
    {
        [HarmonyPatch(nameof(Inventory.LoseItems)), HarmonyPrefix]
        public static bool LoseItems(Inventory __instance, ref bool __result)
        {
            __result = false;

            RefreshLists();

            var quickSlotItems = new List<InventoryItem>();

            for(int i = 0; i < __instance.quickSlots.slotCount; i++)
                if(__instance.quickSlots.binding[i] != null)
                    quickSlotItems.Add(__instance.quickSlots.binding[i]);

            var itemsToLose = new List<InventoryItem>();

            foreach(var item in __instance.container)
                if(item.item.destroyOnDeath && !item.ShouldKeep(quickSlotItems.Contains(item), false))
                    itemsToLose.Add(item);

            foreach(var item in (IItemsContainer)__instance.equipment)
                if(item.item.destroyOnDeath && !item.ShouldKeep(false, true))
                    itemsToLose.Add(item);

            if(itemsToLose.Count > 0)
                foreach(var itemToLose in itemsToLose)
                    if(__instance.InternalDropItem(itemToLose.item, false))
                        __result = true;

            return false;
        }


        public static readonly HashSet<TechType> Whitelist = [];


        public static readonly HashSet<TechType> Blacklist = [];


        public static bool IsWhitelisted(this TechType techType) => Whitelist.Contains(techType);


        public static bool IsBlacklisted(this TechType techType) => Blacklist.Contains(techType);


        public static void RefreshLists()
        {
            Whitelist.Clear();
            Blacklist.Clear();

            RefreshList(Path.Combine(Paths.ConfigurationFolder, "Whitelist.json"), Whitelist);
            RefreshList(Path.Combine(Paths.ConfigurationFolder, "Blacklist.json"), Blacklist);
        }


        public static void RefreshList(string path, HashSet<TechType> hashSet)
        {
            if(!File.Exists(path))
                return;

            var entries = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(path));

            if(entries == null)
                return;

            foreach(var entry in entries)
            {
                if(string.IsNullOrWhiteSpace(entry))
                    continue;

                var trimmedEntry = entry.Trim();

                if(Enum.TryParse(trimmedEntry, true, out TechType techType))
                {
                    hashSet.Add(techType);
                    continue;
                }

                if(EnumHandler.TryGetValue(trimmedEntry, out techType))
                    hashSet.Add(techType);
            }
        }


        public static bool ShouldKeep(this InventoryItem item, bool isHotbar, bool isEquipped)
        {
            var techType = item.item.GetTechType();

            if(KeepInventoryRedux.config.UseBlacklist && techType.IsBlacklisted())
                return false;

            if(KeepInventoryRedux.config.KeepEverything)
                return true;

            if(KeepInventoryRedux.config.UseWhitelist)
            {
                bool isWhitelisted = techType.IsWhitelisted();

                switch(KeepInventoryRedux.config.WhitelistBehaviour)
                {
                    case 0:
                        return isWhitelisted;

                    case 1:
                        return isWhitelisted || isHotbar || isEquipped;

                    case 2:
                        return isWhitelisted || isHotbar;

                    case 3:
                        return isWhitelisted || isEquipped;
                }
            }

            if(isEquipped && KeepInventoryRedux.config.KeepEquipped)
                return true;

            if(isHotbar && KeepInventoryRedux.config.KeepHotbar)
                return true;

            return false;
        }
    }
}