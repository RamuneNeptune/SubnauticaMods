

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
        public const string Version = "1.0.6";

        public void Awake()
        {
            if(!Initializer.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/FindMyUpdates/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            StartCoroutine(WaitToCheckUpdates());
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