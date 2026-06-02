

namespace Ramune.EasyScanUnlock.Patches
{
    [HarmonyPatch(typeof(PDAScanner))]
    public static class PDAScannerPatch
    {
        [HarmonyPatch(nameof(PDAScanner.Scan)), HarmonyPrefix]
        public static void Scan()
        {
            if(PDAScanner.CanScan(PDAScanner.scanTarget) != PDAScanner.Result.Scan)
                return;

            var techType = PDAScanner.scanTarget.techType;
            var entryData = PDAScanner.GetEntryData(techType);

            if(entryData == null || entryData.blueprint == TechType.None || entryData.totalFragments <= 1 || PDAScanner.ContainsCompleteEntry(techType))
                return;

            if(PDAScanner.GetPartialEntryByKey(techType, out PDAScanner.Entry entry))
                entry.unlocked = Mathf.Max(entry.unlocked, entryData.totalFragments - 1);
            else
                PDAScanner.Add(techType, entryData.totalFragments - 1);
        }
    }
}