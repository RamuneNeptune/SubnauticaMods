

namespace Ramune.ModularSurvivalKnife.Prefabs.Tools
{
    public static class ModularSurvivalKnife
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("ModularSurvivalKnife", ImageUtils.GetSprite(TechType.Knife))
            .WithJsonRecipe("ModularSurvivalKnife")
            .WithUnlock(TechType.HeatBlade)
            .WithPDACategoryAfter(TechGroup.Workbench, TechCategory.Workbench, TechType.HeatBlade)
            .WithEquipmentAndQuickSlotType(EquipmentType.Hand, QuickSlotType.Selectable);


        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.HeatBlade)
            { 
                ModifyPrefab = go =>
                {

                }
            });

            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, RamunesWorkbenchUtils.Tabs.Tools, "Blades");
        }
    }
}