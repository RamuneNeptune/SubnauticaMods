

namespace Ramune.ShowUnlockRequirements.Patches
{
    [HarmonyPatch(typeof(TooltipFactory))]
    public static class TooltipFactoryPatch
    {
        public static readonly Dictionary<TechType, List<TechType>> UnlockTechTypes = BuildUnlockTechTypes();


        public static Dictionary<TechType, List<TechType>> BuildUnlockTechTypes()
        {
            var unlockTechTypes = new Dictionary<TechType, List<TechType>>();

            if(KnownTech.analysisTech == null)
                return unlockTechTypes;

            foreach(var entry in KnownTech.analysisTech)
            {
                if(entry.unlockTechTypes == null)
                    continue;

                foreach(var unlockTechType in entry.unlockTechTypes)
                    unlockTechTypes.GetOrAddNew(unlockTechType).Add(entry.techType);
            }

            return unlockTechTypes;
        }


        public static readonly Dictionary<TechType, List<TechType>> CompoundTechTypes = BuildCompoundTechTypes();


        public static Dictionary<TechType, List<TechType>> BuildCompoundTechTypes()
        {
            var compoundTechTypes = new Dictionary<TechType, List<TechType>>();

            if(KnownTech.compoundTech == null)
                return compoundTechTypes;

            foreach(var entry in KnownTech.compoundTech)
            {
                if(entry.dependencies == null || entry.dependencies.Count == 0)
                    continue;

                compoundTechTypes[entry.techType] = entry.dependencies;
            }

            return compoundTechTypes;
        }


        [HarmonyPatch(nameof(TooltipFactory.CraftRecipe)), HarmonyPrefix]
        public static bool CraftRecipe(TechType techType, bool locked, TooltipData data) => !ShowUnlockRequirements.config.AffectCraftingUI || !locked || !TryWriteLockedTooltip(techType, data);


        [HarmonyPatch(nameof(TooltipFactory.BuilderItem)), HarmonyPrefix]
        public static bool BuilderItem(TechType techType, bool locked, TooltipData data) => !ShowUnlockRequirements.config.AffectBuildingUI || !locked || !TryWriteLockedTooltip(techType, data);


        [HarmonyPatch(nameof(TooltipFactory.Blueprint)), HarmonyPrefix]
        public static bool Blueprint(TechType techType, bool locked, TooltipData data) => !ShowUnlockRequirements.config.AffectPDAUI || !locked || !TryWriteLockedTooltip(techType, data);


        public static string Format(string key, params object[] args) => string.Format(key.LangKeyAbbr(), args);


        public static bool TryWriteLockedTooltip(TechType techType, TooltipData data)
        {
            var unlockText = GetUnlockText(techType);
            var description = string.IsNullOrEmpty(unlockText) || !ShowUnlockRequirements.config.RemoveIngredientsUnknown ? Language.main.Get("LockedRecipeHint") + (string.IsNullOrEmpty(unlockText) ? "" : "\n" + unlockText) : unlockText;

            TooltipFactory.WriteTitle(data.prefix, Language.main.Get(techType));
            TooltipFactory.WriteDescription(data.prefix, description);
            return true;
        }


        public static string GetUnlockText(TechType techType)
        {
            var unlockTechs = techType.GetUnlockTechTypes();

            if(unlockTechs.Count > 0)
                return Format("unlocktext", string.Join("</color>, <color=#07fafa>", unlockTechs.Select(Language.main.Get))) + "</color>";

            if(CompoundTechTypes.TryGetValue(techType, out List<TechType> dependencies))
                return Format("unlockcompoundtext", string.Join("</color>, <color=#07fafa>", dependencies.Select(Language.main.Get))) + "</color>";

            if(KnownTechPatch.ScannerUnlockEntries.TryGetValue(techType, out List<PDAScanner.EntryData> scannerEntries))
                foreach(var entryData in scannerEntries.OrderByDescending(x => x.totalFragments))
                    return PDAScanner.GetPartialEntryByKey(entryData.key, out PDAScanner.Entry partialEntry) ? Format("unlockfragmenttext", Language.main.Get(entryData.key), partialEntry.unlocked, entryData.totalFragments) : entryData.totalFragments > 1 || entryData.isFragment ? Format("unlockfragmenttext", Language.main.Get(entryData.key), 0, entryData.totalFragments) : Format("unlocktext", Language.main.Get(entryData.key));

            return "";
        }


        public static List<TechType> GetUnlockTechTypes(this TechType unlockableTechType) => UnlockTechTypes.TryGetValue(unlockableTechType, out List<TechType> techTypes) ? techTypes : [];
    }
}