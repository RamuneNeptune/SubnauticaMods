

namespace Ramune.DecoFabricator.Prefabs.Buildables
{
    public static class DecoFabricator
    {
        public static FabricatorGadget fabricator;

        public static CraftTree.Type craftTreeType = CraftTree.Type.None;

        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("DecoFabricator", ImageUtils.GetSprite(TechType.Fabricator))
                .WithPDACategoryAfter(TechGroup.InteriorModules, TechCategory.InteriorModule, TechType.Fabricator)
                .WithJsonRecipe("DecoFabricator")
                .WithFabricator(out fabricator)
                .WithAutoUnlock();

        public static void Register()
        {
            craftTreeType = fabricator.CraftTreeType;

            var clone = new FabricatorTemplate(Prefab.Info, craftTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Fabricator,
                ModifyPrefab = (go) =>
                {
                    if(!go.TryGetComponentEverywhere<SkinnedMeshRenderer>(out var renderer))
                        return;

                    renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Main, ImageUtils.GetTexture("DecoFabricator.Texture"));
                    renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Specular, ImageUtils.GetTexture("DecoFabricator.Texture"));
                    renderer.SetTexture(RamuneLib.Extensions.RendererExtensions.TextureType.Illum, ImageUtils.GetTexture("DecoFabricator.Illum"));
                    renderer.SetGlowStrength(0.9f, true);
                }
            };

            (string tabName, TechType[] tabItems, Sprite tabSprite)[] TechMap = [
                ("Posters", [TechType.PosterKitty, TechType.Poster, TechType.PosterExoSuit1, TechType.PosterExoSuit2, TechType.PosterAurora], ImageUtils.GetSprite(TechType.PosterKitty)),
                ("Science", [TechType.LabEquipment3, TechType.LabEquipment2, TechType.LabEquipment1, TechType.LabContainer, TechType.LabContainer2, TechType.LabContainer3], ImageUtils.GetSprite(TechType.LabEquipment3)),
                ("Toys", [TechType.ArcadeGorgetoy, TechType.ToyCar, TechType.StarshipSouvenir], ImageUtils.GetSprite(TechType.ArcadeGorgetoy)),
                ("Caps", [TechType.Cap2, TechType.Cap1], ImageUtils.GetSprite(TechType.Cap2))
            ];

            foreach(var (tabName, tabItems, tabSprite) in TechMap)
            {
                fabricator.AddTabNode(tabName, tabName, tabSprite);

                foreach(var techType in tabItems)
                {
                    CraftDataHandler.SetRecipeData(techType, JsonUtils.GetJsonRecipeDataOrDefault(JsonUtils.GetJsonRecipePath(tabName)));
                    KnownTechHandler.UnlockOnStart(techType);
                    fabricator.AddCraftNode(techType, tabName);
                }
            }

            Prefab.SetGameObject(clone);
            Prefab.Register();
        }
    }
}