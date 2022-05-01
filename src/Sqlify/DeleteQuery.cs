using System;
using Sqlify.Core;

namespace Sqlify
{
    public class DeleteQuery : DeleteQueryBase<DeleteQuery>
    {
        public DeleteQuery(ITable table) : base(table)
        {
            string tableAlias = table.GetAlias();
            if (!string.IsNullOrWhiteSpace(tableAlias))
            {
                throw new NotSupportedException("DELETE query does not support table alias");
            }
        }
    }
}