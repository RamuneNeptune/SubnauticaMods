

namespace Ramune.SeaglideUpgrades.Prefabs.Deployables
{
    public static class SeaglideMK2
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideMK2", "seaglidemk2.name".LangKeyAbbr(), "seaglidemk2.desc".LangKeyAbbr(), ImageUtils.GetSprite("SeaglideMK2.Sprite"))
            .WithJsonRecipe("SeaglideMK2")
            .WithEquipment(EquipmentType.Hand)
            .WithUnlock(TechType.Seaglide)
            .WithSize(2, 3);

        public static Texture2D Texture = ImageUtils.GetTexture("SeaglideMK2.Texture");
        public static Texture2D Illum = ImageUtils.GetTexture("SeaglideMK2.Illum");

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