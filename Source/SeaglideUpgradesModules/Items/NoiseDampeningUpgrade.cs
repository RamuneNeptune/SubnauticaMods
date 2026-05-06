

namespace Ramune.SeaglideUpgradesModules.Items
{
    public static class NoiseDampeningUpgrade
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideNoiseDampeningUpgrade", "ramune.sgum.noisedampeningupgrade.name".LangKey(), "ramune.sgum.noisedampeningupgrade.desc".LangKey(), ImageUtils.GetSprite("NoiseDampeningUpgrade", TechType.CyclopsDecoyModule))
            .WithJsonRecipe("NoiseDampeningUpgrade")
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