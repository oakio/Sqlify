using System.Collections.Generic;

namespace Sqlify.Core
{
    public static class SqlWriterUtils
    {
        public static void Append<T>(this ISqlWriter sql, string prefix, string separator, IReadOnlyList<T> items, string suffix)
            where T : ISqlFormattable
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            sql.Append(prefix, separator, items);
            sql.Append(suffix);
        }

        public static void Append<T>(this ISqlWriter sql, string prefix, string separator, IReadOnlyList<T> items)
            where T : ISqlFormattable
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            sql.Append(prefix);
            sql.Append(separator, items);
        }

        public static void Append<T>(this ISqlWriter sql, string separator, IReadOnlyList<T> items)
            where T : ISqlFormattable
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0)
                {
                    sql.Append(separator);
                }

                T item = items[i];
                item.Format(sql);
            }
        }

        public static void Append<T>(this ISqlWriter sql, string prefix, T value)
            where T : ISqlFormattable
        {
            if (value == null)
            {
                return;
            }

            sql.Append(prefix);
            value.Format(sql);
        }

        public static void Append<T>(this ISqlWriter sql, IReadOnlyList<T> items)
            where T : ISqlFormattable
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            foreach (T item in items)
            {
                item.Format(sql);
            }
        }
    }
}