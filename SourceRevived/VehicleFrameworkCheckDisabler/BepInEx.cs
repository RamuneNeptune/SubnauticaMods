

namespace Ramune.VehicleFrameworkCheckDisabler
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class VehicleFrameworkCheckDisabler : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static VehicleFrameworkCheckDisabler Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.VehicleFrameworkCheckDisabler";
        public const string Name = "VehicleFrameworkCheckDisabler";
        public const string Version = "1.0.1";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/VehicleFrameworkCheckDisabler/Version.json"))
                return;
        }
    }
}