

namespace Ramune.BulkVending
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class BulkVending : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static BulkVending Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.BulkVending";
        public const string Name = "BulkVending";
        public const string Version = "1.0.0";

        public static GameInput.Button IncreaseAmount = EnumHandler.AddEntry<GameInput.Button>("ramune.bv.increaseamount")
            .CreateInput("Increase Amount To Produce")
            .WithKeyboardBinding(GameInputHandler.Paths.Mouse.ScrollUp)
            .WithControllerBinding(GameInputHandler.Paths.Gamepad.DpadUp)
            .WithCategory("Bulk Vending")
            .AvoidConflicts();


        public static GameInput.Button DecreaseAmount = EnumHandler.AddEntry<GameInput.Button>("ramune.bv.decreaseamount")
            .CreateInput("Decrease Amount To Produce")
            .WithKeyboardBinding(GameInputHandler.Paths.Mouse.ScrollDown)
            .WithControllerBinding(GameInputHandler.Paths.Gamepad.DpadDown)
            .WithCategory("Bulk Vending")
            .AvoidConflicts();

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/BulkVending/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();
        }


        public static bool HasEnoughPower(PowerRelay powerRelay, float amount) => amount <= 0f || powerRelay != null && powerRelay.GetPower() >= amount;


        public static bool TryConsumePower(PowerRelay powerRelay, float amount) => amount <= 0f || powerRelay != null && HasEnoughPower(powerRelay, amount) && powerRelay.ConsumeEnergy(amount, out _);
    }
}