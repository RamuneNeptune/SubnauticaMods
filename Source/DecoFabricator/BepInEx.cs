

namespace Ramune.DecoFabricator
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class DecoFabricator : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static DecoFabricator Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.DecoFabricator";
        public const string Name = "DecoFabricator";
        public const string Version = "4.0.0";

        public void Awake()
        {
            if(!Initializer.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/DecoFabricator/Version.json"))
                return;

            var prefab = PrefabUtils.CreatePrefab("DecoFabricator", "Decorations fabricator", "Used to fabricate posters, toys, caps, and more.", ImageUtils.GetSprite(TechType.Fabricator))
                .WithPDACategoryAfter(TechGroup.InteriorModules, TechCategory.InteriorModule, TechType.Fabricator)
                .WithFabricator(out var craftTreeType, out var fabricator)
                .WithJsonRecipe("DecoFabricator")
                .WithAutoUnlock();

            var clone = new FabricatorTemplate(prefab.Info, craftTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Fabricator,
                ColorTint = Color.green
            };

            prefab.SetGameObject(clone);
            prefab.Register();

            (string tabName, TechType[] tabItems, Sprite tabSprite)[] TechMap = new[]
            {
                ("Posters", new TechType[] { TechType.PosterKitty, TechType.Poster, TechType.PosterExoSuit1, TechType.PosterExoSuit2, TechType.PosterAurora }, ImageUtils.GetSprite(TechType.PosterKitty)),
                ("Science", new TechType[] { TechType.LabEquipment3, TechType.LabEquipment2, TechType.LabEquipment1, TechType.LabContainer, TechType.LabContainer2, TechType.LabContainer3 }, ImageUtils.GetSprite(TechType.LabEquipment3)),
                ("Toys", new TechType[] { TechType.ArcadeGorgetoy, TechType.ToyCar, TechType.StarshipSouvenir }, ImageUtils.GetSprite(TechType.ArcadeGorgetoy)),
                ("Caps", new TechType[] { TechType.Cap2, TechType.Cap1 }, ImageUtils.GetSprite(TechType.Cap2))
            };

            foreach(var(tabName, tabItems, tabSprite) in TechMap)
            {
                fabricator.AddTabNode(tabName, tabName, tabSprite);

                foreach(var techType in tabItems)
                {
                    KnownTechHandler.UnlockOnStart(techType);
                    fabricator.AddCraftNode(techType, tabName);
                }
            }
        }
    }
}