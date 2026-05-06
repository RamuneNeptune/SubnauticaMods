

namespace Ramune.SeamothBoosting
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class SeamothBoosting : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static SeamothBoosting Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.SeamothBoosting";
        public const string Name = "SeamothBoosting";
        public const string Version = "5.0.0";

        public static GameInput.Button Boost = EnumHandler.AddEntry<GameInput.Button>("ramune.sb.boostseamoth")
            .CreateInput("Activate Boost (Hold)")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.LeftShift)
            .WithControllerBinding(GameInputHandler.Paths.Gamepad.RightStick)
            .WithCategory("Seamoth Boosting")
            .AvoidConflicts();


        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/SeamothSprint/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();
        }
    }
}