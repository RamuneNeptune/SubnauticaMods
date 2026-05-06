

namespace Ramune.SeaglideUpgradesModules.Items
{
    public static class PowerglideUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglidePowerglideUpgrade", "ramune.sgum.powerglideupgrade.name".LangKey(), "ramune.sgum.powerglideupgrade.desc".LangKey(), ImageUtils.GetSprite("PowerglideUpgrade", TechType.MapRoomUpgradeScanSpeed))
            .WithJsonRecipe("PowerglideUpgrade")
            .WithSize(1, 1);


        public static void Patch()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.HullReinforcementModule));
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "ramune.sgu.workbenchtab.name".LangKey()]);
        }
    }
}