using System;
using System.Reflection;

namespace SqlDsl.Core.CodeGen
{
    public static class TableNameResolver
    {
        public static string Resolve(Type type)
        {
            var attribute = type.GetCustomAttribute<TableAttribute>();
            if (attribute != null)
            {
                return attribute.Name;
            }

            string typeName = type.Name;

            if (typeName.Length == 1)
            {
                return typeName;
            }

            int startIndex = typeName[0] == 'I' &&
                             char.IsUpper(typeName[1])
                ? 1
                : 0;
            
            int typeNameLengthWithoutPrefix = typeName.Length - startIndex;

            const string suffix = "Table";

            bool hasTableSuffix = typeName.Length > suffix.Length &&
                                  typeName.EndsWith(suffix);

            int suffixLength = hasTableSuffix
                ? suffix.Length
                : 0;

            int length = typeNameLengthWithoutPrefix - suffixLength;

            return typeName.Substring(startIndex, length);
        }
    }
}