using System;

namespace Sqlify
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ColumnAttribute : Attribute
    {
        public string Name { get; }
        public string Query { get; }

        public ColumnAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }
        public ColumnAttribute(string name, string query)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
            Query = query;
        }
    }
}