using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Postgres.InsertConflict
{
    public sealed class PgIndexColumnsConflictTarget : IPgConflictTarget
    {
        private readonly Expression[] _columns;

        public PgIndexColumnsConflictTarget(Expression[] columns)
        {
            _columns = columns;
        }

        public void Format(ISqlWriter sql) => sql.Append("(", ", ", _columns, ")");
    }
}