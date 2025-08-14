

namespace Ramune.NoUnderwaterCaustics
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class NoUnderwaterCaustics : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static NoUnderwaterCaustics Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.NoUnderwaterCaustics";
        public const string Name = "NoUnderwaterCaustics";
        public const string Version = "1.0.1";

        public void Awake()
        {
            if(!Initializer.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/NoUnderwaterCaustics/Version.json"))
                return;
        }
    }
}