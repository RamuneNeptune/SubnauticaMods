

namespace Ramune.SeaglideBoosting
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideBoosting : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static SeaglideBoosting Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.SeaglideBoosting";
        public const string Name = "SeaglideBoosting";
        public const string Version = "5.0.0";

        public static GameInput.Button Boost = EnumHandler.AddEntry<GameInput.Button>("ramune.sb.boostseaglide")
            .CreateInput("Activate Boost (Hold)")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.LeftShift)
            .WithControllerBinding(GameInputHandler.Paths.Gamepad.RightStick)
            .WithCategory("Seaglide Boosting")
            .AvoidConflicts();

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/SeaglideBoosting/Version.json"))
                return;
        }
    }
}