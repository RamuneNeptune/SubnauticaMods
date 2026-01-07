

namespace Ramune.CustomizableLights
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class CustomizableLights : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static CustomizableLights Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.CustomizableLights";
        public const string Name = "CustomizableLights";
        public const string Version = "4.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/CustomizableLights/Version.json"))
                return;
        }
    }
}