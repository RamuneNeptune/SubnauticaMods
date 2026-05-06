

namespace Ramune.RamunesWorkbench.Prefabs.Buildables
{
    public static class RamunesWorkbench
    {
        public static FabricatorGadget fabricator;

        public static CraftTree.Type craftTreeType = CraftTree.Type.None;

        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("RamunesWorkbench")
                .WithPDACategoryAfter(TechGroup.InteriorModules, TechCategory.InteriorModule, TechType.Workbench)
                .WithJsonRecipe("RamunesWorkbench")
                .WithFabricator(out fabricator)
                .WithAutoUnlock();


        public static void Register()
        {
            craftTreeType = fabricator.CraftTreeType;

            var model = new FabricatorTemplate(Prefab.Info, craftTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Workbench,
                ModifyPrefab = go =>
                {
                    var renderer = go.GetComponentInChildren<Renderer>();

                    renderer.material.SetTexture(ShaderPropertyID._MainTex, ImageUtils.GetTexture("RamunesWorkbench.Texture"));
                    renderer.material.SetTexture(ShaderPropertyID._SpecTex, ImageUtils.GetTexture("RamunesWorkbench.Texture"));
                    renderer.material.SetTexture(ShaderPropertyID._Illum, ImageUtils.GetTexture("RamunesWorkbench.Illum"));
                    renderer.material.EnableKeyword("MARMO_EMISSION");

                    var workbench = go.GetComponent<Workbench>();
                    var ramunesWorkbench = go.AddComponent<Monos.RamunesWorkbench>().CopyComponent(workbench);

                    Object.DestroyImmediate(workbench);

                    ramunesWorkbench.renderer = renderer;
                }
            };

            Prefab.SetGameObject(model);
            Prefab.Register();

            CraftHandler.Initialize();
        }
    }
}