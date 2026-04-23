

namespace Ramune.SeaglideUpgrades.Items
{
    public static class SeaglideMK3
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideMK3", "Seaglide <color=#f81117>MK3</color>", "SPEED: +40%\nConverts torque into thrust underwater via propeller.", ImageUtils.GetSprite("SeaglideMK3.Sprite"))
            .WithJsonRecipe("SeaglideMK3")
            .WithEquipment(EquipmentType.Hand)
            .WithUnlock(TechType.Seaglide)
            .WithSize(2, 3);

        public static Texture2D Texture = ImageUtils.GetTexture("SeaglideMK3.Texture");
        public static Texture2D Illum = ImageUtils.GetTexture("SeaglideMK3.Illum");


        public static void Patch()
        {
            var clone = new CloneTemplate(Prefab.Info, TechType.Seaglide)
            {
                ModifyPrefab = go =>
                {
                    var renderers = go.GetComponentsInChildren<SkinnedMeshRenderer>(true);

                    if(SeaglideUpgrades.config.glossyBool)
                        renderers.ForEach(x => x.material.SetTexture(ShaderPropertyID._SpecTex, Texture));

                    renderers.ForEach(x => x.material.SetTexture(ShaderPropertyID._MainTex, Texture));
                    renderers.ForEach(x => x.material.SetTexture(ShaderPropertyID._Illum, Illum));
                }
            };

            Prefab.SetGameObject(clone);
            Prefab.Register();

            var techType = Prefab.Info.TechType;

            Patches.PlayerToolPatches.ModdedSeaglideTechTypes.Add(techType, () => SeaglideUpgrades.SetSeaglideSpeed(58f, 58f, SeaglideUpgrades.config.speedmk3));

            RamunesWorkbenchUtils.AddCraftNode(techType, [RamunesWorkbenchUtils.Tabs.Equipment, "Seaglides"]);
        }
    }
}