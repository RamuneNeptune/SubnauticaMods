

namespace Ramune.ProgrammaticallyCompressed.Patches
{
    [HarmonyPatch(typeof(CrafterLogic))]
    public static class CrafterLogicPatch
    {
        static CrafterLogicPatch() => BatteryTechTypes = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Path.Combine(Paths.ConfigurationFolder, "BatteryTechTypes.json"))).Select(x => EnumHandler.TryGetValue<TechType>(x, out var techType) ? (TechType?)techType : null).Where(x => x.HasValue).Select(x => x.Value).ToList();

        public static List<TechType> BatteryTechTypes = new();


        [HarmonyPatch(nameof(CrafterLogic.IsCraftRecipeFulfilled)), HarmonyPostfix]
        public static void IsCraftRecipeFulfilled(TechType techType, ref bool __result)
        {
            if(!GameModeUtils.RequiresIngredients() || BatteryTechTypes.Count < 1 || !BatteryTechTypes.Contains(techType))
                return;

            var batteries = Inventory.main.container.GetItems(TechData.GetIngredients(techType).FirstOrDefault()?.techType ?? TechType.None);

            __result = batteries != null && batteries.Any() && batteries.Count(x => x.item.gameObject.TryGetComponent<Battery>(out var battery) && Mathf.RoundToInt(battery.charge) >= Mathf.RoundToInt(battery.capacity)) >= 10;
        }
    }
}