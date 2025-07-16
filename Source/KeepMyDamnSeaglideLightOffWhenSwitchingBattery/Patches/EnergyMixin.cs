﻿

namespace Ramune.KeepMyDamnSeaglideLightOffWhenSwitchingBattery.Patches
{
    [HarmonyPatch(typeof(EnergyMixin))]
    public static class EnergyMixinPatch
    {
        [HarmonyPatch(nameof(EnergyMixin.OnAddItem)), HarmonyPostfix]
        public static void OnAddItem(EnergyMixin __instance, InventoryItem item)
        {
            if(!item.techType.Name().Equals("Seaglide"))
                return;

            __instance.gameObject.GetComponent<Seaglide>()?.toggleLights.SetLightsActive(false);
        }
    }
}