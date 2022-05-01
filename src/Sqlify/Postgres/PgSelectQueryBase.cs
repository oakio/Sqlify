using Sqlify.Core;
using Sqlify.Postgres.Clauses;

namespace Sqlify.Postgres
{
    public abstract class PgSelectQueryBase<TSelectQuery> : SelectQueryBase<TSelectQuery> where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        private OffsetClause? _offsetClause;
        private LimitClause? _limitClause;
        private LockingClause? _lockingClause;

        public TSelectQuery Offset(int offset)
        {
            _offsetClause = new OffsetClause(offset);
            return Self();
        }

        public TSelectQuery Limit(int limit)
        {
            _limitClause = new LimitClause(limit);
            return Self();
        }

        public TSelectQuery For(PgLockMode mode)
        {
            _lockingClause = new LockingClause(mode);
            return Self();
        }

        public override void Format(ISqlWriter sql)
        {
            base.Format(sql);
            
            _offsetClause?.Format(sql);
            _limitClause?.Format(sql);
            _lockingClause?.Format(sql);
        }
    }
}