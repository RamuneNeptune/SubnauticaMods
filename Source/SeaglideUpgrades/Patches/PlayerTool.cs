

namespace Ramune.SeaglideUpgrades.Patches
{
    [HarmonyPatch(typeof(PlayerTool))]
    public static class PlayerToolPatches
    {
        public static Dictionary<TechType, Action> ModdedSeaglideTechTypes = new()
        {
            { TechType.Seaglide, () => SeaglideUpgrades.SetSeaglideSpeed(25f, 36.56f) }
        };


        [HarmonyPatch(nameof(PlayerTool.animToolName), MethodType.Getter), HarmonyPostfix]
        public static void Postfix(PlayerTool __instance, ref string __result)
        {
            if(ModdedSeaglideTechTypes.Keys.Contains(__instance.pickupable?.GetTechType() ?? TechType.None))
                __result = "seaglide";
        }


        [HarmonyPatch(nameof(PlayerTool.OnDraw)), HarmonyPrefix]
        public static void OnDraw(PlayerTool __instance)
        {
            var techType = __instance.pickupable?.GetTechType() ?? TechType.None;

            if(!ModdedSeaglideTechTypes.Keys.Contains(techType))
                return;
            
            ModdedSeaglideTechTypes[techType].Invoke();
        }
    }
}