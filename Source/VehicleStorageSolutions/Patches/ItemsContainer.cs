

namespace Ramune.VehicleStorageSolutions.Patches
{
    [HarmonyPatch(typeof(ItemsContainer))]
    public static class ItemsContainerPatch
    {
        public static readonly Dictionary<StorageType, List<ItemsContainer>> ItemsContainerCache = new();


        public enum StorageType
        {
            Unknown = 0,
            Seamoth,
            Exosuit,
        }


        [HarmonyPatch(MethodType.Constructor, typeof(int), typeof(int), typeof(Transform), typeof(string), typeof(FMODAsset)), HarmonyPostfix]
        public static void Constructor(ItemsContainer __instance)
        {
            if(__instance.tr == null || __instance.tr.gameObject == null)
                return;

            var storageType = GetStorageTypeForContainer(__instance);

            if(storageType == StorageType.Unknown)
                return;
            
            if(!ItemsContainerCache.TryGetValue(storageType, out var list))
            {
                list = new List<ItemsContainer>();

                ItemsContainerCache[storageType] = list;
            }

            list.AddUnique(__instance);
        }


        public static StorageType GetStorageTypeForContainer(ItemsContainer __instance)
        {
            var gameObject = __instance.tr.gameObject;

            if(gameObject.TryGetComponentInParent<SeamothStorageContainer>(out _))
                return StorageType.Seamoth;

            if(gameObject.TryGetComponentInParent<Exosuit>(out _))
                return StorageType.Exosuit;

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