

namespace Ramune.KioniteBatteries.Prefabs.AdvancedMaterials
{
    public static class Kyanicphur
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("Kyanicphur")
            .WithJsonRecipe("Kyanicphur", CraftTree.Type.Fabricator, CraftTreeHandler.Paths.FabricatorsAdvancedMaterials)
            .WithPDACategory(TechGroup.Resources, TechCategory.AdvancedMaterials)
            .WithUnlock(TechType.Sulphur);

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.Kyanite));
            Prefab.Register();
        }
    }
}