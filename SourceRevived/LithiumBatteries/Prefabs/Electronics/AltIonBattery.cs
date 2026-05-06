

namespace Ramune.LithiumBatteries.Prefabs.Electronics
{
    public static class AltIonBattery
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("AltIonBattery", ImageUtils.GetSprite(TechType.PrecursorIonBattery))
            .WithEquipment(EquipmentType.BatteryCharger)
            .WithUnlock(TechType.PrecursorIonBattery)
            .WithJsonRecipe("AltIonBattery");


        public static void Register()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.PrecursorIonBattery);

            Prefab.SetGameObject(clone);
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, RamunesWorkbenchUtils.Tabs.Batteries);
        }
    }
}