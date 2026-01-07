

namespace Ramune.SeaglideUpgrades.Items
{
    public static class SeaglideMK1
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideMK1", "Seaglide <color=#03f0f1>MK1</color>", "SPEED: +15%\nConverts torque into thrust underwater via propeller.", ImageUtils.GetSprite("SeaglideMK1"))
            .WithPDACategory(TechGroup.ExteriorModules, TechCategory.ExteriorOther)
            .WithJsonRecipe("SeaglideMK1")
            .WithUnlock(TechType.Seaglide)
            .WithSize(2, 3);


        public static void Patch()
        {

        }
    }
}