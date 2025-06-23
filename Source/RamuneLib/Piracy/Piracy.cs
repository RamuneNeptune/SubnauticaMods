

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
        /// The filesize used to determine if a user has a modified steam_api64.dll file (does not flag for those VR modified steam_api64.dll files).
        /// </summary>
        private static readonly long steamapisize = 300000;


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
            {
                Logfile.Warning(">> Ahoy matey! Piracy was detected");
                return true;
            }

            if(GameObject.Find("IsClean"))
            {
                Logfile.Info(">> Piracy was not detected");
                return false;
            }


            var directory = Environment.CurrentDirectory;
            var files = Directory.GetFiles(directory);
            var filenames = files.Select(_ => Path.GetFileName(_));


            var steamDllPath = Path.Combine(directory, "Subnautica_Data\\Plugins\\x86_64\\steam_api64.dll");
            bool hasSteamDll = File.Exists(steamDllPath);


            if(hasSteamDll)
            {
                var length = new FileInfo(steamDllPath);

                if(length.Length > steamapisize)
                {
                    new GameObject("IsPirated");
                    DoPiracyPatches();
                    return true;
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

            Logfile.Info(">> Piracy was not detected");
            new GameObject("IsClean");
            return false;
        }


        /// <summary>
        /// Runs Harmony patches and coroutines to do funny things:
        /// </summary>
        private static void DoPiracyPatches()
        {
            Logfile.Warning(">> Ahoy matey! Piracy was detected");


            ///Day night speed is 25% slower.
            ///Constantly displays a list of messages on-screen dedicated to pirates from community members.
            PatchingUtils.ApplyPatch(typeof(Player), nameof(Player.Awake), new(typeof(Patches.PlayerPatch), nameof(Patches.PlayerPatch.Awake)), HarmonyPatchType.Postfix);


            ///Player becomes 1% smaller every time damage is taken.
            PatchingUtils.ApplyPatch(typeof(LiveMixin), nameof(LiveMixin.TakeDamage), new(typeof(Patches.LiveMixinPatch), nameof(Patches.LiveMixinPatch.TakeDamage)), HarmonyPatchType.Postfix);


            ///Outcrops occasionally spawn a crashfish when broken.
            PatchingUtils.ApplyPatch(typeof(BreakableResource), nameof(BreakableResource.BreakIntoResources), new(typeof(Patches.BreakableResourcePatch), nameof(Patches.BreakableResourcePatch.BreakIntoResources)), HarmonyPatchType.Prefix);


            ///Chargers recharge items 50% slower.
            PatchingUtils.ApplyPatch(typeof(Charger), nameof(Charger.Initialize), new(typeof(Patches.ChargerPatch), nameof(Patches.ChargerPatch.Initialize)), HarmonyPatchType.Postfix);


            ///Seaglide acceleration is increased by 0.5% everytime it is equipped.
            PatchingUtils.ApplyPatch(typeof(Seaglide), nameof(Seaglide.OnDraw), new(typeof(Patches.SeaglidePatch), nameof(Patches.SeaglidePatch.OnDraw)), HarmonyPatchType.Postfix);


            ///Crashfish have a 30% chance to explode into another Crashfish.
            PatchingUtils.ApplyPatch(typeof(Crash), nameof(Crash.Detonate), new(typeof(Patches.CrashPatch), nameof(Patches.CrashPatch.Detonate)), HarmonyPatchType.Postfix);


            ///Coffee.. does things to the player.
            PatchingUtils.ApplyPatch(typeof(Survival), nameof(Survival.Eat), new(typeof(Patches.SurvivalPatch), nameof(Patches.SurvivalPatch.Eat)), HarmonyPatchType.Postfix);
        }


        /// <summary>
        /// Recolors every material on the <paramref name="prefab"/> to <paramref name="color"/>.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="color"></param>
        private static void Recolor(GameObject prefab, Color color)
        {
            if(prefab == null)
                return;

            if(!prefab.TryGetComponents<Renderer>(out var renderers))
                return;

            renderers.ForEach(r => r.materials.ForEach(m => m.color = color));
        }


        /// <summary>
        /// Sets the <paramref name="prefab"/>'s local scale to <paramref name="scale"/>.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="scale"></param>
        private static void Resize(GameObject prefab, float scale)
        {
            if(prefab == null)
                return;

            prefab.transform.localScale = new Vector3(scale, scale, scale);
        }


        /// <summary>
        /// Instantiates a Crashfish at the desired <paramref name="spawnPosition"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
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