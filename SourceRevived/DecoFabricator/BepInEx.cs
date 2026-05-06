

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
        public const string Version = "5.0.1";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/DecoFabricator/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            Prefabs.Buildables.DecoFabricator.Register();
        }
    }
}