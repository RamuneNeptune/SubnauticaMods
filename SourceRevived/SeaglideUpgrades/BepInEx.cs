

namespace Ramune.SeaglideUpgrades
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideUpgrades : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static SeaglideUpgrades Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.SeaglideUpgrades";
        public const string Name = "SeaglideUpgrades";
        public const string Version = "5.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/SourceRevived/SeaglideUpgrades/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            RamunesWorkbenchUtils.AddTabNode("workbenchtabname".LangKeyAbbr(), SpriteManager.Get(TechType.Seaglide), RamunesWorkbenchUtils.Tabs.Equipment);

            Prefabs.Deployables.SeaglideMK1.Register();
            Prefabs.Deployables.SeaglideMK2.Register();
            Prefabs.Deployables.SeaglideMK3.Register();
        }


        public static void SetSeaglideSpeed(float speed, float accel, float multiplier)
        {
            var player = Player.main;
            var playerController = player?.playerController;

            if(!player || !playerController || !player.IsUnderwaterForSwimming())
                return;

            playerController.seaglideForwardMaxSpeed = speed * multiplier;
            playerController.seaglideWaterAcceleration = accel * multiplier;
            playerController.SetMotorMode(Player.MotorMode.Dive);
            player.UpdateMotorMode();
        }
    }
}