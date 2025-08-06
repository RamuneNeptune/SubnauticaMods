

namespace Ramune.VehicleStorageSolutions.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    public static class SeaMothPatch
    {
        [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleChange)), HarmonyPostfix]
        public static void OnUpgradeModuleChange(SeaMoth __instance, int slotID, TechType techType, bool added)
        {
            var isModdedStorage = VehicleStorageSolutions.StorageDictionary.ContainsKey(techType);

            if(!isModdedStorage)
                return;

            if(slotID >= 0 && slotID < __instance.storageInputs.Length)
                __instance.storageInputs[slotID].SetEnabled(added && isModdedStorage);

            var vss = __instance.gameObject.EnsureComponent<VehicleStorageSolutions.VSSStorageModule>();

            if(!VehicleStorageSolutions.StorageDictionary.TryGetValue(techType, out var getter))
            {
                Logfile.Fatal("Failed to get value from StorageDictionary");
                Logfile.Fatal("Report to me on Discord: @ramuneneptune");
                return;
            }

            if(added)
                vss.SeamothGetters.Add(slotID, getter.seamothGetter);
            else
                vss.SeamothGetters.Remove(slotID);
        }
    }
}