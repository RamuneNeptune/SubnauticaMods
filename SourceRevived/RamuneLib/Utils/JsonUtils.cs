

namespace RamuneLib.Utils
{
    internal static class JsonUtils
    {
        /// <summary>
        /// 
        /// </summary>
        internal static Dictionary<string, RecipeData> RecipeDataCache = [];


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
        internal static bool TryGetJsonRecipeData(string path, out RecipeData recipeData)
        {
            recipeData = null;

            if(RecipeDataCache.TryGetValue(path, out var cachedRecipeData))
            {
                recipeData = cachedRecipeData;
                return true;
            }

            if(!File.Exists(path))
            {
                Logfile.Error($"Could not find file for recipe at: {path}");
                return false;
            }

            try
            {
                var content = File.ReadAllText(path);

                recipeData = JsonConvert.DeserializeObject<RecipeData>(content, new CustomEnumConverter());

                if(recipeData == null)
                {
                    Logfile.Error($"Invalid RecipeData file at: {path}");
                    return false;
                }

                RecipeDataCache[path] = recipeData;
                return true;
            }
            catch(Exception ex)
            {
                Logfile.Error($"Failed to read recipe data at '{path}': {ex}");
                return false;
            }
        }


        internal static RecipeData GetJsonRecipeDataOrDefault(string path)
        {
            if(TryGetJsonRecipeData(path, out var recipeData))
                return recipeData;

            return DefaultRecipeData;
        }


        internal static readonly RecipeData DefaultRecipeData = new()
        {
            Ingredients = [new(TechType.None, 1)],
            craftAmount = 1,
        };
    }
}