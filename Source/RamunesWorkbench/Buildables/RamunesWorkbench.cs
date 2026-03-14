

namespace Ramune.RamunesWorkbench.Buildables
{
    public static class RamunesWorkbench
    {
        public static List<(string id, string displayName, object spriteOrTechType, string[] pathToTab)> queuedTabNodes = new();
        public static List<(TechType itemToCraft, string[] pathToTab)> queuedCraftNodes = new();
        public static CraftTree.Type craftTreeType;

        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("RamunesWorkbench", "Ramune's workbench", "Used to create items from RamuneNeptune's mods", ImageUtils.GetSprite("RamunesWorkbench"))
            .WithPDACategoryAfter(TechGroup.InteriorModules, TechCategory.InteriorModule, TechType.Workbench)
            .WithJsonRecipe("RamunesWorkbench")
            .WithFabricator(out craftTreeType)
            .WithAutoUnlock();


        public static void Patch()
        {
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
                    GameObject.DestroyImmediate(workbench);

                    ramunesWorkbench.renderer = renderer;
                }
            };

            Prefab.SetGameObject(model);
            Prefab.Register();
        }
    }
}