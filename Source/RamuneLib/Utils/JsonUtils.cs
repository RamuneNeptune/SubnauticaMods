

namespace RamuneLib.Utils
{
    public static class JsonUtils
    {
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, RecipeData> RecipeDataCache = new();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetJsonRecipe(string filename) => Path.Combine(Paths.RecipeFolder, filename + ".json");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static RecipeData GetRecipeData(string filename)
        {
            if(RecipeDataCache.TryGetValue(filename, out var cachedRecipeData))
                return cachedRecipeData;
            
            var path = Path.Combine(Paths.RecipeFolder, filename + ".json");

            if(!File.Exists(path))
            {
                Logfile.Error($"Could not find file for recipe at: {path}");
                return null;
            }

            var content = File.ReadAllText(path);
            var recipeData = JsonConvert.DeserializeObject<RecipeData>(content, new CustomEnumConverter());

            if(recipeData == null)
            {
                Logfile.Error($"Invalid RecipeData file at: {path}");
                return null;
            }

            RecipeDataCache[filename] = recipeData;

            return recipeData;
        }
    }
}