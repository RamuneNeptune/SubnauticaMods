

namespace Ramune.SeaglideUpgradesModules.Items
{
    public static class SpeedUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideSpeedUpgrade", "ramune.sgum.speedupgrade.name".LangKey(), "ramune.sgum.speedupgrade.desc".LangKey(), ImageUtils.GetSprite(TechType.MapRoomUpgradeScanSpeed))
            .WithJsonRecipe("SpeedUpgrade")
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