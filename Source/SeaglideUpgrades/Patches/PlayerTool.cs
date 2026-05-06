

namespace Ramune.SeaglideUpgrades.Patches
{
    [HarmonyPatch(typeof(PlayerTool))]
    public static class PlayerToolPatch
    {
        public static Dictionary<TechType, Action<float>> ModdedSeaglideTechTypes = new()
        {
            { 
                TechType.Seaglide, (multiplier) => SeaglideUpgrades.SetSeaglideSpeed(SeaglideUpgrades.config.vanillaSpeed, SeaglideUpgrades.config.vanillaAcceleration, SeaglideUpgrades.config.vanillaMultiplier + multiplier) 
            },
            { 
                Items.SeaglideMK1.Prefab.Info.TechType, (multiplier) => SeaglideUpgrades.SetSeaglideSpeed(42f, 42f, SeaglideUpgrades.config.speedmk1 * multiplier) 
            },
            { 
                Items.SeaglideMK2.Prefab.Info.TechType, (multiplier) => SeaglideUpgrades.SetSeaglideSpeed(50f, 50f, SeaglideUpgrades.config.speedmk2 * multiplier) 
            },
            { 
                Items.SeaglideMK3.Prefab.Info.TechType, (multiplier) => SeaglideUpgrades.SetSeaglideSpeed(58f, 58f, SeaglideUpgrades.config.speedmk3 * multiplier) 
            }
        };


        [HarmonyPatch(nameof(PlayerTool.animToolName), MethodType.Getter), HarmonyPostfix]
        public static void Postfix(PlayerTool __instance, ref string __result)
        {
            var techType = __instance.pickupable?.GetTechType() ?? TechType.None;

            if(!ModdedSeaglideTechTypes.Keys.Contains(techType))
                return;
            
            __result = "seaglide";
        }
    }
}