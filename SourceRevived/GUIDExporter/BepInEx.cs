

namespace Ramune.GUIDExporter
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class GUIDExporter : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static GUIDExporter Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.GUIDExporter";
        public const string Name = "GUIDExporter";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/GUIDExporter/Version.json"))
                return;

            StartCoroutine(WaitForChainloader());
        }


        public static IEnumerator WaitForChainloader()
        {
            yield return PatchingUtils.WaitForChainloader();

            var o = new Dictionary<string, Version>();
            Chainloader.PluginInfos.ForEach(x => o[x.Key] = x.Value.Metadata.Version);

            File.WriteAllText(Path.Combine(Paths.PluginFolder, "GUIDs.json"), JsonConvert.SerializeObject(o, Formatting.Indented));
        }
    }
}