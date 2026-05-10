

namespace Ramune.SeaglideUpgradesModules.Prefabs.UpgradeModules
{
    public static class SpeedUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("SeaglideSpeedUpgrade", ImageUtils.GetSprite("SpeedUpgrade", TechType.MapRoomUpgradeScanSpeed))
            .WithJsonRecipe("SpeedUpgrade")
            .WithUnlock(TechType.Seaglide)
            .WithSize(1, 1);

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.HullReinforcementModule));
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "ramune.sgu.workbenchtab.name".LangKey()]);
        }
    }
}