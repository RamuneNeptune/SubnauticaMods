

namespace Ramune.ProgrammaticallyCompressed
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class ProgrammaticallyCompressed : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.ProgrammaticallyCompressed";
        public const string Name = "ProgrammaticallyCompressed";
        public const string Version = "1.0.0";


        public class NodeInfo
        {
            public string Name { get; set; }
            public string Sprite { get; set; }
            public string Parent { get; set; }
            public List<string> Items { get; set; }
        }


        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/ProgrammaticallyCompressed/Version.json"))
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


            var filePathsToRead = Directory.GetFiles(Path.Combine(Paths.ConfigurationFolder, "Nodes"), "*.json")
                .Where(filePath => !filePath.StartsWith("Order"));


            var orderedFilePaths = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(Path.Combine(Paths.ConfigurationFolder, "Order.json")))["Order"]
             .Select(name => filePathsToRead.FirstOrDefault(f => Path.GetFileName(f).Equals(name, StringComparison.OrdinalIgnoreCase)))
             .Where(x => x != null);


            var nodes = new Dictionary<string, NodeInfo>();


            foreach(var filePath in orderedFilePaths)
            {
                try
                {
                    JsonConvert.DeserializeObject<Dictionary<string, NodeInfo>>(File.ReadAllText(filePath)).ForEach(x => nodes[x.Key] = x.Value);
                }
                catch(Exception ex)
                {
                    Logfile.Error($"Failed to read .json file at: {filePath}\n\n{ex.Message}");
                }
            }


            foreach(var node in nodes)
            {
                (string id, string name, string sprite, string parent, List<string> items) = (node.Key, node.Value.Name, node.Value.Sprite, node.Value.Parent, node.Value.Items);
                
                fabricator.AddTabNode(id, name, sprite.IsNullOrWhiteSpace() ? ImageUtils.GetSprite(TechType.None) : sprite.StartsWith("Fabricator_") ? SpriteManager.Get(SpriteManager.Group.Category, sprite, ImageUtils.GetSprite(TechType.None)) : sprite.StartsWith("TechType_") ? ImageUtils.GetSprite((TechType)Enum.Parse(typeof(TechType), sprite.Replace("TechType_", ""))) : ImageUtils.GetSprite(sprite), parentTabId: parent.IsNullOrWhiteSpace() ? null : parent);

                var techTypes = items
                    .Select(techTypeString => Enum.TryParse<TechType>(techTypeString, true, out var techType) ? techType : EnumHandler.TryGetValue<TechType>(techTypeString, out var moddedTechType) ? moddedTechType : (TechType?)null)
                    .Where(techType => techType.HasValue)
                    .Select(techType => techType.Value);

                foreach(var techType in techTypes)
                {
                    var tabPath = GetTabPath(id, nodes);

                    var compressedPrefab = PrefabUtils.CreatePrefab(techType.ID() + "Compressed", techType.Name() + " (compressed)", techType.Desc(), ImageUtils.GetSprite(techType))
                        .WithRecipe(PrefabUtils.CreateRecipe(1, new Ingredient(techType, 10)), craftTreeType, tabPath)
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


        public static string[] GetTabPath(string tabId, Dictionary<string, NodeInfo> nodes) => tabId.IsNullOrWhiteSpace() || !nodes.TryGetValue(tabId, out var node) ? Array.Empty<string>() : GetTabPath(node.Parent, nodes).Append(tabId).ToArray();


        public static Dictionary<TechType, TechType> TechTypeMap = new();

        public static Dictionary<TechType, TechType> TechTypeMapReversed = new();

        public static CraftTree.Type craftTreeType;
    }
}