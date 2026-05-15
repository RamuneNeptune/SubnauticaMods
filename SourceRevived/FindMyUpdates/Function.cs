

namespace Ramune.FindMyUpdates
{
    public static class Function
    {
        public const string GUIDListURL = "https://ramune.fyi/assets/json/GUIDs.json";

        public const string NexusURL = "https://api.nexusmods.com/v2/graphql";

        public const int SubnauticaGameId = 1155;

        public const int NexusQueryBatchSize = 25;


        public static readonly HashSet<string> RegisteredGUIDs = [];

        public static readonly HashSet<string> RegisteredMods = [];


        public class ModData
        {
            [JsonProperty(Required = Required.Always)]
            public string ModName { get; set; }

            [JsonProperty(Required = Required.Always)]
            public string LatestVersion { get; set; }

            [JsonProperty(Required = Required.Always)]
            public string LatestURL { get; set; }
        }


        public enum Error
        {
            None = 0,

            // Message argument issues
            ArgsNotString,
            ArgsEmptyString,

            // URL issues
            InvalidUrl,
            InvalidUrlScheme,
            InvalidUrlExtension,

            // Web request issues
            NotFound404,
            NetworkOrHttpError,
            NullDownloadedText,

            // JSON deserialization issues
            JsonDeserializationFailed,
            JsonNullAfterDeserialization,

            // Mod info issues
            InvalidVersionFormat,
            CurrentVersionNotFound,

            // Version comparison
            UpToDate,
            AheadOfLatest,
            Outdated
        }


        public static Dictionary<Error, string> ErrorDB = new()
        {
            // ModMessage args issues
            { Error.ArgsNotString, "fmu.logfile.errordb.ArgsNotString".LangKey() },
            { Error.ArgsEmptyString, "fmu.logfile.errordb.ArgsEmptyString".LangKey() },

            // URL issues
            { Error.InvalidUrl, "fmu.logfile.errordb.InvalidUrl".LangKey() },
            { Error.InvalidUrlScheme, "fmu.logfile.errordb.InvalidUrlScheme".LangKey() },
            { Error.InvalidUrlExtension, "fmu.logfile.errordb.InvalidUrlExtension".LangKey() },

            // Web request issues
            { Error.NotFound404, "fmu.logfile.errordb.NotFound404".LangKey() },
            { Error.NetworkOrHttpError, "fmu.logfile.errordb.NetworkOrHttpError".LangKey() },
            { Error.NullDownloadedText, "fmu.logfile.errordb.NullDownloadedText".LangKey() },

            // JSON deserialization issues
            { Error.JsonDeserializationFailed, "fmu.logfile.errordb.JsonDeserializationFailed".LangKey() },
            { Error.JsonNullAfterDeserialization, "fmu.logfile.errordb.JsonNullAfterDeserialization".LangKey() },

            // Mod info issues
            { Error.InvalidVersionFormat, "fmu.logfile.errordb.InvalidVersionFormat".LangKey() },
            { Error.CurrentVersionNotFound, "fmu.logfile.errordb.CurrentVersionNotFound".LangKey() },
        };


        public sealed class NexusModReference
        {
            public string GUID { get; set; }

            public string Name { get; set; }

            public string URL { get; set; }

            public int ModId { get; set; }

            public Version CurrentVersion { get; set; }
        }


        public static IEnumerator Validate(object[] args)
        {
            if(args == null || args.Length < 1)
            {
                Logfile.Error(string.Format(ErrorDB[Error.ArgsNotString]));
                yield break;
            }

            if(args[0] is not string url)
            {
                Logfile.Error(string.Format(ErrorDB[Error.ArgsNotString]));
                yield break;
            }

            if(url.Length < 1)
            {
                Logfile.Error(string.Format(ErrorDB[Error.ArgsEmptyString]));
                yield break;
            }

            if(!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                Logfile.Error(string.Format(ErrorDB[Error.InvalidUrl], url));
                yield break;
            }

            bool isFile = uri.Scheme == Uri.UriSchemeFile;

            if(uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps && !isFile)
            {
                Logfile.Error(string.Format(ErrorDB[Error.InvalidUrlScheme], url));
                yield break;
            }

            if(!url.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                Logfile.Error(string.Format(ErrorDB[Error.InvalidUrlExtension], url));
                yield break;
            }

            using UnityWebRequest request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            LogWebRequest(url);

            if(request.responseCode == 404)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NotFound404], url));
                Logfile.Error(request.error);
                yield break;
            }

            if(request.isNetworkError || request.isHttpError)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NetworkOrHttpError], url));
                Logfile.Error(request.error);
                yield break;
            }

            string text = request.downloadHandler.text;

            if(text == null)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NullDownloadedText], url));

                if(!request.error.IsNullOrWhiteSpace())
                    Logfile.Error(request.error);

                yield break;
            }

            if(isFile && File.Exists(uri.LocalPath))
            {
                File.Delete(uri.LocalPath);
            }

            ModData modData;

            try
            {
                modData = JsonConvert.DeserializeObject<ModData>(text);
            }
            catch(JsonSerializationException jsonEx)
            {
                Logfile.Error(string.Format(ErrorDB[Error.JsonDeserializationFailed], url));
                Logfile.Error(jsonEx.Message);
                yield break;
            }

            if(modData == null)
            {
                Logfile.Error(string.Format(ErrorDB[Error.JsonNullAfterDeserialization], url));
                yield break;
            }

            string name = modData.ModName;
            string latestUrl = modData.LatestURL;

            if(!TryParseLooseVersion(modData.LatestVersion, out Version latestVersion))
            {
                Logfile.Error(string.Format(ErrorDB[Error.InvalidVersionFormat], url));
                yield break;
            }

            if(!TryGetPluginVersionByName(name, out Version currentVersion))
            {
                Logfile.Error(string.Format(ErrorDB[Error.CurrentVersionNotFound], name, url));
                yield break;
            }

            string guid = Chainloader.PluginInfos.Values.FirstOrDefault(info => info.Metadata.Name == name)?.Metadata.GUID;

            if(FindMyUpdates.IsGUIDBlacklisted(guid))
            {
                LogBlacklistedMod(name);
                yield break;
            }

            RegisterModVersion(guid, name, latestUrl, currentVersion, latestVersion, modData.LatestVersion);
        }


        public static IEnumerator CheckGUIDUpdate(string guid)
        {
            if(guid.IsNullOrWhiteSpace())
                yield break;

            yield return CheckGUIDUpdates(new HashSet<string>(StringComparer.OrdinalIgnoreCase) { guid });
        }


        public static IEnumerator CheckGUIDUpdates(HashSet<string> allowedGUIDs)
        {
            if(!FindMyUpdates.config.DoNexusCheck)
                yield break;

            string registryText = null;

            yield return DownloadText(GUIDListURL, value => registryText = value);

            if(registryText.IsNullOrWhiteSpace())
                yield break;

            Dictionary<string, string> guidRegistry;

            try
            {
                guidRegistry = JsonConvert.DeserializeObject<Dictionary<string, string>>(registryText);
            }
            catch(JsonException jsonEx)
            {
                Logfile.Error($"Failed to deserialize GUID registry from '{GUIDListURL}'.");
                Logfile.Error(jsonEx.Message);
                yield break;
            }

            if(guidRegistry == null || guidRegistry.Count < 1)
                yield break;

            List<NexusModReference> installedNexusMods = [];

            foreach(var pair in guidRegistry)
            {
                if(!Chainloader.PluginInfos.TryGetValue(pair.Key, out var pluginInfo))
                    continue;

                if(allowedGUIDs != null && !allowedGUIDs.Contains(pair.Key))
                    continue;

                if(RegisteredGUIDs.Contains(pair.Key))
                    continue;

                if(FindMyUpdates.IsGUIDBlacklisted(pair.Key))
                {
                    LogBlacklistedMod(pluginInfo.Metadata.Name);
                    continue;
                }

                if(!FindMyUpdates.PluginInfos.TryGetValue(pair.Key, out Version currentVersion))
                    continue;

                if(!TryParseNexusUrl(pair.Value, out int modId))
                    continue;

                installedNexusMods.Add(new NexusModReference
                {
                    GUID = pair.Key,
                    Name = pluginInfo.Metadata.Name,
                    URL = pair.Value,
                    ModId = modId,
                    CurrentVersion = currentVersion
                });
            }

            if(installedNexusMods.Count < 1)
                yield break;

            for(int i = 0; i < installedNexusMods.Count; i += NexusQueryBatchSize)
            {
                List<NexusModReference> batch = installedNexusMods.Skip(i).Take(NexusQueryBatchSize).ToList();

                if(batch.Count < 1)
                    continue;

                StringBuilder queryBuilder = new();
                queryBuilder.AppendLine("query {");

                for(int batchIndex = 0; batchIndex < batch.Count; batchIndex++)
                {
                    queryBuilder.AppendLine($"  m{batchIndex}: mod(modId: {batch[batchIndex].ModId}, gameId: {SubnauticaGameId}) {{ version }}");
                    queryBuilder.AppendLine($"  f{batchIndex}: modFiles(modId: {batch[batchIndex].ModId}, gameId: {SubnauticaGameId}) {{ name version category date }}");
                }

                queryBuilder.Append('}');

                string responseText = null;

                yield return SendGraphQlQuery(queryBuilder.ToString(), value => responseText = value);

                if(responseText.IsNullOrWhiteSpace())
                    continue;

                JObject response;

                try
                {
                    response = JObject.Parse(responseText);
                }
                catch(JsonException jsonEx)
                {
                    Logfile.Error("Failed to parse Nexus GraphQL mod response.");
                    Logfile.Error(jsonEx.Message);
                    continue;
                }

                if(response["errors"] is JArray errors)
                {
                    foreach(JToken error in errors)
                        Logfile.Error("Nexus GraphQL error: " + (error?["message"]?.Value<string>() ?? error.ToString()));
                }

                JToken data = response["data"];

                if(data == null)
                    continue;

                for(int batchIndex = 0; batchIndex < batch.Count; batchIndex++)
                {
                    NexusModReference reference = batch[batchIndex];
                    JToken mod = data[$"m{batchIndex}"];
                    JToken files = data[$"f{batchIndex}"];

                    if(!TryGetLatestNexusVersion(reference, mod, files, out string latestVersionString))
                        continue;

                    if(!TryParseLooseVersion(latestVersionString, out Version latestVersion))
                    {
                        Logfile.Error(string.Format(ErrorDB[Error.InvalidVersionFormat], reference.URL));
                        continue;
                    }

                    RegisterModVersion(reference.GUID, reference.Name, reference.URL, reference.CurrentVersion, latestVersion, latestVersionString);
                }
            }
        }


        public static IEnumerator SendGraphQlQuery(string query, Action<string> onSuccess)
        {
            string body = JsonConvert.SerializeObject(new { query });

            using UnityWebRequest request = new(NexusURL, UnityWebRequest.kHttpVerbPOST);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            LogWebRequest(NexusURL);

            if(request.isNetworkError || request.isHttpError)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NetworkOrHttpError], NexusURL));

                if(!request.error.IsNullOrWhiteSpace())
                    Logfile.Error(request.error);

                yield break;
            }

            string responseText = request.downloadHandler.text;

            if(!responseText.IsNullOrWhiteSpace())
            {
                try
                {
                    JObject response = JObject.Parse(responseText);

                    if(response["errors"] is JArray errors)
                    {
                        foreach(JToken error in errors)
                            Logfile.Error("Nexus GraphQL error: " + (error?["message"]?.Value<string>() ?? error.ToString()));
                    }
                }
                catch(JsonException)
                {
                }
            }

            onSuccess?.Invoke(responseText);
        }


        public static IEnumerator DownloadText(string url, Action<string> onSuccess)
        {
            using UnityWebRequest request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            LogWebRequest(url);

            if(request.responseCode == 404)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NotFound404], url));

                if(!request.error.IsNullOrWhiteSpace())
                    Logfile.Error(request.error);

                yield break;
            }

            if(request.isNetworkError || request.isHttpError)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NetworkOrHttpError], url));

                if(!request.error.IsNullOrWhiteSpace())
                    Logfile.Error(request.error);

                yield break;
            }

            string text = request.downloadHandler.text;

            if(text == null)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NullDownloadedText], url));

                if(!request.error.IsNullOrWhiteSpace())
                    Logfile.Error(request.error);

                yield break;
            }

            onSuccess?.Invoke(text);
        }


        public static void RegisterModVersion(string guid, string name, string latestUrl, Version currentVersion, Version latestVersion, string latestVersionText)
        {
            if(FindMyUpdates.IsGUIDBlacklisted(guid))
                return;

            latestVersionText = latestVersionText.IsNullOrWhiteSpace() ? latestVersion.ToString() : latestVersionText;

            if(!guid.IsNullOrWhiteSpace())
            {
                if(!RegisteredGUIDs.Add(guid))
                    return;
            }
            else
            {
                string key = $"{name}\n{latestUrl}".ToLowerInvariant();

                if(!RegisteredMods.Add(key))
                    return;
            }

            int comparison = CompareVersions(currentVersion, latestVersion);

            switch(comparison)
            {
                case 0:
                    if(FindMyUpdates.config.LogForUpToDateMods)
                        Logfile.Info(string.Format("fmu.logfile.updated".LangKey(), name, currentVersion, latestVersionText));

                    Patches.uGUI_OptionsPanelPatch.RegisterMod(guid, name, latestUrl, currentVersion, latestVersion, latestVersionText, true);
                    break;

                case > 0:
                    if(FindMyUpdates.config.LogForOverdatedMods)
                        Logfile.Info(string.Format("fmu.logfile.overdated".LangKey(), name, currentVersion, latestVersionText));

                    Patches.uGUI_OptionsPanelPatch.RegisterMod(guid, name, latestUrl, currentVersion, latestVersion, latestVersionText, true);
                    break;

                case < 0:
                    if(FindMyUpdates.config.LogForOutdatedMods)
                        Logfile.Warning(string.Format("fmu.logfile.outdated".LangKey(), name, currentVersion, latestVersionText));

                    Patches.uGUI_OptionsPanelPatch.RegisterMod(guid, name, latestUrl, currentVersion, latestVersion, latestVersionText, false);
                    break;
            }
        }


        public static bool TryParseNexusUrl(string url, out int modId)
        {
            modId = 0;

            if(url.IsNullOrWhiteSpace() || !Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                return false;

            if(!uri.Host.EndsWith("nexusmods.com", StringComparison.OrdinalIgnoreCase))
                return false;

            string[] segments = uri.AbsolutePath.Trim('/').Split('/');

            if(segments.Length < 3 || !segments[0].Equals("subnautica", StringComparison.OrdinalIgnoreCase) || !segments[1].Equals("mods", StringComparison.OrdinalIgnoreCase))
                return false;

            return int.TryParse(segments[2], out modId);
        }


        public static bool TryParseLooseVersion(string value, out Version version)
        {
            version = null;

            if(Version.TryParse(value, out version))
                return true;

            if(value.IsNullOrWhiteSpace())
                return false;

            Match prereleaseMatch = Regex.Match(value, @"(?i)(\d+(?:\.\d+){1,3})\s*[-_ ]?(?:pre|preview|alpha|beta|rc)[. -]?(\d+)");

            if(prereleaseMatch.Success)
            {
                string baseVersion = prereleaseMatch.Groups[1].Value;
                string prereleaseNumber = prereleaseMatch.Groups[2].Value;

                if(Version.TryParse(baseVersion, out Version parsedBaseVersion) && int.TryParse(prereleaseNumber, out int parsedPrereleaseNumber))
                {
                    int[] parts = [parsedBaseVersion.Major, parsedBaseVersion.Minor, System.Math.Max(parsedBaseVersion.Build, 0), System.Math.Max(parsedBaseVersion.Revision, 0)];

                    if(parts[3] == 0)
                        parts[3] = parsedPrereleaseNumber;

                    version = new Version(parts[0], parts[1], parts[2], parts[3]);
                    return true;
                }
            }

            Match match = Regex.Match(value, @"\d+(?:\.\d+){0,3}");

            bool success = match.Success && Version.TryParse(match.Value, out version);

            return success;
        }


        public static bool TryGetLatestNexusVersion(NexusModReference reference, JToken mod, JToken files, out string latestVersionString)
        {
            latestVersionString = null;

            if(files is JArray fileArray)
            {
                List<JToken> candidates = fileArray
                    .Where(file =>
                    {
                        string version = file["version"]?.Value<string>();
                        return !version.IsNullOrWhiteSpace();
                    })
                    .Where(file =>
                    {
                        string category = file["category"]?.Value<string>();
                        return IsRelevantFileCategory(category);
                    })
                    .ToList();

                if(candidates.Count > 0)
                {
                    List<JToken> matchingCandidates = candidates
                        .Where(file => FileMatchesModName(file["name"]?.Value<string>(), reference.Name))
                        .ToList();

                    IEnumerable<JToken> pool = matchingCandidates.Count > 0 ? matchingCandidates : candidates;

                    latestVersionString = pool
                        .OrderBy(file =>
                        {
                            string category = file["category"]?.Value<string>();
                            return GetFileCategoryPriority(category);
                        })
                        .ThenByDescending(file => file["date"]?.Value<long>() ?? long.MinValue)
                        .Select(file => (string)file["version"])
                        .FirstOrDefault(version => !version.IsNullOrWhiteSpace());
                }
            }

            if(latestVersionString.IsNullOrWhiteSpace())
                latestVersionString = mod?["version"]?.Value<string>();

            return !latestVersionString.IsNullOrWhiteSpace();
        }


        public static bool IsRelevantFileCategory(string category) => category == "MAIN" || category == "UPDATE" || category == "OPTIONAL";


        public static int GetFileCategoryPriority(string category) => category switch
        {
            "MAIN" => 0,
            "UPDATE" => 1,
            "OPTIONAL" => 2,
            _ => int.MaxValue
        };


        public static bool FileMatchesModName(string fileName, string modName)
        {
            if(fileName.IsNullOrWhiteSpace() || modName.IsNullOrWhiteSpace())
                return false;

            string normalizedFileName = NormalizeName(fileName);
            string normalizedModName = NormalizeName(modName);

            if(normalizedFileName.IsNullOrWhiteSpace() || normalizedModName.IsNullOrWhiteSpace())
                return false;

            return normalizedFileName.Contains(normalizedModName) || normalizedModName.Contains(normalizedFileName);
        }


        public static string NormalizeName(string value) => new(value.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());


        public static int CompareVersions(Version currentVersion, Version latestVersion)
        {
            int[] currentParts = [currentVersion.Major, currentVersion.Minor, System.Math.Max(currentVersion.Build, 0), System.Math.Max(currentVersion.Revision, 0)];
            int[] latestParts = [latestVersion.Major, latestVersion.Minor, System.Math.Max(latestVersion.Build, 0), System.Math.Max(latestVersion.Revision, 0)];

            for(int i = 0; i < currentParts.Length; i++)
            {
                int comparison = currentParts[i].CompareTo(latestParts[i]);

                if(comparison != 0)
                    return comparison;
            }

            return 0;
        }


        public static void LogWebRequest(string url)
        {
            if(!FindMyUpdates.config.LogWebRequests)
                return;

            Logfile.WithLevel(FindMyUpdates.config.LoggingLevel switch
            {
                0 => LogLevel.Debug,
                1 => LogLevel.Info,
                2 => LogLevel.Warning,
                3 => LogLevel.Error,
                4 => LogLevel.Fatal,
                _ => LogLevel.Debug
            }, string.Format("fmu.logfile.sentwebrequest".LangKey(), url));
        }


        public static void LogBlacklistedMod(string name)
        {
            if(name.IsNullOrWhiteSpace() || !FindMyUpdates.config.LogForUpToDateMods)
                return;

            Logfile.Info($"{name} will not be checked for updates because it is blacklisted");
        }


        public static bool TryGetPluginVersionByName(string name, out Version currentVersion) => (currentVersion = Chainloader.PluginInfos.Values.FirstOrDefault(info => info.Metadata.Name == name)?.Metadata.Version) != null;
    }
}