

namespace RamuneLib.Utils
{
    internal static class PrefabUtils
    {
        /// <summary>
        /// Creates a custom prefab with the specified ID, name, and description.
        /// </summary>
        /// <param name="id">The unique identifier for the prefab.</param>
        /// <param name="name">The name of the prefab.</param>
        /// <param name="description">The description of the prefab.</param>
        /// <param name="defaultSprite">When true this sets your sprite to a question mark, otherwise it searches for a sprite with the name of your id followed by Texture, e.g. an id of "Titanium" will look for "TitaniumSprite".</returns>
        internal static CustomPrefab CreatePrefab(string id, string name, string description, bool defaultSprite = true)
        {
            return new CustomPrefab(id, name, description, defaultSprite ? SpriteManager.Get(TechType.None) : ImageUtils.GetSprite(id + "Sprite"));
        }


        /// <summary>
        /// Creates a custom prefab with the specified ID, name, description, and <see cref="Sprite"/>.
        /// </summary>
        /// <param name="id">The unique identifier for the prefab.</param>
        /// <param name="name">The name of the prefab.</param>
        /// <param name="description">The description of the prefab.</param>
        /// <param name="sprite">The <see cref="UnityEngine.Sprite"/> to associate with the prefab.</param>
        /// <returns>The created custom prefab.</returns>
        internal static CustomPrefab CreatePrefab(string id, string name, string description, Sprite sprite)
        {
            return new CustomPrefab(id, name, description, sprite);
        }


        internal static CustomPrefab CreatePrefabWithLocalization(string id, Sprite sprite)
        {
            var idLower = id.ToLower();
            return new CustomPrefab(id, $"{idLower}.name".LangKeyAbbr(), $"{idLower}.desc".LangKeyAbbr(), sprite);
        }


        internal static CustomPrefab CreatePrefabWithLocalization(string id, string toRemove, Sprite sprite)
        {
            id = id.Replace(toRemove, "");

            var idLower = id.ToLower();
            return new CustomPrefab(id, $"{idLower}.name".LangKeyAbbr(), $"{idLower}.desc".LangKeyAbbr(), sprite);
        }


        internal static CustomPrefab CreatePrefabWithLocalization(string id)
        {
            var idLower = id.ToLower();
            return new CustomPrefab(id, $"{idLower}.name".LangKeyAbbr(), $"{idLower}.desc".LangKeyAbbr(), ImageUtils.GetSprite(id));
        }


        internal static CustomPrefab CreatePrefabWithLocalization(string id, string toRemove)
        {
            id = id.Replace(toRemove, "");

            var idLower = id.ToLower();
            return new CustomPrefab(id, $"{idLower}.name".LangKeyAbbr(), $"{idLower}.desc".LangKeyAbbr(), ImageUtils.GetSprite(id));
        }


        /// <summary>
        /// Creates a <see cref="RecipeData="/> with the specified crafting amount and ingredients.
        /// </summary>
        /// <param name="craftAmount">The amount of your item that the recipe will provide.</param>
        /// <param name="ingredients">The array of ingredients required for crafting.</param>
        /// <returns>The created <see cref="RecipeData="/>.</returns>
        internal static RecipeData CreateRecipe(int craftAmount, params Ingredient[] ingredients)
        {
            return new RecipeData()
            {
                craftAmount = craftAmount,
                Ingredients = ingredients.ToList()
            };
        }


        /// <summary>
        /// Creates a <see cref="RecipeData="/> with the specified crafting amount, ingredients, and linked items.
        /// </summary>
        /// <param name="craftAmount">The amount of your item that the recipe will provide.</param>
        /// <param name="recipeItems">An array of recipe items, list your ingredients first, then list TechTypes for the linked items.</param>
        /// <returns>The created <see cref="RecipeData="/>.</returns>
        internal static RecipeData CreateRecipe(int craftAmount, params object[] recipeItems)
        {
            return new RecipeData()
            {
                craftAmount = craftAmount,
                Ingredients = recipeItems.OfType<Ingredient>().ToList(),
                LinkedItems = recipeItems.OfType<TechType>().ToList()
            };
        }
    }
}