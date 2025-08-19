

namespace RamuneLib
{
    public static class Variables
    {
        public static string name { get; set; }

        public static Harmony harmony { get; set; }

        public static ManualLogSource logger { get; set; }

        public static BaseUnityPlugin instance { get; set; }

        public static class Paths
        {
            private static readonly string Location = Assembly.GetExecutingAssembly().Location;

            public static string GameFolder => BepInExPaths.GameRootPath;

            public static string BepInExFolder => BepInExPaths.BepInExRootPath;

            public static string BepInExConfigFolder => BepInExPaths.ConfigPath;

            public static string BepInExPluginFolder => BepInExPaths.PluginPath;

            public static string BepInExPatcherFolder => BepInExPaths.PatcherPluginPath;

            public static string PluginFolder => Path.GetDirectoryName(Location);

            public static string AssetsFolder => Path.Combine(PluginFolder, "Assets");

            public static string RecipeFolder => Path.Combine(PluginFolder, "Recipes");

            public static string LocalizationFolder => Path.Combine(PluginFolder, "Localization");

            public static string ConfigurationFolder => Path.Combine(PluginFolder, "Configuration");
        }
    }
}