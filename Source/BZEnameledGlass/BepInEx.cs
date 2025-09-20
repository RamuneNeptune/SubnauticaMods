

namespace Ramune.BZEnameledGlass
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class BZEnameledGlass : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static BZEnameledGlass Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.BZEnameledGlass";
        public const string Name = "BZEnameledGlass";
        public const string Version = "4.0.3";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/BZEnameledGlass/Version.json"))
                return;

            Items.EnameledGlassClone.Patch();
        }
    }
}