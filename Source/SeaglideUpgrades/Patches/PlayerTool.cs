

namespace Ramune.SeaglideUpgrades.Patches
{
    [HarmonyPatch(typeof(PlayerTool))]
    public static class PlayerToolPatches
    {
        public static Dictionary<TechType, Action> ModdedSeaglideTechTypes = new()
        {
            { TechType.Seaglide, () => SeaglideUpgrades.SetSeaglideSpeed(25f, 36.56f) }
        };


        public static TechType ActiveSeaglideTechType;


        [HarmonyPatch(nameof(PlayerTool.animToolName), MethodType.Getter), HarmonyPostfix]
        public static void Postfix(PlayerTool __instance, ref string __result)
        {
            var techType = __instance.pickupable?.GetTechType() ?? TechType.None;

            if(!ModdedSeaglideTechTypes.Keys.Contains(techType))
                return;
            
            __result = "seaglide";
        }


        [HarmonyPatch(nameof(PlayerTool.OnDraw)), HarmonyPrefix]
        public static void OnDraw(PlayerTool __instance)
        {
            var techType = __instance.pickupable?.GetTechType() ?? TechType.None;

            if(!ModdedSeaglideTechTypes.Keys.Contains(techType))
                return;

            ActiveSeaglideTechType = techType;

            ModdedSeaglideTechTypes[techType].Invoke();
        }
    }
}