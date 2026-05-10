

namespace Ramune.SeaglideUpgradesModules.Prefabs.UpgradeModules
{
    public static class BoostUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("SeaglideBoostUpgrade", ImageUtils.GetSprite("BoostUpgrade", TechType.ExosuitJetUpgradeModule))
            .WithJsonRecipe("BoostUpgrade")
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