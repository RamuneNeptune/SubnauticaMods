

namespace RamuneLib.Extensions
{
    public static class TechTypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <returns></returns>
        public static string Name(this TechType techType) => Language.main.Get(techType);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <returns></returns>
        public static string Desc(this TechType techType) => Language.main.Get("Tooltip_" + techType);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <returns></returns>
        public static Atlas.Sprite Sprite(this TechType techType) => SpriteManager.Get(techType);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<TechType> Repeat(this TechType techType, int amount) => Enumerable.Repeat(techType, amount);
    }
}