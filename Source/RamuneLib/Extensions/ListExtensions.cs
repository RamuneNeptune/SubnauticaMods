

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


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="items"></param>
        /// <returns>Amount of items added to the <param name="list"></returns>
        public static int AddRangeUnique<T>(this List<T> list, IEnumerable<T> items)
        {
            int added = 0;

            foreach(var item in items)
            {
                if(!list.Contains(item))
                {
                    list.Add(item);
                    added++;
                }
            }

            return added;
        }
    }
}