

namespace Ramune.SeamothDepthModulesRedux.Prefabs.UpgradeModules
{
    public static class SeamothDepthModuleMK4
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("SeamothDepthModuleMK4", ImageUtils.GetSprite(TechType.VehicleHullModule3))
            .WithJsonRecipe("SeamothDepthModuleMK4", CraftTree.Type.SeamothUpgrades, CraftTreeHandler.Paths.VehicleUpgradesSeamothModules)
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