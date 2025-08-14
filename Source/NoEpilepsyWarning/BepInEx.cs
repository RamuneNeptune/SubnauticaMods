

namespace Ramune.NoEpilepsyWarning
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class NoEpilepsyWarning : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static NoEpilepsyWarning Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.NoEpilepsyWarning";
        public const string Name = "NoEpilepsyWarning";
        public const string Version = "4.0.1";

        public void Awake()
        {
            if(!Initializer.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/NoEpilepsyWarning/Version.json"))
                return;
        }
    }
}