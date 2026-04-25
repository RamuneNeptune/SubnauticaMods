

namespace Ramune.SeaglideUpgradesModules.Items
{
    public static class BoostUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideBoostUpgrade", "ramune.sgum.boostupgrade.name".LangKey(), "ramune.sgum.boostupgrade.desc".LangKey(), ImageUtils.GetSprite(TechType.ExosuitJetUpgradeModule))
            .WithJsonRecipe("BoostUpgrade")
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