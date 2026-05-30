

namespace Ramune.ArchonCheckDisabler
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInDependency("com.ironfox.subnautica.archon.mod")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class ArchonCheckDisabler : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static ArchonCheckDisabler Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.ArchonCheckDisabler";
        public const string Name = "ArchonCheckDisabler";
        public const string Version = "1.0.1";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/ArchonCheckDisabler/Version.json"))
                return;
        }
    }
}