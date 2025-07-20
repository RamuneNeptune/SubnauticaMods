

namespace RamuneLib.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddUnique<T>(this List<T> list, T item)
        {
            if(!list.Contains(item))
            {
                list.Add(item);
                return true;
            }

            return false;
        }
    }
}