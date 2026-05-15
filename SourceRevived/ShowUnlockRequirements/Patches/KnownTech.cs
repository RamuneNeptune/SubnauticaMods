

namespace Ramune.ShowUnlockRequirements.Patches
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
            TechType.EatMyDiction,
            TechType.Terraformer,
            TechType.PowerGlide,
            TechType.LithiumIonBattery,
            TechType.Transfuser
        ];


        [HarmonyPatch(nameof(KnownTech.GetTechUnlockState), [typeof(TechType), typeof(int), typeof(int)], [ArgumentType.Normal, ArgumentType.Out, ArgumentType.Out]), HarmonyPostfix]
        public static void GetTechUnlockState(TechType techType, ref int __1, ref int __2, ref TechUnlockState __result)
        {
            if(!ShowUnlockRequirements.config.ShowAllUnknownNodes || IgnoredTechTypes.Contains(techType))
                return;

            if(__result == TechUnlockState.Available && CrafterLogic.IsCraftRecipeUnlocked(techType))
                return;

            __result = TechUnlockState.Locked;

            if(__2 < 1)
            {
                __1 = 0;
                __2 = 1;

                if(TryGetScannerProgress(techType, out int unlocked, out int total))
                {
                    __1 = unlocked;
                    __2 = total;
                }
            }
        }


        public static bool TryGetScannerProgress(TechType techType, out int unlocked, out int total)
        {
            unlocked = 0;
            total = 0;

            if(!ScannerUnlockEntries.TryGetValue(techType, out List<PDAScanner.EntryData> entries))
                return false;

            foreach(var entryData in entries)
            {
                if(entryData.totalFragments <= total)
                    continue;

                total = entryData.totalFragments;
                unlocked = PDAScanner.GetPartialEntryByKey(entryData.key, out PDAScanner.Entry partialEntry) ? partialEntry.unlocked : 0;
            }

            return total > 0;
        }


        public static readonly Dictionary<TechType, List<PDAScanner.EntryData>> ScannerUnlockEntries = BuildScannerUnlockEntries();


        public static Dictionary<TechType, List<PDAScanner.EntryData>> BuildScannerUnlockEntries()
        {
            var scannerUnlockEntries = new Dictionary<TechType, List<PDAScanner.EntryData>>();
            var scannerEntries = PDAScanner.GetAllEntriesData();

            while(scannerEntries.MoveNext())
            {
                var entryData = scannerEntries.Current.Value;

                scannerUnlockEntries.GetOrAddNew(entryData.blueprint).Add(entryData);
            }

            return scannerUnlockEntries;
        }
    }
}