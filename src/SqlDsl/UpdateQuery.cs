using System;
using SqlDsl.Core;

namespace SqlDsl
{
    public class UpdateQuery : UpdateQueryBase<UpdateQuery>
    {
        public UpdateQuery(Table table) : base(table)
        {
            string tableAlias = table.GetAlias();
            if (!string.IsNullOrWhiteSpace(tableAlias))
            {
                throw new NotSupportedException("UPDATE query does not support table alias");
            }
        }
    }
}