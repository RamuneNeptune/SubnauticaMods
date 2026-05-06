

namespace Ramune.IonThermalPlant.Patches
{
    [HarmonyPatch(typeof(ThermalPlant))]
    public static class ThermalPlantPatch
    {
        [HarmonyPatch(nameof(ThermalPlant.AddPower)), HarmonyPrefix]
        public static bool AddPower(ThermalPlant __instance)
        {
            if(GetTechType(__instance.gameObject) != Prefabs.ExteriorModules.IonThermalPlant.Prefab.Info.TechType)
                return true;

            __instance.powerSource.maxPower = IonThermalPlant.config.powerMaxCapacity;

            if(!__instance.constructable.constructed || __instance.temperature <= 25f)
                return false;

            float num1 = 2f * DayNightCycle.main.dayNightSpeed;
            float num2 = Mathf.Clamp01(Mathf.InverseLerp(25f, 100f, __instance.temperature));
            float amount = 1.6500001f * num1 * num2 * IonThermalPlant.config.powerMultiplier;

            __instance.powerSource.AddEnergy(amount, out _);
            return false;
        }
    }
}