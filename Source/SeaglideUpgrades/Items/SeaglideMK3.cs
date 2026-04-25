

namespace Ramune.SeaglideUpgrades.Items
{
    public static class SeaglideMK3
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideMK3", "ramune.sgu.seaglidemk3.name".LangKey(), "ramune.sgu.seaglidemk3.desc".LangKey(), ImageUtils.GetSprite("SeaglideMK3.Sprite"))
            .WithJsonRecipe("SeaglideMK3")
            .WithEquipment(EquipmentType.Hand)
            .WithUnlock(TechType.Seaglide)
            .WithSize(2, 3);

        public static Texture2D Texture = ImageUtils.GetTexture("SeaglideMK3.Texture");
        public static Texture2D Illum = ImageUtils.GetTexture("SeaglideMK3.Illum");

        public static readonly List<Action<GameObject>> ModifyPrefabCallbacks = [];


        public static void Patch()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.Seaglide)
            {
                ModifyPrefab = go =>
                {
                    var lightController = go.EnsureComponent<Monos.SeaglideLightController>();
                    lightController.techType = Prefab.Info.TechType;

                    var renderers = go.GetComponentsInChildren<SkinnedMeshRenderer>(true);

                    if(SeaglideUpgrades.config.specTexChoice == 1) 
                        renderers.ForEach(x => x.material.SetTexture(ShaderPropertyID._SpecTex, Texture));

                    renderers.ForEach(x => x.material.SetTexture(ShaderPropertyID._MainTex, Texture));
                    renderers.ForEach(x => x.material.SetTexture(ShaderPropertyID._Illum, Illum));

                    ModifyPrefabCallbacks.ForEach(x => x.Invoke(go));
                }
            };

            Prefab.SetGameObject(clone);
            Prefab.Register();

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "ramune.sgu.workbenchtab.name".LangKey()]);
        }
    }
}