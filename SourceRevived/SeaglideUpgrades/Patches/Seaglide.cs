

namespace Ramune.SeaglideUpgrades.Patches
{
    [HarmonyPatch(typeof(Seaglide))]
    public static class SeaglidePatch
    {
        public static TechType ActiveSeaglideTechType;


        [HarmonyPatch(nameof(Seaglide.OnDraw)), HarmonyPostfix]
        public static void OnDraw(Seaglide __instance)
        {
            var techType = __instance.pickupable?.GetTechType() ?? TechType.None;

            if(!PlayerToolPatch.ModdedSeaglideTechTypes.Keys.Contains(techType))
                return;

            ActiveSeaglideTechType = techType;

            PlayerToolPatch.ModdedSeaglideTechTypes[techType].Invoke(1f);
        }
    }
}