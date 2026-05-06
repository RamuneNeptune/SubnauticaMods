

namespace Ramune.SeamothDepthModulesRedux.Patches
{
    [HarmonyPatch(typeof(CrushDamage))]
    public static class CrushDamagePatch
    {
        [HarmonyPatch(nameof(CrushDamage.SetExtraCrushDepth)), HarmonyPrefix]
        public static void SetExtraCrushDepth(CrushDamage __instance, ref float depth)
        {
            if(__instance == null || __instance.vehicle is not SeaMoth seamoth || seamoth.modules == null)
                return;

            depth = Mathf.Max(depth, seamoth.modules.GetCount(Prefabs.UpgradeModules.SeamothDepthModuleMK5.Prefab.Info.TechType) > 0 ? SeamothDepthModulesRedux.config.SeamothDepthModuleMK5Depth : seamoth.modules.GetCount(Prefabs.UpgradeModules.SeamothDepthModuleMK4.Prefab.Info.TechType) > 0 ? SeamothDepthModulesRedux.config.SeamothDepthModuleMK4Depth : 0f);
        }
    }
}