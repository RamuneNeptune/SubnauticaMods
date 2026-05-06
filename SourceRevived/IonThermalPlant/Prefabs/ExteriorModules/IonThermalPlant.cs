

namespace Ramune.IonThermalPlant.Prefabs.ExteriorModules
{
    public static class IonThermalPlant
    {
        public static CustomPrefab Prefab = PrefabUtils.CreatePrefabWithLocalization("IonThermalPlant")
            .WithPDACategoryAfter(TechGroup.ExteriorModules, TechCategory.ExteriorModule, TechType.ThermalPlant)
            .WithUnlock(TechType.PrecursorIonBattery)
            .WithJsonRecipe("IonThermalPlant");

        public static void Register()
        {
            Prefab.SetGameObject(new CloneTemplate(Prefab.Info, TechType.ThermalPlant)
            {
                ModifyPrefab = (go) =>
                {
                    var modelRoot = go.FindChild("model").FindChild("root");
                    var headRenderer = modelRoot.FindChild("head").FindChild("Thermal_reactor_head").GetComponent<MeshRenderer>();
                    var bodyRenderer = modelRoot.FindChild("Thermal_reactor_body").GetComponent<MeshRenderer>();

                    headRenderer.material.mainTexture = ImageUtils.GetTexture("IonThermalPlantTexture");
                    headRenderer.material.SetTexture("_SpecTex", ImageUtils.GetTexture("IonThermalPlantTexture"));
                    headRenderer.material.SetColor("_GlowColor", Color.green);
                    headRenderer.material.SetFloat("_GlowStrength", 4f);

                    bodyRenderer.material.mainTexture = ImageUtils.GetTexture("IonThermalPlantTexture");
                    bodyRenderer.material.SetTexture("_SpecTex", ImageUtils.GetTexture("IonThermalPlantTexture"));
                    bodyRenderer.material.SetColor("_GlowColor", Color.green);
                    bodyRenderer.material.SetFloat("_GlowStrength", 4f);

                    bodyRenderer.materials[1].mainTexture = ImageUtils.GetTexture("IonThermalPlantScreenTexture");
                    bodyRenderer.materials[1].SetTexture("_Illum", ImageUtils.GetTexture("IonThermalPlantScreenTexture"));

                    go.GetComponent<PowerSource>().maxPower = Ramune.IonThermalPlant.IonThermalPlant.config.powerMaxCapacity;
                }
            });

            Prefab.Register();
        }
    }
}