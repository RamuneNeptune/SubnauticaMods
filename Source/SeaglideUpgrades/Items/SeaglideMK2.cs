

namespace Ramune.SeaglideUpgrades.Items
{
    public static class SeaglideMK2
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefab("SeaglideMK2", "Seaglide <color=#bde170>MK2</color>", "SPEED: +25%\nConverts torque into thrust underwater via propeller.", ImageUtils.GetSprite("SeaglideMK2.Sprite"))
            .WithJsonRecipe("SeaglideMK2")
            .WithEquipment(EquipmentType.Hand)
            .WithUnlock(TechType.Seaglide)
            .WithSize(2, 3);

        public static Texture2D Texture = ImageUtils.GetTexture("SeaglideMK2.Texture");
        public static Texture2D Illum = ImageUtils.GetTexture("SeaglideMK2.Illum");


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

            Patches.PlayerToolPatches.ModdedSeaglideTechTypes.Add(techType, () => SeaglideUpgrades.SetSeaglideSpeed(50f, 50f, SeaglideUpgrades.config.speedmk2));

            RamunesWorkbenchUtils.AddCraftNode(techType, [RamunesWorkbenchUtils.Tabs.Equipment, "Seaglides"]);
        }
    }
}