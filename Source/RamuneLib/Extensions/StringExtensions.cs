

namespace RamuneLib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Inserts <paramref name="indentAmount"/> spaces directly before <paramref name="target"/> in the <paramref name="source"/> string.
        /// </summary>
        /// <param name="target">The text to target</param>
        /// <param name="indentAmount">The amount of spaces to add</param>
        /// <returns></returns>
        public static string IndentBefore(this string source, int indentAmount, string target) => source.Replace(target, new string(' ', indentAmount) + target);


        /// <summary>
        /// Inserts <paramref name="indentAmount"/> spaces directly after <paramref name="target"/> in the <paramref name="source"/> string.
        /// </summary>
        /// <param name="target">The text to target</param>
        /// <param name="indentAmount">The amount of spaces to add</param>
        /// <returns></returns>
        public static string IndentAfter(this string source, int indentAmount, string target) => source.Replace(target, target + new string(' ', indentAmount));


        /// <summary>
        /// Inserts <paramref name="textToInsert"/> directly before <paramref name="target"/> in the <paramref name="source"/> string.
        /// </summary>
        /// <param name="target">The text to target</param>
        /// <param name="textToInsert">The text to insert before the target</param>
        /// <returns></returns>
        public static string InsertBefore(this string source, string target, string textToInsert) => source.Replace(target, textToInsert + target);


        /// <summary>
        /// Inserts <paramref name="textToInsert"/> directly after <paramref name="target"/> in the <paramref name="source"/> string.
        /// </summary>
        /// <param name="target">The text to target</param>
        /// <param name="textToInsert">The text to insert after the target</param>
        /// <returns></returns>
        public static string InsertAfter(this string source, string target, string textToInsert) => source.Replace(target, target + textToInsert);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="count">The amount of times to repeat the <paramref name="source"/> string</param>
        /// <returns></returns>
        public static string Repeat(this string source, int count) => string.Concat(Enumerable.Repeat(source, count));


        /// <summary>
        /// Checks if the <paramref name="source"/> string is a valid http/https URL.
        /// </summary>
        /// <returns></returns>
        public static bool IsValidURL(this string source) => new Uri(source) is var uri && uri.IsAbsoluteUri && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);


        /// <summary>
        /// Returns the localized text for <paramref name="source"/> string if it exists.
        /// </summary>
        /// <returns></returns>
        public static string LangKey(this string source) => Language.main?.Get(source) ?? source;
    }
}