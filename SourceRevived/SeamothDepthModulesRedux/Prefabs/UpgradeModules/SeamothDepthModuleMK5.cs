

namespace Ramune.SeamothDepthModulesRedux.Prefabs.UpgradeModules
{
    public static class SeamothDepthModuleMK5
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("SeamothDepthModuleMK5", ImageUtils.GetSprite(TechType.VehicleHullModule3))
            .WithJsonRecipe("SeamothDepthModuleMK5", CraftTree.Type.SeamothUpgrades, CraftTreeHandler.Paths.VehicleUpgradesSeamothModules)
            .WithPDACategoryAfter(TechGroup.VehicleUpgrades, TechCategory.VehicleUpgrades, TechType.VehicleHullModule3)
            .WithEquipmentAndQuickSlotType(EquipmentType.SeamothModule, QuickSlotType.Passive)
            .WithUnlock(TechType.VehicleHullModule3);

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.VehicleHullModule3));
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, RamunesWorkbenchUtils.Tabs.Seamoth);
        }
    }
}