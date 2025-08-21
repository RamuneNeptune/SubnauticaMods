

namespace Ramune.FindMyUpdates
{
    [BepInDependency("com.snmodding.nautilus")]
    [BepInPlugin(GUID, Name, Version)]
    [BepInProcess("Subnautica.exe")]
    public class FindMyUpdates : BaseUnityPlugin
    {
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();
        public static readonly Harmony harmony = new(GUID);
        public const string GUID = "com.ramune.FindMyUpdates";
        public const string Name = "FindMyUpdates";
        public const string Version = "1.0.7";

        public void Awake()
        {
            if(!this.Initialize(harmony, Logger, Name, Version, config.EnableThisMod, "https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/refs/heads/main/Source/FindMyUpdates/Version.json"))
                return;

            LanguageHandler.RegisterLocalizationFolder();

            SceneUtils.RegisterOnMenuEnvironmentLoaded(() =>
            {
                if(Patches.uGUI_OptionsPanelPatch.ShouldNotify && config.MainMenuNotice && Hint.main != null)
                {
                    var outdatedMessage = Hint.main.message;
                    outdatedMessage.ox = 60f;
                    outdatedMessage.oy = 0f;
                    outdatedMessage.anchor = TextAnchor.MiddleLeft;
                    outdatedMessage.SetBackgroundColor(Color.cyan);
                    outdatedMessage.SetText("Some mods are outdated. Check the updates tab.", TextAnchor.UpperCenter);
                    outdatedMessage.Show(config.MainMenuNoticeDuration, 0f, 0.25f, 0.25f, null);
                }
            });

            StartCoroutine(WaitToCheckUpdates());
        }

        public static IEnumerator WaitToCheckUpdates()
        {
            yield return PatchingUtils.WaitForChainloader();

            var inbox = new ModInbox("FindMyUpdates", true);

            ModMessageSystem.RegisterInbox(inbox);

            var reader = new BasicModMessageReader("FindMyUpdates", args => CoroutineHost.StartCoroutine(Function.Validate(args)));

            inbox.AddMessageReader(reader);

            inbox.ReadAnyHeldMessages();
        }
    }
}