

namespace Ramune.BZEnameledGlass.Prefabs.Materials
{
    public static class EnameledGlassClone
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("EnameledGlassClone", ImageUtils.GetSprite(TechType.EnameledGlass))
            .WithJsonRecipe("EnameledGlassClone", CraftTree.Type.Fabricator, CraftTreeHandler.Paths.FabricatorsBasicMaterials)
            .WithPDACategoryAfter(TechGroup.Resources, TechCategory.BasicMaterials, TechType.EnameledGlass)
            .WithAutoUnlock();

        public static void Register()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.EnameledGlass);

            Prefab.SetGameObject(clone);
            Prefab.Register();
        }
    }
}