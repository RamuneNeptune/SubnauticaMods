

namespace Ramune.BuildableBrainCoral
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class BuildableBrainCoral : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static BuildableBrainCoral Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.BuildableBrainCoral";
        public const string Name = "BuildableBrainCoral";
        public const string Version = "1.0.1";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/BuildableBrainCoral/Version.json"))
                return;

            Items.BuildableBrainCoral.Patch();
        }
    }
}