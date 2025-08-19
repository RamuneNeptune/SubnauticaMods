

namespace RamuneLib.Piracy
{
    /// <summary>
    /// Where the magic happens.
    /// </summary>
    public static class Piracy
    {
        public static string HelloThereDecompiler = @"
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣶⣿⣿⣿⣿⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⠿⠟⠛⠻⣿⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣆⣀⣀⠀⣿⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠻⣿⣿⣿⠅⠛⠋⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                         ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢼⣿⣿⣿⣃⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣟⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣛⣛⣫⡄⠀⢸⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣴⣾⡆⠸⣿⣿⣿⡷⠂⠨⣿⣿⣿⣿⣶⣦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣾⣿⣿⣿⣿⡇⢀⣿⡿⠋⠁⢀⡶⠪⣉⢸⣿⣿⣿⣿⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⣿⣿⣿⣿⣿⡏⢸⣿⣷⣿⣿⣷⣦⡙⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣇⢸⣿⣿⣿⣿⣿⣷⣦⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀
                          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣵⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⡁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                      We're no strangers to love
                                    You know the rules and so do I
                                A full commitment's what I'm thinking of
                                You wouldn't get this from any other guy

                                 I just wanna tell you how I'm feeling
                                     Gotta make you understand

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                  We've known each other for so long
                                    Your heart's been aching, but
                                       You're too shy to say it
                                Inside, we both know what's been going on
                                 We know the game and we're gonna play it

                                   And if you ask me how I'm feeling
                                 Don't tell me you're too blind to see

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                        (Ooh, give you up)
                                        (Ooh, give you up)
                                Never gonna give, never gonna give
                                          (Give you up)
                                Never gonna give, never gonna give
                                          (Give you up)

                                 We've known each other for so long
                                   Your heart's been aching, but
                                     You're too shy to say it
                              Inside, we both know what's been going on
                               We know the game and we're gonna play it

                                I just wanna tell you how I'm feeling
                                     Gotta make you understand

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you

                                       Never gonna give you up
                                      Never gonna let you down
                                 Never gonna run around and desert you
                                       Never gonna make you cry
                                       Never gonna say goodbye
                                  Never gonna tell a lie and hurt you";


        /// <summary>
        /// A list of filenames & folder names which you will find in pirated copies. In the event that a legitimate user has these (they've mixed their pirated copy with their legitimate copy), they can be safely removed to ZERO detriment.
        /// </summary>
        private static readonly List<string> PiracyTargets = new() {
            "steam_api64.cdx", "steam_api64.ini", "steam_emu.ini",
            "Torrent-Igruha.Org.URL", "oalinst.exe", "account_name.txt",
            "valve.ini", "chuj.cdx", "SteamUserID.cfg", "Achievements.bin",
            "steam_settings", "user_steam_id.txt", "Free Steam Games Pre-installed for PC.url",
            "Subnautica_Data/Plugins/steam_api64.cdx", "Subnautica_Data/Plugins/steam_api64.ini",
            "Subnautica_Data/Plugins/steam_emu.ini", "Subnautica_Data/Plugins/x86_64/steam_settings",
            "Profile/Stats/Achievements.Bin", "Profile/SteamUserID.cfg",
        };


        /// <summary>
        /// The SHA256 hash for a legitimate steam_api64.dll file
        /// </summary>
        private static readonly string steamapihash = "4df999c0c8cb12589f0864d52be5d4c775577aeb27fee28b49b188f9ba083eea";


        /// <summary>
        /// Checks if this is a pirated copy of Subnautica
        /// </summary>
        /// <returns><code>true</code> If this is a pirated copy of Subnautica. <code>false</code> If this appears to be a legitimate copy of Subnautica.</returns>
        public static bool Exists()
        {
            // If a user has multiple of my mods installed, the first mod to load
            // will create a GameObject to indicate the result of its piracy check,
            // then any more of my mods which load after will simply check for the
            // GameObjects instead of running the checks again

            if(GameObject.Find("IsPirated"))
                return true;

            if(GameObject.Find("IsNotPirated"))
                return false;

            var directory = Environment.CurrentDirectory;
            var files = Directory.GetFiles(directory);
            var filenames = files.Select(_ => Path.GetFileName(_));

            var steamDllPaths = FindAllSteamDLLs(directory);

            if(steamDllPaths.Count > 0)
            {
                foreach(var steamDllPath in steamDllPaths)
                {
                    using var fileStream = File.OpenRead(steamDllPath);

                    using var sha256 = SHA256.Create();

                    var bytes = sha256.ComputeHash(fileStream);

                    if(bytes != null && bytes.Length == 0)
                    {
                        var builder = new StringBuilder();

                        bytes.ForEach(x => builder.Append(x.ToString("x2")));

                        var hash = builder.ToString();

                        if(!hash.Equals(steamapihash))
                        {
                            new GameObject("IsPirated");
                            DoPiracyPatches();
                            return true;
                        }
                    }
                }
            }

            foreach(var target in PiracyTargets)
            {
                var path = Path.Combine(directory, target);

                if(File.Exists(path) || Directory.Exists(path))
                {
                    new GameObject("IsPirated");
                    DoPiracyPatches();
                    return true;
                }
            }

            new GameObject("IsNotPirated");
            return false;
        }


        /// <summary>
        /// Searches through the game directory & subfolders to find the paths for all "steam_api64.dll" files
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static List<string> FindAllSteamDLLs(string directory)
        {
            try
            {
                return Directory.Exists(directory) ? Directory.EnumerateFiles(directory, "steam_api64.dll", SearchOption.AllDirectories).ToList() : new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }


        /// <summary>
        /// Runs Harmony patches and coroutines to do funny things:
        /// </summary>
        private static void DoPiracyPatches()
        {
            Logfile.Warning("Ahoy matey! Piracy was detected");


            PatchingUtils.ApplyPatch(typeof(Player), nameof(Player.Awake), new(typeof(Patches.PlayerPatches), nameof(Patches.PlayerPatches.Awake)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(Player), nameof(Player.Update), new(typeof(Patches.PlayerPatches), nameof(Patches.PlayerPatches.Update)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(Player), nameof(Player.OnTakeDamage), new(typeof(Patches.PlayerPatches), nameof(Patches.PlayerPatches.OnTakeDamage)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(FireExtinguisher), nameof(FireExtinguisher.Update), new(typeof(Patches.FireExtinguisherPatch), nameof(Patches.FireExtinguisherPatch.Update)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(LiveMixin), nameof(LiveMixin.TakeDamage), new(typeof(Patches.LiveMixinPatch), nameof(Patches.LiveMixinPatch.TakeDamage)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(BreakableResource), nameof(BreakableResource.BreakIntoResources), new(typeof(Patches.BreakableResourcePatch), nameof(Patches.BreakableResourcePatch.BreakIntoResources)), HarmonyPatchType.Prefix);


            PatchingUtils.ApplyPatch(typeof(Charger), nameof(Charger.Initialize), new(typeof(Patches.ChargerPatch), nameof(Patches.ChargerPatch.Initialize)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(Poop), nameof(Poop.Perform), new(typeof(Patches.PoopPatch), nameof(Patches.PoopPatch.Perform)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(Seaglide), nameof(Seaglide.OnDraw), new(typeof(Patches.SeaglidePatch), nameof(Patches.SeaglidePatch.OnDraw)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(Crash), nameof(Crash.Detonate), new(typeof(Patches.CrashPatch), nameof(Patches.CrashPatch.Detonate)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(StasisSphere), nameof(StasisSphere.UpdateMaterials), new(typeof(Patches.StasisSpherePatch), nameof(Patches.StasisSpherePatch.UpdateMaterials)), HarmonyPatchType.Prefix);


            PatchingUtils.ApplyPatch(typeof(Survival), nameof(Survival.Eat), new(typeof(Patches.SurvivalPatch), nameof(Patches.SurvivalPatch.Eat)), HarmonyPatchType.Postfix);


            PatchingUtils.ApplyPatch(typeof(Trashcan), nameof(Trashcan.Update), new(typeof(Patches.TrashcanPatch), nameof(Patches.TrashcanPatch.Update)), HarmonyPatchType.Prefix);
        }


        /// <summary>
        /// Instantiates a Crashfish at the desired <paramref name="spawnPosition"/>.
        /// </summary>
        /// <param name="spawnPosition"></param>
        /// <returns></returns>
        public static IEnumerator SpawnCrashfish(Vector3 spawnPosition)
        {
            var task = GetPrefabForTechTypeAsync(TechType.Crash);

            yield return task;

            var prefab = task.GetResult();

            // Look at the player (with intent to explode in face)
            var rotation = Quaternion.LookRotation(Player.main.transform.position - spawnPosition);

            Object.Instantiate(prefab, spawnPosition, rotation);
        }
    }
}