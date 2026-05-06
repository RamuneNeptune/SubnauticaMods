

namespace Ramune.SimpleCoordinates
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class SimpleCoordinates : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static SimpleCoordinates Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.SimpleCoordinates";
        public const string Name = "SimpleCoordinates";
        public const string Version = "5.0.0";

        public static GameInput.Button ToggleCoordinatesDisplay = EnumHandler.AddEntry<GameInput.Button>("ramune.sc.togglecoordinatesdisplay")
            .CreateInput("Toggle Coordinates Display")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Numpad8)
            .WithCategory("Simple Coordinates")
            .AvoidConflicts();

        public static GameInput.Button ToggleTargetCoordinatesDisplay = EnumHandler.AddEntry<GameInput.Button>("ramune.sc.toggletargetcoordinatesdisplay")
            .CreateInput("Toggle Target Coordinates Display")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Numpad9)
            .WithCategory("Simple Coordinates")
            .AvoidConflicts();

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/SimpleCoordinates/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();
        }
    }
}