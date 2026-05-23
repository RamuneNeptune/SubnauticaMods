

namespace Ramune.Headlamp.Prefabs.Equipment
{
    public static class Headlamp
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("Headlamp", ImageUtils.GetSprite(TechType.Flashlight))
            .WithPDACategory(TechGroup.Workbench, TechCategory.Workbench)
            .WithUnlock(TechType.Flashlight)
            .WithEquipment(EquipmentType.Chip)
            .WithJsonRecipe("Headlamp");

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.MapRoomHUDChip));
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "Headwear"]);
        }
    }
}