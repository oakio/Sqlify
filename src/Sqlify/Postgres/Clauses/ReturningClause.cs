using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Postgres.Clauses
{
    public readonly struct ReturningClause : ISqlFormattable
    {
        private readonly Expression[] _columns;

        public ReturningClause(Expression[] columns)
        {
            _columns = columns;
        }

        public void Format(ISqlWriter sql)
        {
            if (_columns == null || _columns.Length == 0)
            {
                sql.Append(" RETURNING *");
            }
            else
            {
                sql.Append(" RETURNING ", ", ", _columns);
            }
        }
    }
}