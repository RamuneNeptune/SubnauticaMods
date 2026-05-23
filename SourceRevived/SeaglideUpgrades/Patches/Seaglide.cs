

namespace Ramune.SeaglideUpgrades.Patches
{
    [HarmonyPatch(typeof(Seaglide))]
    public static class SeaglidePatch
    {
        public static TechType ActiveSeaglideTechType;

        public static Func<Seaglide, float, float> ModifySpeedMultiplier;


        [HarmonyPatch(nameof(Seaglide.OnDraw)), HarmonyPostfix]
        public static void OnDraw(Seaglide __instance)
        {
            var techType = __instance.pickupable?.GetTechType() ?? TechType.None;

            if(!PlayerToolPatch.ModdedSeaglideTechTypes.Keys.Contains(techType))
                return;

            ActiveSeaglideTechType = techType;

            var multiplier = 1f;

            if(ModifySpeedMultiplier != null)
            {
                foreach(var callback in ModifySpeedMultiplier.GetInvocationList().Cast<Func<Seaglide, float, float>>())
                    multiplier = callback(__instance, multiplier);
            }

            PlayerToolPatch.ModdedSeaglideTechTypes[techType].Invoke(multiplier);
        }
    }
}