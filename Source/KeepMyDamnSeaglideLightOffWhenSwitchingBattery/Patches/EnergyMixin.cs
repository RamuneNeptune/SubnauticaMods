﻿

namespace Ramune.KeepMyDamnSeaglideLightOffWhenSwitchingBattery.Patches
{
    [HarmonyPatch(typeof(EnergyMixin))]
    public static class EnergyMixinPatch
    {
        [HarmonyPatch(nameof(EnergyMixin.OnAddItem)), HarmonyPostfix]
        public static void OnAddItem(EnergyMixin __instance, InventoryItem item)
        {
            if(__instance.gameObject.name != "SeaGlide(Clone)")
                return;

            __instance.gameObject.GetComponent<Seaglide>()?.toggleLights.SetLightsActive(false);
        }
    }
}