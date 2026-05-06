

namespace Ramune.PrawnSuitLightSwitch
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class PrawnSuitLightSwitch : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static PrawnSuitLightSwitch Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.PrawnSuitLightSwitch";
        public const string Name = "PrawnSuitLightSwitch";
        public const string Version = "5.0.0";

        public static GameInput.Button ToggleLights = EnumHandler.AddEntry<GameInput.Button>("ramune.psls.togglelights")
            .CreateInput("Toggle Lights")
            .WithKeyboardBinding(GameInputHandler.Paths.Mouse.MiddleButton)
            .WithCategory("Prawn Suit Light Switch")
            .AvoidConflicts();

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/PrawnSuitLightSwitch/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();
        }
    }
}