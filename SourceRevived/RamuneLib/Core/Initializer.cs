

namespace RamuneLib
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Initializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="harmony"></param>
        /// <param name="logger"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="patchAll"></param>
        internal static bool Initialize(this BaseUnityPlugin instance, Harmony harmony, ManualLogSource logger, string name, string version, bool enableThisMod, string versionJsonUrl, bool patchAll = true)
        {
            Variables.name = name;
            Variables.harmony = harmony;
            Variables.logger = logger;
            Variables.instance = instance;
            Variables.abbreviation = name.ToAbbreviation();

            if(Piracy.Piracy.Exists())
            {
                Logfile.Warning("Ahoy matey! Piracy was detected");
                return false;
            }

            ModMessageSystem.SendGlobal("FindMyUpdates", versionJsonUrl);

            if(!enableThisMod)
            {
                Logfile.Warning("This mod has been disabled in the config and will not be loaded");
                return false;
            }

            CompatUtils.Initialize();

            if(patchAll)
            {
                Logfile.Info($"Loading harmony patches for '{name} {version}'");
                harmony.PatchAll();
                Logfile.Info($"Loaded harmony patches for '{name} {version}'");
            }

            return true;
        }


        internal static string ToAbbreviation(this string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return string.Empty;

            StringBuilder result = new();

            for(int i = 0; i < name.Length; i++)
            {
                char c = name[i];

                bool isFirst = i == 0;
                bool isUpper = char.IsUpper(c);
                bool isDigit = char.IsDigit(c);

                if(isFirst || isUpper || isDigit)
                    result.Append(c);
            }

            return result.ToString().ToLower();
        }
    }
}