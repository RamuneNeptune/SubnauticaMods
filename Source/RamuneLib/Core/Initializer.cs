

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
    }
}