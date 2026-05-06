

namespace RamuneLib.Extensions
{
    internal static class TechTypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <returns></returns>
        internal static string ID(this TechType techType, bool lowercase = false) => techType.AsString(lowercase);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <returns></returns>
        internal static string Name(this TechType techType) => Language.main?.Get(techType) ?? techType.AsString();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <returns></returns>
        internal static string Desc(this TechType techType) => Language.main?.Get("Tooltip_" + techType) ?? "Tooltip_" + techType.AsString();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <returns></returns>
        internal static Sprite Sprite(this TechType techType) => SpriteManager.Get(techType);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="techType"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        internal static IEnumerable<TechType> Repeat(this TechType techType, int amount) => Enumerable.Repeat(techType, amount);
    }
}