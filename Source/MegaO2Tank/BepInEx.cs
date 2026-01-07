

namespace Ramune.MegaO2Tank
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class MegaO2Tank : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static MegaO2Tank Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.MegaO2Tank";
        public const string Name = "MegaO2Tank";
        public const string Version = "4.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/MegaO2Tank/Version.json"))
                return;
        }
    }
}