

namespace Ramune.SeaglideUpgradesModules.Items
{
    public static class EfficiencyUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideEfficiencyUpgrade", "ramune.sgum.efficiencyupgrade.name".LangKey(), "ramune.sgum.efficiencyupgrade.desc".LangKey(), ImageUtils.GetSprite(TechType.PowerUpgradeModule))
            .WithJsonRecipe("EfficiencyUpgrade")
            .WithUnlock(TechType.Seaglide)
            .WithSize(1, 1);


        public static void Patch()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.HullReinforcementModule));
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "ramune.sgu.workbenchtab.name".LangKey()]);
        }
    }
}