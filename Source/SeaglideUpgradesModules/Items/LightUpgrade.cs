

namespace Ramune.SeaglideUpgradesModules.Items
{
    public static class LightUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideLightUpgrade", "ramune.sgum.lightupgrade.name".LangKey(), "ramune.sgum.lightupgrade.desc".LangKey(), ImageUtils.GetSprite("LightUpgrade", TechType.LEDLight))
            .WithJsonRecipe("LightUpgrade")
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