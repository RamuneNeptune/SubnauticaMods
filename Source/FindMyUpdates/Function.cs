

namespace Ramune.FindMyUpdates
{
    public static class Function
    {
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
            { Error.ArgsNotString, "Received a mod message with invalid args: Must be a string!" },
            { Error.ArgsEmptyString, "Received a mod message with invalid args: Must not be empty!" },

            // URL issues
            { Error.InvalidUrl, "Received a mod message with invalid URL: Failed to parse as URL! ({0})" },
            { Error.InvalidUrlScheme, "Received a mod message with invalid URL: Must be HTTP or HTTPS! ({0})" },
            { Error.InvalidUrlExtension, "Received a mod message with invalid URL: Must lead directly to a JSON file! ({0})" },

            // Web request issues
            { Error.NotFound404, "Received a mod message with invalid URL: 404 not found! ({0})" },
            { Error.NetworkOrHttpError, "Received a mod message with valid URL, but web request failed: Netowrk/HTTP error! ({0})" },
            { Error.NullDownloadedText, "Received a mod message with valid URL, but web request failed: Returned null text from URL! ({0})" },

            // JSON deserialization issues
            { Error.JsonDeserializationFailed, "Received a mod message with valid URL, but invalid JSON: Could not parse information from JSON! ({0})" },
            { Error.JsonNullAfterDeserialization, "Received a mod message with valid URL, but invalid JSON: Could not parse information from JSON! ({0})" },

            // Mod info issues
            { Error.InvalidVersionFormat, "Received a mod message with valid URL, but invalid JSON data: Could not parse 'LatestVersion' as a 'System.Version'! ({0})" },
            { Error.CurrentVersionNotFound, "Received a mod message with valid URL, but invalid JSON data: Could not find a mod named '{0}' that is currently loaded! ({1})" },
        };


        public static IEnumerator Validate(object[] args)
        {
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

            if(uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                Logfile.Error(string.Format(ErrorDB[Error.InvalidUrlScheme], url));
                yield break;
            }

            if(!url.EndsWith(".json"))
            {
                Logfile.Error(string.Format(ErrorDB[Error.InvalidUrlExtension], url));
                yield break;
            }

            var request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

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

            var text = request.downloadHandler.text;

            if(text == null)
            {
                Logfile.Error(string.Format(ErrorDB[Error.NullDownloadedText], url));
                
                if(!request.error.IsNullOrWhiteSpace())
                    Logfile.Error(request.error);

                yield break;
            }

            var modData = new ModData();

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

            var _name = modData.ModName;
            var _url = modData.LatestURL;

            if(!Version.TryParse(modData.LatestVersion, out var _latestVersion))
            {
                Logfile.Error(string.Format(ErrorDB[Error.InvalidVersionFormat], url));
                yield break;
            }

            if(!TryGetPluginVersionByName(_name, out var currentVersion))
            {
                Logfile.Error(string.Format(ErrorDB[Error.CurrentVersionNotFound], _name, url));
                yield break;
            }

            int comparison = currentVersion.CompareTo(_latestVersion);

            switch(comparison)
            {
                case 0:
                    Logfile.Info($"{_name} is up-to date. You are using {currentVersion} and the latest available version is {_latestVersion}");
                    Patches.uGUI_OptionsPanelPatch.RegisterMod(_name, _url, currentVersion, _latestVersion, true);
                    break;

                case > 0:
                    Logfile.Info($"{_name} is more than up-to date. You are using {currentVersion} and the latest available version is {_latestVersion}");
                    Patches.uGUI_OptionsPanelPatch.RegisterMod(_name, _url, currentVersion, _latestVersion, true);
                    break;

                case < 0:
                    Logfile.Warning($"{_name} is outdated. You are using {currentVersion} and the latest available version is {_latestVersion}");
                    Patches.uGUI_OptionsPanelPatch.RegisterMod(_name, _url, currentVersion, _latestVersion, false);
                    break;
            }
        }


        public static bool TryGetPluginVersionByName(string name, out Version currentVersion) => (currentVersion = Chainloader.PluginInfos.Values.FirstOrDefault(info => info.Metadata.Name == name)?.Metadata.Version) != null;
    }
}