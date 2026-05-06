

namespace RamuneLib.Utils
{
    internal static class JsonUtils
    {
        /// <summary>
        /// 
        /// </summary>
        internal static Dictionary<string, RecipeData> RecipeDataCache = new();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        internal static string GetJsonRecipePath(string filename, string extension = ".json") => Path.Combine(Paths.RecipeFolder, filename + extension);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal static bool TryGetJsonRecipeData(string path, out RecipeData outputRecipeData)
        {
            outputRecipeData = null;

            if(RecipeDataCache.TryGetValue(path, out var cachedRecipeData))
            {
                outputRecipeData = cachedRecipeData;
                return true;
            }
            
            if(!File.Exists(path))
            {
                Logfile.Error($"Could not find file for recipe at: {path}");
                return false;
            }

            var content = File.ReadAllText(path);
            var recipeData = JsonConvert.DeserializeObject<RecipeData>(content, new CustomEnumConverter());

            if(recipeData == null)
            {
                Logfile.Error($"Invalid RecipeData file at: {path}");
                return false;
            }

            RecipeDataCache[path] = recipeData;

            outputRecipeData = recipeData;
            return true;
        }
    }
}