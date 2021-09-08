using System;
using SqlDsl.Core;

namespace SqlDsl
{
    public class InsertQuery : InsertQueryBase<InsertQuery>
    {
        public InsertQuery(Table table) : base(table)
        {
            string tableAlias = table.GetAlias();
            if (!string.IsNullOrWhiteSpace(tableAlias))
            {
                throw new NotSupportedException("INSERT query does not support table alias");
            }
        }
    }
}