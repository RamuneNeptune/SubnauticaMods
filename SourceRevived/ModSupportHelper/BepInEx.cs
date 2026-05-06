

namespace Ramune.ModSupportHelper
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class ModSupportHelper : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static ModSupportHelper Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.ModSupportHelper";
        public const string Name = "ModSupportHelper";
        public const string Version = "1.0.0";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/ModSupportHelper/Version.json"))
                return;

            CompatUtils.RegisterOnChainloaderFinishedEvent(() =>
            {
                Logfile.Divider();

                foreach(var pluginInfo in Chainloader.PluginInfos.Values)
                    Logfile.Info($"{pluginInfo.Metadata.GUID} :: {pluginInfo.Metadata.Version}");

                Logfile.Divider();
            });
        }
    }
}