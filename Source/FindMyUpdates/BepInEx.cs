

namespace Ramune.FindMyUpdates
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class FindMyUpdates : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static FindMyUpdates Instance;
        public static ManualLogSource logger => Instance.Logger;
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.FindMyUpdates";
        public const string Name = "FindMyUpdates";
        public const string Version = "1.0.4";

        public void Awake()
        {
            ModMessageSystem.SendGlobal("FindMyUpdates", "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/FindMyUpdates/Version.json");

            if(!config.EnableThisMod)
            {
                Logfile.Warning("This mod has been disabled in the config and will not be loaded");
                return;
            }

            Initializer.Initialize(harmony, Logger, Name, Version);
            LanguageHandler.RegisterLocalizationFolder();

            CoroutineHost.StartCoroutine(WaitToCheckUpdates());
        }

        public static IEnumerator WaitToCheckUpdates()
        {
            yield return PatchingUtils.WaitForChainloader();

            var inbox = new ModInbox("FindMyUpdates", true);

            ModMessageSystem.RegisterInbox(inbox);

            var reader = new BasicModMessageReader("FindMyUpdates", args =>
            {
                CoroutineHost.StartCoroutine(Function.Validate(args));
            });

            inbox.AddMessageReader(reader);

            inbox.ReadAnyHeldMessages();
        }
    }
}