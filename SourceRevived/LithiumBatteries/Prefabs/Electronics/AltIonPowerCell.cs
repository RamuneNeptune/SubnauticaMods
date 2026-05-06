

namespace Ramune.LithiumBatteries.Prefabs.Electronics
{
    public static class AltIonPowerCell
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("AltIonPowerCell", ImageUtils.GetSprite(TechType.PrecursorIonPowerCell))
            .WithEquipment(EquipmentType.PowerCellCharger)
            .WithUnlock(TechType.PrecursorIonBattery)
            .WithJsonRecipe("AltIonPowerCell");


        public static void Register()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.PrecursorIonPowerCell);

            Prefab.SetGameObject(clone);
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, RamunesWorkbenchUtils.Tabs.PowerCells);
        }
    }
}