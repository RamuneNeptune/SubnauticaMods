

namespace Ramune.NoPassiveScannerRoomPowerDrain
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class NoPassiveScannerRoomPowerDrain : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static NoPassiveScannerRoomPowerDrain Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.NoPassiveScannerRoomPowerDrain";
        public const string Name = "NoPassiveScannerRoomPowerDrain";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!Initializer.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/NoPassiveScannerRoomPowerDrain/Version.json"))
                return;
        }
    }
}