

namespace Ramune.SeaglideUpgrades.Prefabs.Deployables
{
    public static class SeaglideMK1
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideMK1", "seaglidemk1.name".LangKeyAbbr(), "seaglidemk1.desc".LangKeyAbbr(), ImageUtils.GetSprite("SeaglideMK1.Sprite"))
            .WithJsonRecipe("SeaglideMK1")
            .WithEquipment(EquipmentType.Hand)
            .WithUnlock(TechType.Seaglide)
            .WithSize(2, 3);

        public static Texture2D Texture = ImageUtils.GetTexture("SeaglideMK1.Texture");
        public static Texture2D Illum = ImageUtils.GetTexture("SeaglideMK1.Illum");

        public static readonly List<Action<GameObject>> ModifyPrefabCallbacks = [];


        public static void Register()
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

            RamunesWorkbenchUtils.AddCraftNode(Prefab.Info.TechType, [RamunesWorkbenchUtils.Tabs.Equipment, "workbenchtabname".LangKeyAbbr()]);
        }
    }
}