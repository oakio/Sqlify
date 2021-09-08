using System;
using SqlDsl.Core;

namespace SqlDsl
{
    public class DeleteQuery : DeleteQueryBase<DeleteQuery>
    {
        public DeleteQuery(Table table) : base(table)
        {
            string tableAlias = table.GetAlias();
            if (!string.IsNullOrWhiteSpace(tableAlias))
            {
                throw new NotSupportedException("DELETE query does not support table alias");
            }
        }
    }
}