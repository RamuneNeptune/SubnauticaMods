

namespace Ramune.EasyScanUnlock
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class EasyScanUnlock : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static EasyScanUnlock Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.EasyScanUnlock";
        public const string Name = "EasyScanUnlock";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/EasyScanUnlock/Version.json"))
                return;

            ConsoleCommandsHandler.RegisterConsoleCommands(typeof(ConsoleCommands));

            // Someone asked for this and it was very easy so yah
        }
    }
}