﻿

namespace Ramune.KeepMyDamnSeaglideLightOffWhenSwitchingBattery
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class KeepMyDamnSeaglideLightOffWhenSwitchingBattery : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static KeepMyDamnSeaglideLightOffWhenSwitchingBattery Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.KeepMyDamnSeaglideLightOffWhenSwitchingBattery";
        public const string Name = "KeepMyDamnSeaglideLightOffWhenSwitchingBattery";
        public const string Version = "1.0.0";

        public void Awake()
        {
            ModMessageSystem.SendGlobal("FindMyUpdates", "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/KeepMyDamnSeaglideLightOffWhenSwitchingBattery/Version.json");

            if(!config.EnableThisMod)
            {
                Logfile.Warning("This mod has been disabled in the config and will not be loaded");
                return;
            }

            Initializer.Initialize(harmony, Logger, Name, Version);
        }
    }
}