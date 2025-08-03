

namespace Ramune.RamunesCustomizedStorage.Patches
{
    [HarmonyPatch(typeof(ItemsContainer))]
    public static class ItemsContainerPatches
    {
        public static readonly Dictionary<StorageType, List<ItemsContainer>> ItemsContainerCache = new();


        public enum StorageType
        {
            Unknown = 0,
            Inventory,
            Seamoth,
            Exosuit,
            CyclopsLocker, // doesnt
            WallLocker,
            StandingLocker,
            LifepodLocker,
            WaterproofLocker,
            CarryAll,
            BioReactor,
            WaterFiltration,
            NuclearWaste,
            TrashCan
        }


        [HarmonyPatch(MethodType.Constructor, typeof(int), typeof(int), typeof(Transform), typeof(string), typeof(FMODAsset)), HarmonyPostfix]
        public static void Constructor(ItemsContainer __instance)
        {
            if(__instance.tr == null || __instance.tr.gameObject == null)
                return;

            var storageType = GetStorageTypeForContainer(__instance);

            if(storageType == StorageType.Unknown)
            {
                Logfile.Debug($"Could not identify correct StorageType to use for GameObject:\n > transform.name: {__instance.tr?.name}\n > ItemsContainer._label: {__instance._label}");

                return;
            }
            
            if(!ItemsContainerCache.TryGetValue(storageType, out var list))
            {
                list = new List<ItemsContainer>();

                ItemsContainerCache[storageType] = list;
            }

            list.AddUnique(__instance);

            Logfile.Debug($"Added item to {storageType} (count: {list.Count}): {__instance.tr?.name} (ItemsContainer._label: {__instance._label})");
        }


        public static StorageType GetStorageTypeForContainer(ItemsContainer __instance)
        {
            var gameObject = __instance.tr.gameObject;

            var label = __instance._label.ToLower();

            if(label.Equals("inventorylabel"))
                return StorageType.Inventory;

            if(gameObject.TryGetComponentInParent<SeamothStorageContainer>(out _))
                return StorageType.Seamoth;

            if(gameObject.TryGetComponentInParent<Exosuit>(out _))
                return StorageType.Exosuit;

            if(gameObject.TryGetComponentInParent<StorageContainer>(out var storageContainer))
            {
                var name = storageContainer.gameObject.name.ToLower();

                if(name.StartsWith("locker"))
                    return StorageType.StandingLocker;

                else if(name.StartsWith("smalllocker"))
                    return StorageType.WallLocker;
            }

            if(label.Equals("escapepodstoragelabel"))
                return StorageType.LifepodLocker;

            if(gameObject.TryGetComponent<SmallStorage>(out _))
                return StorageType.WaterproofLocker;

            if(gameObject.transform.root.gameObject.TryGetComponentInChildren<PickupableStorage>(out _))
                return StorageType.CarryAll;

            if(label.Equals("basebioreactorstoragelabel"))
                return StorageType.BioReactor;

            if(label.Equals("filtrationmachinestoragelabel"))
                return StorageType.WaterFiltration;

            if(gameObject.TryGetComponentInParent<Trashcan>(out var trashcan))
                return trashcan.biohazard ? StorageType.NuclearWaste : StorageType.TrashCan;

            if(gameObject.transform.parent.gameObject.TryGetComponentInParent<SubRoot>(out _))
                return StorageType.CyclopsLocker;

            return StorageType.Unknown;
        }


        public static bool TryGetStorageTypeForContainerFromCache(ItemsContainer container, out StorageType storageType)
        {
            foreach(var kvp in ItemsContainerCache)
            {
                var list = kvp.Value;

                if(list == null || list.Count == 0)
                    continue;

                list.RemoveAll(item => item == null);

                if(list.Contains(container))
                {
                    storageType = kvp.Key;

                    return true;
                }
            }

            storageType = StorageType.Unknown;

            return false;
        }
    }
}