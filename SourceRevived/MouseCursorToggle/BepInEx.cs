

namespace Ramune.MouseCursorToggle
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class MouseCursorToggle : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static MouseCursorToggle Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.MouseCursorToggle";
        public const string Name = "MouseCursorToggle";
        public const string Version = "1.0.0";

        public static GameInput.Button EnterCursorMode = EnumHandler.AddEntry<GameInput.Button>("ramune.mct.togglecursormode")
            .CreateInput("Enter Cursor Mode")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Backquote)
            .WithControllerBinding("None")
            .WithCategory("Mouse Cursor Toggle")
            .AvoidConflicts();

        public static GameInput.Button ExitCursorMode = EnumHandler.AddEntry<GameInput.Button>("ramune.mct.exitcursormode")
            .CreateInput("Exit Cursor Mode")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Escape)
            .WithControllerBinding("None")
            .WithCategory("Mouse Cursor Toggle")
            .AvoidConflicts();

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/MouseCursorToggle/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();
        }
    }
}