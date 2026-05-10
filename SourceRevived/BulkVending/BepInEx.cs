

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

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/BulkVending/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();
        }


        public static bool HasEnoughPower(PowerRelay powerRelay, float amount) => amount <= 0f || powerRelay != null && powerRelay.GetPower() >= amount;


        public static bool TryConsumePower(PowerRelay powerRelay, float amount) => amount <= 0f || powerRelay != null && powerRelay.ConsumeEnergy(amount, out _);
    }
}