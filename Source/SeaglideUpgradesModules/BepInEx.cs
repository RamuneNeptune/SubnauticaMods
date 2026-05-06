

namespace Ramune.SeaglideUpgradesModules
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInDependency("com.ramune.RamunesWorkbench")]
    [BepInDependency("com.ramune.SeaglideUpgrades")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class SeaglideUpgradesModules : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static SeaglideUpgradesModules Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.SeaglideUpgradesModules";
        public const string Name = "SeaglideUpgrades:Modules";
        public const string Version = "1.0.0";


        public static GameInput.Button OpenModuleStorage = EnumHandler.AddEntry<GameInput.Button>("ramune.sgum.openmodulestorage")
            .CreateInput("Open Module Storage")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.Y)
            .WithControllerBinding(GameInputHandler.Paths.Gamepad.Start, GameInputHandler.Paths.Gamepad.ButtonNorth)
            .WithCategory("Seaglide Upgrades: Modules")
            .AvoidConflicts();

        public static GameInput.Button OpenBatteryUpgradeStorage = EnumHandler.AddEntry<GameInput.Button>("ramune.sgum.openbatteryupgradestorage")
            .CreateInput("Open Battery Upgrade Storage")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.U)
            .WithControllerBinding(GameInputHandler.Paths.Gamepad.Start, GameInputHandler.Paths.Gamepad.ButtonWest)
            .WithCategory("Seaglide Upgrades: Modules")
            .AvoidConflicts();

        public static GameInput.Button UseBoostUpgrade = EnumHandler.AddEntry<GameInput.Button>("ramune.sgum.useboostupgrade")
            .CreateInput("Activate Boost Upgrade (Hold)")
            .WithKeyboardBinding(GameInputHandler.Paths.Keyboard.LeftShift)
            .WithControllerBinding(GameInputHandler.Paths.Gamepad.RightStick)
            .WithCategory("Seaglide Upgrades: Modules")
            .AvoidConflicts();


        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/SeaglideUpgradesModules/Version.json"))
                return;

            if(GameInput.GetButtonHeld(OpenModuleStorage))
            {

            }

            LanguageHandler.RegisterLocalizationFolder();

            Items.BatterySwapUpgrade.Patch();
            Items.BoostUpgrade.Patch();
            Items.EfficiencyUpgrade.Patch();
            Items.LightUpgrade.Patch();
            Items.NoiseDampeningUpgrade.Patch();
            Items.PowerglideUpgrade.Patch();
            Items.SpeedUpgrade.Patch();

            CompatUtils.RegisterOnChainloaderFinishedEvent(() =>
            {
                SeaglideUpgrades.Items.SeaglideMK1.ModifyPrefabCallbacks.Add(go =>
                {
                    var moduleRoot = new GameObject("SeaglideModulesRoot");
                    moduleRoot.transform.SetParent(go.transform, false);

                    var batteryRoot = new GameObject("SeaglideBatteriesRoot");
                    batteryRoot.transform.SetParent(go.transform, false);

                    var upgradeManager = go.EnsureComponent<Monos.SeaglideUpgradeManager>();
                    upgradeManager.techType = SeaglideUpgrades.Items.SeaglideMK1.Prefab.Info.TechType;

                    var moduleStorage = Utility.PrefabUtils.AddStorageContainer(moduleRoot, "SeaglideModules", "SeaglideModulesRoot", config.ModuleStorageWidthMk1, config.ModuleStorageHeightMk1, true);
                    upgradeManager.moduleStorage = moduleStorage;

                    var batteryStorage = Utility.PrefabUtils.AddStorageContainer(batteryRoot, "SeaglideBatteries", "SeaglideBatteriesRoot", 1, 1, true);
                    upgradeManager.batteryStorage = batteryStorage;
                });

                SeaglideUpgrades.Items.SeaglideMK2.ModifyPrefabCallbacks.Add(go =>
                {
                    var moduleRoot = new GameObject("SeaglideModulesRoot");
                    moduleRoot.transform.SetParent(go.transform, false);

                    var batteryRoot = new GameObject("SeaglideBatteriesRoot");
                    batteryRoot.transform.SetParent(go.transform, false);

                    var upgradeManager = go.EnsureComponent<Monos.SeaglideUpgradeManager>();
                    upgradeManager.techType = SeaglideUpgrades.Items.SeaglideMK2.Prefab.Info.TechType;

                    var moduleStorage = Utility.PrefabUtils.AddStorageContainer(moduleRoot, "SeaglideModules", "SeaglideModulesRoot", config.ModuleStorageWidthMk2, config.ModuleStorageHeightMk2, true);
                    upgradeManager.moduleStorage = moduleStorage;

                    var batteryStorage = Utility.PrefabUtils.AddStorageContainer(batteryRoot, "SeaglideBatteries", "SeaglideBatteriesRoot", 1, 1, true);
                    upgradeManager.batteryStorage = batteryStorage;
                });

                SeaglideUpgrades.Items.SeaglideMK3.ModifyPrefabCallbacks.Add(go =>
                {
                    var moduleRoot = new GameObject("SeaglideModulesRoot");
                    moduleRoot.transform.SetParent(go.transform, false);

                    var batteryRoot = new GameObject("SeaglideBatteriesRoot");
                    batteryRoot.transform.SetParent(go.transform, false);

                    var upgradeManager = go.EnsureComponent<Monos.SeaglideUpgradeManager>();
                    upgradeManager.techType = SeaglideUpgrades.Items.SeaglideMK3.Prefab.Info.TechType;

                    var moduleStorage = Utility.PrefabUtils.AddStorageContainer(moduleRoot, "SeaglideModules", "SeaglideModulesRoot", config.ModuleStorageWidthMk3, config.ModuleStorageHeightMk3, true);
                    upgradeManager.moduleStorage = moduleStorage;

                    var batteryStorage = Utility.PrefabUtils.AddStorageContainer(batteryRoot, "SeaglideBatteries", "SeaglideBatteriesRoot", 1, 1, true);
                    upgradeManager.batteryStorage = batteryStorage;
                });

                SeaglideUpgrades.Monos.SeaglideLightControllerManager.ModifyLightsCallbacks.Add((controller, settings) =>
                {
                    if(controller == null || settings == null)
                        return;

                    if(!controller.TryGetComponent<Monos.SeaglideUpgradeManager>(out var manager))
                        return;

                    settings.range *= manager.GetLightRangeMultiplier();
                    settings.intensity *= manager.GetLightIntensityMultiplier();
                    settings.conesize *= manager.GetLightConesizeMultiplier();
                });
            });
        }
    }
}