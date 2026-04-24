

namespace RamuneLib.Utils
{
    internal static class ImageUtils
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, Sprite> CachedSprites = new();


        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, Texture2D> CachedTextures = new();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        internal static string GetAssetPath(string filename, string extension = ".png") => Path.Combine(Paths.AssetsFolder, filename + extension);


        /// <summary>
        /// Loads and returns an <see cref="Atlas.Sprite"/> loaded from the Assets folder using the filename provided
        /// </summary>
        /// <param name="filename">The filename of the sprite to load.</param>
        /// <returns>The loaded <see cref="Atlas.Sprite"/>.
        internal static Sprite GetSprite(string filename, string extension = ".png")
        {
            if(CachedSprites.TryGetValue(filename + extension, out var cachedSprite))
                return cachedSprite;

            var sprite = Utility.ImageUtils.LoadSpriteFromFile(GetAssetPath(filename, extension));

            if(sprite == null)
            {
                Logfile.Error($"Failed to load sprite from path: {GetAssetPath(filename, extension)}");
                return GetSprite(TechType.None);
            }

            CachedSprites.Add(filename + extension, sprite);

            return sprite;
        }


        /// <summary>
        /// Gets and returns an <see cref="Atlas.Sprite"/> associated with the given TechType.
        /// </summary>
        /// <param name="techType">The TechType of the sprite to retrieve.</param>
        /// <returns>The retrieved <see cref="Atlas.Sprite"/>.
        internal static Sprite GetSprite(TechType techType) => SpriteManager.Get(techType);


        /// <summary>
        /// Loads and returns a <see cref="Texture2D"/> loaded from the Assets folder using the filename provided.
        /// </summary>
        /// <param name="filename">The filename of the texture to load.</param>
        /// <returns>The loaded <see cref="Texture2D"/>.
        internal static Texture2D GetTexture(string filename, string extension = ".png")
        {
            if(CachedTextures.TryGetValue(filename + extension, out var cachedTexture))
                return cachedTexture;

            var texture = Utility.ImageUtils.LoadTextureFromFile(GetAssetPath(filename, extension));

            if(texture == null)
            {
                Logfile.Error($"Failed to load texture from path: {GetAssetPath(filename, extension)}");
                return null;
            }

            CachedTextures.Add(filename + extension, texture);

            return texture;
        }
    }
}