

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

            string middle = $"[{name} {version}]";
            string start = new('=', ($">> Finished loading harmony patches for ' {name} {version} '".Length - middle.Length) / 2);
            string finish = new('=', $">> Finished loading harmony patches for ' {name} {version} '".Length);

            Logfile.Info(start + middle + start);

            CoroutineHost.StartCoroutine(PatchingUtils.WaitForChainloader());

            if(patchAll)
            {
                Logfile.Info($">> Loading harmony patches for '{name} {version}'");

                harmony.PatchAll();

                Logfile.Info($">> Finished loading harmony patches for '{name} {version}'");
            }

            Logfile.Info(finish);
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
                Logfile.Warning($">> Failed to check for update: {request.error}");
                yield break;
            }

            var text = request.downloadHandler.text;

            if(text == null)
            {
                Logfile.Warning($">> Failed to read text from request while checking for updates");
                yield break;
            }

            var modData = JsonConvert.DeserializeObject<ModData>(text);

            if(modData == null)
            {
                Logfile.Warning($">> Failed to serialize text as ModData while checking for updates");
                yield break;
            }

            var current = new Version(currentVersion);
            var latest = new Version(modData.Latest);

            if(current < latest)
            {
                PatchingUtils.ApplyPatch(typeof(Player), nameof(Player.Start), new(typeof(Patches.PlayerPatch), nameof(Patches.PlayerPatch.Start)), HarmonyPatchType.Postfix);

                Logfile.Warning($">> An update is available! Grab version {latest} from: {modData.Where}");

                if(modData.Message != string.Empty)
                {
                    Logfile.Warning($">> '{modData.Name}' has an update message: \"{modData.Message}\"");
                }
            }
        }


        private class ModData
        {
            public string Name { get; set; }

            public string Latest { get; set; }

            public string Where { get; set; }

            public string Message { get; set; }
        }
    }
}