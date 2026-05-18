

namespace Ramune.SeaglideUpgradesModules.Prefabs.UpgradeModules
{
    public static class BatterySwapUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("SeaglideBatterySwapUpgrade", ImageUtils.GetSprite("BatterySwapUpgrade", TechType.CyclopsShieldModule))
            .WithJsonRecipe("BatterySwapUpgrade")
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