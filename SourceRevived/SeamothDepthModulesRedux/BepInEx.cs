

namespace Ramune.SeamothDepthModulesRedux
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInDependency("com.ramune.RamunesWorkbench", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class SeamothDepthModulesRedux : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static SeamothDepthModulesRedux Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.SeamothDepthModulesRedux";
        public const string Name = "SeamothDepthModulesRedux";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/SeamothDepthModulesRedux/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            Prefabs.UpgradeModules.SeamothDepthModuleMK4.Register();
            Prefabs.UpgradeModules.SeamothDepthModuleMK5.Register();
        }
    }
}