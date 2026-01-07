

namespace Ramune.SeaglideUpgrades
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideUpgrades : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static SeaglideUpgrades Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.SeaglideUpgrades";
        public const string Name = "SeaglideUpgrades";
        public const string Version = "4.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/SeaglideUpgrades/Version.json"))
                return;
        }
    }
}