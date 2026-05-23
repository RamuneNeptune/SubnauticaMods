

namespace Ramune.ModularSurvivalKnife
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class ModularSurvivalKnife : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static ModularSurvivalKnife Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.ModularSurvivalKnife";
        public const string Name = "ModularSurvivalKnife";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/ModularSurvivalKnife/Version.json"))
                return;

            RamunesWorkbenchUtils.AddTabNode(RamunesWorkbenchUtils.Tabs.Tools, ImageUtils.GetSprite(TechType.Knife), "Blades");
        }
    }
}