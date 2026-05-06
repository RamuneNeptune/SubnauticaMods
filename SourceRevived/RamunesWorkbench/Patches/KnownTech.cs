

namespace Ramune.RamunesWorkbench.Patches
{
    [HarmonyPatch(typeof(KnownTech))]
    public static class KnownTechPatch
    {
        public static List<string> ModdedTechTypeStrings = [];


        [HarmonyPatch(nameof(KnownTech.GetTechUnlockState), [typeof(TechType), typeof(int), typeof(int)], [ArgumentType.Normal, ArgumentType.Out, ArgumentType.Out]), HarmonyPostfix]
        public static void GetTechUnlockState(TechType techType, ref TechUnlockState __result)
        {
            if(!ModdedTechTypeStrings.Contains(techType.AsString()))
                return;

            __result = (__result != TechUnlockState.Available || !CrafterLogic.IsCraftRecipeUnlocked(techType)) ? TechUnlockState.Locked : __result;
        }
    }
}