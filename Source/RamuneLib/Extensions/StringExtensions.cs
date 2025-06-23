

namespace RamuneLib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Inserts a space directly before a specified target in a string
        /// </summary>
        /// <param name="target">The string to target</param>
        /// <returns></returns>
        public static string SpaceBefore(this string inputString, string target) => inputString.Replace(target, " " + target);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="toLookup"></param>
        /// <returns></returns>
        public static string LangKey(this string input)
        {
            if(!Language.main)
                return input;

            return Language.main?.Get(input);
        }
    }
}