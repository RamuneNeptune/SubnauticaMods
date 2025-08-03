

namespace RamuneLib.Extensions
{
    public static class CodeMatcherExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matcher"></param>
        /// <param name="operand"></param>
        /// <returns></returns>
        public static CodeMatcher SetOperand(this CodeMatcher matcher, object operand)
        {
            matcher.Operand = operand;
            return matcher;
        }
    }
}