

namespace Ramune.SeaglideUpgradesModules.Prefabs.UpgradeModules
{
    public static class EfficiencyUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("SeaglideEfficiencyUpgrade", ImageUtils.GetSprite("EfficiencyUpgrade", TechType.PowerUpgradeModule))
            .WithJsonRecipe("EfficiencyUpgrade")
            .WithUnlock(TechType.Seaglide)
            .WithSize(1, 1);

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.HullReinforcementModule));
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "ramune.su.workbenchtabname".LangKey()]);
        }
    }
}