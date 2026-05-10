

namespace Ramune.SeaglideUpgradesModules.Patches
{
    [HarmonyPatch(typeof(Seaglide))]
    public static class SeaglidePatch
    {
        [HarmonyPatch(nameof(Seaglide.GetCustomUseText)), HarmonyPostfix]
        public static void GetCustomUseText(Seaglide __instance, ref string __result)
        {
            if(!__instance.TryGetComponent<Monos.SeaglideUpgradeManager>(out var manager))
                return;

            var device = GameInput.PrimaryDevice;

            bool showAllModuleStorage = HasPrimaryAndSecondary(device, SeaglideUpgradesModules.OpenModuleStorage);
            bool showAllBatteryStorage = HasPrimaryAndSecondary(device, SeaglideUpgradesModules.OpenBatteryUpgradeStorage);

            __result += $"\n{Language.main?.GetFormat("ramune.sgum.accessupgrades", manager.GetTotalModules(), (manager.moduleStorage?.container?.sizeX ?? 0) * (manager.moduleStorage?.container?.sizeY ?? 0), GameInput.FormatButton(SeaglideUpgradesModules.OpenModuleStorage, showAllModuleStorage))}" + (manager.batterySwapModules > 0 ? $", {Language.main?.GetFormat("ramune.sgum.accessbatteries", manager.batteryStorageItems.Count, manager.batterySwapModules, GameInput.FormatButton(SeaglideUpgradesModules.OpenBatteryUpgradeStorage, showAllBatteryStorage))}" : "");
        }


        private static bool HasPrimaryAndSecondary(GameInput.Device device, GameInput.Button button)
        {
            return !string.IsNullOrWhiteSpace(GameInput.GetBinding(device, button, GameInput.BindingSet.Primary)) &&
                   !string.IsNullOrWhiteSpace(GameInput.GetBinding(device, button, GameInput.BindingSet.Secondary));
        }
    }
}