

namespace Ramune.MegaO2Tank.Prefabs.Equipment
{
    public static class MegaO2Tank
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("MegaO2Tank")
            .WithPDACategoryAfter(TechGroup.Workbench, TechCategory.Workbench, TechType.HighCapacityTank)
            .WithUnlock(TechType.HighCapacityTank)
            .WithEquipment(EquipmentType.Tank)
            .WithJsonRecipe("MegaO2Tank");

        public static void Register()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.HighCapacityTank)
            {
                ModifyPrefab = go =>
                {
                    var oxygen = go.EnsureComponent<Oxygen>();
                    oxygen.oxygenCapacity = Ramune.MegaO2Tank.MegaO2Tank.config.oxygenCapacity;
                }
            };

            Prefab.SetGameObject(clone);
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "Tanks"]);
        }
    }
}