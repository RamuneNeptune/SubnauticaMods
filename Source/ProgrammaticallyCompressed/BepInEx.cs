

namespace Ramune.ProgrammaticallyCompressed
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class ProgrammaticallyCompressed : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static ProgrammaticallyCompressed Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.ProgrammaticallyCompressed";
        public const string Name = "ProgrammaticallyCompressed";
        public const string Version = "1.0.0";


        public Dictionary<(string Name, string Icon, string Path), List<TechType>> Tech = new()
        {
            {
                ("CompressedMinerals", "", "CompressedRawMaterials"), new() { TechType.CrashPowder, TechType.Copper, TechType.Sulphur, TechType.Diamond, TechType.Gold, TechType.PrecursorIonCrystal, TechType.Kyanite, TechType.Lead, TechType.Lithium, TechType.Magnetite, TechType.ScrapMetal, TechType.Nickel, TechType.Quartz, TechType.AluminumOxide, TechType.Salt, TechType.Silver, /*TechType.Titanium,*/ TechType.UraniniteCrystal }
            },
            {
                ("CompressedBiological", "", "CompressedRawMaterials"), new() { TechType.AcidMushroom, TechType.KooshChunk, TechType.CoralChunk, TechType.CreepvinePiece, TechType.CreepvineSeedCluster, TechType.WhiteMushroom, TechType.EyesPlantSeed, TechType.TreeMushroomPiece, TechType.GasPod, TechType.JellyPlant, TechType.RedGreenTentacleSeed, TechType.SeaCrownSeed, TechType.StalkerTooth, TechType.JeweledDiskPiece }
            }
        };


        public void Awake()
        {
            if(!Initializer.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/ProgrammaticallyCompressed/Version.json"))
                return;

            var compressionWorkbench = PrefabUtils.CreatePrefab("CompressionWorkbench", "Compression workbench", "Compression workbench.")
                .WithAutoUnlock()
                .WithFabricator(out craftTreeType, out var fabricator)
                .WithPDACategory(TechGroup.InteriorModules, TechCategory.InteriorModule)
                .WithRecipe(PrefabUtils.CreateRecipe(1, new Ingredient(TechType.PlasteelIngot, 1), new Ingredient(TechType.ComputerChip, 1)));

            var backgroundType = EnumHandler.AddEntry<BackgroundType>("PCBG")
                .WithBackground(ImageUtils.GetSprite("BG"));

            compressionWorkbench.SetGameObject(new FabricatorTemplate(compressionWorkbench.Info, craftTreeType)
            {
                FabricatorModel = FabricatorTemplate.Model.Workbench
            });

            foreach(var group in groups.Keys)
            {
                var groupCategories = groups[group];

                foreach(TechCategory category in groupCategories.Keys)
                {
                    if(category != TechCategory.Electronics && category != TechCategory.Water && category != TechCategory.CookedFood && category != TechCategory.CuredFood && category != TechCategory.BasicMaterials && category != TechCategory.AdvancedMaterials)
                        continue;

                    var categoryName1 = $"Compressed{category}";

                    fabricator.AddTabNode(categoryName1, $"Compressed {string.Concat(category.ToString().Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c : c.ToString())).ToLower()}", SpriteManager.Get(SpriteManager.Group.Category, "Fabricator_" + category.ToString()));

                    foreach(var techType in groupCategories[category])
                    {
                        var compressedPrefab = PrefabUtils.CreatePrefab(techType.ID() + "Compressed", techType.Name() + " (compressed)", techType.Desc(), ImageUtils.GetSprite(techType))
                            .WithRecipe(PrefabUtils.CreateRecipe(1, new Ingredient(techType, 10)), craftTreeType, categoryName1)
                            .WithAutoUnlock();

                        compressedPrefab.SetGameObject(new CloneTemplate(compressedPrefab.Info, techType));

                        compressedPrefab.Register();

                        var decompressedPrefab = PrefabUtils.CreatePrefab(techType.ID() + "Decompressed", techType.Name() + " (x10)", techType.Desc(), ImageUtils.GetSprite(techType))
                            .WithRecipe(PrefabUtils.CreateRecipe(0, new Ingredient(compressedPrefab.Info.TechType, 1), techType, techType, techType, techType, techType, techType, techType, techType, techType, techType))
                            .WithAutoUnlock();

                        decompressedPrefab.SetGameObject(new CloneTemplate(decompressedPrefab.Info, techType));

                        decompressedPrefab.Register();

                        TechTypeMap.Add(compressedPrefab.Info.TechType, decompressedPrefab.Info.TechType);

                        CraftDataHandler.SetBackgroundType(compressedPrefab.Info.TechType, backgroundType);
                    }
                }
            }

            fabricator.AddTabNode("CompressedRawMaterials", $"Compressed raw materials", SpriteManager.Get(TechType.None));

            foreach(var entry in Tech)
            {
                var category = entry.Key.Name;
                var root = entry.Key.Path;

                var categoryName1 = $"{category}";

                fabricator.AddTabNode(categoryName1, $"Compressed {string.Concat(category.ToString().Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c : c.ToString())).ToLower()}", SpriteManager.Get(SpriteManager.Group.Category, "Fabricator_" + category.ToString()), parentTabId:root);

                foreach(var techType in entry.Value)
                {
                    var compressedPrefab = PrefabUtils.CreatePrefab(techType.ID() + "Compressed", techType.Name() + " (compressed)", techType.Desc(), ImageUtils.GetSprite(techType))
                            .WithRecipe(PrefabUtils.CreateRecipe(1, new Ingredient(techType, 10)), craftTreeType, root, categoryName1)
                            .WithAutoUnlock();

                    compressedPrefab.SetGameObject(new CloneTemplate(compressedPrefab.Info, techType));

                    compressedPrefab.Register();

                    var decompressedPrefab = PrefabUtils.CreatePrefab(techType.ID() + "Decompressed", techType.Name() + " (x10)", techType.Desc(), ImageUtils.GetSprite(techType))
                        .WithRecipe(PrefabUtils.CreateRecipe(0, new Ingredient(compressedPrefab.Info.TechType, 1), techType, techType, techType, techType, techType, techType, techType, techType, techType, techType))
                        .WithAutoUnlock();

                    decompressedPrefab.SetGameObject(new CloneTemplate(decompressedPrefab.Info, techType));

                    decompressedPrefab.Register();

                    TechTypeMap.Add(compressedPrefab.Info.TechType, decompressedPrefab.Info.TechType);

                    CraftDataHandler.SetBackgroundType(compressedPrefab.Info.TechType, backgroundType);
                }
            }

            compressionWorkbench.Register();

            TechTypeMapReversed = TechTypeMap.ToDictionary(kv => kv.Value, kv => kv.Key);
        }


        public static Dictionary<TechType, TechType> TechTypeMap = new();

        public static Dictionary<TechType, TechType> TechTypeMapReversed = new();

        public static CraftTree.Type craftTreeType;
    }
}