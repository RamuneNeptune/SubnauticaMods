

namespace Ramune.SeaglideUpgradesModules.Items
{
    public static class BatterySwapUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideBatterySwapUpgrade", "ramune.sgum.batteryswapupgrade.name".LangKey(), "ramune.sgum.batteryswapupgrade.desc".LangKey(), ImageUtils.GetSprite(TechType.CyclopsShieldModule))
            .WithJsonRecipe("BatterySwapUpgrade")
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