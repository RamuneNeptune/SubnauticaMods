

namespace Ramune.ShowUnknownItems.Patches
{
    [HarmonyPatch(typeof(KnownTech))]
    public static class KnownTechPatch
    {
        public static readonly List<TechType> IgnoredTechTypes = 
        [
            TechType.Centrifuge,
            TechType.StarshipCargoCrate,
            TechType.StarshipCircuitBox,
            TechType.StarshipMonitor,
            TechType.DevTestItem,
            TechType.BikemanHullPlate,
            TechType.DioramaHullPlate,
            TechType.EatMyDictionHullPlate,
            TechType.GilathissHullPlate,
            TechType.IGPHullPlate,
            TechType.JackSepticEyeHullPlate,
            TechType.LordMinionHullPlate,
            TechType.MarkiplierHullPlate,
            TechType.MuyskermHullPlate,
            TechType.SpecialHullPlate,
            TechType.Marki1,
            TechType.Marki2,
            TechType.JackSepticEye,
            TechType.EatMyDiction
        ];


        [HarmonyPatch(nameof(KnownTech.GetTechUnlockState), [typeof(TechType), typeof(int), typeof(int)], [ArgumentType.Normal, ArgumentType.Out, ArgumentType.Out]), HarmonyPostfix]
        public static void GetTechUnlockState(TechType techType, ref TechUnlockState __result)
        {
            if(IgnoredTechTypes.Contains(techType))
                return;

            __result = (__result != TechUnlockState.Available || !CrafterLogic.IsCraftRecipeUnlocked(techType)) ? TechUnlockState.Locked : __result;
        }
    }
}