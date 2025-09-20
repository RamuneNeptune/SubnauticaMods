

namespace Ramune.EnableAchievements
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class EnableAchievements : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static EnableAchievements Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.EnableAchievements";
        public const string Name = "EnableAchievements";
        public const string Version = "4.0.3";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/EnableAchievements/Version.json"))
                return;

            ConsoleCommandsHandler.RegisterConsoleCommands(typeof(ConsoleCommands));
        }
    }
}