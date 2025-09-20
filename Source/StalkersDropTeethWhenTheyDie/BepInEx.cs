

namespace Ramune.StalkersDropTeethWhenTheyDie
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class StalkersDropTeethWhenTheyDie : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static StalkersDropTeethWhenTheyDie Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.StalkersDropTeethWhenTheyDie";
        public const string Name = "StalkersDropTeethWhenTheyDie";
        public const string Version = "4.0.1";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/StalkersDropTeethWhenTheyDie/Version.json"))
                return;
        }
    }
}