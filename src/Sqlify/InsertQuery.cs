using System;
using Sqlify.Core;

namespace Sqlify
{
    public class InsertQuery : InsertQueryBase<InsertQuery>
    {
        public InsertQuery(ITable table) : base(table)
        {
            string tableAlias = table.GetAlias();
            if (!string.IsNullOrWhiteSpace(tableAlias))
            {
                throw new NotSupportedException("INSERT query does not support table alias");
            }
        }
    }
}