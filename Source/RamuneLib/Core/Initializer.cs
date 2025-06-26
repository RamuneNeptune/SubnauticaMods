

namespace RamuneLib
{
    /// <summary>
    /// 
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="harmony"></param>
        /// <param name="logger"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="patchAll"></param>
        public static void Initialize(Harmony harmony, ManualLogSource logger, string name, string version, bool patchAll = true)
        {
            Variables.name = name;
            Variables.harmony = harmony;
            Variables.logger = logger;

            if(Piracy.Piracy.Exists())
                return;

            CoroutineHost.StartCoroutine(PatchingUtils.WaitForChainloader());

            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if(isWindows) 
                Logfile.Info($"{(isWindows ? "\x1b[32m" : "")}[{name} {version}]{(isWindows ? "\x1b[0m" : "")}");

            if(patchAll)
            {
                Logfile.Info($"Loading harmony patches for '{name} {version}'");
                    
                harmony.PatchAll();

                Logfile.Info($"Loaded harmony patches for '{name} {version}'");
            }
        }


        public static void UpdateCheck(string modName, string currentVersion, bool shouldRun)
        {
            if(!shouldRun)
                return;

            CoroutineHost.StartCoroutine(UpdateCheckAsync(modName, currentVersion));
        }


        public static IEnumerator UpdateCheckAsync(string modName, string currentVersion)
        {
            var url = $"https://raw.githubusercontent.com/RamuneNeptune/SubnauticaMods/main/Source/{modName}/Version.json";

            var request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if(request.responseCode == 404)
                yield break;

            if(request.isNetworkError || request.isHttpError)
            {
                Logfile.Error($"Failed at 1/3 to check for update (network/http error). If you are playing offline, you should disable checking for updates in the config: {request.error}");
                yield break;
            }

            var text = request.downloadHandler.text;

            if(text == null)
            {
                Logfile.Error($"Failed at 2/3 to read text from request while checking for updates (the text is null)");
                yield break;
            }

            var modData = JsonConvert.DeserializeObject<ModData>(text);

            if(modData == null)
            {
                Logfile.Error($"Failed at 3/3 to serialize text as ModData while checking for updates (parsing the .json as ModData failed)");
                yield break;
            }

            Patches.PlayerPatch.modData = modData;

            var current = new Version(currentVersion);
            var latest = new Version(modData.Latest);

            if(current < latest)
            {
                PatchingUtils.ApplyPatch(typeof(Player), nameof(Player.Start), new(typeof(Patches.PlayerPatch), nameof(Patches.PlayerPatch.Start)), HarmonyPatchType.Postfix);

                Logfile.Warning($"[Update] An update is available: {latest} @ {modData.Where}" + modData.Message != string.Empty ? $" (\"{modData.Message}\")" : "");
            }
            else
            {
                Logfile.Info($"The latest available version of this mod ({modData.Latest}) is currently installed, there is no need to update");
            }
        }


        public class ModData
        {
            public string Name { get; set; }

            public string Latest { get; set; }

            public string Where { get; set; }

            public string Message { get; set; }
        }
    }
}