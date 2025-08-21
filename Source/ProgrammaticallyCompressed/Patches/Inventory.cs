

namespace Ramune.ProgrammaticallyCompressed.Patches
{
    [HarmonyPatch(typeof(Inventory))]
    public static class InventoryPatch
    {
        [HarmonyPatch(nameof(Inventory.ConsumeResourcesForRecipe)), HarmonyPrefix]
        public static bool ConsumeResourcesForRecipe(Inventory __instance, TechType techType)
        {
            if(!GameModeUtils.RequiresIngredients())
                return false;

            if(!CrafterLogicPatch.BatteryTechTypes.Contains(techType))
                return true;

            var ingredient = TechData.GetIngredients(techType)?.FirstOrDefault()?.techType ?? TechType.None;

            if(ingredient == TechType.None) 
                return true;

            var items = __instance.container.GetItems(ingredient).ToList();

            var consumed = 0;

            foreach(var item in items)
            {
                if(consumed >= 10) 
                    break;

                if(!item.item.gameObject.TryGetComponent<Battery>(out var battery) || !(Mathf.RoundToInt(battery.charge) >= Mathf.RoundToInt(battery.capacity)))
                    continue;

                __instance.container.RemoveItem(item.item, true);
                uGUI_IconNotifier.main.Play(ingredient, uGUI_IconNotifier.AnimationType.To, null);
                consumed++;
            }
            
            return false;
        }
    }
}