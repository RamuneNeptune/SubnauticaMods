

namespace Ramune.SeaglideUpgradesModules.Prefabs.UpgradeModules
{
    public static class PowerglideUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("SeaglidePowerglideUpgrade", ImageUtils.GetSprite("PowerglideUpgrade", TechType.MapRoomUpgradeScanSpeed))
            .WithJsonRecipe("PowerglideUpgrade")
            .WithSize(1, 1);

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.HullReinforcementModule));
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "ramune.su.workbenchtabname".LangKey()]);
        }
    }
}