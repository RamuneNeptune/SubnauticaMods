

namespace Ramune.VehicleStorageSolutions.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    public static class ExosuitPatch
    {
        [HarmonyPatch(nameof(Exosuit.OnUpgradeModuleChange)), HarmonyPrefix]
        public static void OnUpgradeModuleChange(Exosuit __instance, TechType techType, bool added)
        {
            if(!VehicleStorageSolutions.StorageDictionary.ContainsKey(techType))
                return;

            var vss = __instance.gameObject.EnsureComponent<VehicleStorageSolutions.VSSStorageModule>();

            if(!VehicleStorageSolutions.StorageDictionary.TryGetValue(techType, out var getter))
            {
                Logfile.Fatal("Failed to get value from StorageDictionary");
                Logfile.Fatal("Report to me on Discord: @ramuneneptune");
                return;
            }

            if(added)
                vss.Getters.Add(getter.seamothGetter);
            else
                vss.Getters.Remove(getter.seamothGetter);
        }
    }
}