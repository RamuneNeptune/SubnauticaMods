

namespace RamuneLib
{
    public static class Variables
    {
        public static string name { get; set; }

        public static Harmony harmony { get; set; }

        public static ManualLogSource logger { get; set; }


        public static class Paths
        {
            public static string PluginFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            public static string AssetsFolder => Path.Combine(PluginFolder, "Assets");

            public static string RecipeFolder => Path.Combine(PluginFolder, "Recipes");

            public static string GetFolder(string folderName)
            {
                var path = Path.Combine(PluginFolder, folderName);

                if(Directory.Exists(path))
                    return path;

                throw new Exception($"Could not find a directory at path: {path}");
            }
        }
    }
}