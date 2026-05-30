

namespace Ramune.ScannerBlipIcons
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class ScannerBlipIcons : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static ScannerBlipIcons Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.ScannerBlipIcons";
        public const string Name = "ScannerBlipIcons";
        public const string Version = "1.0.0";


        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/ScannerBlipIcons/Version.json"))
                return;

            RefreshBlacklist();
        }


        public static readonly HashSet<TechType> BlacklistedTechTypes = [];


        public static void RefreshBlacklist()
        {
            BlacklistedTechTypes.Clear();

            var path = Path.Combine(Paths.ConfigurationFolder, "BlacklistedTechTypes.json");

            if(!File.Exists(path))
                return;

            var techTypeStrings = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(path));

            if(techTypeStrings == null)
                return;

            foreach(var techTypeString in techTypeStrings)
            {
                if(string.IsNullOrWhiteSpace(techTypeString))
                    continue;

                if(!TechTypeExtensions.FromString(techTypeString, out TechType techType, true) && !EnumHandler.TryGetValue(techTypeString, out techType))
                    continue;

                BlacklistedTechTypes.Add(techType);
            }
        }
    }
}