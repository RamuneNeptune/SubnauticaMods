

namespace Ramune.BigFriendlyFish
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class BigFriendlyFish : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static BigFriendlyFish Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.BigFriendlyFish";
        public const string Name = "BigFriendlyFish";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/BigFriendlyFish/Version.json"))
                return;

            CraftTreeHandler.AddTabNode(CraftTree.Type.Constructor, "BFF", "Big friendly fish", ImageUtils.GetSprite(TechType.Peeper));

            var techTypeStrings = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Path.Combine(Paths.ConfigurationFolder, "BigFriendlyTechTypes.json")));

            foreach(var techTypeString in techTypeStrings)
            {
                var techType = TechType.None;

                if(!TechTypeExtensions.FromString(techTypeString, out techType, true))
                {
                    if(!EnumHandler.TryGetValue(techTypeString, out techType))
                        continue;
                }

                var prefab = PrefabUtils.CreatePrefab($"BigFriendly{techType.ID()}", $"Big friendly {techType.Name().ToLower()}", techType.Desc(), ImageUtils.GetSprite(techType))
                    .WithRecipe(PrefabUtils.CreateRecipe(1, [new(techType, 10)]), CraftTree.Type.Constructor, "BFF")
                    .WithEquipmentAndQuickSlotType(EquipmentType.None, QuickSlotType.Instant)
                    .WithPDACategory(TechGroup.Constructor, TechCategory.Constructor)
                    .WithAutoUnlock()
                    .WithSize(3, 3);

                prefab.SetGameObject(new CloneTemplate(prefab.Info, techType)
                {
                    ModifyPrefab = (go) =>
                    {
                        go.transform.localScale *= config.bigFriendlyScale;

                        var model = go.transform.Find("model");

                        if(model != null)
                            model.localScale *= config.bigFriendlyScale;
                    }
                });

                prefab.Register();

                Logfile.Info($"Registered prefab for: {techType.AsString()}");
            }
        }
    }
}