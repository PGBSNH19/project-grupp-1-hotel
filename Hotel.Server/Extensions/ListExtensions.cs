using System.Collections.Generic;

namespace Hotel.Server.Extensions
{
    public static class ListExtensions
    {
        public static List<T> PopAt<T>(this List<T> list, int index)
        {
            list.RemoveAt(index);
            return list;
        }

        public static List<T> PopLast<T>(this List<T> list)
        {
            list.RemoveAt(list.Count - 1);
            return list;
        }
    }
}
