

namespace Ramune.BZEnameledGlass.Items
{
    public static class EnameledGlassClone
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("EnameledGlassClone", "EnameledGlass".LangKey(), "Tooltip_EnameledGlass".LangKey(), ImageUtils.GetSprite(TechType.EnameledGlass))
            .WithJsonRecipe("EnameledGlassClone", CraftTree.Type.Fabricator, CraftTreeHandler.Paths.FabricatorsBasicMaterials)
            .WithAutoUnlock();

        public static void Patch()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.EnameledGlass);

            Prefab.SetGameObject(clone);

            Prefab.Register();
        }
    }
}