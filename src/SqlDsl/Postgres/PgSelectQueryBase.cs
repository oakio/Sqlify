using SqlDsl.Core;
using SqlDsl.Postgres.Clauses;

namespace SqlDsl.Postgres
{
    public abstract class PgSelectQueryBase<TSelectQuery> : SelectQueryBase<TSelectQuery> where TSelectQuery : SelectQueryBase<TSelectQuery>, new()
    {
        private OffsetClause? _offsetClause;
        private LimitClause? _limitClause;

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

        public override void Format(ISqlWriter sql)
        {
            base.Format(sql);
            
            _offsetClause?.Format(sql);
            _limitClause?.Format(sql);
        }
    }
}