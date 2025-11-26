

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

            var isFile = uri.Scheme == Uri.UriSchemeFile;

            if(uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps && !isFile)
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

            if(FindMyUpdates.config.LogWebRequests)
            {
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

            if(isFile && File.Exists(url))
            {
                File.Delete(url);
                Logfile.Debug("Deleted temporary file used for Nautilus update check: " + url);
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
                    if(FindMyUpdates.config.LogForUpToDateMods)
                        Logfile.Info(string.Format("fmu.logfile.updated".LangKey(), _name, currentVersion, _latestVersion));

                    Patches.uGUI_OptionsPanelPatch.RegisterMod(_name, _url, currentVersion, _latestVersion, true);
                    break;

                case > 0:
                    if(FindMyUpdates.config.LogForOverdatedMods)
                        Logfile.Info(string.Format("fmu.logfile.overdated".LangKey(), _name, currentVersion, _latestVersion));

                    Patches.uGUI_OptionsPanelPatch.RegisterMod(_name, _url, currentVersion, _latestVersion, true);
                    break;

                case < 0:
                    if(FindMyUpdates.config.LogForOutdatedMods)
                        Logfile.Warning(string.Format("fmu.logfile.outdated".LangKey(), _name, currentVersion, _latestVersion));

                    Patches.uGUI_OptionsPanelPatch.RegisterMod(_name, _url, currentVersion, _latestVersion, false);
                    break;
            }
        }


        public static bool TryGetPluginVersionByName(string name, out Version currentVersion) => (currentVersion = Chainloader.PluginInfos.Values.FirstOrDefault(info => info.Metadata.Name == name)?.Metadata.Version) != null;
    }
}