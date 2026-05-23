

namespace Ramune.Headlamp
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class Headlamp : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static Headlamp Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.Headlamp";
        public const string Name = "Headlamp";
        public const string Version = "1.0.0";

        public static GameInput.Button ToggleHeadlamp = EnumHandler.AddEntry<GameInput.Button>("ramune.h.toggleheadlamp")
            .CreateInput("Toggle Headlamp")
            .WithKeyboardBinding(GameInputHandler.Paths.Mouse.RightButton)
            .WithControllerBinding("None")
            .WithCategory("Headlamp")
            .AvoidConflicts();

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/Headlamp/Version.json"))
                return;

            RamunesWorkbenchUtils.AddTabNode("Headwear", ImageUtils.GetSprite(TechType.Rebreather), RamunesWorkbenchUtils.Tabs.Equipment);

            Prefabs.Equipment.Headlamp.Register();
        }
    }
}