

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
    }
}