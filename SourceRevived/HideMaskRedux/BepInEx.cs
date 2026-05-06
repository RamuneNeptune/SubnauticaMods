

namespace Ramune.HideMaskRedux
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class HideMaskRedux : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static HideMaskRedux Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.HideMaskRedux";
        public const string Name = "HideMaskRedux";
        public const string Version = "1.0.0";

        public static GameInput.Button SwitchMask = EnumHandler.AddEntry<GameInput.Button>("ramune.hmr.switchmask")
            .CreateInput("Switch Mask State")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Numpad1)
            .WithCategory("Hide Mask Redux")
            .AvoidConflicts();

        public static GameInput.Button SetMaskOn = EnumHandler.AddEntry<GameInput.Button>("ramune.hmr.setmaskon")
            .CreateInput("Set Mask State: On")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Numpad2)
            .WithCategory("Hide Mask Redux")
            .AvoidConflicts();

        public static GameInput.Button SetMaskOff = EnumHandler.AddEntry<GameInput.Button>("ramune.hmr.setmaskoff")
            .CreateInput("Set Mask State: Off")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Numpad3)
            .WithCategory("Hide Mask Redux")
            .AvoidConflicts();

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/HideMaskRedux/Version.json"))
                return;
        }
    }
}