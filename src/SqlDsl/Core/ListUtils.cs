using System.Collections.Generic;

namespace SqlDsl.Core
{
    internal static class ListUtils
    {
        public static void Add<T>(ref List<T> items, T item)
        {
            if (items == null)
            {
                items = new List<T>();
            }

            items.Add(item);
        }

        public static void AddRange<T>(ref List<T> items, IReadOnlyCollection<T> values)
        {
            if (items == null)
            {
                items = new List<T>(values);
            }
            else
            {
                items.AddRange(values);
            }
        }
    }
}