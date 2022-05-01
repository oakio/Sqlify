using Sqlify.Core;
using Sqlify.Postgres.InsertConflict;

namespace Sqlify.Postgres.Clauses
{
    public readonly struct OnConflictClause : ISqlFormattable
    {
        private readonly IPgConflictTarget _target;
        private readonly IPgConflictAction _action;

        public OnConflictClause(IPgConflictTarget target, IPgConflictAction action)
        {
            _target = target;
            _action = action;
        }

        public void Format(ISqlWriter sql)
        {
            sql.Append(" ON CONFLICT ", _target);
            sql.Append(" DO ", _action);
        }
    }
}