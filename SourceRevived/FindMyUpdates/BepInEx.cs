

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
        public const string Version = "2.0.0";
        public static readonly HashSet<string> BlacklistedGUIDs = new(StringComparer.OrdinalIgnoreCase);
        public static string BlacklistPath => Path.Combine(Paths.ConfigurationFolder, "Blacklist.json");


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
                    outdatedMessage.SetText(string.Format("fmu.warning.mainmenuoutdated".LangKey(), "fmu.ui.tabname".LangKey()), TextAnchor.UpperCenter);
                    outdatedMessage.Show(config.MainMenuNoticeDuration, 0f, 0.25f, 0.25f, null);
                }
            });

            StartCoroutine(WaitToCheckUpdates());
        }

        public static Dictionary<string, Version> PluginInfos = [];

        public static readonly List<object[]> PendingValidationArgs = [];

        public static bool InitialValidationComplete = false;

        public static IEnumerator WaitToCheckUpdates()
        {
            yield return PatchingUtils.WaitForChainloader();

            Chainloader.PluginInfos.Values.ForEach(x => PluginInfos.Add(x.Metadata.GUID, x.Metadata.Version));

            LoadBlacklist();

            foreach(string guid in BlacklistedGUIDs)
                if(Chainloader.PluginInfos.TryGetValue(guid, out var pluginInfo))
                    Patches.uGUI_OptionsPanelPatch.RegisterBlacklistedMod(guid, pluginInfo.Metadata.Name, pluginInfo.Metadata.Version);

            var inbox = new ModInbox("FindMyUpdates", true);

            ModMessageSystem.RegisterInbox(inbox);

            var reader = new BasicModMessageReader("FindMyUpdates", args =>
            {
                if(InitialValidationComplete)
                {
                    CoroutineHost.StartCoroutine(Function.Validate(args));
                }
                else
                {
                    PendingValidationArgs.Add(args);
                }
            });

            inbox.AddMessageReader(reader);

            inbox.ReadAnyHeldMessages();

            foreach(var args in PendingValidationArgs)
                yield return Function.Validate(args);

            PendingValidationArgs.Clear();
            InitialValidationComplete = true;

            yield return Function.CheckGUIDUpdates(null);
        }


        public static bool IsGUIDBlacklisted(string guid) => !guid.IsNullOrWhiteSpace() && BlacklistedGUIDs.Contains(guid);


        public static bool AddGUIDToBlacklist(string guid)
        {
            if(guid.IsNullOrWhiteSpace() || !BlacklistedGUIDs.Add(guid))
                return false;

            SaveBlacklist();
            return true;
        }


        public static bool RemoveGUIDFromBlacklist(string guid)
        {
            if(guid.IsNullOrWhiteSpace() || !BlacklistedGUIDs.Remove(guid))
                return false;

            SaveBlacklist();
            return true;
        }


        public static void LoadBlacklist()
        {
            BlacklistedGUIDs.Clear();

            if(!File.Exists(BlacklistPath))
            {
                SaveBlacklist();
                return;
            }

            try
            {
                string[] entries = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(BlacklistPath));

                if(entries == null)
                    return;

                foreach(string entry in entries)
                    if(!entry.IsNullOrWhiteSpace())
                        BlacklistedGUIDs.Add(entry.Trim());
            }
            catch(Exception ex) when(ex is IOException || ex is UnauthorizedAccessException || ex is JsonException)
            {
                Logfile.Error($"Failed to load blacklist from '{BlacklistPath}'.");
                Logfile.Error(ex.Message);
            }
        }


        public static void SaveBlacklist()
        {
            try
            {
                Directory.CreateDirectory(Paths.ConfigurationFolder);
                File.WriteAllText(BlacklistPath, JsonConvert.SerializeObject(BlacklistedGUIDs.OrderBy(x => x), Formatting.Indented));
            }
            catch(Exception ex) when(ex is IOException || ex is UnauthorizedAccessException || ex is JsonException)
            {
                Logfile.Error($"Failed to save blacklist to '{BlacklistPath}'.");
                Logfile.Error(ex.Message);
            }
        }
    }
}